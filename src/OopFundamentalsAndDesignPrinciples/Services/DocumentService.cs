using OopFundamentalsAndDesignPrinciples.Models;

namespace OopFundamentalsAndDesignPrinciples.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IFileRepository _fileRepository;
        private readonly ICacheService<Document> _cacheService;

        public DocumentService(IFileRepository fileRepository, ICacheService<Document> cacheService)
        {
            _fileRepository = fileRepository;
            _cacheService = cacheService;
        }

        public List<string> GetAllDocumentFileNames()
        {
            return _fileRepository.GetAllDocumentsByName();
        }

        public Document GetDocumentById(int id)
        {
            var cachedDocument = _cacheService.Retrive(id.ToString());

            if (cachedDocument == null)
            {
                var readDocument = _fileRepository.FindById(id);

                if (readDocument == null)
                {
                    throw new Exception($"There is no any documents with document id = {id}");
                }

                _cacheService.Store(readDocument);

                return readDocument;
            }
            else
            {
                return cachedDocument;
            }
        }
    }
}
