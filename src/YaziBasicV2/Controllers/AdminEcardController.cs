using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YaziBasicV2.Helpers;
using YaziBasicV2.Models;

namespace YaziBasicV2.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("admin/ecards")]
    public class AdminEcardController : Controller
    {
        [HttpGet("add-post")]
        public async Task<IActionResult> AddPost()
        {
            var categoryList = new List<CategoryForDisplay>();
            var authorList = new List<string>();
            var displayModel = new AdminDisplayModel();
            try
            {
                var category = await new ApiHandler().GetData(UriHelper.BaseUrl, UriHelper.GetCategoryFromEcard);
                categoryList = JsonConvert.DeserializeObject<List<CategoryForDisplay>>(category);
                var authors = await new ApiHandler().GetData(UriHelper.BaseUrl, UriHelper.GetAuthors);
                authorList = JsonConvert.DeserializeObject<List<string>>(authors);
            }
            catch (Exception ex)
            {
                //
            }
            displayModel.Categories = categoryList;
            displayModel.Authors = authorList;
            return View(displayModel);
        }

        [HttpPost("add-post/{type}/{id?}")]
        public async Task<IActionResult> AddPost(string type, string id, ECardDto eCardDto)
        {
            try
            {
                eCardDto.MetaDescription = eCardDto.Description.Substring(0, Math.Min(eCardDto.MetaDescription.Length, 50));
                var response = "";
                if (type == "add")
                    response = await new ApiHandler().PostEcardData(UriHelper.BaseUrl, UriHelper.PostEcard, eCardDto);
                else
                    response = await new ApiHandler().UpdateEcardData(UriHelper.BaseUrl, UriHelper.PutEcard + "/" + id, eCardDto);
            }
            catch (Exception ex)
            {
                //
            }
            return RedirectToAction("ViewPost", "AdminEcard");
        }

        [Route("edit-post")]
        public async Task<IActionResult> EditPost(string id)
        {
            var categoryList = new List<CategoryForDisplay>();
            var authorList = new List<string>();
            var eCardDto = new EcardForDisplay();
            var displayModel = new AdminDisplayModel();
            try
            {
                var eCards = await new ApiHandler().GetData(UriHelper.BaseUrl, UriHelper.GetEcard + "/" + id);
                eCardDto = JsonConvert.DeserializeObject<EcardForDisplay>(eCards);
                var category = await new ApiHandler().GetData(UriHelper.BaseUrl, UriHelper.GetCategoryFromEcard);
                categoryList = JsonConvert.DeserializeObject<List<CategoryForDisplay>>(category);
                var authors = await new ApiHandler().GetData(UriHelper.BaseUrl, UriHelper.GetAuthors);
                authorList = JsonConvert.DeserializeObject<List<string>>(authors);
            }
            catch (Exception ex)
            {
                //
            }
            displayModel.Authors = authorList;
            displayModel.Categories = categoryList;
            displayModel.EcardsDto = eCardDto;
            return View(displayModel);
        }

        [Route("view-post")]
        public async Task<IActionResult> ViewPost()
        {
            var eCardList = new List<EcardForDisplay>();
            var displayModel = new AdminDisplayModel();
            try
            {
                var facts = await new ApiHandler().GetData(UriHelper.BaseUrl, UriHelper.GetEcards);
                eCardList = JsonConvert.DeserializeObject<List<EcardForDisplay>>(facts);
            }
            catch (Exception ex)
            {
                //
            }
            displayModel.Ecards = eCardList;
            return View(displayModel);
        }

        [Route("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var facts = await new ApiHandler().DeleteData(UriHelper.BaseUrl, UriHelper.DeleteEcard, id);
            }
            catch (Exception ex)
            {
                //
            }
            return RedirectToAction("ViewPost", "AdminEcard");
        }

        [HttpGet("category")]
        public async Task<IActionResult> Category()
        {
            var categoryList = new List<CategoryForDisplay>();
            var displayModel = new AdminDisplayModel();
            try
            {
                var category = await new ApiHandler().GetData(UriHelper.BaseUrl, UriHelper.GetCategoryFromEcard);
                categoryList = JsonConvert.DeserializeObject<List<CategoryForDisplay>>(category);
            }
            catch (Exception ex)
            {
                //
            }
            displayModel.Categories = categoryList;
            return View(displayModel);
        }

        [HttpPost("category")]
        public async Task<IActionResult> Category(CategoryDto category)
        {
            try
            {
                category.ArticleTypeId = (int)ArticleTypeEnum.Ecards;
                var response = await new ApiHandler().PostCategoryData(UriHelper.BaseUrl, UriHelper.PostCategoryInEcard, category);
            }
            catch (Exception ex)
            {
                //
            }
            return RedirectToAction("category", "AdminEcard");
        }

        [HttpPost("updatecategory")]
        public async Task<IActionResult> UpdateCategory(CategoryDto category)
        {
            try
            {
                category.ArticleTypeId = (int)ArticleTypeEnum.Ecards;
                var response = await new ApiHandler().UpdateCategoryData(UriHelper.BaseUrl,
                    UriHelper.PutCategory + $"/{category.CategoryId}", category);
            }
            catch (Exception ex)
            {
                //
            }
            return RedirectToAction("category", "AdminEcard");
        }

        [HttpGet("delete-category")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            try
            {
                var eCards = await new ApiHandler().DeleteData(UriHelper.BaseUrl, UriHelper.DeleteCategory, id);
            }
            catch (Exception ex)
            {
                //
            }
            return RedirectToAction("category", "AdminEcard");
        }
    }
}
