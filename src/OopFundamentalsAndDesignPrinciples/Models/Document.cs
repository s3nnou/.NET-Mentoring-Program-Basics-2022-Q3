namespace OopFundamentalsAndDesignPrinciples.Models
{
    public class Document
    {
        public int Id { get; set; }

        public IDocumentItem Item { get; set; }

        public T GetItem<T>() where T: IDocumentItem
        {
            return (T)Item;
        }

        public string FileName => $"{Item.DocumentType}_{Id}";
    }
}
