using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopFundamentalsAndDesignPrinciples.Models
{
    public interface IDocumentItem
    {
        public DocumentItemType DocumentType { get; }
    }
}
