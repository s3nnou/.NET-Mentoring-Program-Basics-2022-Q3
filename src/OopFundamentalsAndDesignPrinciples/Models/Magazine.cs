using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
