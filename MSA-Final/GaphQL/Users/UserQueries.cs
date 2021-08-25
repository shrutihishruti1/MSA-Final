using HotChocolate;
using HotChocolate.Types;
using MSA_Final.Data;
using MSA_Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSA_Final.GaphQL.Users
{
    [ExtendObjectType(name: "Query")]
    public class UserQueries
    {
        public IQueryable<User> GetUsers([ScopedService] AppDbContext context)
        {
            return context.Users;
        }
    }
}
