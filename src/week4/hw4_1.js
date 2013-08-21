use hw4_1

db.products.insert({
	_id: 1,
	sku : '1',
	price: 1234,
	description: 'some product',
	category : 'cat',
	brand : 'br',
	reviews : [
		{author : 'me'}
	]
});

db.products.ensureIndex({sku : 1}, {unique : true});
db.products.ensureIndex({price : -1});
db.products.ensureIndex({description : 1});
db.products.ensureIndex({category : 1, brand : 1});
db.products.ensureIndex({'reviews.author': 1});