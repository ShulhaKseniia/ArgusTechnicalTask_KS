using ArgusTechnicalTask_KS.Const;

namespace ArgusTechnicalTask_KS.Asserters
{
    public sealed class CalculationAsserter
    {
        public void AssertBillTotal(Bill bill, decimal total)
        {
            var expectedTotal = 0m;
            var discount = 0m;
            var ticks = 0m;

            foreach (var order in bill)
            {
                //get total cost for every set in each order
                var starters = order.StartersCount * PriceAndDiscountConst.StartersCost;
                var mains = order.MainsCount * PriceAndDiscountConst.MainCost;
                var drinks = order.DrinksCount * PriceAndDiscountConst.DrinkCost;

                //substract discount from drinks total if required
                if (order.OrderTime.TimeOfDay < new TimeSpan(19, 0, 0))
                {
                    var currentOrderDiscount = Math.Round(drinks / 100 * PriceAndDiscountConst.DiscountPercentage, 2, MidpointRounding.AwayFromZero);
                    drinks -= currentOrderDiscount;
                    discount += currentOrderDiscount;
                }

                //calculate total for order
                var orderTotal = starters + mains + drinks;

                //add to order sum service charge on food 
                decimal currentOrderTicks = Math.Round((starters + mains) / 100.00m * PriceAndDiscountConst.TicksPercentage, 2, MidpointRounding.AwayFromZero);
                orderTotal += currentOrderTicks;
                ticks += currentOrderTicks;

                //add calculated order amount to bill amount
                expectedTotal += orderTotal;
            }

            total.Should().Be(expectedTotal, because: $"Expected total: {expectedTotal}, include discount: {discount} and ticks: {ticks}, actual {total}");
        }
    }
}
