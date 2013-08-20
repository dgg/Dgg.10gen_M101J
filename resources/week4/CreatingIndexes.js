use school;

// check that students is made of _id, _name and scores[]
db.students.find().limit(10).pretty();

// find something at the end of the collection 197
db.students.find({name : 'Tonisha Games'});

// .findOne() stops after finding the first marching document

db.students.ensureIndex({name : -1});

/*
Please provide the mongo shell command to add an index to a collection named students,
having the index key be class, student_name. 
*/

// db.students.ensureIndex({class : 1, student_name : 1})

// discover the indexes in the database
db.system.indexes.find();

// discover the indexes in a collection
db.students.getIndexes();

/*
Suppose we have a collection foo that has an index created as follows:

db.foo.ensureIndex({a:1, b:1})

Which of the following inserts are valid to this collection?
*/

// db.foo.insert({a:["apples","oranges"], b:"grapes"})
// db.foo.insert({a:"grapes", b:"oranges"})
// db.foo.insert({a:"grapes", b:[8,9,10]})
// NOT VALID: db.foo.insert({a:[1,2,3], b:[5,6,7]}) bcause there is an index defined on two array properties