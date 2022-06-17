using System;

namespace Domain
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public DateTime Created { get; set; }
        public long ISBN { get; set; }
        public byte[] Cover { get; set; }
        public string Description { get; set; }
    }
}
