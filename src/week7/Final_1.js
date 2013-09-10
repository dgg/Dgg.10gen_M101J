use enron;

assert(db.messages.count() === 120477);
 
assert(db.messages.count(
{
	'headers.From': 'andrew.fastow@enron.com', 
	'headers.To': 'john.lavorato@enron.com'
}) === 1);

db.messages.count(
{
	'headers.From': 'andrew.fastow@enron.com',
	'headers.To': 'jeff.skilling@enron.com'
});

// --> 3