using HotChocolate;
using HotChocolate.Types;
using MSA_Final.Data;
using MSA_Final.Extensions;
using MSA_Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSA_Final.GaphQL.Viewers 
{
    [ExtendObjectType(name: "Query")]
    public class ViewerQueries
    {
        [UseAppDbContext]
        [UsePaging]
        public IQueryable<Viewer> GetViewers([ScopedService] AppDbContext context)
        {
            return context.Viewers;
        }

        [UseAppDbContext]
        public Viewer GetViewer(int id, [ScopedService] AppDbContext context)
        {
            return context.Viewers.Find(id);
        }
    }
}
