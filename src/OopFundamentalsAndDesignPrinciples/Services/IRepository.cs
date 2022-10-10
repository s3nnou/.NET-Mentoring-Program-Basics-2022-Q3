using OopFundamentalsAndDesignPrinciples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopFundamentalsAndDesignPrinciples.Services
{
    public interface IRepository
    {
        public Document FindById(int id);
    }
}
