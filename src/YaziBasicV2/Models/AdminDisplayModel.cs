using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YaziBasicV2.Models
{
    public class AdminDisplayModel
    {
        public List<VerityForDisplayDto> Facts { get; set; }
        public VerityForDisplayDto FactDto { get; set; }
        public List<CategoryForDisplay> Categories { get; set; }
        public List<string> Authors { get; set; }
        public Dictionary<int, string> CategoryIdName { get; set; }
    }
}
