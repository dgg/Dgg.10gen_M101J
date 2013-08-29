use blog;

/*

Now use the aggregation framework to calculate the author with the greatest number of comments.

To help you verify your work before submitting, the author with the least comments is
Efrain Claw and he commented 384 times. 

*/

db.posts.aggregate([
	{$unwind: '$comments'},
	{$group : {_id : '$comments.author', count : {$sum :1}}},
	{$sort : {'count' : -1}},
	{$limit : 1}
]);