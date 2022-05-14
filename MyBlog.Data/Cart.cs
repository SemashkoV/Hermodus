using System.Collections.Generic;
using System.Linq;

namespace MyBlog.Data
{
    public class Cart
    {
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
    }

    public class CartLine
    {
        public Watch Watch { get; set; }
        public int Quantity { get; set; }
    }
}