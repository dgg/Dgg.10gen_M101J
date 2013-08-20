use test;

db.products.drop();
db.products.insert({items: 'DVI-to-VGA cable'});
db.products.insert({items: 'iphone cradle'});
db.products.insert({items: 'jeans', size : '32x32'});
db.products.insert({items: 'polo shirt', size : 'medium'});

// ERROR: many documets have no size
db.products.ensureIndex({size : 1}, {unique: true});

// parse index allows unique creation
db.products.ensureIndex({size : 1}, {unique: true, sparse : true});

db.products.find({size : 'medium'}); // --> polo shirt

db.products.find(); // --> all (4) docs

db.products.find().sort({size : 1}); // --> only 2 docs, those in the index

db.products.dropIndex({size : 1});

// without the index, all docs are found
db.products.find().sort({size : 1}); // --> all (4) docs