Feature: ColorMath
	In order to make everyone super happy
	As a bad programmer
	I want to retrieve the complementary/triadic/analogous color of the input color

Scenario: Retrieve Complement
	Given I have the color red
	When I have requested to get the complement
	Then the result should be the color teal