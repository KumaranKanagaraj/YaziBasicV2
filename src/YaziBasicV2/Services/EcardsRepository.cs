using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YaziBasicV2.Entities;
using YaziBasicV2.Helpers;

namespace YaziBasicV2.Services
{
    public class EcardsRepository : IEcardsRepository
    {
        private ArticlesContext _context;

        public EcardsRepository(ArticlesContext context)
        {
            _context = context;
        }

        public Guid AddECard(Ecards eCard)
        {
            eCard.CreatedTime = DateTime.Now;
            eCard.UpdatedTime = DateTime.Now;
            _context.Ecards.Add(eCard);
            return eCard.Id;
        }

        public void DeleteECard(Ecards eCard)
        {
            _context.Ecards.Remove(eCard);
        }

        public IEnumerable<Ecards> GetAll()
        {
            return _context.Ecards.
                   OrderByDescending(t => t.UpdatedTime).ToList();
        }

        public PagedList<Ecards> GetEcards(ResourceParameters resourceParameters)
        {
            var collectionBeforePaging = _context.Ecards
                   .OrderByDescending(t => t.UpdatedTime);

            return PagedList<Ecards>.Create(collectionBeforePaging,
                resourceParameters.PageNumber,
                resourceParameters.PageSize);
        }

        public Ecards GetEcard(Guid eCardId)
        {
            return _context.Ecards.FirstOrDefault(a => a.Id == eCardId);
        }

        public IEnumerable<Ecards> GetECardsByCategory(int categoryId)
        {
            return _context.Ecards.Where(a => a.CategoryId == categoryId)
                   .OrderByDescending(t => t.UpdatedTime).ToList();
        }

        public bool IsECardExists(Guid eCardId)
        {
            return _context.Ecards.Any(a => a.Id == eCardId);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateECard(Ecards eCard)
        {
            throw new NotImplementedException();
        }
    }
}
