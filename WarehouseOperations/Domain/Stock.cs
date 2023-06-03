using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseOperations.Domain
{
    public class Stock
    {
        Guid StockId { get; set; }
        string Name { get; set; }
        int ProductId { get; set; }
        int ProductQuantity { get; set; }
        int ProductPrice { get; set; }
    }
}
