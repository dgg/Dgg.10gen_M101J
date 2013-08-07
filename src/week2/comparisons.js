db.scores.find({score : {$gt : 95}});
db.scores.find({score : {$gt : 95}, type : "essay"});

// this query does not constrain score between 95 and 98
db.scores.find({score : {$gt : 95}, type : "essay", score : {$lt : 98}});

// this query does constrain score between 95 and 98
db.scores.find({score : {$gt : 95, $lt : 98}, type : "essay"});

/*
Which of these finds documents with a score between 50 and 60, inclusive?
*/
//db.scores.find({ score : { $gte : 50 , $lte : 60 } } );