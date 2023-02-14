Feature: SingleOrderTests

Check cases when single order should be calculated

Background:
	Given a group orders 4 starters, 4 mains and 4 drinks
	

Scenario: Make order, get bill	
	When the order is sent to the endpoint
	Then the total is calculated correctly in the bill

	
Scenario: Make order, change it, get bills
	When the order is sent to the endpoint
	Then the total is calculated correctly in the bill
	When order is now updated to 3 starters, 3 mains and 3 drinks
	And the order is sent to the endpoint
	Then the total is calculated correctly in the bill
