namespace OopFundamentalsAndDesignPrinciples.Services
{
    public interface IFileRepository : IRepository
    {
        public List<string> GetAllDocumentsByName();
    }
}
