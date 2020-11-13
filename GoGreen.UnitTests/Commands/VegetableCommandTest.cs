using GoGreen.API.Commands;
using GoGreen.API.Queries;
using GoGreen.Domain.Aggregates.VegetableAggregate;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GoGreen.UnitTests.Commands
{
    public class VegetableCommandTest
    {
        private readonly Mock<IVegetableRepository> vegetableRepositoryMock;

        public VegetableCommandTest()
        {

            vegetableRepositoryMock = new Mock<IVegetableRepository>();
        }

        [Fact]
        public async Task ShouldReturnNullIfAddVegetableFailed()
        {
            var fakeVegetableDTO = new VegetableDTO
            {
                Name = "fakeVegetable",
                Price = 1.0M
            };

            vegetableRepositoryMock.Setup(repo => repo.UnitOfWork.SaveEntitiesAsync(default))
                .Returns(Task.FromResult(false));

            var command = new VegetableCommands(vegetableRepositoryMock.Object);
            var result = await command.AddAsync(fakeVegetableDTO);
            Assert.Null(result);
        }

        [Fact]
        public async Task ShouldReturnDTOIfAddVegetableSucceeded()
        {
            var fakeVegetableDTO = new VegetableDTO
            {
                Name = "fakeVegetable",
                Price = 1.0M
            };

            vegetableRepositoryMock.Setup(repo => repo.UnitOfWork.SaveEntitiesAsync(default))
                .Returns(Task.FromResult(true));
            vegetableRepositoryMock.Setup(repo => repo.Add(It.IsAny<Vegetable>()))
                .Returns(FakeVegetable());

            var command = new VegetableCommands(vegetableRepositoryMock.Object);
            var result = await command.AddAsync(fakeVegetableDTO);
            Assert.NotNull(result);
            Assert.IsType<VegetableDTO>(result);
        }

        [Fact]
        public async Task ShouldReturnTrueIfUpdateVegetableSucceeded()
        {
            var fakeVegetableDTO = new VegetableDTO
            {
                Name = "fakeVegetable",
                Price = 1.0M
            };

            vegetableRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<int>()))
               .Returns(Task.FromResult(FakeVegetable()));

            vegetableRepositoryMock.Setup(repo => repo.UnitOfWork.SaveEntitiesAsync(default))
                .Returns(Task.FromResult(true));

            var command = new VegetableCommands(vegetableRepositoryMock.Object);
            var result = await command.UpdateAsync(fakeVegetableDTO);
            Assert.True(result);
        }

        private Vegetable FakeVegetable()
        {
            return new Vegetable("fakeVegetable", 1.0M);
        }
    }
}
