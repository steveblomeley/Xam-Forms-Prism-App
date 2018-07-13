using System.Collections.Generic;
using XamFormsPrism.Models;

namespace XamFormsPrism.Services
{
    public interface IContactService
    {
        IEnumerable<Contact> GetContacts();
    }
}