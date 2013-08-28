use agg

// average price by manufacturer

db.products.aggregate({$group : {_id : {maker : '$manufacturer'}, prices : {$avg : '$price'}}})

/*

 Given population data by zip code (postal code) that looks like this:

{
	"city" : "FISHERS ISLAND",
	"loc" : [
		-72.017834,
		41.263934
	],
	"pop" : 329,
	"state" : "NY",
	"_id" : "06390"
}

Write an aggregation expression to calculate the average population of a zip code (postal code) by state.
As before, the postal code is in the _id field and is unique.
The collection is assumed to be called "zips" and you should name the key in the result set "average_pop". 
*/

use postalCodes

db.zips.aggregate([{$group : {_id : '$state', 'average_pop' : {$avg : '$pop'}}}]);