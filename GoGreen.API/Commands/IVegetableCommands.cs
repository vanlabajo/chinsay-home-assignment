using GoGreen.API.Queries;
using System.Threading.Tasks;

namespace GoGreen.API.Commands
{
    public interface IVegetableCommands
    {
        Task<VegetableDTO> AddAsync(VegetableDTO vegetable);
        Task<bool> UpdateAsync(VegetableDTO vegetable);
        Task<bool> RemoveAsync(int vegetableId);
    }
}
