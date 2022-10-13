using OopFundamentalsAndDesignPrinciples.Models;
using Spectre.Console;

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

            try
            {
                var cachedDocument = _cacheService.Retrive(id.ToString());
                
                if (cachedDocument == null)
                {
                    var readDocument = _fileRepository.FindById(id);

                    _cacheService.Store(readDocument);

                    return readDocument;
                }
                else
                {
                    return cachedDocument;
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.Write($"Error happend while loading a document id = {id} with message {ex.Message}");
                return null;
            }


        }
    }
}
