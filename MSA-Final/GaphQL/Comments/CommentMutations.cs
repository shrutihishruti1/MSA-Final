using HotChocolate;
using HotChocolate.Types;
using MSA_Final.Data;
using MSA_Final.Extensions;
using MSA_Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MSA_Final.GaphQL.Comments
{
    [ExtendObjectType(name: "Mutation")]
    public class CommentMutations
    {
        [UseAppDbContext]
        public async Task<Comment> AddCommentAsync(AddCommentInput input,
        [ScopedService] AppDbContext context, CancellationToken cancellationToken)
        {
            var comment = new Comment
            {
                Description = input.Description,
                ContentId = int.Parse(input.ContentId),
                ViewerId = int.Parse(input.ViewerId),
                Modified = DateTime.Now,
                Created = DateTime.Now,
            };
            context.Comments.Add(comment);

            await context.SaveChangesAsync(cancellationToken);

            return comment;
        }

        [UseAppDbContext]
        public async Task<Comment> EditCommentAsync(EditCommentInput input,
                [ScopedService] AppDbContext context, CancellationToken cancellationToken)
        {
            var comment = await context.Comments.FindAsync(int.Parse(input.CommentId));
            comment.Description = input.Description ?? comment.Description;

            await context.SaveChangesAsync(cancellationToken);

            return comment;
        }
    }
}
