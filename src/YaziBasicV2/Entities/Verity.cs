using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YaziBasicV2.Entities
{
    public class Verity
    {
        [Key]
        public Guid VerityId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string MetaDescription { get; set; }

        public string ImagePath { get; set; }

        public string Tags { get; set; }

        public bool IsPublic { get; set; }

        public DateTime CreatedTime { get; set; }

        public DateTime UpdatedTime { get; set; }

        //[ForeignKey("CategoryId")]
        //public Category Category { get; set; }
        public int CategoryId { get; set; }

        //[ForeignKey("AuthorId")]
        //public Author Author { get; set; }
        public int AuthorId { get; set; }

        //[ForeignKey("SocialImpressionId")]
        //public SocialImpression SocialImpression { get; set; }
        public Guid SocialImpressionId { get; set; }

    }
}
