using Hermodus.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermodus.Service
{
    public interface IShippingDetailRepository
    {
        void Save(ShippingDetail shippingDetail);
        IEnumerable<ShippingDetail> OrdersIEnum { get; }
        IEnumerable<ShippingDetail> Last10Orders { get; }

        ShippingDetail Details(int? Id);
        IQueryable<ShippingDetail> OrdersList { get; }

        ShippingDetail Delete(int? Id);
    }
}
