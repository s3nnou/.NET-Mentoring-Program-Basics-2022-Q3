using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Task1.DoNotChange;

namespace Task1
{
    public static class LinqTask
    {
        public static IEnumerable<Customer> Linq1(IEnumerable<Customer> customers, decimal limit)
        {
            return customers.Where(i => i.Orders.Sum(x => x.Total) > limit);
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            return customers.Select(i => (i, suppliers.Where(y => i.City == y.City && i.Country == y.Country)));
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2UsingGroup(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            return  from customer in customers
                    join supplier in suppliers on new { customer.City, customer.Country } equals new { supplier.City, supplier.Country } into gj
                    from subpet in gj.DefaultIfEmpty()
                    select (customer, gj);
        }

        public static IEnumerable<Customer> Linq3(IEnumerable<Customer> customers, decimal limit)
        {
            return customers.Where(i => i.Orders.Any(x => x.Total >= limit));
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq4(
            IEnumerable<Customer> customers
        )
        {
            return customers.Select(i => (i, i.Orders.OrderBy(x => x.OrderDate).Select(i => i.OrderDate).FirstOrDefault())).Where(i => i.Item2 != default);
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq5(
            IEnumerable<Customer> customers
        )
        {
            return Linq4(customers).OrderBy(i => i.customer.CompanyName).OrderByDescending(i => i.customer.Orders.Sum(i => i.Total)).OrderBy(i => i.dateOfEntry.Month).OrderBy(i => i.dateOfEntry.Year);
        }

        public static IEnumerable<Customer> Linq6(IEnumerable<Customer> customers)
        {
            const string nubmersPattern = "^[0-9]+$";
            const string bracesPattern = "^\\(.*?\\)";
            return customers.Where(i => !Regex.IsMatch(i.PostalCode, nubmersPattern) || string.IsNullOrEmpty(i.Region) || !Regex.IsMatch(i.Phone, bracesPattern));
        }

        public static IEnumerable<Linq7CategoryGroup> Linq7(IEnumerable<Product> products)
        {
            /* example of Linq7result

             category - Beverages
	            UnitsInStock - 39
		            price - 18.0000
		            price - 19.0000
	            UnitsInStock - 17
		            price - 18.0000
		            price - 19.0000
             */
            return from product in products
                   group product by product.Category into g1
                   select new Linq7CategoryGroup
                   {
                        Category = g1.Key,
                        UnitsInStockGroup = from p in g1
                                            group p by p.UnitsInStock into g2
                                            select new Linq7UnitsInStockGroup
                                            {
                                            UnitsInStock = g2.Key,
                                            Prices = from prod in g2
                                                     select prod.UnitPrice,
                                            }        
                   };
        }

        public static IEnumerable<(decimal category, IEnumerable<Product> products)> Linq8(
            IEnumerable<Product> products,
            decimal cheap,
            decimal middle,
            decimal expensive
        )
        {
            const int zeroPrice = 0;
            var cheapProducts = Linq8GetFilteredProducts(products, zeroPrice, cheap);
            var middleProducts = Linq8GetFilteredProducts(products, cheap, middle);
            var expensiveProducts = Linq8GetFilteredProducts(products, middle, expensive);


            return cheapProducts.Concat(middleProducts).Concat(expensiveProducts);
        }

        private static IEnumerable<(decimal category, IEnumerable<Product> products)> Linq8GetFilteredProducts(IEnumerable<Product> products,
            decimal priceFrom,
            decimal priceTo)
        {
            return new List<(decimal category, IEnumerable<Product> products)>{ (priceTo, products.Where(i => i.UnitPrice > priceFrom && i.UnitPrice <= priceTo)) };
        }

        public static IEnumerable<(string city, int averageIncome, int averageIntensity)> Linq9(
            IEnumerable<Customer> customers
        )
        {
           return from customer in customers
                  group customer by customer.City into g
                  select (g.Key, (int) Math.Round(g.Sum(i => i.Orders.Sum(i => i.Total)) / g.Count()), g.Sum(i => i.Orders.Count()) / g.Count());              
        }

        public static string Linq10(IEnumerable<Supplier> suppliers)
        {
            var countries = suppliers.OrderBy(i => i.Country).Select(i => i.Country).Distinct().OrderBy(i => i.Length);
            return string.Join("", countries);
        }
    }
}