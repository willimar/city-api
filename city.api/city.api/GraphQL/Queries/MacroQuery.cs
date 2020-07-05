using GraphQL.Types;
using System.Linq;

namespace city.api.GraphQL.Queries
{
    public class MacroQuery : ObjectGraphType
    {
        public MacroQuery(CityQuery city, StateQuery state, CountryQuery country)
        {
            city.Fields.ToList().ForEach(item => this.AddField(item));
            state.Fields.ToList().ForEach(item => this.AddField(item));
            country.Fields.ToList().ForEach(item => this.AddField(item));
        }
    }
}
