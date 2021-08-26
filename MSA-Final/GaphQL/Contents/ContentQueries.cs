using HotChocolate;
using HotChocolate.Types;
using MSA_Final.Data;
using MSA_Final.Extensions;
using MSA_Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSA_Final.GaphQL.Contents
{
    [ExtendObjectType(name: "Query")]
    public class ContentQueries 
    {
        [UseAppDbContext]
        [UsePaging]
        public IQueryable<Content> GetContents([ScopedService] AppDbContext context)
        {
            return context.Contents.OrderBy(p => p.Created);
        }

        [UseAppDbContext]
        public Content GetContent(int id, [ScopedService] AppDbContext context)
        {
            return context.Contents.Find(id);
        }
    }
}
