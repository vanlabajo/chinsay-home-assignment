using GoGreen.Domain.Aggregates.VegetableAggregate;
using System.Linq;

namespace GoGreen.Infra.EFCore.Data
{
    public class DbInitializer
    {
        public void Initialize(GoGreenContext context)
        {
            context.Database.EnsureCreated();

            // Look for any vegetables.
            if (context.Vegetables.Any())
            {
                return;   // DB has been seeded
            }

            var vegetables = new Vegetable[]
            {
                new Vegetable("Asparagus", 0.1M),
                new Vegetable("Beans", 0.15M),
                new Vegetable("Cabbages", 0.13M),
                new Vegetable("Chokos", 0.11M),
                new Vegetable("Cucumber", 0.18M),
                new Vegetable("Mushrooms", 0.2M)
            };

            context.Vegetables.AddRange(vegetables);
            context.SaveChanges();
        }
    }
}
