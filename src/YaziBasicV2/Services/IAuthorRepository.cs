using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YaziBasicV2.Entities;

namespace YaziBasicV2.Services
{
    public interface IAuthorRepository
    {
        Author GetAuthorFromVerity(int authorId);
        void AddAuthor(Author author);
        void UpdateAuthor(Author author);
        void DeleteAuthor(Author author);
        bool IsAuthorExists(int id);
        IEnumerable<Author> GetAuthorFromVerity();
        bool Save();
    }
}
