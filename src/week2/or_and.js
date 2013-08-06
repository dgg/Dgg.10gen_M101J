// people that either have a name ending in "e" or have an age
db.people.find({$or : [{name : {$regex : "e$"}}, {age : {$exists : true}}]});

// How would you find all documents in the scores collection where the score is less than 50 or greater than 90?
db.scores.find({$or : [{score : {$lt : 50}}, {score : {$gt : 90}}]});

// and 
db.people.find({$and : [{name : {$gt : "C"}}, {name : {$regex : "a"}}]});
db.people.find({name : {$gt : "C", $regex : "a"}});

//What will the following query do?
//db.scores.find( { score : { $gt : 50 }, score : { $lt : 60 } } );
// equivalent to:
db.scores.find( { score : { $lt : 60 } } );
// as the score key will be overwritten