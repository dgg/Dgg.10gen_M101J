use test;

for(var i = 0; i < 10000; i++) db.foo.insert({a : i, b : i, c:i});

db.foo.ensureIndex({a : 1, b :1, c : 1});

// query that cannot use index
db.foo.find({c : 1});

// check that we cannot use index: cursor = "BasicCursor"
db.foo.find({c : 1}).explain();

// query that uses the index
db.foo.find({a: 500});

// cursor: "name_of_index" 
db.foo.find({a: 500}).explain();

// covered index: indexOnly : true
db.foo.find({a: 500}, {a : 1, _id: 0}).explain();

// cannot use the index: n : 250, nscannedObjects: 10000
db.foo.find({c : {$gt : 500, $lt : 750}}).explain()

// tells us that an index is used, yet, nscanned is way more than n => index used for sorting
db.foo.find({c : {$gt : 500, $lt : 750}}).sort({a: 1, b:1}).explain()

/*
Given the following output from explain, what is the best description of what happened during the query?

{
	"cursor" : "BasicCursor",
	"isMultiKey" : false,
	"n" : 100000,
	"nscannedObjects" : 10000000,
	"nscanned" : 10000000,
	"nscannedObjectsAllPlans" : 10000000,
	"nscannedAllPlans" : 10000000,
	"scanAndOrder" : false,
	"indexOnly" : false,
	"nYields" : 7,
	"nChunkSkips" : 0,
	"millis" : 5151,
	"indexBounds" : {
		
	},
	"server" : "Andrews-iMac.local:27017"
}
*/

// The query scanned 10,000,000 documents, returning 100,000 in 5.2 seconds.