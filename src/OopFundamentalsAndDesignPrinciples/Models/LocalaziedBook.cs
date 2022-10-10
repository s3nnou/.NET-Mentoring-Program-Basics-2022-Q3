using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopFundamentalsAndDesignPrinciples.Models
{
    public class LocalaziedBook : IDocumentItem
    {
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
