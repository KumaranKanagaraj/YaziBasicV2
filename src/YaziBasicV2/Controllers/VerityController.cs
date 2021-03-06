﻿using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YaziBasicV2.Entities;
using YaziBasicV2.Helpers;
using YaziBasicV2.Models;
using YaziBasicV2.Services;

namespace YaziBasicV2.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api")]
    public class VerityController : Controller
    {
        private IVerityRepository _verityRepository;
        private ICategoryRepository _categoryRepository;
        private ISocialImpressionRepository _socialImpressionRepository;
        private ILogger<VerityController> _logger;
        private IUrlHelper _urlHelper;

        public VerityController(IVerityRepository verityRepository,
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


        [HttpGet("test")]
        public IEnumerable<string> GetTest()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// Get verities without pagination 
        /// </summary>
        /// <returns></returns>
        [HttpGet("verities")]
        public IActionResult GetAll()
        {
            var verityEntity = _verityRepository.GetAll();
            var categoryEntity = _categoryRepository.GetCategoryFromVerity();
            var impressionEntity = _socialImpressionRepository.GetImpressionFromVerity();
            var varityForDisplayDto = new VerityModel().GetVerityForDisplayDto(verityEntity, categoryEntity, impressionEntity);
            return Ok(varityForDisplayDto);
        }

        /// <summary>
        /// Get single verity based on verity guid
        /// </summary>
        /// <param name="id">verity guid</param>
        /// <returns></returns>
        [HttpGet("verity/{id}", Name = "GetVerity")]
        public IActionResult GetVerity(Guid id)
        {
            if (!_verityRepository.IsVerityExists(id))
            {
                return NotFound();
            }
            var verityEntity = _verityRepository.GetVerity(id);
            var categoryEntity = _categoryRepository.GetCategoryFromVerity(verityEntity.CategoryId);
            var impressionEntity = _socialImpressionRepository.GetImpression(id);
            var varityForDisplayDto = new VerityModel().GetVerityForDisplayDto(verityEntity, categoryEntity, impressionEntity);
            return Ok(varityForDisplayDto);
        }

        /// <summary>
        /// Get verity list based on category Id
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <returns></returns>
        [HttpGet("category/{id:int}/verity")]
        public IActionResult GetVerity(int id)
        {
            var verityEntity = _verityRepository.GetVerityByCategory(id);
            if (verityEntity == null)
            {
                return NotFound();
            }
            var categoryEntity = _categoryRepository.GetCategoryFromVerity(id);
            var impressionEntity = _socialImpressionRepository.GetImpressionFromVerity();
            var varityForDisplayDto = new VerityModel().GetVerityForDisplayDto(verityEntity, categoryEntity, impressionEntity);
            return Ok(varityForDisplayDto);
        }

        /// <summary>
        /// Get verity list based on category name
        /// </summary>
        /// <param name="name">category name</param>
        /// <returns></returns>
        [HttpGet("category/{name}/verity")]
        public IActionResult GetVerityByCategoryName(string name)
        {
            name = name.Trim().Replace("-", " ");
            var categoryEntity = _categoryRepository.GetCategory(name);
            if (categoryEntity == null)
            {
                return NotFound();
            }
            var impressionEntity = _socialImpressionRepository.GetImpressionFromVerity();
            var verityEntity = _verityRepository.GetVerityByCategory(categoryEntity.CategoryId);
            var varityForDisplayDto = new VerityModel().GetVerityForDisplayDto(verityEntity, categoryEntity, impressionEntity);
            return Ok(varityForDisplayDto);
        }

        /// <summary>
        /// Add verity item
        /// </summary>
        /// <param name="verityDto">verityDto model object</param>
        /// <returns></returns>
        [HttpPost("verity")]
        public IActionResult AddVerity([FromBody]VerityDto verityDto)
        {
            if (verityDto == null)
            {
                return BadRequest();
            }
            var guid = Guid.NewGuid();

            var verityEntity = Mapper.Map<Verity>(verityDto);
            verityEntity.VerityId = guid;
            verityEntity.SocialImpressionId = guid;
            verityEntity.MetaTitle = SlugGenerator.GenerateTitleSlug(verityEntity.Title, guid);

            _socialImpressionRepository.AddImpression(new SocialImpression()
            {
                SocialImpressionId = guid,
                CategoryId = verityEntity.CategoryId,
                ArticleTypeId = (int)ArticleTypeEnum.Verity
            });
            if (!_socialImpressionRepository.Save())
            {
                throw new Exception($"Adding impression failed on save .");
            }

            _verityRepository.AddVerity(verityEntity);
            if (!_verityRepository.Save())
            {
                throw new Exception($"Adding verity failed on save .");
            }

            var verityModel = Mapper.Map<VerityDto>(verityEntity);
            return CreatedAtRoute("GetVerity",
                new { id = verityModel.VerityId },
                verityModel);

        }

        /// <summary>
        /// Delete verity based on verity Guid
        /// </summary>
        /// <param name="id">Verity Guid</param>
        /// <returns></returns>
        [HttpDelete("verity/{id}")]
        public IActionResult DeleteVerity(Guid id)
        {
            var verityEntity = _verityRepository.GetVerity(id);
            if (verityEntity == null)
            {
                return NotFound();
            }

            _verityRepository.DeleteVerity(verityEntity);

            if (!_verityRepository.Save())
            {
                throw new Exception($"Deleting fact {id}  failed on save.");
            }

            var impressionEntity = _socialImpressionRepository.GetImpression(id);
            if (impressionEntity == null)
            {
                return NotFound();
            }

            _socialImpressionRepository.DeleteImpression(impressionEntity);

            if (!_socialImpressionRepository.Save())
            {
                throw new Exception($"Deleting impression {id}  failed on save.");
            }

            return NoContent();
        }

        /// <summary>
        /// Update the existing verity based on verity guid
        /// </summary>
        /// <param name="id">verity Guid</param>
        /// <param name="verityDto">verityDto model object</param>
        /// <returns></returns>
        [HttpPut("verity/{id}")]
        public IActionResult UpdateVerity(Guid id, [FromBody]VerityDto verityDto)
        {
            if (verityDto == null)
            {
                return BadRequest();
            }

            var verityEntity = _verityRepository.GetVerity(id);
            if (verityEntity == null)
            {
                return NotFound();
            }

            verityDto.CreatedTime = verityEntity.CreatedTime;
            verityDto.UpdatedTime = DateTime.Now;
            verityDto.VerityId = verityEntity.VerityId;
            verityDto.SocialImpressionId = verityEntity.SocialImpressionId;
            verityEntity.MetaTitle = SlugGenerator.GenerateTitleSlug(verityEntity.Title, id);
            verityDto.ImagePath = (verityDto.ImagePath == "" || verityDto.ImagePath == null) ? verityEntity.ImagePath : verityDto.ImagePath;

            Mapper.Map(verityDto, verityEntity);
            if (!_verityRepository.Save())
            {
                throw new Exception($"Upserting fact {id}  failed on save.");
            }
            return NoContent();
        }

        /// <summary>
        /// Get tags from verity article
        /// </summary>
        /// <param name="categoryName">category-name</param>
        /// <returns></returns>
        [HttpGet("verity/tags/{categoryName?}")]
        public IActionResult GetVerityTags(string categoryName="")
        {
            IEnumerable<SocialImpression> impressionEntity;
            if (categoryName != "")
            {
                categoryName = categoryName.Trim().Replace("-", " ");
                var categoryEntity = _categoryRepository.GetCategory(categoryName);
                if (categoryEntity == null)
                {
                    return NoContent();
                }
                impressionEntity = _socialImpressionRepository.GetImpressionFromCategory(categoryEntity.CategoryId);
            }
            else
            {
                impressionEntity = _socialImpressionRepository.GetImpressionFromVerity();
                if (impressionEntity == null)
                {
                    return NoContent();
                }
            }
            var impressionId = new List<Guid>();
            var guidByAgree = impressionEntity.Where(articleId => articleId.ArticleTypeId.Equals((int)ArticleTypeEnum.Verity)).
                            OrderBy(impression => impression.Agree).
                            Select(id => id.SocialImpressionId).
                            Take(10);
            var guidByDisAgree = impressionEntity.Where(articleId => articleId.ArticleTypeId.Equals((int)ArticleTypeEnum.Verity)).
                            OrderBy(impression => impression.DisAgree).
                            Select(id => id.SocialImpressionId).
                            Take(10);
            var guidByWhatsAppShare = impressionEntity.Where(articleId => articleId.ArticleTypeId.Equals((int)ArticleTypeEnum.Verity)).
                            OrderBy(impression => impression.WhatsAppShare).
                            Select(id => id.SocialImpressionId).
                            Take(10);
            var guidByLikes = impressionEntity.Where(articleId => articleId.ArticleTypeId.Equals((int)ArticleTypeEnum.Verity)).
                            OrderBy(impression => impression.Likes).
                            Select(id => id.SocialImpressionId).
                            Take(10);
            var guidByFBShare = impressionEntity.Where(articleId => articleId.ArticleTypeId.Equals((int)ArticleTypeEnum.Verity)).
                            OrderBy(impression => impression.FacebookShare).
                            Select(id => id.SocialImpressionId).
                            Take(10);
            var guidByTwitterShare = impressionEntity.Where(articleId => articleId.ArticleTypeId.Equals((int)ArticleTypeEnum.Verity)).
                            OrderBy(impression => impression.TwitterShare).
                            Select(id => id.SocialImpressionId).
                            Take(10);

            impressionId.AddRange(guidByAgree);
            impressionId.AddRange(guidByDisAgree);
            impressionId.AddRange(guidByAgree);
            impressionId.AddRange(guidByWhatsAppShare);
            impressionId.AddRange(guidByLikes);
            impressionId.AddRange(guidByFBShare);
            impressionId.AddRange(guidByTwitterShare);
            impressionId = impressionId.Distinct().ToList();
            var verityEntity = _verityRepository.GetAll();
            var tagsEntity = new VerityModel().GetVerityTagsByImpression(verityEntity, impressionId).Distinct();
            var verityTags = new List<string>();
            foreach (var tags in tagsEntity)
            {
                var tagList = tags.Split(',');
                verityTags.AddRange(tagList);
            }
            return Ok(verityTags);
        }

        /// <summary>
        /// Get verity by tag search
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        [HttpGet("verity/search/{searchString}")]
        public IActionResult SearchVerityByTags(string searchString)
        {
            var verityEntity = _verityRepository.GetAll();
            var categoryEntity = _categoryRepository.GetCategoryFromVerity();
            var impressionEntity = _socialImpressionRepository.GetImpressionFromVerity();
            var varityForDisplayDto = new VerityModel().GetVerityByTagName(verityEntity, impressionEntity, categoryEntity,searchString);
            return Ok(varityForDisplayDto);
        }

        /// <summary>
        /// Get verity by tag and category name search
        /// </summary>
        /// <param name="searchString">tag</param>
        /// <param name="categoryName">category-name</param>
        /// <returns></returns>
        [HttpGet("verity/search/{searchString}/{categoryName}")]
        public IActionResult SearchVerityByTags(string searchString,string categoryName)
        {
            categoryName = categoryName.Trim().Replace("-", " ");
            var categoryEntity = _categoryRepository.GetCategory(categoryName);
            if (categoryEntity == null)
            {
                return NoContent();
            }
            var verityEntity = _verityRepository.GetVerityByCategory(categoryEntity.CategoryId);
            var impressionEntity = _socialImpressionRepository.GetImpressionFromCategory(categoryEntity.CategoryId);
            var varityForDisplayDto = new VerityModel().GetVerityByTagName(verityEntity, impressionEntity, categoryEntity, searchString);
            return Ok(varityForDisplayDto);
        }

    }
}
