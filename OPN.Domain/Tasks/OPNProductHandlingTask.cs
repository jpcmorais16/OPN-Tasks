using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPN.Domain.Tasks
{
    public class OPNProductHandlingTask: OPNTask
    {
        public OPNProductHandlingTask(string userIDN, Product product, Tuple<string, int> institutionWithProportion)
        {
            UserIDN = userIDN;
            Product = product;
            InstitutionWithProportion = institutionWithProportion;
            CreationTime = DateTime.Now;
            Goal = $"Levar {institutionWithProportion.Item2} de {Product.Name} para {institutionWithProportion.Item1}";

        }
        public int Id { get; set; }
        public Product Product { get; set; }
        public string InstitutionName { get; set; }
        public Tuple<string, int> InstitutionWithProportion { get; set; }

    }
}
