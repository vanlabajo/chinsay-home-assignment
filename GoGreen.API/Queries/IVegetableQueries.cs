using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoGreen.API.Queries
{
    public interface IVegetableQueries
    {
        Task<IEnumerable<VegetableDTO>> GetVegetablesAsync();
        Task<VegetableDTO> GetVegetableAsync(int id);
    }
}
