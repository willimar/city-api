using city.core.entities;
using GraphQL.Types;

namespace city.api.GraphQL.Types
{
    public class StateType : ObjectGraphType<State>
    {
        public StateType()
        {
            Field(x => x.Id, type: typeof(GuidGraphType)).Description("Property Id is Guid type and unique in database.");
            Field(x => x.IbgeCode);
            Field(x => x.Initials);
            Field(x => x.Name);
            Field(x => x.NumberCities);
            Field(x => x.Region);
        }
    }
}