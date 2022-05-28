
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebServer2.Models
{
    public class User
    {
        public string Id { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public ICollection<Contact> Contacts { get; set; }
    }
}
