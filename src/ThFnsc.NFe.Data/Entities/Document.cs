using Microsoft.EntityFrameworkCore;
using System;
using ThFnsc.NFe.Data.Entities.Shared;

namespace ThFnsc.NFe.Data.Entities
{
    [Index(nameof(DocType))]
    [Index(nameof(DocIdentifier))]
    [Index(nameof(Email))]
    public class Document : BaseSoftDeleteEntity
    {
        public string DocType { get; private set; }

        public string DocIdentifier { get; private set; }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public Address Address { get; private set; }

        public Document(string docType, string docIdentifier, string name, string email, Address address)
        {
            DocType = docType ?? throw new ArgumentNullException(nameof(docType));
            DocIdentifier = docIdentifier ?? throw new ArgumentNullException(nameof(docIdentifier));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }

        protected Document() { }
    }
}
