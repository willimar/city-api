using city.api.GraphQL.Types;
using city.core.entities;
using crud.api.core.repositories;
using graph.simplify.core.queries;

namespace city.api.GraphQL.Queries
{
    public class CityQuery : AppQuery<City, CityType>
    {
        public CityQuery(IRepository<City> repository) : base(repository)
        {
        }
    }
}
