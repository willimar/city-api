using crud.api.core.entities;

namespace city.core.entities
{
    public class Country: BaseEntity
    {
        public string Name { get; set; }
        public string Initials { get; set; }
        public string Language { get; set; }
        public string TimeZone1 { get; set; }
        public string TimeZone2 { get; set; }
    }
}