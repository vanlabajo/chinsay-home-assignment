using GoGreen.Domain.Aggregates.VegetableAggregate;
using GoGreen.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GoGreen.Infra.EFCore.Repositories
{
    public class VegetableRepository : IVegetableRepository
    {
        private readonly GoGreenContext context;
        public IUnitOfWork UnitOfWork => context;

        public VegetableRepository(GoGreenContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Vegetable Add(Vegetable vegetable)
        {
            return context.Vegetables.Add(vegetable).Entity;
        }

        public async Task<Vegetable> GetAsync(int vegetableId)
        {
            var vegetable = await context.Vegetables
                                .FirstOrDefaultAsync(v => v.Id == vegetableId);
            if (vegetable == null)
            {
                vegetable = context
                            .Vegetables
                            .Local
                            .FirstOrDefault(v => v.Id == vegetableId);
            }

            return vegetable;
        }

        public void Update(Vegetable vegetable)
        {
            context.Entry(vegetable).State = EntityState.Modified;
        }

        public void Remove(Vegetable vegetable)
        {
            context.Entry(vegetable).State = EntityState.Deleted;
        }
    }
}
