using GoGreen.API.Commands;
using GoGreen.API.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoGreen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VegetablesController : ControllerBase
    {
        private readonly IVegetableQueries queries;
        private readonly IVegetableCommands commands;

        public VegetablesController(IVegetableQueries queries,
            IVegetableCommands commands)
        {
            this.queries = queries;
            this.commands = commands;
        }

        // GET: api/<VegetablesController>
        [HttpGet]
        public async Task<IEnumerable<VegetableDTO>> Get()
        {
            return await queries.GetVegetablesAsync();
        }

        // GET api/<VegetablesController>/5
        [HttpGet("{id}")]
        public async Task<VegetableDTO> Get(int id)
        {
            return await queries.GetVegetableAsync(id);
        }

        // POST api/<VegetablesController>
        [HttpPost]
        public async Task<VegetableDTO> Post([FromBody] VegetableDTO vegetableDTO)
        {
            return await commands.AddAsync(vegetableDTO);
        }

        // PUT api/<VegetablesController>/5
        [HttpPut("{id}")]
        public async Task<bool> Put(int id, [FromBody] VegetableDTO vegetableDTO)
        {
            var exists = await queries.GetVegetableAsync(id);
            if (exists != null)
            {
                return await commands.UpdateAsync(vegetableDTO);
            }

            return false;
        }

        // DELETE api/<VegetablesController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await commands.RemoveAsync(id);
        }
    }
}
