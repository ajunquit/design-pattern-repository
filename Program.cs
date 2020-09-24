using design_pattern_repository.Data;
using design_pattern_repository.Data.Repositories;
using design_pattern_repository.Domain.Entities;
using design_pattern_repository.Domain.Interfaces;
using design_pattern_repository.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Authentication.ExtendedProtection;

namespace design_pattern_repository
{
    class Program
    {
        static IUnitOfWork _unitOfWork;
        static void Main(string[] args)
        {
            CosmoContext context = new CosmoContext();
            _unitOfWork = new UnitOfWork(context);
            RegisterServices();
            ShowMenu();
            Console.ReadLine();
        }

        private static void ShowMenu()
        {
            Console.WriteLine("Name: ");
            var name = Console.ReadLine();
            Console.WriteLine("Has Oxygen: ");
            var hasOxygen = Console.ReadLine();
            Console.WriteLine("Diameter: ");
            var diameter = Console.ReadLine();

            SavePlanet(name, hasOxygen, diameter);
        }

        private static void SavePlanet(string name, string hasOxygen, string diameter)
        {
            Planet planet;
           
            try
            {
                planet = new Planet();
                planet.Name = name;
                planet.HasOxygen = hasOxygen == "yes" ? true : false;
                planet.Diameter = long.Parse(diameter);
                planet.CreatedBy = "user.console";
                planet.CreatedDate = DateTime.Now;
                _unitOfWork.Planets.Add(planet);
                if (_unitOfWork.SaveChanges() > 0)
                {
                    Console.WriteLine("Save planet.");
                }
                else { 
                    Console.WriteLine("Oh no, the planet hasn't save.");
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                throw;
            }
        }

        private static void RegisterServices()
        {
            ConnectionDatabase connection = ConnectionDatabase.GetConnectionDatabase();
            var builder = new HostBuilder()
                   .ConfigureServices((hostContext, services) =>
                   {
                       services.AddLogging(configure => configure.AddConsole())
                       //.AddTransient<MyApplication>()
                       //.AddScoped<IBusinessLayer, BusinessLayer>()
                       // .AddSingleton<IDataAccessLayer, CDataAccessLayer>()
                       .AddDbContext<CosmoContext>(options =>
                       {
                           // options.UseMySql("Server=db4free.net;Port=3306;Database=cosmodb;Uid=usrcosmo;Pwd=auKn9j5Z_8QHv!X;");
                           options.UseMySql(connection.GetConnectionString());
                       });
                   }).UseConsoleLifetime();

            var host = builder.Build();
        }
    }
}
