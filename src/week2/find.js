for (i = 0; i< 100; i++) { 
	var names = ["exam", "essay", "quiz"];
	for (j = 0; j < 3; j++) { 
		db.scores.insert({student : i, type: names[j], score :  Math.round(Math.random()*100)});
	}
}

db.scores.find({type : "essay"}, {score: true});

db.scores.find({type: "essay", score : 50}, {student : true, _id : false});