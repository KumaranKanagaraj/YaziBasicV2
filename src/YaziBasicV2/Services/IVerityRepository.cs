using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YaziBasicV2.Entities;
using YaziBasicV2.Helpers;

namespace YaziBasicV2.Services
{
    public interface IVerityRepository
    {
        IEnumerable<Verity> GetAll();
        Verity GetVerity(Guid verityId);
        PagedList<Verity> GetVerities(VerityResourceParameters verityResourceParameters);
        Guid AddVerity(Verity verity);
        void UpdateVerity(Verity verity);
        void DeleteVerity(Verity verity);
        bool IsVerityExists(Guid verityId);
        IEnumerable<Verity> GetVerityByAuthor(int authorId);
        IEnumerable<Verity> GetVerityByCategory(int categoryId);
        IEnumerable<Verity> GetVerityByCategoryAuthor(int categoryId, int authorId);
        bool Save();
    }
}
