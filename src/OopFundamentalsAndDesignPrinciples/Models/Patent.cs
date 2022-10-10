﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopFundamentalsAndDesignPrinciples.Models
{
    public class Patent : IDocumentItem
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Authors { get; set; }

        public DateTime ExpirationDate { get; set; }

        public DateTime PublishDate { get; set; }

        public DocumentItemType DocumentType => DocumentItemType.Patent;
    }
}
