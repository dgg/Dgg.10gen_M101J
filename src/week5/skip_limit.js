use postalCodes

// population descending

db.zips.aggregate([
	{$match : 	{ state : 'NY' }},
	{$group : {
		_id : '$city',
		population : {$sum : '$pop'}
	}},
	{$project : {_id : 0, city : '$_id', population : 1}},
	{$sort: {population : -1}},
	{$skip : 10},
	{$limit: 5}
]);

/*

Suppose you change the order of skip and limit in the query shown in the lesson, to look like this:

db.zips.aggregate([
    {$match:
     {
	 state:"NY"
     }
    },
    {$group:
     {
	 _id: "$city",
	 population: {$sum:"$pop"},
     }
    },
    {$project:
     {
	 _id: 0,
	 city: "$_id",
	 population: 1,
     }
    },
    {$sort:
     {
	 population:-1
     }
    },
    {$limit: 5},
    {$skip: 10} 
])

How many documents do you think will be in the result set? 

*/

use postalCodes

db.zips.aggregate([
	{$match : 	{ state : 'NY' }},
	{$group : {
		_id : '$city',
		population : {$sum : '$pop'}
	}},
	{$project : {_id : 0, city : '$_id', population : 1}},
	{$sort: {population : -1}},
	{$limit: 5},
	{$skip : 10}
]);

// yields 0 results, as 10 are skipped of the five there are in the pipeline