using System;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using MSA_Final.Models;
using MSA_Final.Data;
using MSA_Final.Extensions;
using MSA_Final.Extensions;

namespace MSA_Final.GaphQL.Contents

{
    [ExtendObjectType(name: "Mutation")]
public class ContentMutations
{
    [UseAppDbContext]
    public async Task<Content> AddContentAsync(AddContentInput input,
        [ScopedService] AppDbContext context, CancellationToken cancellationToken)
    {
        var content = new Content
        {
            Name = input.Name,
            Description = input.Description,
            Link = input.Link,
            Year = (Year)Enum.Parse(typeof(Year), input.Year),
            ViewerId = int.Parse(input.ViewerId),
            Modified = DateTime.Now,
            Created = DateTime.Now,
        };
        context.Contents.Add(content);

        await context.SaveChangesAsync(cancellationToken);

        return content;
    }

    [UseAppDbContext]
    public async Task<Content> EditContentAsync(EditContentInput input,
        [ScopedService] AppDbContext context, CancellationToken cancellationToken)
    {
        var content = await context.Contents.FindAsync(int.Parse(input.ContentId));

        content.Name = input.Name ?? content.Name;
        content.Description = input.Description ?? content.Description;
        content.Link = input.Link ?? content.Link;
        content.Modified = DateTime.Now;

        await context.SaveChangesAsync(cancellationToken);

        return content;
    }
}
}
