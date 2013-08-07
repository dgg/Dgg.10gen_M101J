// prints values
cur = db.people.find();
// does not print values, only null
cur = db.people.find(); null;
// nothing printed
var cur = db.people.find();

// true if more docs. Does not move cursor
cur.hasNext(); // --> true
// prints current and advances cursor
cur.next(); // next doc
// iterate entire cursor
while (cur.hasNext()) printjson(cur.next());
// bring back two elements
cur.limit(2);
// fetch by name descending
cur.sort({name : -1});
// compose methods
cur.limit(3).sort({name : -1});
cur.sort({name : -1}).limit(3).skip(2);

//
