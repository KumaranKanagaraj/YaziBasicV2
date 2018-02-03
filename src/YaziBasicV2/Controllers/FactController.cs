using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YaziBasicV2.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("fact")]
    public class FactController : Controller
    {
        [Route("~/")]
        [Route("")]
        [Route("Index")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet("{categoryName}")]
        public async Task<IActionResult> SpecificCategory(string categoryName)
        {
            return View();
        }


        [HttpGet("{categoryName}/{id}")]
        public async Task<IActionResult> SinglePost(string categoryName,int id)
        {
            return View();
        }
    }
}
