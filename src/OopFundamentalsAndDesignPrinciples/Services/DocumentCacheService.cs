using System.Runtime.Caching;
using OopFundamentalsAndDesignPrinciples.Models;

namespace OopFundamentalsAndDesignPrinciples.Services
{
    public class DocumentCacheService : ICacheService<Document>
    {
        private static readonly MemoryCache _cache = MemoryCache.Default;
        private static readonly Dictionary<DocumentItemType, DateTimeOffset> ItemPolicy
            = new Dictionary<DocumentItemType, DateTimeOffset>
            {
                { DocumentItemType.Book, MemoryCache.InfiniteAbsoluteExpiration },
                { DocumentItemType.Patent, DateTime.Now.AddDays(1) },
                { DocumentItemType.LocalaziedBook, DateTime.Now.AddDays(2) },
            };

        public Document Retrive(string key)
        {
            if (_cache.Contains(key))
            {
                var retrivedItem = _cache.Get(key) as Document;

                return retrivedItem;
            }

            return null;
        }

        public void Store(Document item)
        {
            var restrictedDocuments = new List<DocumentItemType> { DocumentItemType.Unknown, DocumentItemType.Magazine };

            if (restrictedDocuments.Contains(item.Item.DocumentType))
            {
                return;
            }

            if (_cache.Contains(item.Id.ToString()))
            {
                return;
            }

            var cacheItemPolicy = new CacheItemPolicy()
            {
                AbsoluteExpiration = ItemPolicy[item.Item.DocumentType],
            };

            _cache.Add(item.Id.ToString(), item, cacheItemPolicy);
        }
    }
}
