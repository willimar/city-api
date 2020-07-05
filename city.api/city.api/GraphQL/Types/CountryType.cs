using city.core.entities;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace city.api.GraphQL.Types
{
    public class CountryType : ObjectGraphType<Country>
    {
        public CountryType()
        {
            Field(x => x.Id, type: typeof(GuidGraphType));
            Field(f => f.Name);
            Field(f => f.Initials);
            Field(f => f.Language);
            Field(f => f.TimeZone1);
            Field(f => f.TimeZone2);
        }
    }
}
