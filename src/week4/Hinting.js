use test;

db.foo.drop();

for(var i = 0; i < 10000; i++) db.foo.insert({a : i, b : i, c:i});

db.foo.ensureIndex({a : 1});
db.foo.ensureIndex({b : 1});
db.foo.ensureIndex({c : 1});
db.foo.ensureIndex({d : 1});

db.foo.find({a: 500, b : 500, c : 500}); // --> one doc

// uses index on a
db.foo.find({a: 500, b : 500, c : 500}).explain();
// use no index
db.foo.find({a: 500, b : 500, c : 500}).hint({$natural : 1}).explain();
// uses index on c
db.foo.find({a: 500, b : 500, c : 500}).hint({c : 1}).explain();
// returns the doc by scanning all docs
db.foo.find({a: 500, b : 500, c : 500}).hint({d : 1}).explain();

db.foo.ensureIndex({e : 1}, {sparse : true});

db.foo.find({a: 500, b : 500, c : 500}).hint({e : 1}); // --> no documents!
