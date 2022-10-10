namespace OopFundamentalsAndDesignPrinciples.Models
{
    public class Book : IDocumentItem
    {
        public string ISBN { get; set; }    
        public string Title { get; set; }    

        public string Authors { get; set; }

        public string Publisher { get; set; }

        public int NumberOfPages { get; set; }

        public DateTime PublishDate { get; set; }

        public DocumentItemType DocumentType => DocumentItemType.Book;
    }
}
