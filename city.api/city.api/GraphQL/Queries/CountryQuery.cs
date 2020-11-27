using city.api.Context;
using city.api.GraphQL.Types;
using city.core.entities;
using crud.api.core.repositories;
using graph.simplify.core.queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace city.api.GraphQL.Queries
{
    public class CountryQuery : AppQuery<Country, CountryType>
    {
        public CountryQuery(IRepository<Country> repository, ExternalAccessSettings externalApiSettings) : base(repository)
        {
            this.UseAuthenticate = true;
            this.AuthenticateApi = externalApiSettings.AuthenticateApi;
        }
    }
}
