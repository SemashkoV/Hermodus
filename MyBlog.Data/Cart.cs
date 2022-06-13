using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MyBlog.Data
{
    public class Cart
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Укажите как вас зовут")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите номер телефона")]
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Address { get; set; }
        public string Comment { get; set; }
        public string Cartt { get; set; }
        [DisplayName("Published")]
        public bool Publish { get; set; }
        [DisplayName("Create Time:")]
        [Column(TypeName = "DateTime2")]
        public DateTime Create_time { get; set; }

        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Watch watch, int quantity)
        {
            CartLine line = lineCollection
                .Where(g => g.Watch.Id == watch.Id)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Watch = watch,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Watch watch)
        {
            lineCollection.RemoveAll(l => l.Watch.Id == watch.Id);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Watch.Price * e.Quantity);

        }
        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
        public string TotalValue()
        {
            string total = "";
            foreach(var item in lineCollection)
            {
                total += item.Watch.CompanyId + " " + item.Watch.Model + "\n";
            }
            return total;

        }
    }

    public class CartLine
    {
        public Watch Watch { get; set; }
        public int Quantity { get; set; }
    }
}