using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YaziBasicV2.Entities;

namespace YaziBasicV2.Services
{
    public interface ISocialImpressionRepository
    {
        SocialImpression GetImpression(Guid socialImpressionId);
        void AddImpression(SocialImpression socialImpression);
        void UpdateImpression(SocialImpression socialImpression);
        void DeleteImpression(SocialImpression socialImpression);
        bool IsImpressionExists(Guid socialImpressionId);
        IEnumerable<SocialImpression> GetImpressionFromVerity();
        IEnumerable<SocialImpression> GetImpressionFromCategory(int categoryId);
        bool Save();
    }
}
