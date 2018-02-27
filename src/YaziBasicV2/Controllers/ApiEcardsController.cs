using AutoMapper;
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
    public class ApiEcardsController : Controller
    {
        private IEcardsRepository _eCardsRepository;
        private ICategoryRepository _categoryRepository;
        private ISocialImpressionRepository _socialImpressionRepository;
        private IUserAndPostInfoRepository _userAndPostInfoRepository;
        private ILogger<VerityController> _logger;
        private IUrlHelper _urlHelper;
        const int maxPageSize = 10;

        public ApiEcardsController(
            IEcardsRepository eCardsRepository,
            ICategoryRepository categoryRepository,
            ISocialImpressionRepository socialImpressionRepository,
            IUserAndPostInfoRepository userAndPostInfoRepository,
            ILogger<VerityController> logger, IUrlHelper urlHelper)
        {
            _logger = logger;
            _eCardsRepository = eCardsRepository;
            _categoryRepository = categoryRepository;
            _socialImpressionRepository = socialImpressionRepository;
            _userAndPostInfoRepository = userAndPostInfoRepository;
            _urlHelper = urlHelper;
        }

        /// <summary>
        /// Get ecards without pagination 
        /// </summary>
        /// <returns></returns>
        [HttpGet("ecards")]
        public IActionResult GetAll([FromQuery]string socialId)
        {

            var eCardsEntity = _eCardsRepository.GetAll();
            var categoryEntity = _categoryRepository.GetCategoryFromEcard();
            var impressionEntity = _socialImpressionRepository.GetImpressionFromEcards();
            var eCardsForDisplay = new EcardModel().GetEcardsForDisplayDto(eCardsEntity, categoryEntity, impressionEntity).ToList();
            eCardsForDisplay.ForEach(a => a.IsLiked = _userAndPostInfoRepository.IsLiked(socialId, a.Id));
            return Ok(eCardsForDisplay);
        }

        /// <summary>
        /// Get ecards with pagination 
        /// </summary>
        /// <returns></returns>
        [HttpGet("ecardsdetails", Name = "GetEcardsDetails")]
        public IActionResult GetEcardsDetails(ResourceParameters resourceParameter, [FromQuery]string socialId)
        {
            var eCardsEntity = _eCardsRepository.GetEcards(resourceParameter);
            var previousPageLink = eCardsEntity.HasPrevious ?
                                   ResourceUri.CreateResourceUri(resourceParameter, _urlHelper, "ecardsdetails", ResourceUriType.PreviousPage) : null;
            var nextPageLink = eCardsEntity.HasNext ?
                                   ResourceUri.CreateResourceUri(resourceParameter, _urlHelper, "ecardsdetails", ResourceUriType.NextPage) : null;

            var paginationMetaData = new
            {
                totalCount = eCardsEntity.TotalCount,
                pageSize = eCardsEntity.PageSize,
                currentPage = eCardsEntity.CurrentPage,
                totalPages = eCardsEntity.TotalPages,
                previousPageLink = previousPageLink,
                nextPageLink = nextPageLink
            };

            var categoryEntity = _categoryRepository.GetCategoryFromEcard();
            var impressionEntity = _socialImpressionRepository.GetImpressionFromEcards();
            var eCardsForDisplay = new EcardModel().GetEcardsForDisplayDto(eCardsEntity, categoryEntity, impressionEntity);
            if (socialId != null)
                eCardsForDisplay.ToList().ForEach(a => a.IsLiked = _userAndPostInfoRepository.IsLiked(socialId, a.Id));

            Response.Headers.Add("X-Pagination",
                Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetaData));

            return Ok(eCardsForDisplay);
        }

        [HttpGet("ecard/{id}", Name = "GetEcard")]
        public IActionResult GetEcard(Guid id, [FromQuery]string socialId)
        {
            if (!_eCardsRepository.IsECardExists(id))
            {
                return NotFound();
            }
            var eCardEntity = _eCardsRepository.GetEcard(id);
            var categoryEntity = _categoryRepository.GetCategoryFromEcard(eCardEntity.CategoryId);
            var impressionEntity = _socialImpressionRepository.GetImpression(id);
            var eCardForDisplayDto = new EcardModel().GetECardForDisplayDto(eCardEntity, categoryEntity, impressionEntity);
            eCardForDisplayDto.IsLiked = socialId == null ? false : _userAndPostInfoRepository.IsLiked(socialId, id);
            return Ok(eCardForDisplayDto);
        }

        /// <summary>
        /// Get ecards list based on category Id
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <returns></returns>
        [HttpGet("category/{id:int}/ecards")]
        public IActionResult GetECards(int id,[FromQuery]string socialId)
        {
            var eCardsEntity = _eCardsRepository.GetECardsByCategory(id);
            if (eCardsEntity == null)
            {
                return NotFound();
            }
            var categoryEntity = _categoryRepository.GetCategoryFromEcard(id);
            var impressionEntity = _socialImpressionRepository.GetImpressionFromEcards();
            var eCardForDisplayDto = new EcardModel().GetECardsForDisplayDto(eCardsEntity, categoryEntity, impressionEntity).ToList();
            if (socialId != null)
                eCardForDisplayDto.ForEach(a => a.IsLiked = _userAndPostInfoRepository.IsLiked(socialId, a.Id));
            return Ok(eCardForDisplayDto);
        }

        /// <summary>
        /// Get ecards list based on category name
        /// </summary>
        /// <param name="name">category name</param>
        /// <returns></returns>
        [HttpGet("category/{name}/ecards")]
        public IActionResult GetEcardsByCategoryName(string name, [FromQuery]string socialId)
        {
            name = name.Trim().Replace("-", " ");
            var categoryEntity = _categoryRepository.GetCategory(name);
            if (categoryEntity == null)
            {
                return NotFound();
            }
            var impressionEntity = _socialImpressionRepository.GetImpressionFromEcards();
            var eCardsEntity = _eCardsRepository.GetECardsByCategory(categoryEntity.CategoryId);
            var eCardsForDisplayDto = new EcardModel().GetECardsForDisplayDto(eCardsEntity, categoryEntity, impressionEntity).ToList();
            if (socialId != null)
                eCardsForDisplayDto.ForEach(a => a.IsLiked = _userAndPostInfoRepository.IsLiked(socialId, a.Id));
            return Ok(eCardsForDisplayDto);
        }

        /// <summary>
        /// Add ecard item
        /// </summary>
        /// <param name="eCardDto">EcardDto model object</param>
        /// <returns></returns>
        [HttpPost("ecard")]
        public IActionResult AddEcard([FromBody]ECardDto eCardDto)
        {
            if (eCardDto == null)
            {
                return BadRequest();
            }
            var guid = Guid.NewGuid();

            var eCardsEntity = Mapper.Map<Ecards>(eCardDto);
            eCardsEntity.Id = guid;
            eCardsEntity.SocialImpressionId = guid;
            eCardsEntity.MetaTitle = SlugGenerator.GenerateTitleSlug(eCardsEntity.Title, guid);

            _socialImpressionRepository.AddImpression(new SocialImpression()
            {
                SocialImpressionId = guid,
                CategoryId = eCardsEntity.CategoryId,
                ArticleTypeId = (int)ArticleTypeEnum.Ecards
            });
            if (!_socialImpressionRepository.Save())
            {
                throw new Exception($"Adding impression failed on save .");
            }

            _eCardsRepository.AddECard(eCardsEntity);
            if (!_eCardsRepository.Save())
            {
                throw new Exception($"Adding ecard failed on save .");
            }

            var eCardModel = Mapper.Map<ECardDto>(eCardsEntity);
            return CreatedAtRoute("GetEcard",
                new { id = eCardModel.Id },
                eCardModel);

        }

        /// <summary>
        /// Delete ecard based on ecard Guid
        /// </summary>
        /// <param name="id">Ecard Guid</param>
        /// <returns></returns>
        [HttpDelete("ecard/{id}")]
        public IActionResult DeleteEcard(Guid id)
        {
            var eCardEntity = _eCardsRepository.GetEcard(id);
            if (eCardEntity == null)
            {
                return NotFound();
            }

            _eCardsRepository.DeleteECard(eCardEntity);

            if (!_eCardsRepository.Save())
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
        /// Update the existing Ecard based on ecard guid
        /// </summary>
        /// <param name="id">Ecard Guid</param>
        /// <param name="eCardDto">Ecard model object</param>
        /// <returns></returns>
        [HttpPut("ecard/{id}")]
        public IActionResult UpdateEcard(Guid id, [FromBody]ECardDto eCardDto)
        {
            if (eCardDto == null)
            {
                return BadRequest();
            }

            var eCardEntity = _eCardsRepository.GetEcard(id);
            if (eCardEntity == null)
            {
                return NotFound();
            }

            eCardDto.CreatedTime = eCardEntity.CreatedTime;
            eCardDto.UpdatedTime = DateTime.Now;
            eCardDto.Id = eCardEntity.Id;
            eCardDto.SocialImpressionId = eCardEntity.SocialImpressionId;
            eCardEntity.MetaTitle = SlugGenerator.GenerateTitleSlug(eCardEntity.Title, id);
            eCardDto.ImagePath = (eCardDto.ImagePath == "" || eCardDto.ImagePath == null) ? eCardEntity.ImagePath : eCardDto.ImagePath;

            Mapper.Map(eCardDto, eCardEntity);
            if (!_eCardsRepository.Save())
            {
                throw new Exception($"Upserting fact {id}  failed on save.");
            }
            return NoContent();
        }
    }
}
