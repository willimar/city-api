using crud.api.core.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace city.core.entities
{
    public class State: BaseEntity
    {
        public string Name { get; set; }
        public int IbgeCode { get; set; }
        public string Initials { get; set; }
        public string Region { get; set; }
        public int NumberCities { get; set; }
    }
}
