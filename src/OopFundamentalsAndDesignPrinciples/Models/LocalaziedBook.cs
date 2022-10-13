namespace OopFundamentalsAndDesignPrinciples.Models
{
    public class LocalaziedBook : IDocumentItem
    {
        public LocalaziedBook(string iSBN, string title, string authors, string originalPublisher, string localPublisher, string countryOfLocalization, int numberOfPages, DateTime publishDate)
        {
            ISBN = iSBN;
            Title = title;
            Authors = authors;
            OriginalPublisher = originalPublisher;
            LocalPublisher = localPublisher;
            CountryOfLocalization = countryOfLocalization;
            NumberOfPages = numberOfPages;
            PublishDate = publishDate;
        }

        public string ISBN { get; set; }

        public string Title { get; set; }

        public string Authors { get; set; }

        public string OriginalPublisher { get; set; }

        public string LocalPublisher { get; set; }

        public string CountryOfLocalization { get; set; }

        public int NumberOfPages { get; set; }

        public DateTime PublishDate { get; set; }

        public DocumentItemType DocumentType => DocumentItemType.LocalaziedBook;
    }
}
