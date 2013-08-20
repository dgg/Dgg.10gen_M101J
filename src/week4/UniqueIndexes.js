use test;

db.stuff.insert({thing : 'pear'});
db.stuff.ensureIndex({thing : 1});

// we can add another pear, no problem
db.stuff.insert({thing : 'pear'});
db.stuff.insert({thing : 'apple'});

// remove the non-unique index
db.stuff.dropIndex({thing : 1});

// add unique index on thing --> ERROR because there are two pears
db.stuff.ensureIndex({thing : 1}, {unique: true});

db.stuff.remove({thing : 'pear'});

// BOOM, as thereis already an apple
db.stuff.insert({thing : 'apple'});

// it is ok to have one pear
db.stuff.insert({thing : 'pear'});

// index on _id is indeed unique
db.stuff.insert({_id : 1, b :1});
// same error as when isnerting a second apple
db.stuff.insert({_id : 1, b :2});

/*
Please provide the mongo shell command to add a unique index to the collection students on the keys
student_id, class_id.
*/
// db.students.ensureIndex({student_id : 1, class_id : 1}, {unique : true})

db.things.drop();
db.things.insert({thing : 'pear', color : 'green'});
db.things.insert({thing : 'apple', color : 'red'});
db.things.insert({thing : 'pear', shape : 'round'});

// fail because for duplicate
db.things.ensureIndex({thing : 1}, {unique: true});
// sledgehammer
db.things.ensureIndex({thing : 1}, {unique: true, dropDups :  true});

/*
If you choose the dropDups option when creating a unique index,
what will the MongoDB do to documents that conflict with an existing index entry? 
*/
// Delete them for ever and ever, Amen.