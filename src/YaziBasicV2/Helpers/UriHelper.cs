using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YaziBasicV2.Helpers
{
    public class UriHelper
    {




        public static string BaseUrl = "http://localhost:60540";
        public static string DbConnectionString = "server=localhost;port=3306;database=world;user=root;password=Coolcomp8*";

        public static string GetVerities = $"/api/verities";
        public static string GetVerity = $"/api/verity";
        public static string DeleteVerity = $"/api/verity";
        public static string GetCategoryFromVerity = $"/api/verity/category";
        public static string PostCategoryInVerity = $"/api/verity/category";
        public static string GetCategory = $"/api/category";
        public static string PutCategory = $"/api/category";
        public static string DeleteCategory = $"/api/category";
        public static string GetAuthors = $"/api/author";
        public static string PostVerity = $"/api/verity";
        public static string PutVerity = $"/api/verity";

        public static string GetEcards = $"/api/ecards";
        public static string GetEcard = $"/api/ecard";
        public static string DeleteEcard = $"/api/ecard";
        public static string GetCategoryFromEcard = $"/api/ecards/category";
        public static string PostCategoryInEcard = $"/api/ecards/category";
        public static string PostEcard = $"/api/ecard";
        public static string PutEcard = $"/api/ecard";
    }
}
