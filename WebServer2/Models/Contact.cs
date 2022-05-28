namespace WebServer2.Models
{
    public class Contact
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Server { get; set; }
        public ICollection<User> Users { get; set; }

    }
}
