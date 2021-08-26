using HotChocolate;
using HotChocolate.Types;
using MSA_Final.Data;
using MSA_Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MSA_Final.GaphQL.Comments
{
    public class CommentType : ObjectType<Comment>
    {
        protected override void Configure(IObjectTypeDescriptor<Comment> descriptor)
        {
            descriptor.Field(u => u.Id).Type<NonNullType<IdType>>();
            descriptor.Field(u => u.Description).Type<NonNullType<StringType>>();

            descriptor
                .Field(u => u.Content)
                .ResolveWith<Resolvers>(r => r.GetContent(default!, default!, default))
                .UseDbContext<AppDbContext>()
                .Type<NonNullType<CommentType>>();

            descriptor
                .Field(u => u.Viewer)
                .ResolveWith<Resolvers>(r => r.GetViewer(default!, default!, default))
                .UseDbContext<AppDbContext>()
                .Type<NonNullType<CommentType>>();

            descriptor.Field(p => p.Modified).Type<NonNullType<DateTimeType>>();
            descriptor.Field(p => p.Created).Type<NonNullType<DateTimeType>>();

        }

        private class Resolvers
        {
            public async Task<Content> GetContent(Comment comment, [ScopedService] AppDbContext context,
                CancellationToken cancellationToken)
            {
                return await context.Contents.FindAsync(new object[] { comment.ContentId }, cancellationToken);
            }

            public async Task<Viewer> GetViewer(Comment comment, [ScopedService] AppDbContext context,
                CancellationToken cancellationToken)
            {
                return await context.Viewers.FindAsync(new object[] { comment.ViewerId }, cancellationToken);
            }
        }
    }
}
