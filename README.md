# design-pattern-repository
Repository pattern is a kind of container where data access logic is stored. It hides the details of data access logic from business logic. In other words, we allow business logic to access the data object without having knowledge of underlying data access architecture.
## EF Core with MySQL Databsase and Migrations.
**The fisrt Step**
We will add them all by NuGet Package Manager. The list of packages is below:
- Microsoft.EntityFrameworkCore
- Pomelo.EntityFrameworkCore.MySql
- Microsoft.EntityFrameworkCore.Tools

> I use Polemo provider instead Oracle provider (MySql.Data.EntityFrameworkCore) because Oracle’s connector doesn’t support EF migrations.

**The Second Step**
