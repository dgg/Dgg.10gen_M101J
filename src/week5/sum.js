use agg

// sum prices by manufacturer

db.products.aggregate({$group : {_id : {maker : '$manufacturer'}, prices : {$sum : '$price'}}})

/*

 Suppose we have a collection of populations by postal code. The postal codes in are in the _id field, and are therefore unique. Documents look like this:

{
	"city" : "CLANTON",
	"loc" : [
		-86.642472,
		32.835532
	],
	"pop" : 13990,
	"state" : "AL",
	"_id" : "35045"
}

Write an aggregation query to sum up the population (pop) by state and
put the result in a field called population.
Don't use a compound _id key (you don't need one and the quiz checker is not expecting one).
The collection name is zips. so something along the lines of db.zips.aggregrate...

*/

use postalCodes


db.zips.aggregate([{$group : {_id : '$state', population : {$sum : '$pop'}}}]);