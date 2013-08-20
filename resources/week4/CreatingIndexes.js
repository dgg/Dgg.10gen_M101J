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