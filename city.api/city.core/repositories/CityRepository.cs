using city.core.entities;
using crud.api.core.repositories;
using data.provider.core;

namespace city.core.repositories
{
    public class CityRepository : BaseRepository<City>
    {
        public CityRepository(IDataProvider provider) : base(provider)
        {
        }
    }
}
