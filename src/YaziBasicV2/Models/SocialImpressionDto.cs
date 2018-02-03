using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YaziBasicV2.Models
{
    public class SocialImpressionDto
    {
        public Guid SocialImpressionId { get; set; }

        public int ArticleTypeId { get; set; } = 1;

        public int CategoryId { get; set; } = 1;

        public int Likes { get; set; } = 0;

        public int WhatsAppShare { get; set; } = 0;

        public int FacebookShare { get; set; } = 0;

        public int TwitterShare { get; set; } = 0;
    }
}
