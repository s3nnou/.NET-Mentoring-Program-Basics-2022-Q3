namespace OopFundamentalsAndDesignPrinciples.Models
{
    public class Patent : IDocumentItem
    {
        public Patent(string id, string title, string authors, DateTime expirationDate, DateTime publishDate)
        {
            Id = id;
            Title = title;
            Authors = authors;
            ExpirationDate = expirationDate;
            PublishDate = publishDate;
        }

        public string Id { get; set; }

        public string Title { get; set; }

        public string Authors { get; set; }

        public DateTime ExpirationDate { get; set; }

        public DateTime PublishDate { get; set; }

        public DocumentItemType DocumentType => DocumentItemType.Patent;
    }
}
