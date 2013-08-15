--use school
db.students.insert({_id : 0, name : "Andrew Erlichson", teachers : [0, 1]});
db.students.insert({_id : 1, name : "Richard Kreuter", teachers : [0, 1, 3]});
db.students.insert({_id : 2, name : "Eliot Horowitz", teachers : [1, 2, 3]});
db.students.insert({_id : 3, name : "Mark Heinrich", teachers : [0, 3]});


db.teachers.insert({_id : 0, name : "Mark Horowitz"});
db.teachers.insert({_id : 1, name : "John Hennessy"});
db.teachers.insert({_id : 2, name : "Bruce Wolley"});
db.teachers.insert({_id : 3, name : "James Plummer"});

db.students.ensureIndex({teachers: 1});

// all pupils that have both John and James as teachers
db.students.find({teachers : {"$all" :  [1, 3]}});

// chekc that the query uses an index
db.students.find({teachers : {"$all" :  [1, 3]}}).explain();