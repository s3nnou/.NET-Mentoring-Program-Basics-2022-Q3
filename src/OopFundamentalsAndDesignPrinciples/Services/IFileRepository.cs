using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopFundamentalsAndDesignPrinciples.Services
{
    public interface IFileRepository : IRepository
    {
        public List<string> GetAllDocumentsByName();
    }
}
