using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YaziBasicV2.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api")]
    public class AuthorController : Controller
    {
        /// <summary>
        /// Get all authors
        /// </summary>
        /// <returns></returns>
        [HttpGet("author")]
        public IActionResult GetAuthors()
        {
            var authors = new List<string>()
            {
                "Admin",
                "Kesavan Kanagaraj",
                "Swathi Kesavan",
                "Kumaran Kanagaraj",
                "Deepak Subaramani"
            };
            return Ok(authors);
        }
    }
}
