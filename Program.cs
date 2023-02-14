using ArgusTechnicalTask_KS.Const;
using System.Collections;

namespace ArgusTechnicalTask_KS
{
    public sealed class Program
    {
        //Check can contain one or more orders, total amount should be calculated as sum of orders
        public decimal GetBillTotal(Bill bill)
        {
            var checkTotal = 0m;

            if(bill==null || bill.Orders == null || bill.Orders.Count == 0)
            {
                throw new ArgumentException("Can not calculate empty bill");
            }
            
            foreach (var order in bill)
            {
                //get total cost for every set in each order
                var starters = GetTotalSetCost(order.StartersCount, PriceAndDiscountConst.StartersCost);
                var mains = GetTotalSetCost(order.MainsCount, PriceAndDiscountConst.MainCost);
                var drinks = GetTotalSetCost(order.DrinksCount, PriceAndDiscountConst.DrinkCost);

                //substract discount from drinks total if required
                if (order.OrderTime.TimeOfDay < new TimeSpan(19, 0, 0))
                {
                    drinks -= drinks / 100 * PriceAndDiscountConst.DiscountPercentage;
                }

                //calculate total for order
                var orderTotal = starters + mains + drinks;

                //add to order sum service charge on food 
                orderTotal += (starters + mains) / 100 * PriceAndDiscountConst.TicksPercentage;

                //add calculated order amount to bill amount
                checkTotal += orderTotal;
            }

            if(checkTotal <= 0)
            {
                throw new ArgumentException("Something went wrong, total amount less or equal 0");
            }

            return checkTotal;
        }

        private decimal GetTotalSetCost(int count, decimal cost)
        {
            return count * cost;
        }

    }

    public class Order
    {
        public int StartersCount { get; set; }
        public int MainsCount { get; set; }
        public int DrinksCount { get; set; }
        public DateTime OrderTime { get; set; } = DateTime.Now;
    }

    public class Bill : IEnumerable<Order>
    {
        public Bill(List<Order>? orders)
        {
            Orders = orders;
        }

        public List<Order>? Orders { get; set; }

        public IEnumerator<Order> GetEnumerator()
        {
            return Orders!.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Orders!.GetEnumerator();
        }
    }    
}
