namespace OopFundamentalsAndDesignPrinciples.Models
{
    public interface IDocumentItem
    {
        public string Title { get; set; }

        public DocumentItemType DocumentType { get; }
    }
}
