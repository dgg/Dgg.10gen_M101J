// remove Alice
db.people.remove({name : "Alice"});
// removes Thompson
db.people.remove({name : {$gt : "M"}});

/*
Recall the schema of the scores collection:
{
	"_id" : ObjectId("50844162cb4cf4564b4694f8"),
	"student" : 0,
	"type" : "exam",
	"score" : 75
}

How would you delete every record whose score was less than 60?
*/
// db.scores.remove({score : {$lt : 60}})