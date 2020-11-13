using GoGreen.API.Commands;
using GoGreen.API.Controllers;
using GoGreen.API.Queries;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GoGreen.UnitTests.APIs
{
    public class VegetableApiTest
    {
        private readonly Mock<IVegetableQueries> vegetableQueriesMock;
        private readonly Mock<IVegetableCommands> vegetableCommandsMock;

        public VegetableApiTest()
        {
            vegetableQueriesMock = new Mock<IVegetableQueries>();
            vegetableCommandsMock = new Mock<IVegetableCommands>();
        }

        [Fact]
        public async Task ShouldReturnIEnumerableDTO()
        {
            vegetableQueriesMock.Setup(query => query.GetVegetablesAsync())
                .Returns(Task.FromResult(new List<VegetableDTO>().AsEnumerable()));

            var vegetableController = new VegetablesController(vegetableQueriesMock.Object, vegetableCommandsMock.Object);
            var result = await vegetableController.Get();
            Assert.True(result is IEnumerable<VegetableDTO>);
        }

        [Fact]
        public async Task ShouldReturnEmpty()
        {
            var vegetableController = new VegetablesController(vegetableQueriesMock.Object, vegetableCommandsMock.Object);
            var result = await vegetableController.Get();
            Assert.Empty(result);
        }

        [Fact]
        public async Task ShouldReturnDTO()
        {
            vegetableQueriesMock.Setup(query => query.GetVegetableAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new VegetableDTO()));

            var vegetableController = new VegetablesController(vegetableQueriesMock.Object, vegetableCommandsMock.Object);
            var result = await vegetableController.Get();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldReturnNull()
        {
            var vegetableController = new VegetablesController(vegetableQueriesMock.Object, vegetableCommandsMock.Object);
            var result = await vegetableController.Get(1);
            Assert.Null(result);
        }
    }
}
