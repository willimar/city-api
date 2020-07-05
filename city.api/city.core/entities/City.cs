using crud.api.core.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace city.core.entities
{

    public class City : BaseEntity
    {
        public string Name { get; set; }
        public State State { get; set; }
        public string Uf { get; set; }
        public int IbgeCode { get; set; }
        public int Ibge7Code { get; set; }
        public string Region { get; set; }
        public int Population { get; set; }
        public string Size { get; set; }
        public bool IsCapital { get; set; }
        public string HashId { get; set; }

        public override bool Equals(object obj)
        {
            var aux = obj as City;

            return aux?.HashId == this.HashId;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
