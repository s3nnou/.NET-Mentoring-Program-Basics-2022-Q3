using OopFundamentalsAndDesignPrinciples.Models;

namespace OopFundamentalsAndDesignPrinciples.Services
{
    public interface IDocumentService
    {
        public List<string> GetAllDocumentFileNames();

        public Document GetDocumentById(int id);
    }
}
