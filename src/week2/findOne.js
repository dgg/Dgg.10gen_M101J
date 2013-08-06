var doc = {name : 'Smith', age: 30, profession: 'hacker', _id : 1};

db.things.insert(doc);

db.users.findOne({"username" : "dwight"}, {email: true, _id: false});