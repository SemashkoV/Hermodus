using Hermodus.Data;

using Hermodus.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Hermodus.Repo
{
    public class EFShippingDetailRepository : IShippingDetailRepository
    {
        EFDbContext context = new EFDbContext();
        public IEnumerable<ShippingDetail> OrdersIEnum
        {
            get
            {
                return context.ShippingDetailses;
            }
        }

        public IQueryable<ShippingDetail> OrdersList
        {
            get
            {
                return context.ShippingDetailses.AsQueryable();
            }
        }

        public IEnumerable<ShippingDetail> Last10Orders
        {
            get
            {
                return context.ShippingDetailses.OrderByDescending(c => c.Create_time).Take(10);
            }
        }

        public ShippingDetail Details(int? Id)
        {
            ShippingDetail dbEntry = context.ShippingDetailses.Find(Id);

            return dbEntry;
        }

        public ShippingDetail Delete(int? Id)
        {
            ShippingDetail dbEntry = context.ShippingDetailses.Find(Id);
            if (dbEntry != null)
            {
                context.ShippingDetailses.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public void Save(ShippingDetail commnet)
        {


            if (commnet.Id == 0)
            {
                ShippingDetail _Comment = new ShippingDetail();
                
                _Comment.Name = commnet.Name;
                _Comment.Create_time = commnet.Create_time;
                _Comment.Phone = commnet.Phone;
                _Comment.Cart = commnet.Cart;
                _Comment.Mail = commnet.Mail;
                _Comment.Address = commnet.Address;
                _Comment.Publish = commnet.Publish;
                _Comment.Comment = commnet.Comment;

                context.ShippingDetailses.Add(_Comment);

                context.SaveChanges();



            }
            else
            {
                ShippingDetail dbEntry = context.ShippingDetailses.Find(commnet.Id);
                if (dbEntry != null)
                {
                    dbEntry.Id = commnet.Id;
                    dbEntry.Name = commnet.Name;
                    dbEntry.Create_time = commnet.Create_time;
                    dbEntry.Phone = commnet.Phone;
                    dbEntry.Cart = commnet.Cart;
                    dbEntry.Mail = commnet.Mail;
                    dbEntry.Address = commnet.Address;
                    dbEntry.Publish = commnet.Publish;
                    dbEntry.Comment = commnet.Comment;
                    //  dbEntry.Create_time = category.Create_time;

                    //   context.Categories.Add(dbEntry);
          
                    context.SaveChanges();
                    commnet.Id = dbEntry.Id;
                }
            }


        }
    }
}
