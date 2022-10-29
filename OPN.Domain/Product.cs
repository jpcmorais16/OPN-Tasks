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
        public List<string> Institutions { get; set; }
        private Dictionary<string, int> InstitutionProportions { get; set; }
        private int NumberOfInstitutions { get; set; }
        public bool IsFinished { get; set; }

        public Product(int id, string name, Dictionary<string, int> institutionProportions)
        {
            Id = id;
            Name = name;
            InstitutionProportions = institutionProportions;
            NumberOfInstitutions = InstitutionProportions.Count;

        }
        public int GetInstitutionProportion(string institution)
        {
            return InstitutionProportions[institution];
        }

        public Tuple<string, int> GetRandomInstitutionWithProportion(Func<string, bool> filterFunction)
        {
            Random rand = new Random();
            var institution = (InstitutionProportions.Keys.Where(filterFunction).ToList())[rand.Next(0, NumberOfInstitutions)];

            return new Tuple<string, int>(institution, InstitutionProportions[institution]);
            
        }
    }
}
