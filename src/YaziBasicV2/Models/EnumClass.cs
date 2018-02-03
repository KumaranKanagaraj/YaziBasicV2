using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YaziBasicV2.Models
{
    public enum ArticleTypeEnum
    {
        Verity = 1,
        News,
        Quotes
    }

    public enum AuthorEnum
    {
        Admin = 1,
        [Display(Name = "Kesavan Kanagaraj")]
        KesavanKanagaraj = 2,
        [Display(Name = "Swathi Kanagaraj")]
        SwathiKesavan = 3,
        [Display(Name = "Kumaran Kanagaraj")]
        KumaranKanagaraj = 4,
        [Display(Name = "Deepak Subaramani")]
        DeepakSubaramani = 5
    }
}
