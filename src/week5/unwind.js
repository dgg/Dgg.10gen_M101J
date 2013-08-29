use agg

db.stuff.insert({_id : new ObjectId(), a: 1, b:2, c: ['apple', 'pear', 'orange']});

// a document per array element, with the 
db.stuff.aggregate([{$unwind: '$c'}]);
// {"a" : 1, "b" : 2,"c" : "apple"}
// {"a" : 1, "b" : 2, "c" : "pear"}
// {"a" : 1,"b" : 2,"c" : "orange"}

// know how many posts are for each tag

use blog;

db.posts.aggregate([
	// document explosion
	{$unwind : '$tags'},
	// count posts with each tag
	{$group : {
		_id : '$tags',
		count : {$sum : 1}
	}},
	// sort by popularity
	{$sort : {count : -1}},
	// top 10
	{$limit : 10},
	// reshape
	{$project : {_id : 0, tag : '$_id', count : 1}}
]);

// double unwind

use agg;

db.inventory.drop();
db.inventory.insert({name : 'Polo shirt', sizes : ['Small', 'Medium', 'Large'], colors : ['navy', 'white', 'orange', 'red']});
db.inventory.insert({name : 'T-shirt', sizes : ['Small', 'Medium', 'Large', 'X-Large'], colors : ['navy', 'black', 'orange', 'red']});
db.inventory.insert({name : 'Chino Pants', sizes : ['32x32', '31x30', '36x32'], colors : ['navy', 'white', 'orange', 'violet']});

db.inventory.aggregate([
	{$unwind: '$sizes'},
	{$unwind: '$colors'},
	{$group : {
		_id : {size : '$sizes', color : '$colors'},
		count : {$sum : 1}
	}}
]);

/*

Can a double unwind be "undone" by push?

*/

// YUP
use agg;


db.inventory.aggregate([
	{$unwind : '$sizes'},
	{$unwind : '$colors'},
	// re-wind color array
	{$group : {_id : {name : '$name', size : '$sizes'}, colors : {$push : '$colors'}}},
	// re-wind sizes arrray
	{$group : {_id : {name: '$_id.name', colors : '$colors'}, sizes : {$push : '$_id.size'}}},
	// make it look "normal"
	{$project : {_id: 0, name : '$_id.name', sizes: 1, colors: 1}}
]);