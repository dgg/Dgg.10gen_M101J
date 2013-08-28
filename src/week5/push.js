use agg

// which category does each manufacturer produce. Allowing repetition
// (eg. Amazon has two tablets: tablets appears more than once)

db.products.aggregate([{$group :
{
	_id : {maker : '$manufacturer'},
	categories : {$push : '$category'}
}}]);

/*

Given the zipcode dataset (explained more fully in the using $sum quiz) that has documents that
look like this:

> db.zips.findOne()
{
	"city" : "ACMAR",
	"loc" : [
		-86.51557,
		33.584132
	],
	"pop" : 6055,
	"state" : "AL",
	"_id" : "35004"
}

would you expect the following two queries to produce the same result or different results?

db.zips.aggregate([{"$group":{"_id":"$city", "postal_codes":{"$push":"$_id"}}}])

db.zips.aggregate([{"$group":{"_id":"$city", "postal_codes":{"$addToSet":"$_id"}}}])
 
*/

// same result as postal codes are unique, so there are no repetitions