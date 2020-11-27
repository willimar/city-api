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
    public class StateQuery : AppQuery<State, StateType>
    {
        public StateQuery(IRepository<State> repository, ExternalAccessSettings externalApiSettings) : base(repository)
        {
            this.UseAuthenticate = false;
            this.AuthenticateApi = externalApiSettings.AuthenticateApi;
        }
    }
}
