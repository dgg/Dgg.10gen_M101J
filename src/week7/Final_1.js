/*

In this dataset, each document is an email message. Like all Email messages, there is one sender but there can be multiple recipients.

Construct a query to calculate the number of messages sent by Andrew Fastow, CFO, to Jeff Skilling, the president. Andrew Fastow's email addess was andrew.fastow@enron.com. Jeff Skilling's email was jeff.skilling@enron.com.

For reference, the number of email messages from Andrew Fastow to John Lavorato (john.lavorato@enron.com) was 1. 

*/


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