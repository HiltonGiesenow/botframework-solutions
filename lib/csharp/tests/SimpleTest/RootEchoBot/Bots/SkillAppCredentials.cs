﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Bot.Builder.Skills.Auth;
using Microsoft.Bot.Connector.Authentication;

namespace RootEchoBot.Bots
{
    public class SkillAppCredentials : MicrosoftAppCredentials, IServiceClientCredentials
    {
        public SkillAppCredentials(string appId, string password, string oauthScope)
            : base(appId, password)
        {
            OAuthScope = oauthScope;
        }

        public override string OAuthScope { get; }

        public override string OAuthEndpoint => "https://login.microsoftonline.com/botframework.com";
    }
}
