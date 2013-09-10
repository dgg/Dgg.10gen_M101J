rd d:\temp\mongo_data\%1
md d:\temp\mongo_data\%1
"D:\Program Files\MongoDb_2.4.5\mongod.exe" --replSet rs1 --logpath %1.log --dbpath d:\temp\mongo_data\%1 --port %2 --smallfiles


