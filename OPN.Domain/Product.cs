using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPN.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public List<string> Institutions { get; set; }
        private Dictionary<string, int> InstitutionProportions { get; set; }

        public Product()
        {

        }

        public Product(int id, string name, Dictionary<string, int> institutionProportions, int amount)
        {
            Id = id;
            Name = name;
            InstitutionProportions = institutionProportions;
            Institutions = institutionProportions.Keys.ToList();
            Amount = amount;
        }
        public int GetInstitutionProportion(string institution)
        {
            return InstitutionProportions[institution];
        }

    }
}
