use agg

db.stuff.insert({_id : new ObjectId(), a: 1, b:2, c: ['apple', 'pear', 'orange']});

// a document per array element, with the 
db.stuff.aggregate([{$unwind: '$c'}]);
// {"a" : 1, "b" : 2,"c" : "apple"}
// {"a" : 1, "b" : 2, "c" : "pear"}
// {"a" : 1,"b" : 2,"c" : "orange"}