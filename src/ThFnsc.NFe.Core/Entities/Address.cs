using Microsoft.EntityFrameworkCore;
using System;
using ThFnsc.NFe.Core.Entities.Shared;

namespace ThFnsc.NFe.Core.Entities
{
    [Index(nameof(PostalCode))]
    [Index(nameof(CityId))]
    [Index(nameof(State))]
    public class Address : BaseEntity
    {
        public string Street { get; private set; }

        public string StreetNumber { get; private set; }

        public string City { get; private set; }

        public int CityId { get; private set; }

        public string Complement { get; private set; }

        public string Neighborhood { get; private set; }

        public string State { get; private set; }

        public string PostalCode { get; private set; }

        public Address(string street, string streetNumber, string city, int cityId, string complement, string neighborhood, string state, string postalCode)
        {
            Street = street ?? throw new ArgumentNullException(nameof(street));
            StreetNumber = streetNumber ?? throw new ArgumentNullException(nameof(streetNumber));
            City = city ?? throw new ArgumentNullException(nameof(city));
            CityId = cityId;
            Complement = complement;
            Neighborhood = neighborhood ?? throw new ArgumentNullException(nameof(state));
            State = state ?? throw new ArgumentNullException(nameof(state));
            PostalCode = postalCode ?? throw new ArgumentNullException(nameof(postalCode));
        }

        protected Address() { }
    }
}
