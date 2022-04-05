﻿using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using BasicSecurityASP.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace BasicSecurityASP.Security
{
    public class BasicAuthHandler:AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserService _userService;


        public BasicAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger,
            UrlEncoder encoder, ISystemClock clock, IUserService usernService):base(options, logger, encoder, clock)
        {
            _userService = usernService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if(!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("No viene el header");

            bool result = false;

            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                var email = credentials[0];
                var password = credentials[1];

                result = _userService.IsUser(email, password);

            }
            catch (Exception e)
            {
                return AuthenticateResult.Fail("Error inesperado");
            }

            if(!result)
                return AuthenticateResult.Fail("Usuario o contraseña incorrecta");

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "id"),
                new Claim(ClaimTypes.Name, "user"),
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
