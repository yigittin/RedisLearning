# RedisLearning

Projeyi çalıştırmadan önce EFCore içerisindeki Migration klasörünü silip aşağıdaki komutlar ile sisteminizi hazırlayabilirsiniz <br />
Bu hazırlama aşamasında 10.000 adet rastgele oluşturulmuş isimlerden bir veri havuzunu hazırlayabilirsiniz. DumbData içerisine eklemeler yaparak veri havuzunu özelleştirebilirsiniz

docker run --name my-redis -p 6379:6379 -d redis  // Örnek redis containerı <br />
dotnet ef migrations add InitialMigration -s ..\RedisLearning\RedisLearning.csproj <br />
dotnet ef database update -s ..\RedisLearning\RedisLearning.csproj
