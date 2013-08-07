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

db.people.update({name : "Jones"}, {$unset : {profession : 1}});

/*
Write an update query that will unset the interests key in the following document in the collection users. The primary key is username.
{'username':'jimmy', favorite_color:'blue', interests:['debating', 'politics']}
*/
// db.users.update({username : "jimmy"}, {$unset : {interests : 1}})

db.arrays.insert({_id : 0, a : [1, 2, 3, 4]});
// changes the third element of the array a
db.arrays.update({_id:0}, {$set : {"a.2" : 5}});
// add an element to the right of the array
db.arrays.update({_id:0}, {$push : {a : 6}});
// remove the right-most element of the array
db.arrays.update({_id:0}, {$pop : {a : 1}});
// remove the left-most element of the array
db.arrays.update({_id:0}, {$pop : {a : -1}});
// add some elements to the right of the array
db.arrays.update({_id:0}, {$pushAll : {a : [7, 8, 9]}});
// remove an element from the array, regardless of its position
db.arrays.update({_id:0}, {$pull : {a : 5}});
// remove some elements from the array, regardless of their position
db.arrays.update({_id:0}, {$pullAll : {a : [2, 4, 8]}});
// adds a value treating the array as it if was a set (at most one value)
db.arrays.update({_id:0}, {$addToSet : {a : 7}}); // no addition
db.arrays.update({_id:0}, {$addToSet : {a : 5}}); // push

/*
Suppose you have the following document in your friends collection:
{ _id : "Mike", interests : [ "chess", "botany" ] }

What will the result of the following updates be?
db.friends.update( { _id : "Mike" }, { $push : { interests : "skydiving" } } );
db.friends.update( { _id : "Mike" }, { $pop : { interests : -1 } } );
db.friends.update( { _id : "Mike" }, { $addToSet : { interests : "skydiving" } } );
db.friends.update( { _id : "Mike" }, { $pushAll: { interests : [ "skydiving" , "skiing" ] } } );
*/
//{ _id : "Mike", interests : [ "chess", "botany", "skydiving" ] }
//{ _id : "Mike", interests : [ "botany", "skydiving" ] }
//{ _id : "Mike", interests : [ "botany", "skydiving" ] }
//-->{ _id : "Mike", interests : [ "botany", "skydiving", "skydiving", "skiing" ] }

// multi-document update to add a title
db.people.update({}, {$set : {title : "Dr"}}, {multi : true});

/*
Recall the schema of the scores collection:
{
	"_id" : ObjectId("50844162cb4cf4564b4694f8"),
	"student" : 0,
	"type" : "exam",
	"score" : 75
}
How would you give every record whose score was less than 70 an extra 20 points?
*/
// db.scores.update({score : {$lt : 70}}, {$inc : {score: 20}}, {multi: true})