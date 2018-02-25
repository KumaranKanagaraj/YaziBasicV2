using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace YaziBasicV2.Helpers
{
    public class SlugGenerator
    {
        public static string GenerateSlug(string phrase)
        {
            string str = RemoveAccent(phrase).ToLower();

            str = Regex.Replace(str, @"[^a-z0-9\s-]", ""); // invalid chars           
            str = Regex.Replace(str, @"\s+", " ").Trim(); // convert multiple spaces into one space   
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim(); // cut and trim it   
            str = Regex.Replace(str, @"\s", "-"); // hyphens   

            return str;
        }

        public static string RemoveAccent(string pharse)
        {
            if (string.IsNullOrWhiteSpace(pharse))
                return pharse;

            pharse = pharse.Normalize(NormalizationForm.FormD);
            var chars = pharse.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
            return new string(chars).Normalize(NormalizationForm.FormC);
        }

        public static string GenerateTitleSlug(string pharse, Guid guid)
        {
            return $"{GenerateSlug(pharse)}-{guid}";
        }

        public static string GenerateTitleSlug(string pharse)
        {
            return $"{GenerateSlug(pharse)}";
        }
    }
}
