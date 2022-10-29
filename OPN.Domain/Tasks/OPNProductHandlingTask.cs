using OPN.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPN.Domain.Tasks
{
    public class OPNProductHandlingTask: OPNTask
    {
        public IProductHandlingTaskDataCommiter _handler;

        public OPNProductHandlingTask(string userIDN, Product product, Tuple<string, int> institutionWithProportion, IProductHandlingTaskDataCommiter handler)
        {
            UserIDN = userIDN;
            Product = product;
            InstitutionWithProportion = institutionWithProportion;
            CreationTime = DateTime.Now;
            Goal = $"Levar {institutionWithProportion.Item2} de {Product.Name} para {institutionWithProportion.Item1}";
            _handler = handler;
        }
        public int Id { get; set; }
        public Product Product { get; set; }
        public string InstitutionName { get; set; }
        public Tuple<string, int> InstitutionWithProportion { get; set; }

        public override void Register()
        {
            _handler.Commit(Goal, UserIDN, CreationTime, InstitutionWithProportion.Item1, Product.Name);
        }
    }
}
