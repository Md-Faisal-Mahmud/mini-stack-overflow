# mini-stack-overflow
Bluebay It Limited
#migration commands:
cd \mini-stack-overflow\src
0. dotnet ef migrations remove --project MiniStackOverflow.Web --context ApplicationDbContext
1. dotnet ef migrations add AddMyMigration --project MiniStackOverflow.Web --context ApplicationDbContext
2. dotnet ef database update --project MiniStackOverflow.Web --context ApplicationDbContext