using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YaziBasicV2.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        //[ForeignKey("ArticleTypeId")]
        //public ArticleType ArticleType { get; set; }
        public int ArticleTypeId { get; set; }

    }
}
