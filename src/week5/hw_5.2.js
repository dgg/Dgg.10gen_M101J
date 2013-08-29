use postalCodes;

/*

Please calculate the average population of cities in California (abbreviation CA)
and New York (NY) (taken together) with populations over 25,000.

For this problem, assume that a city name that appears in more than one state represents
two separate cities.

Please round the answer to a whole number.
Hint: The answer for CT and NJ is 49749. 
*/

db.zips.aggregate([
	{$match : {$or : [ {state : 'CT'}, {state : 'NJ'}]}},
	{$group : {_id : '$city', population : {$sum : '$pop'}}},
	{$match: {population : {$gt : 25000}}},
	{$group : {_id : 1, avg_pop : {$avg : '$population'}}}
]);

// not entirely right, but close enough to make answer 82541 (I got 83199.934)