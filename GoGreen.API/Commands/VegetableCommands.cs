using GoGreen.API.Queries;
using GoGreen.Domain.Aggregates.VegetableAggregate;
using System.Threading.Tasks;

namespace GoGreen.API.Commands
{
    public class VegetableCommands : IVegetableCommands
    {
        private readonly IVegetableRepository repository;

        public VegetableCommands(IVegetableRepository repository)
        {
            this.repository = repository;
        }

        public async Task<VegetableDTO> AddAsync(VegetableDTO vegetable)
        {
            var newVegetable = new Vegetable(vegetable.Name, vegetable.Price);
            var persistedVegetable = repository.Add(newVegetable);
            var result = await repository.UnitOfWork.SaveEntitiesAsync();
            if (result)
            {
                return new VegetableDTO
                {
                    Id = persistedVegetable.Id,
                    Name = persistedVegetable.Name,
                    Price = persistedVegetable.Price,
                    RowVersion = persistedVegetable.RowVersion
                };
            }

            return null;
        }

        public async Task<bool> RemoveAsync(int vegetableId)
        {
            var vegetable = await repository.GetAsync(vegetableId);
            if (vegetable != null)
            {
                repository.Remove(vegetable);
                return await repository.UnitOfWork.SaveEntitiesAsync();
            }

            return false;
        }

        public async Task<bool> UpdateAsync(VegetableDTO vegetable)
        {
            var data = await repository.GetAsync(vegetable.Id);
            if (data != null)
            {
                //if (!vegetable.RowVersion.Equals(data.RowVersion))
                //    throw new DBConcurrencyException();

                data.SetName(vegetable.Name);
                data.SetPrice(vegetable.Price);
                repository.Update(data);
                return await repository.UnitOfWork.SaveEntitiesAsync();
            }

            return false;
        }
    }
}
