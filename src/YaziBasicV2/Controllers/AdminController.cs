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
    [Route("admin")]
    public class AdminController : Controller
    {
        [HttpGet("add-post")]
        public async Task<IActionResult> AddPost()
        {
            var categoryList = new List<CategoryForDisplay>();
            var authorList = new List<string>();
            var displayModel = new AdminDisplayModel();
            try
            {
                var category = await new ApiHandler().GetData(UriHelper.BaseUrl, UriHelper.GetCategoryFromVerity);
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

        [HttpPost("add-post/{type}/{factId?}")]
        public async Task<IActionResult> AddPost(string type, string factId, VerityDto verityDto)
        {
            long size = 0;
            try
            {
                verityDto.MetaDescription = verityDto.Description.Substring(0, Math.Min(verityDto.Description.Length, 20));
                var response = "";
                if (type == "add")
                    response = await new ApiHandler().PostFactData(UriHelper.BaseUrl, UriHelper.PostVerity, verityDto);
                else
                    response = await new ApiHandler().UpdateFactData(UriHelper.BaseUrl, UriHelper.PutVerity+"/"+factId, verityDto);
            }
            catch (Exception ex)
            {
                //
            }
            return RedirectToAction("ViewPost", "Admin");
        }

        [Route("edit-post")]
        public async Task<IActionResult> EditPost(string id)
        {
            var categoryList = new List<CategoryForDisplay>();
            var authorList = new List<string>();
            var factDto = new VerityForDisplayDto();
            var displayModel = new AdminDisplayModel();
            try
            {
                var facts = await new ApiHandler().GetData(UriHelper.BaseUrl, UriHelper.GetVerity+"/"+id);
                factDto = JsonConvert.DeserializeObject<VerityForDisplayDto>(facts);
                var category = await new ApiHandler().GetData(UriHelper.BaseUrl, UriHelper.GetCategoryFromVerity);
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
            displayModel.FactDto = factDto;
            return View(displayModel);
        }

        [Route("view-post")]
        public async Task<IActionResult> ViewPost()
        {
            var factList = new List<VerityForDisplayDto>();
            var displayModel = new AdminDisplayModel();
            try
            {
                var facts = await new ApiHandler().GetData(UriHelper.BaseUrl, UriHelper.GetVerities);
                factList = JsonConvert.DeserializeObject<List<VerityForDisplayDto>>(facts);
            }
            catch (Exception ex)
            {
                //
            }
            displayModel.Facts = factList;
            return View(displayModel);
        }

        [Route("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var facts = await new ApiHandler().DeleteData(UriHelper.BaseUrl, UriHelper.DeleteVerity, id);
            }
            catch (Exception ex)
            {
                //
            }
            return RedirectToAction("ViewPost", "Admin");
        }

        [HttpGet("category")]
        public async Task<IActionResult> Category()
        {
            var categoryList = new List<CategoryForDisplay>();
            var displayModel = new AdminDisplayModel();
            try
            {
                var category = await new ApiHandler().GetData(UriHelper.BaseUrl, UriHelper.GetCategoryFromVerity);
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
                category.ArticleTypeId = (int)ArticleTypeEnum.Verity;
                var response = await new ApiHandler().PostCategoryData(UriHelper.BaseUrl, UriHelper.PostCategoryInVerity, category);
            }
            catch (Exception ex)
            {
                //
            }
            return RedirectToAction("category", "Admin");
        }

        [HttpPost("updatecategory")]
        public async Task<IActionResult> UpdateCategory(CategoryDto category)
        {
            try
            {
                category.ArticleTypeId = (int)ArticleTypeEnum.Verity;
                var response = await new ApiHandler().UpdateCategoryData(UriHelper.BaseUrl, 
                    UriHelper.PutCategory+$"/{category.CategoryId}", category);
            }
            catch (Exception ex)
            {
                //
            }
            return RedirectToAction("category", "Admin");
        }

        [HttpGet("delete-category")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            try
            {
                var facts = await new ApiHandler().DeleteData(UriHelper.BaseUrl, UriHelper.DeleteCategory, id);
            }
            catch (Exception ex)
            {
                //
            }
            return RedirectToAction("category", "Admin");
        }
    }
}
