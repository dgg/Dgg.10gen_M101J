db.people.insert({name: "Alice"});
db.people.insert({name: "Bob"});
db.people.insert({name: "Charlie"});
db.people.insert({name: "Dave"});
db.people.insert({name: "Edgar"});
db.people.insert({name: "Fred"});

// people whose names are less than D: Alice bob, and Charlie
db.people.find({name : {$lt : "D"}});

// wrong, wrong, wrong way of model data
db.people.insert({name: 42});
// there is no way that {name: 42} will be returned from a query that compares strings
db.people.find({name : {$lt : "D"}});

/*
Which of the following will find all users with name between "F" and "Q"?
*/
//db.users.find( { name : { $gte : "F" , $lte : "Q" } } );
//db.users.find( { name : { $lte : "Q" , $gte : "F" } } );