using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YaziBasicV2.Entities;
using YaziBasicV2.Helpers;

namespace YaziBasicV2.Services
{
    public interface IEcardsRepository
    {
        IEnumerable<Ecards> GetAll();
        PagedList<Ecards> GetEcards(ResourceParameters resourceParameters);
        Ecards GetEcard(Guid eCardId);
        Guid AddECard(Ecards eCard);
        void UpdateECard(Ecards eCard);
        void DeleteECard(Ecards eCard);
        bool IsECardExists(Guid eCardId);
        IEnumerable<Ecards> GetECardsByCategory(int categoryId);
        bool Save();
    }

}
