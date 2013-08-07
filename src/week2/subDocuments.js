db.users.insert({name : "richard", email : {work : "richard@10gen.com", personal: "kreuter@example.com"}});

// found
db.users.find({email : {work : "richard@10gen.com", personal: "kreuter@example.com"}});
// not found because order is different
db.users.find({email : {personal: "kreuter@example.com", work : "richard@10gen.com"}});
// not found because does not match complete sub-document
db.users.find({email : { work : "richard@10gen.com"}});
// found
db.users.find({"email.work" : "richard@10gen.com"});

/*
Write a query that finds all products that cost more than 10,000 and that have a rating of 5 or better.
*/
//db.catalog.find({price : {$gt : 10000}, "reviews.rating" : {$gte: 5}})