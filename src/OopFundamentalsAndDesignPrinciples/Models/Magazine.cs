namespace OopFundamentalsAndDesignPrinciples.Models
{
    public class Magazine : IDocumentItem
    {
        public string Title { get; set; }

        public string Publisher { get; set; }

        public int ReleaseNumber { get; set; }

        public DateTime PublishDate { get; set; }

        public DocumentItemType DocumentType => DocumentItemType.Magazine;
    }
}
