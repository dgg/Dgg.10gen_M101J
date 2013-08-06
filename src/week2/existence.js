db.people.insert( { name : "Smith", age : 30, profession : "hacker" } );
db.people.insert( { name : "Jones", age : 35, profession : "backer" } );

// only those documents that contain the field "profession"
db.people.find({ profession : {$exists : true} });
// only those documents that do not contain the field "profession"
db.people.find({ profession : {$exists : false} });

// only those documents which "name" field is a string
db.people.find({ name : {$type : 2 /* string in BSON */} });

// people whose name contains the letter "a"
db.people.find({name : {$regex : "a"}});
// people whose name ends with the letter "e"
db.people.find({name : {$regex : "e$"}});
// people whose name starts with the letter "A"
db.people.find({name : {$regex : "^A"}});

//Write a query that retrieves documents from a users collection where the name has a "q" in it, and the document has an email field.
db.users.find({name : {$regex : "q"}, email: {$exists : true}});