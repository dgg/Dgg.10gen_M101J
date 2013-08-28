use agg

// change them documents

db.products.aggregate([
	{$project :
	{
		_id : 0,
		maker : { $toLower : '$manufacturer' },
		details :
		{
			category : '$category',
			price : {$multiply : ['$price', 10]}
		},
		item : '$name'
	}
	}
]);

/*

Write an aggregation query with a single projection stage that will transform the documents in the zips collection from this:

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

to documents in the result set that look like this:

{
	"city" : "acmar",
	"pop" : 6055,
	"state" : "AL",
	"zip" : "35004"
}

So that the checker works properly, please specify what you want to do with the _id key
as the first item. The other items should be ordered as above.
As before, assume the collection is called zips.
You are running only the projection part of the pipeline for this quiz.

A few facts not mentioned in the lesson that you will need to know to get this right:
If you don't mention a key, it is not included, except for _id,
which must be explicitly suppressed.
If you want to include a key exactly as it is named in the source document, you just write key:1,
where key is the name of the key.
 
*/

use postalCodes

db.zips.aggregate([{$project : {_id : 0, city : {$toLower : '$city'}, pop : 1, state : 1, zip : '$_id'}}]);