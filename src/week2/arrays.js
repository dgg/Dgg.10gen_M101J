db.accounts.insert({name : "George", favorites: ["ice cream", "pretzels"]});
db.accounts.insert({name : "Howard", favorites: ["pretzels", "beer"]});

// all accounts that like pretzels
db.accounts.find({favorites : "pretzels"});
// the account that likes beer
db.accounts.find({favorites : "beer"});
db.accounts.find({favorites : "beer", name : {$gt: "H"}});

//Which of the following documents would be returned by this query?
//db.products.find( { tags : "shiny" } );
// { _id : 42 , name : "Whizzy Wiz-o-matic", tags : [ "awesome", "shiny" , "green" ] }
// { _id : 1040 , name : "Snappy Snap-o-lux", tags : "shiny" }

db.accounts.insert({name : "Irving", favorites: ["beer", "pretzels", "cheese"]});
db.accounts.insert({name : "John", favorites: ["beer", "cheese"]});

// all aacounts that like both pretzels and beer
db.accounts.find({favorites : {$all : ["pretzels", "beer"]}});

// all accounts which name is either Howard or John
db.accounts.find({name : {$in : ["Howard", "John"]}});
// all accounts that like either beer or ice cream
db.accounts.find({favorites : {$in : ["beer", "ice cream"]}});

//Which of the following documents matches this query?
//db.users.find( { friends : { $all : [ "Joe" , "Bob" ] }, favorites : { $in : [ "running" , "pickles" ] } } )
// { name : "Cliff" , friends : [ "Pete" , "Joe" , "Tom" , "Bob" ] , favorites : [ "pickles", "cycling" ] }