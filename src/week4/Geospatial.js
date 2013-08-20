use test

db.stores.insert({name : 'Rubys', type : 'Barber', location : [40, 74]});
db.stores.insert({name : 'ACE Hardare', type : 'Hardware', location : [40.232, -74.343]});
db.stores.insert({name : 'Tickle Candy', type : 'Food', location : [41.232, -75.343]});


db.stores.ensureIndex({location : '2d', type : 1});

// establishments near a point in increasing distance
db.stores.find({location : {$near : [50, 50]}});

/*
Suppose you have a 2D geospatial index defined on the key location in the collection places.
Write a query that will find the closest three places (the closest three documents)
to the location 74, 140.
*/
// db.places.find({location : {$near: [74, 140]}}).limit(3)

// location now is interpreted as long, lat
db.runCommand({geoNear: 'stores', near : [50, 50], spherical : true, maxDitance : 1 /* rad*/});