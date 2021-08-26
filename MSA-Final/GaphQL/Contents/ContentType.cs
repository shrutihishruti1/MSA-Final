using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using MSA_Final.Data;
using MSA_Final.GaphQL.Comments;
using MSA_Final.GaphQL.Viewers;
using MSA_Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MSA_Final.GaphQL.Contents
{
    public class ContentType : ObjectType<Content>
    {
        protected override void Configure(IObjectTypeDescriptor<Content> descriptor)
        {
            descriptor.Field(p => p.Id).Type<NonNullType<IdType>>();
            descriptor.Field(p => p.Name).Type<NonNullType<StringType>>();
            descriptor.Field(p => p.Description).Type<NonNullType<StringType>>();
            descriptor.Field(p => p.Link).Type<NonNullType<StringType>>();
            descriptor.Field(p => p.Year).Type<NonNullType<EnumType<Year>>>();

            descriptor
                .Field(p => p.Viewer)
                .ResolveWith<Resolvers>(r => r.GetViewer(default!, default!, default))
                .UseDbContext<AppDbContext>()
                .Type<NonNullType<ViewerType>>();

            descriptor
                .Field(p => p.Comments)
                .ResolveWith<Resolvers>(r => r.GetComments(default!, default!, default))
                .UseDbContext<AppDbContext>()
                .Type<NonNullType<ListType<NonNullType<CommentType>>>>();

            descriptor.Field(p => p.Modified).Type<NonNullType<DateTimeType>>();
            descriptor.Field(p => p.Created).Type<NonNullType<DateTimeType>>();

        }


        private class Resolvers
        {
            public async Task<Viewer> GetViewer(Content content, [ScopedService] AppDbContext context,
                CancellationToken cancellationToken)
            {
                return await context.Viewers.FindAsync(new object[] {content.ViewerId }, cancellationToken);
            }

            public async Task<IEnumerable<Comment>> GetComments(Content content, [ScopedService] AppDbContext context,
                CancellationToken cancellationToken)
            {
                return await context.Comments.Where(c => c.ContentId == content.Id).ToArrayAsync(cancellationToken);
            }
        }
    }
}