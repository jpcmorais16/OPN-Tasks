using OPN.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPN.Domain.Tasks
{
    public class OPNProductHandlingTask : OPNTask
    {
        public OPNProductHandlingTask()
        {

        }
        public OPNProductHandlingTask(int taskId, string userIDN, Product product, string institutionName, int institutionProportion)
        {
            Id = taskId;
            UserIDN = userIDN;
            Product = product;
            InstitutionName = institutionName;
            InstitutionProportion = institutionProportion;
            CreationTime = DateTime.Now;
            Quantity = InstitutionProportion * Product.Amount / 100;
            Goal = $"Levar {Quantity} de {Product.Name} para {InstitutionName}";
        }
        public Product Product { get; set; }
        public string InstitutionName { get; set; }
        public int InstitutionProportion { get; set; }
        public int Quantity { get; set; }
        public DateTime? CancellationTime { get; set; }
    }
}
