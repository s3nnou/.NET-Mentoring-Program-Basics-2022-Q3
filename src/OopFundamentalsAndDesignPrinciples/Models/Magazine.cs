namespace OopFundamentalsAndDesignPrinciples.Models
{
    public class Magazine : IDocumentItem
    {
        public Magazine(string publisher, int releaseNumber, DateTime publishDate, string title)
        {
            Publisher = publisher;
            ReleaseNumber = releaseNumber;
            PublishDate = publishDate;
            Title = title;
        }

        public string Publisher { get; set; }

        public int ReleaseNumber { get; set; }

        public DateTime PublishDate { get; set; }

        public DocumentItemType DocumentType => DocumentItemType.Magazine;

        public string Title { get; set; }
    }
}
