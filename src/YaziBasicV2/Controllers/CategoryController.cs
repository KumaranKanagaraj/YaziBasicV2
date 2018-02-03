using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YaziBasicV2.Entities;
using YaziBasicV2.Models;
using YaziBasicV2.Services;

namespace YaziBasicV2.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api")]
    public class CategoryController : Controller
    {
        private IVerityRepository _verityRepository;
        private ICategoryRepository _categoryRepository;
        private ISocialImpressionRepository _socialImpressionRepository;
        private ILogger<VerityController> _logger;
        private IUrlHelper _urlHelper;

        public CategoryController(IVerityRepository verityRepository,
                ICategoryRepository categoryRepository,
                ISocialImpressionRepository socialImpressionRepository,
                ILogger<VerityController> logger, IUrlHelper urlHelper)
        {
            _logger = logger;
            _verityRepository = verityRepository;
            _categoryRepository = categoryRepository;
            _socialImpressionRepository = socialImpressionRepository;
            _urlHelper = urlHelper;
        }

        /// <summary>
        /// Create category for the verity
        /// </summary>
        /// <param name="categoryDto">categoryDto model object</param>
        /// <returns></returns>
        [HttpPost("verity/category")]
        public IActionResult CreateCategoryForVerity([FromBody]CategoryDto categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest();
            }
            var categoryEntity = Mapper.Map<Category>(categoryDto);
            _categoryRepository.AddCategory(categoryEntity);
            if (!_categoryRepository.Save())
            {
                throw new Exception($"Adding category failed on save.");
            }
            var categoryModel = Mapper.Map<CategoryDto>(categoryEntity);
            return CreatedAtRoute("GetCategory",
                new { categoryId = categoryModel.CategoryId },
                categoryModel);
        }

        /// <summary>
        /// Get single category based on category id
        /// </summary>
        /// <param name="id">category id</param>
        /// <returns></returns>
        [HttpGet("category", Name = "GetCategory")]
        public IActionResult GetCategory(int id)
        {
            var categoryEntity = _categoryRepository.GetCategory(id);
            var categoryForDisplayDto = new CategoryModel().GetCategoryForDisplayDto(categoryEntity);
            return Ok(categoryForDisplayDto);
        }

        /// <summary>
        /// Get all category from  verity
        /// </summary>
        /// <returns></returns>
        [HttpGet("verity/category")]
        public IActionResult GetCategoryInVerity()
        {
            var categoryEntity = _categoryRepository.GetCategoryFromVerity();
            var categoryForDisplayDto = new CategoryModel().GetCategoryForDisplayDto(categoryEntity);
            return Ok(categoryForDisplayDto);
        }

        /// <summary>
        /// Delte category based on category id
        /// </summary>
        /// <param name="id">category id</param>
        /// <returns></returns>
        [HttpDelete("category/{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var categoryFromRepo = _categoryRepository.GetCategory(id);
            if (categoryFromRepo == null)
            {
                return NotFound();
            }

            _categoryRepository.DeleteCategory(categoryFromRepo);

            if (!_categoryRepository.Save())
            {
                throw new Exception($"Deleting category {id}  failed on save.");
            }

            return NoContent();
        }

        /// <summary>
        /// Update category based on category id
        /// </summary>
        /// <param name="id">category id</param>
        /// <param name="categoryDto">categoryDto model object</param>
        /// <returns></returns>
        [HttpPut("category/{id}")]
        public IActionResult UpdateCategory(int id, [FromBody]CategoryDto categoryDto)
        {

            if (categoryDto == null)
            {
                return BadRequest();
            }

            var categoryFromRepo = _categoryRepository.GetCategory(id);
            if (categoryFromRepo == null)
            {
                return NotFound();
            }

            categoryDto.CategoryId = id;
            Mapper.Map(categoryDto, categoryFromRepo);
            if (!_categoryRepository.Save())
            {
                throw new Exception($"Upserting category {id}  failed on save.");
            }
            return NoContent();
        }
    }
}
