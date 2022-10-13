namespace OopFundamentalsAndDesignPrinciples.Models
{
    public class Book : IDocumentItem
    {
        public Book(string iSBN, string title, string authors, string publisher, int numberOfPages, DateTime publishDate)
        {
            ISBN = iSBN;
            Title = title;
            Authors = authors;
            Publisher = publisher;
            NumberOfPages = numberOfPages;
            PublishDate = publishDate;
        }

        public string ISBN { get; set; }    
        public string Title { get; set; }    

        public string Authors { get; set; }

        public string Publisher { get; set; }

        public int NumberOfPages { get; set; }

        public DateTime PublishDate { get; set; }

        public DocumentItemType DocumentType => DocumentItemType.Book;
    }
}
