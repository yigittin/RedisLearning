# RedisLearning

Projeyi çalıştırmadan önce aşağıdaki komutlar ile sisteminizi hazırlayabilirsiniz

docker run --name my-redis -p 6379:6379 -d redis  // Örnek redis containerı
dotnet ef migrations add InitialMigration -s ..\RedisLearning\RedisLearning.csproj
dotnet ef database update -s ..\RedisLearning\RedisLearning.csproj
