# Design Pattern- Repository
Repository pattern is a kind of container where data access logic is stored. It hides the details of data access logic from business logic. In other words, we allow business logic to access the data object without having knowledge of underlying data access architecture.
## EF Core with MySQL Database and Migrations.
**The fisrt Step:** Installing dependencies
We will add them all by NuGet Package Manager. The list of packages is below:
- Microsoft.EntityFrameworkCore
- Pomelo.EntityFrameworkCore.MySql
- Microsoft.EntityFrameworkCore.Tools

> I use Polemo provider instead Oracle provider (MySql.Data.EntityFrameworkCore) because Oracle’s connector doesn’t support EF migrations.

**The Second Step:** Models and Database Context

Start Model
```csharp
public class Star
{
    public string Name { get; set; }
    public int TemperatureInGradeCelsius { get; set; }
    public int Weight { get; set; }
    public bool GeneratesEnergy { get; set; }
}
```

Planet Model
```csharp
public class Planet
{
    public string Name { get; set; }
    public bool HasOxygen { get; set; }
    public long Diameter { get; set; }
}
```

