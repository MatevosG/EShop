using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvebtBus.Inf.Models
{
    public class CreateProductEvent
    {
        public string? ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public float ProductPrice { get; set; }
        public string? CategoryId { get; set; }
    }
}
