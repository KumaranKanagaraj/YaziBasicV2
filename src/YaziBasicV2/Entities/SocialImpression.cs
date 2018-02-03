using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace YaziBasicV2.Entities
{
    public class SocialImpression
    {
        [Key]
        public Guid SocialImpressionId { get; set; }

        //[ForeignKey("ArticleTypeId")]
        //public ArticleType ArticleType { get; set; }
        public int ArticleTypeId { get; set; } = 1;

        //[ForeignKey("CategoryId")]
        //public Category Category { get; set; }
        public int CategoryId { get; set; } = 1;

        public int Likes { get; set; } = 0;

        public int WhatsAppShare { get; set; } = 0;

        public int FacebookShare { get; set; } = 0;

        public int TwitterShare { get; set; } = 0;

        public int Agree { get; set; } = 0;

        public int DisAgree { get; set; } = 0;

    }
}
