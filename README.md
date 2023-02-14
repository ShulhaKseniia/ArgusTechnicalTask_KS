# ArgusTechnicalTask_KS

You are testing a checkout system for a restaurant. There is a new endpoint that will calculate the total of the order, and add a 10% service charge on food.

The restaurant only serves starters, mains and drinks, and the set cost for each is:

Starters cost £4.00, 
Mains cost £7.00, 
Drinks cost £2.50
Drinks have a 30% discount when ordered before 19:00

Assumptions:
* if the bill total is less or equal to 0 error should be thrown.
* if the bill or its content is empty, an error should be thrown.

* rounding to 2 symbols after the comma in decimal for amounts.
* if the time of order is not set in the scenario, use the current time.
* if the order was changed, ticks should be calculated from the final order.
