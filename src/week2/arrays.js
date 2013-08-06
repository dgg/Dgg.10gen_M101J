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