using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using MSA_Final.Data;
using MSA_Final.GaphQL.Comments;
using MSA_Final.GaphQL.Contents;
using MSA_Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MSA_Final.GaphQL.Viewers 
{
    public class ViewerType : ObjectType<Viewer>
    {
        protected override void Configure(IObjectTypeDescriptor<Viewer> descriptor)
        {
            descriptor.Field(u => u.Id).Type<NonNullType<IdType>>();
            descriptor.Field(u => u.Name).Type<NonNullType<StringType>>();
            descriptor.Field(u => u.GitHub).Type<NonNullType<StringType>>();
            descriptor.Field(u => u.ImageURI).Type<NonNullType<StringType>>();

            descriptor
                .Field(u => u.Contents)
                .ResolveWith<Resolvers>(r => r.GetContents(default!, default!, default))
                .UseDbContext<AppDbContext>()
                .Type<NonNullType<ListType<NonNullType<ContentType>>>>();

            descriptor
                .Field(u => u.Comments)
                .ResolveWith<Resolvers>(r => r.GetComments(default!, default!, default))
                .UseDbContext<AppDbContext>()
                .Type<NonNullType<ListType<NonNullType<CommentType>>>>();
        }

        private class Resolvers
        {
            public async Task<IEnumerable<Content>> GetContents(Viewer viewer, [ScopedService] AppDbContext context,
                CancellationToken cancellationToken)
            {
                return await context.Contents.Where(c => c.ViewerId == viewer.Id).ToArrayAsync(cancellationToken);
            }

            public async Task<IEnumerable<Comment>> GetComments(Viewer viewer ,[ScopedService] AppDbContext context,
                CancellationToken cancellationToken)
            {
                return await context.Comments.Where(c => c.ViewerId == viewer.Id).ToArrayAsync(cancellationToken);
            }
        }
    }
}