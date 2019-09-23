﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.StreamingExtensions;

namespace Microsoft.Bot.Builder.Skills.Protocol
{
    internal class Router
    {
        private readonly TrieNode _root;
        private readonly IEnumerable<RouteTemplate> _routes;

        public Router(IEnumerable<RouteTemplate> routes)
        {
            _routes = routes;
            _root = new TrieNode("root");
            Compile();
        }

        public RouteContext GetRoute(ReceiveRequest request)
        {
            var path = request.Path;
            if (Uri.IsWellFormedUriString(path, UriKind.Absolute))
            {
                var uri = new Uri(path);
                path = uri.AbsolutePath;
            }

            if (path.StartsWith("/", StringComparison.InvariantCultureIgnoreCase))
            {
                path = path.Substring(1);
            }

            var parts = path.Split('/');

            if (_root.TryGetNext(request.Verb, out var current))
            {
                var found = false;
                var routeData = new ExpandoObject() as IDictionary<string, object>;
                foreach (var part in parts)
                {
                    if (current.TryGetNext(part, out var next))
                    {
                        // found an exact match, keep going
                        current = next;
                    }
                    else
                    {
                        // check for variables and continue
                        var variables = current.GetVariables();
                        if (variables.Any())
                        {
                            // TODO: we are only going to allow 1 variable for now
                            current = variables.First();
                            routeData[current.VariableName] = part;
                            found = true;
                        }
                    }
                }

                if (found && current.ActionAsync != null)
                {
                    return new RouteContext()
                    {
                        Request = request,
                        RouteData = routeData,
                        ActionAsync = current.ActionAsync,
                    };
                }
            }

            return null;
        }

        private void Compile()
        {
            foreach (var route in _routes)
            {
                var path = route.Path;
                if (path.StartsWith("/", StringComparison.InvariantCultureIgnoreCase))
                {
                    path = path.Substring(1);
                }

                var parts = path.Split('/');

                var methodNode = _root.Add(route.Method.ToUpperInvariant());
                var current = methodNode;
                foreach (var part in parts)
                {
                    current = current.Add(part);
                }

                if (current.ActionAsync != null)
                {
                    throw new InvalidOperationException("Route already exists");
                }

                current.ActionAsync = route.ActionAsync;
            }
        }

        private class TrieNode
        {
            public TrieNode(string value)
            {
                Value = value;
                IsVariable = value[0] == '{' && value[value.Length - 1] == '}';
                if (IsVariable)
                {
                    VariableName = value.Substring(1, value.Length - 2);
                }

                Next = new Dictionary<string, TrieNode>();
            }

            public string Value { get; }

            public bool IsVariable { get; }

            public string VariableName { get; }

            public RouteAction ActionAsync { get; set; }

            public IDictionary<string, TrieNode> Next { get; }

            public TrieNode Add(string value)
            {
                if (!Next.TryGetValue(value, out var next))
                {
                    next = new TrieNode(value);
                    Next.Add(value, next);
                }

                return next;
            }

            public bool TryGetNext(string value, out TrieNode node)
            {
                return Next.TryGetValue(value, out node);
            }

            public IEnumerable<TrieNode> GetVariables()
            {
                return Next.Values.Where(n => n.IsVariable);
            }
        }
    }
}
