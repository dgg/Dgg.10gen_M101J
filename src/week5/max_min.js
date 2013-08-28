use agg

// most expensive product per manufacturer

db.products.aggregate([{$group :
{
	_id : {maker : '$manufacturer'},
	maxPrice : {$max : '$price'}
}}]);

// cheapest product by category
db.products.aggregate([{$group :
{
	_id : {type : '$category'},
	minPrice : {$min : '$price'}
}}]);

/*

Again thinking about the zip code database, write an aggregation query
that will return the population of the postal code in each state with the highest population.
It should return output that looks like this:

{
			"_id" : "WI",
			"pop" : 57187
		},
		{
			"_id" : "WV",
			"pop" : 70185
		},
..and so on

Once again, the collection is named zips.
 
*/

use postalCodes

db.zips.aggregate([{$group: {_id : '$state', pop : {$max : '$pop'}}}]);