using OopFundamentalsAndDesignPrinciples.Models;

namespace OopFundamentalsAndDesignPrinciples.Services
{
    public interface IRepository
    {
        public Document FindById(int id);
    }
}
