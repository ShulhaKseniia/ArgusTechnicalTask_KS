using ArgusTechnicalTask_KS.Asserters;
using System.Globalization;

namespace ArgusTechnicalTask_KS.StepDefinitions
{
    [Binding]
    public class StepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly Program _program;
        private readonly CalculationAsserter _calculationAsserter;

        private readonly string TotalContext = "Total";
        private readonly string OrdersContext = "Orders";
        private readonly string TimeFormat = "HH:mm";

        public StepDefinitions(ScenarioContext testContext, Program program, CalculationAsserter calculationAsserter)
        {
            _scenarioContext = testContext;
            _program = program;
            _calculationAsserter = calculationAsserter;
        }      

        [When(@"the order is sent to the endpoint")]
        public void WhenTheOrderIsSentToTheEndpoint()
        {
            //check filled test data for calculation
            _scenarioContext.TryGetValue<List<Order>>(OrdersContext, out var orders);
            orders.Should().NotBeNullOrEmpty("Order should contain data for calculation");

            //call endpoint and store data in context
            _scenarioContext[TotalContext] = _program.GetBillTotal(new Bill(orders));
        }

        [Then(@"the total is calculated correctly in the bill")]
        public void ThenTheTotalIsCalculatedCorrectlyInTheBill()
        {
            //get data from context
            _scenarioContext.TryGetValue<List<Order>>(OrdersContext, out var orders);
            _scenarioContext.TryGetValue<decimal>(TotalContext, out var actualTotal); ;

            //assert
            _calculationAsserter.AssertBillTotal(new Bill(orders), actualTotal);
        }

        [Given(@"a group orders (.*) starters, (.*) mains and (.*) drinks")]
        [When(@"order is now updated to (.*) starters, (.*) mains and (.*) drinks")]
        public void WhenOrderIsNowUpdatedToStartersMainsAndDrinks(int p0, int p1, int p2)
        {
            //fill new orders list
            var orders = new List<Order> {new Order { StartersCount = p0, MainsCount = p1, DrinksCount = p2 }};

            //store in context
            _scenarioContext[OrdersContext] = orders;
        }

        [Given(@"a group order (.*) starter and (.*) mains and (.*) drinks at (.*)")]
        public void GivenAGroupOrderStarterAndMainsAndDrinksBefore(int p0, int p1, int p2, string p3)
        {
            //parse time
            var time = DateTime.ParseExact(p3, TimeFormat, CultureInfo.InvariantCulture);

            //fill and store new orders list with time
            var orders = new List<Order> { new Order { StartersCount = p0, MainsCount = p1, DrinksCount = p2, OrderTime = time }};
            _scenarioContext[OrdersContext] = orders;
        }

        [When(@"at (.*) order (.*) mains and (.*) drinks")]
        public void WhenAtOrderMainsAndDrinks(string p0, int p1, int p2)
        {
            //parse time
            var time = DateTime.ParseExact(p0, TimeFormat, CultureInfo.InvariantCulture);
            _scenarioContext.TryGetValue<List<Order>>(OrdersContext, out var orders);

            //add one more order with time
            orders.Add(new Order { MainsCount = p1, DrinksCount = p2, OrderTime = time });
            _scenarioContext[OrdersContext] = orders;
        }
    }
}
