using System.ComponentModel.DataAnnotations;

namespace YaziBasicV2.Entities
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        public string Name { get; set; }
    }
}
