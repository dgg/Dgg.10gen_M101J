// use students
db.grades.count(); 
// --> 800

db.grades.aggregate(
	{ '$group': { '_id':'$student_id', 'average': { $avg:'$score' } } },
	{ '$sort': { 'average':-1 } },
	{'$limit':1}
);
// --> _id 161

/*
Now it�s your turn to analyze the data set.
Find all exam scores greater than or equal to 65. and sort those scores from lowest to highest.

What is the student_id of the lowest exam score above 65?
*/
db.grades.find({type : "exam", score : {$gte : 65}}).sort({score : 1});
//114