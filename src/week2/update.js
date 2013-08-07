// wholesale
db.people.update({name : "Smith"}, {name : "Thompson", salary : 50000});

/*
Let's say you had a collection with the following document in it:
{ "_id" : "Texas", "population" : 2500000, "land_locked" : 1 }
What would be the state of the collection after db.foo.update({_id:"Texas"},{population:30000000})?
*/
//{ "_id" : "Texas", "population" : 30000000 }

// give "Alice" an age
db.people.update({name : "Alice"}, {$set : {age : 30}});
// changes Alice's age to 31
db.people.update({name : "Alice"}, {$set : {age : 31}});
//increment Alice's age
db.people.update({name : "Alice"}, {$inc : {age : 1}}); // Alice will be 32
//$inc creates the property if not there
db.people.update({name : "Bob"}, {$inc : {age : 1}}); // Bob will be one as it did not have age

/*
Given the document
{'username':'splunker', 'country':'US', 'phone':'718-343-3433'}
in the collection users, write the shell command for updating the country to 'RU' for only this user.
*/
//db.users.update({username : 'splunker'}, {$set : {country : 'RU'}})