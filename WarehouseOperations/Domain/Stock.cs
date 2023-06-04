using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseOperations.Domain
{
    public class Stock
    {
        public Guid StockId { get { return _StockId; } }
        private Guid _StockId { get; set; }

        public string Name { get { return _Name; } }
        private string _Name { get; set; }

        public int ProductId { get { return _ProductId; } }
        private int _ProductId { get; set; }

        public int ProductQuantity { get { return _ProductQuantity; } }
        private int _ProductQuantity { get; set; }

        public int ProductPrice { get { return _ProductPrice; } }
        private int _ProductPrice { get; set; }
    }
}
