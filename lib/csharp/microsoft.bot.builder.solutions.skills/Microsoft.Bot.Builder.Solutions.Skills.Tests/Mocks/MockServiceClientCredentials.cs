﻿using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Skills.Auth;
using Microsoft.Bot.Builder.Solutions.Skills.Auth;

namespace Microsoft.Bot.Builder.Solutions.Skills.Tests.Mocks
{
    public class MockServiceClientCredentials : IServiceClientCredentials
    {
        public string MicrosoftAppId { get; set; } = Guid.NewGuid().ToString();

        public Task<string> GetTokenAsync(bool forceRefresh = false)
        {
            return Task.FromResult(string.Empty);
        }

        public Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
