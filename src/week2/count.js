db.scores.count({type : "exam"});

/*
How would you count the documents in the scores collection where the type was "essay" and the score was greater than 90?
*/
//db.scores.count({type : "essay", score : {$gt : 90}});