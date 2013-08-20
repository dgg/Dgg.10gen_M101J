use test;

db.bbb.insert({a: 1, b: 1});

db.bbb.ensureIndex({a:1, b:1});

db.bbb.insert({a: [1, 2, 3], b : 1});

db.bbb.insert({a : 5, b : [1, 2, 3]});

db.bbb.getIndexes();

// ERROR: problem when both indexes properties are arrays
db.bbb.insert({a: [1, 2], b : [2, 3]});