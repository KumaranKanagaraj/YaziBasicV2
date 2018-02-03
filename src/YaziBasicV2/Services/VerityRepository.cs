using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YaziBasicV2.Entities;
using YaziBasicV2.Helpers;

namespace YaziBasicV2.Services
{
    public class VerityRepository : IVerityRepository
    {
        private ArticlesContext _context;

        public VerityRepository(ArticlesContext context)
        {
            _context = context;
        }

        public Guid AddVerity(Verity verity)
        {
            //verity.VerityId = Guid.NewGuid();
            verity.CreatedTime = DateTime.Now;
            verity.UpdatedTime = DateTime.Now;
            _context.Verity.Add(verity);
            return verity.VerityId;
        }

        public void DeleteVerity(Verity verity)
        {
            _context.Verity.Remove(verity);
        }

        public IEnumerable<Verity> GetAll()
        {
            return _context.Verity.
                   OrderByDescending(t => t.UpdatedTime).ToList();
        }

        public PagedList<Verity> GetVerities(VerityResourceParameters verityResourceParameters)
        {
            return PagedList<Verity>.Create(_context.Verity.OrderBy(i => i.CreatedTime),
                verityResourceParameters.PageNumber,
                verityResourceParameters.PageSize);
        }

        public Verity GetVerity(Guid verityId)
        {
            return _context.Verity.FirstOrDefault(a => a.VerityId == verityId);
        }

        public IEnumerable<Verity> GetVerityByAuthor(int authorId)
        {
            return _context.Verity.Where(a => a.AuthorId == authorId)
                   .OrderByDescending(t => t.UpdatedTime).ToList();
        }

        public IEnumerable<Verity> GetVerityByCategory(int categoryId)
        {
            return _context.Verity.Where(a => a.CategoryId == categoryId)
                   .OrderByDescending(t => t.UpdatedTime).ToList();
        }

        public IEnumerable<Verity> GetVerityByCategoryAuthor(int categoryId, int authorId)
        {
            return _context.Verity.Where(a => a.CategoryId == categoryId && a.AuthorId == authorId)
                   .OrderByDescending(t => t.UpdatedTime).ToList();
        }

        public bool IsVerityExists(Guid verityId)
        {
            return _context.Verity.Any(a => a.VerityId == verityId);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateVerity(Verity verity)
        {
            throw new NotImplementedException();
        }
    }
}
