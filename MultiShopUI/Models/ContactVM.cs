using Entities.Concrete;

namespace EShopUI.Models
{
    public class ContactVM
    {
        public List<Contact> GetContacts { get; set; }
        public ContactForm ContactForm { get; set; }
    }
}
