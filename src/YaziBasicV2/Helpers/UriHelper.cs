using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YaziBasicV2.Helpers
{
    public class UriHelper
    {
        

        public static string BaseUrl = "";
        public static string DbConnectionString = "";

        public static string GetVerities = $"/api/verities";
        public static string GetVerity = $"/api/verity";
        public static string DeleteVerity = $"/api/verity";
        public static string GetCategoryFromVerity = $"/api/verity/category";
        public static string PostCategoryInVerity = $"/api/verity/category";
        public static string PutCategory = $"/api/category";
        public static string DeleteCategory = $"/api/category";
        public static string GetAuthors = $"/api/author";
        public static string PostVerity = $"/api/verity";
        public static string PutVerity = $"/api/verity";
    }
}
