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
        public IProductHandlingTaskDataCommiter _commiter;

        public OPNProductHandlingTask()
        {

        }
        public OPNProductHandlingTask(string userIDN, Product product, string institutionName, int institutionProportion, IProductHandlingTaskDataCommiter handler)
        {
            UserIDN = userIDN;
            Product = product;
            InstitutionName = institutionName;
            InstitutionProportion = institutionProportion;
            CreationTime = DateTime.Now;
            Goal = $"Levar {InstitutionProportion} de {Product.Name} para {InstitutionName}";
            _commiter = handler;
        }
        public int Id { get; set; }
        public Product Product { get; set; }
        public string InstitutionName { get; set; }
        public int InstitutionProportion { get; set; }

        public override void Register()
        {
            if (_commiter == null)
                throw new Exception("Essa task já está registrada");

            _commiter.Commit(Goal, Id, UserIDN, CreationTime, InstitutionName, Product.Name, Product.Id);
        }
    }
}
