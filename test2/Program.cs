using System;
using System.Data.Entity;
using System.Linq;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

public class AppDbContext : DbContext
{
    public AppDbContext() : base("name=AppDbContext")
    {
        Database.SetInitializer(new CreateDatabaseIfNotExists<AppDbContext>());
    }

    public DbSet<Product> Products { get; set; }
}

class Program
{
    static void Main()
    {
        using (var db = new AppDbContext())
        {
            try
            {
                if (db.Database.Exists())
                {
                    Console.WriteLine("Baza danych istnieje.");
                }
                else
                {
                    Console.WriteLine("Baza danych została utworzona.");
                }

                db.Products.Add(new Product { Name = "Chleb", Price = 3.50m });
                db.Products.Add(new Product { Name = "Masło", Price = 7.99m });
                db.SaveChanges();

                var products = db.Products.ToList();
                foreach (var p in products)
                {
                    Console.WriteLine($"{p.Id}: {p.Name} - {p.Price} zł");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wystąpił błąd: " + ex.ToString());
            }

            Console.WriteLine("Naciśnij dowolny klawisz, aby zakończyć...");
            Console.ReadKey();
        }
    }
}
