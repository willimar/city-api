using GraphQL.Types;
using System.Linq;

namespace city.api.GraphQL.Queries
{
    public class MacroQuery : ObjectGraphType
    {
        public MacroQuery(CityQuery city, StateQuery state)
        {
            city.Fields.ToList().ForEach(item => this.AddField(item));
            state.Fields.ToList().ForEach(item => this.AddField(item));
        }
    }
}
