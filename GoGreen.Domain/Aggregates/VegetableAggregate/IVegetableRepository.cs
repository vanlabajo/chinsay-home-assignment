using GoGreen.Domain.Common;
using System.Threading.Tasks;

namespace GoGreen.Domain.Aggregates.VegetableAggregate
{
    public interface IVegetableRepository : IRepository<Vegetable>
    {
        Vegetable Add(Vegetable vegetable);

        void Update(Vegetable vegetable);

        void Remove(Vegetable vegetable);

        Task<Vegetable> GetAsync(int vegetableId);
    }
}
