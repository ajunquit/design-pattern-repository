# Design Pattern - Repository
Repository pattern is a kind of container where data access logic is stored. It hides the details of data access logic from business logic. In other words, we allow business logic to access the data object without having knowledge of underlying data access architecture.
## The Repository and Unit of Work Patterns
The repository and unit of work patterns are intended to create an abstraction layer between the data access layer and the business logic layer of an application. Implementing these patterns can help insulate your application from changes in the data store and can facilitate automated unit testing or test-driven development (TDD).

In this tutorial you'll implement a repository class for each entity type. For the  `Planet`  entity type you'll create a repository interface and a repository class. When you instantiate the repository in your controller, you'll use the interface so that the controller will accept a reference to any object that implements the repository interface. When the controller runs under a web server, it receives a repository that works with the Entity Framework. When the controller runs under a unit test class, it receives a repository that works with data stored in a way that you can easily manipulate for testing, such as an in-memory collection.

Later in the tutorial you'll use multiple repositories and a unit of work class for the  `Planet`  and  `Star`  entity types. The unit of work class coordinates the work of multiple repositories by creating a single database context class shared by all of them. If you wanted to be able to perform automated unit testing, you'd create and use interfaces for these classes in the same way you did for the  `Planet`  repository. However, to keep the tutorial simple, you'll create and use these classes without interfaces.

The following illustration shows one way to conceptualize the relationships between the controller and context classes compared to not using the repository or unit of work pattern at all.

![Repository_pattern_diagram](https://asp.net/media/2578149/Windows-Live-Writer_8c4963ba1fa3_CE3B_Repository_pattern_diagram_1df790d3-bdf2-4c11-9098-946ddd9cd884.png)

## Project's Structure
:::image type="content" source="Resources/Images/structure-project.png" alt-text="structure project":::
![Alt text](Resources/Images/structure-project.png?raw=true "structure project")

## Creating the Generic Interface Repository

In the _Domain/Interfaces_ folder, create a class file named _IRepository.cs_ and replace the existing code with the following code:

**C#**
```csharp
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace design_pattern_repository.Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        TEntity Get(int id);
        TEntity GetSingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    }
}
```
This code declares a typical set of CRUD methods, including two read methods — one that returns all `Planet` entities, and one that finds a single `Planet` entity by ID.

Creating a repository class for each entity type could result in a lot of redundant code, and it could result in partial updates. For example, suppose you have to update two different entity types as part of the same transaction. If each uses a separate database context instance, one might succeed and the other might fail. One way to minimize redundant code is to use a generic repository, and one way to ensure that all repositories use the same database context (and thus coordinate all updates) is to use a unit of work class.

In this section of the tutorial, you'll create a  `Repository`  class and a  `UnitOfWork`  class, and use them in the  `Planet`  controller to access both the  `Star`  and the  `Planet`  entity sets. As explained earlier, to keep this part of the tutorial simple, you aren't creating interfaces for these classes. But if you were going to use them to facilitate TDD, you'd typically implement them with interfaces the same way you did the  `Planet`  repository.

The definition of the generic interface has an implementation of a generic repository. Create a class with the name _Repository_ in the folder _Data/Repositories_ and replace this code.
```csharp
using design_pattern_repository.Data;
using design_pattern_repository.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace design_pattern_repository.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly CosmoContext _context;
        protected readonly DbSet<TEntity> _entities;

        public Repository(CosmoContext context)
        {
            this._context = context;
            this._entities = context.Set<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            _entities.Add(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            _entities.AddRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            _entities.Update(entity);
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            _entities.UpdateRange(entities);
        }

        public virtual void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
        }

        public virtual int Count()
        {
            return _entities.Count();
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate);
        }

        public virtual TEntity GetSingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.SingleOrDefault(predicate);
        }

        public virtual TEntity Get(int id)
        {
            return _entities.Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _entities.ToList();
        }
    }
}
```
## Creating the Interface Planet Repository

In the _Domain/Interfaces_ folder, create a class file named _IPlanetRepository.cs_ and replace the existing code with the following code:
```csharp
using design_pattern_repository.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace design_pattern_repository.Domain.Interfaces
{
    public interface IPlanetRepository: IRepository<Planet>
    {
        IEnumerable<Planet> GetPlanetsHasOxygen();
    }
}
```
>Note: The interface is customized by adding a method that allows to obtain all the planets that have oxygen.

  
It can be seen that the interface _IPlanetRepository_ implements the generic interface _IRepository_, which in the generic entity specifies the model `Planet`  on which the respective data will be accessed through the defined CRUD methods.

In the _Data/Repositories_ folder, create a class file named _PlanetRepository.cs_ file. Replace the existing code with the following code, which implements the `IPlanetRepository` interface:
```csharp
using design_pattern_repository.Domain.Entities;
using design_pattern_repository.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace design_pattern_repository.Data.Repositories
{
    public class PlanetRepository : Repository<Planet>, IPlanetRepository
    {
        private CosmoContext _cosmoContext => (CosmoContext)_context;

        public PlanetRepository(CosmoContext context) : base(context)
        {
        }
        public IEnumerable<Planet> GetPlanetsHasOxygen()
        {
            return _cosmoContext.Planet.Where(x => x.HasOxygen == true).ToList();
        }
    }
}
```
  
The `PlanetRepository` inherits the generic methods of the class `Repository` specifying the `Planet` entity type and in turn implements the interface _IPlanetRepository_ which contains in this case a custom method `GetPlanetsHasOxygen()`.

## Creating the Unit of Work Class
The unit of work class serves one purpose: to make sure that when you use multiple repositories, they share a single database context. That way, when a unit of work is complete you can call the  `SaveChanges`  method on that instance of the context and be assured that all related changes will be coordinated. All that the class needs is a  `Save`  method and a property for each repository. Each repository property returns a repository instance that has been instantiated using the same database context instance as the other repository instances.

In the  _Data_  folder, create a class file named  _UnitOfWork.cs_  and replace the template code with the following code:
```csharp
using design_pattern_repository.Data.Repositories;
using design_pattern_repository.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace design_pattern_repository.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly CosmoContext _context;

        IPlanetRepository _planets;
        IStarRepository _stars;

        public UnitOfWork(CosmoContext context)
        {
            _context = context;
        }

        public IPlanetRepository Planets
        {
            get
            {
                if (_planets == null)
                    _planets = new PlanetRepository(_context);
                return _planets;
            }
        }

        public IStarRepository Stars
        {
            get
            {
                if (_stars == null)
                    _stars = new StarRepository(_context);
                return _stars;
            }
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
```
This implementation _UnitOfWork_ has the next interface definition:
 ```csharp
 using System;
using System.Collections.Generic;
using System.Text;

namespace design_pattern_repository.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IPlanetRepository Planets { get; }
        IStarRepository Stars { get; }
        int SaveChanges();
    }
}
 ```
 
 ## Conclusion
You have now implemented both the repository and unit of work patterns.
  
It is possible that the concept of "Repository" is perverted today, with respect to the initial concept. But it has been adapting to the different developments of platforms and languages. There are people who do not agree with the name "Repository" and who think that it is simply a DAO. Although I think it's not quite either one or the other, just take some of each. Objects of this type are very useful for decoupling the application and separating the data from the real business. They also provide us with a proven and working way to solve this problem, without us having to invent anything new. They are easy to test and a very useful abstraction for large projects. So if it is really implemented in a way that contains the information about storage, removing that responsibility from the rest of the components, it is a very valid option. But obviously and as with all design patterns, you have to stick to real needs and not fall into the typical anti-patterns of applying it when it is not necessary or applying it in a way that is not entirely correct.

## Bibliography
**[Web]** https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application

# Support
Support This Project to keep it active.
<br>
<p>
<a href="https://www.buymeacoffee.com/ajunquit" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/v2/default-yellow.png" alt="Buy Me A Coffee" style="height: 60px !important;width: 217px !important;" ></a>
</p>

# About me
I'm developer software from Ecuador
> Name: Alejandro Junqui (@ajunquit)
> Web Site: ajunquit.com/website
> Email: ajunquit@gmail.com
