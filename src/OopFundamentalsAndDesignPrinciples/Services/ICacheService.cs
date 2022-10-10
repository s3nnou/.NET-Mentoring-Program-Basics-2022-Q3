namespace OopFundamentalsAndDesignPrinciples.Services
{
    public interface ICacheService<T> where T : class
    {
        void Store(T item);

        T Retrive(string key);
    }
}
