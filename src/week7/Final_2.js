/*

Please use the Enron dataset you imported for the previous problem. For this question you will use the aggregation framework to figure out pairs of people that tend to communicate a lot. To do this, you will need to unwind the To list for each message.

This problem is a little tricky because a recipient may appear more than once in the To list for a message. You will need to fix that in a stage of the aggregation before doing your grouping and counting of (sender, recipient) pairs.

Which pair of people have the greatest number of messages in the dataset?

*/


use enron;

assert(db.messages.count() === 120477);

db.messages.aggregate([
	{$project : {_id : '$_id', sender : '$headers.From', receivers : '$headers.To'}},
	{$unwind : '$receivers'},
	{$group : {_id : {_id : '$_id', sender : '$sender'}, receivers : {$addToSet : '$receivers'}}},
	{$unwind : '$receivers'},
	{$project : {_id : false, sender : '$_id.sender', receiver : '$receivers'}},
	{$group : {_id : {sender : '$sender', receiver : '$receiver'}, count : {$sum :1}}},
	{$sort : {count : -1}},
	{$limit : 1}
]);

// -->  { "_id" : { "sender" : "susan.mara@enron.com", "receiver" : "jeff.dasovich@enron.com" }, "count" : 750 }
 
