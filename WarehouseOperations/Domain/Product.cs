using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseOperations.Domain
{
    public class Product
    {
        int ProductId { get; set; }
        public string Name { get; set; }
        public Guid Barcode { get; set; }
    }
}
