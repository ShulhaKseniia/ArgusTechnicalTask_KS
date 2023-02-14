Feature: SeveralOrdersTests

Check cases when several orders should be calculated

Scenario: Make few orders and get bills
	Given a group order 1 starter and 2 mains and 2 drinks at 18:59
	When the order is sent to the endpoint 
	Then the total is calculated correctly in the bill
	When at 20:00 order 2 mains and 2 drinks
	And the order is sent to the endpoint
	Then the total is calculated correctly in the bill
