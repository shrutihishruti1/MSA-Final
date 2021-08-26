using System;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using MSA_Final.Models;
using MSA_Final.Data;
using MSA_Final.Extensions;
using Octokit;
using HotChocolate.AspNetCore;
using MSA_Final.GaphQL.Viewers;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace MSA_Final.GaphQL.Viewers
{
    [ExtendObjectType(name: "Mutation")]
    public class ViewerMutations
    {
        [UseAppDbContext]
        public async Task<Viewer> AddViewerAsync(AddViewerInput input,
        [ScopedService] AppDbContext context, CancellationToken cancellationToken)
        {
            var viewer = new Viewer
            {
                Name = input.Name,
                GitHub = input.GitHub,
                ImageURI = input.ImageURI,
            };

            context.Viewers.Add(viewer);
            await context.SaveChangesAsync(cancellationToken);

            return viewer;
        }

        [UseAppDbContext]
        public async Task<Viewer> EditViewerAsync(EditViewerInput input,
                [ScopedService] AppDbContext context, CancellationToken cancellationToken)
        {
            var viewer = await context.Viewers.FindAsync(int.Parse(input.Id));

            viewer.Name = input.Name ?? viewer.Name;
            viewer.ImageURI = input.ImageURI ?? viewer.ImageURI;

            await context.SaveChangesAsync(cancellationToken);

            return viewer;
        }
        [UseAppDbContext]
        public async Task<LoginPayload> LoginAsync(LoginInput input, [ScopedService] AppDbContext context, CancellationToken cancellationToken)
        {
            var client = new GitHubClient(new ProductHeaderValue("MSA-Final"));

            var request = new OauthTokenRequest(Startup.Configuration["Github:ClientId"], Startup.Configuration["Github:ClientSecret"], input.Code);
            var tokenInfo = await client.Oauth.CreateAccessToken(request);

            if (tokenInfo.AccessToken == null)
            {
                throw new GraphQLRequestException(ErrorBuilder.New()
                    .SetMessage("Bad code")
                    .SetCode("AUTH_NOT_AUTHENTICATED")
                    .Build());
            }
            client.Credentials = new Credentials(tokenInfo.AccessToken);
            var user = await client.User.Current();

            var viewer = await context.Viewers.FirstOrDefaultAsync(u => u.GitHub == user.Login, cancellationToken);

            if (viewer == null)
            {
                viewer = new Viewer
                {
                    Name = user.Name ?? user.Login,
                    GitHub = user.Login,
                    ImageURI = user.AvatarUrl,
                };

                context.Viewers.Add(viewer);
                await context.SaveChangesAsync(cancellationToken);
            }

            // authentication successful so generate jwt token
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Startup.Configuration["JWT:Secret"]));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, viewer.Id.ToString()),
            };

            var jwtToken = new JwtSecurityToken(
                "MSA-Final",
                "MSA-Viewer",
                claims,
                expires: DateTime.Now.AddDays(90),
                signingCredentials: credentials);

            string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return new LoginPayload(viewer, token);

        }


    }
}
