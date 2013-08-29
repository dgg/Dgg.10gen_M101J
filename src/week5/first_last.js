use postalCodes

// populationget largest city in every state

db.zips.aggregate([
	// population in every city in every state
	{$group : {
		_id : {state : '$state', city :'$city'},
		population : {$sum : '$pop'}
	}},
	// sort by state, population
	{$sort: {'_id.state': 1, population: -1}},
	// group by state, getting the first of each group
	{$group : {
		_id : '$_id.state',
		city : {$first : '$_id.city'},
		population: {$first: '$population'}}
	},
	{$sort : {'_id' : 1}}
]);

/*

Given the following collection:

> db.fun.find()
{ "_id" : 0, "a" : 0, "b" : 0, "c" : 21 }
{ "_id" : 1, "a" : 0, "b" : 0, "c" : 54 }
{ "_id" : 2, "a" : 0, "b" : 1, "c" : 52 }
{ "_id" : 3, "a" : 0, "b" : 1, "c" : 17 }
{ "_id" : 4, "a" : 1, "b" : 0, "c" : 22 }
{ "_id" : 5, "a" : 1, "b" : 0, "c" : 5 }
{ "_id" : 6, "a" : 1, "b" : 1, "c" : 87 }
{ "_id" : 7, "a" : 1, "b" : 1, "c" : 97 }

What would be the value of c in the result from this aggregation query

db.fun.aggregate([
    {$match:{a:0}},
    {$sort:{c:-1}}, 
    {$group:{_id:"$a", c:{$first:"$c"}}}
])

*/

// 1st step
/*
{ "_id" : 0, "a" : 0, "b" : 0, "c" : 21 }
{ "_id" : 1, "a" : 0, "b" : 0, "c" : 54 }
{ "_id" : 2, "a" : 0, "b" : 1, "c" : 52 }
{ "_id" : 3, "a" : 0, "b" : 1, "c" : 17 }
*/

// 2nd step
/*
{ "_id" : 1, "a" : 0, "b" : 0, "c" : 54 }
{ "_id" : 2, "a" : 0, "b" : 1, "c" : 52 }
{ "_id" : 0, "a" : 0, "b" : 0, "c" : 21 }
{ "_id" : 3, "a" : 0, "b" : 1, "c" : 17 }
*/

// 3rd step
/*
{ "_id" : 0, "c" : 54 }
*/