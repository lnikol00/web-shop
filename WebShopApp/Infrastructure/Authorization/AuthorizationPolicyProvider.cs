﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace WebShopApp.Infrastructure.Authorization
{
    public class AuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        private readonly AuthorizationOptions _options;
        private readonly IConfiguration _configuration;

        public AuthorizationPolicyProvider(IOptions<AuthorizationOptions> options, IConfiguration configuration) : base(options)
        {
            _options = options.Value;
            _configuration = configuration;
        }

        public override async Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            var policy = await base.GetPolicyAsync(policyName);

            if (policy == null)
            {
                policy = new AuthorizationPolicyBuilder()
                    .AddRequirements(new PermissionRequirement(policyName))
                    .Build();

                _options.AddPolicy(policyName, policy);
            }
            return policy;
        }

    }
}
