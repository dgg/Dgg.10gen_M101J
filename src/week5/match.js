use postalCodes

// documents in NY state
db.zips.aggregate([
	{$match : 	{ state : 'NY' }}
]);

// sum up population in each city in NY
db.zips.aggregate([
	{$match : 	{ state : 'NY' }},
	{$group : {
		_id : {city : '$city'},
		population : {$sum : '$pop'},
		zip_codes : {$addToSet : '$_id'}
	}}
]);

// prettify the document by renaming _id to city
db.zips.aggregate([
	{$match : 	{ state : 'NY' }},
	{$group : {
		_id : {city : '$city'},
		population : {$sum : '$pop'},
		zip_codes : {$addToSet : '$_id'}
	}},
	{$project : {_id : 0, city : '$_id.city', population : 1, zip_codes : 1}}
]);

/*

Again, thinking about the zipcode collection, write an aggregation query with a single match
phase that filters for zipcodes with greater than 100,000 people.
You may need to look up the use of the $gt operator in the MongoDB docs.

Assume the collection is called zips.

*/

use postalCodes

db.zips.aggregate([{$match : {pop : {$gt : 100000}}}]);