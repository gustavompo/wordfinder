Feature: FindWord

Scenario: Find a existent word
	Given A valid existent word
	When I try to find the word
	Then the result message should be ok
	And the result must contain the index of the word
	And the result must contain the calls count


Scenario: Return a message informing the word was not found
	Given An inexistent word
	When I try to find the word
	Then the result message should be ok
	And the result must contain the error code of inexistent word
