using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseOperations.Domain
{
    public class Product
    {
        public Product(int productId, string name, Guid barcode)
        {
            _ProductId = productId;
            _Name = name;
            _Barcode = barcode;
        }

        public int ProductId { get { return _ProductId; } }
        private int _ProductId { get; set; }

        public string Name { get { return _Name; } }
        private string _Name { get; set; }

        public Guid Barcode { get { return _Barcode; } }
        private Guid _Barcode { get; set; }
    }
}
