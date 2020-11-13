using GoGreen.Domain.Aggregates.VegetableAggregate;
using System;
using Xunit;

namespace GoGreen.UnitTests.Domain
{
    public class VegetableAggregateTest
    {
        [Fact]
        public void ShouldCreateVegetable()
        {
            var name = "fakeVegetable";
            var price = 1.0M;

            var fakeVegetable = new Vegetable(name, price);
            Assert.NotNull(fakeVegetable);
        }

        [Fact]
        public void ShouldNotCreateVegetableIfNameIsEmpty()
        {  
            var name = string.Empty;
            var price = 1.0M;

            Assert.Throws<ArgumentNullException>(() => new Vegetable(name, price));
        }

        [Fact]
        public void ShouldNotCreateVegetableIfPriceIsLessThanZero()
        {
            var name = "fakeVegetable";
            var price = -0.1M;

            Assert.Throws<ArgumentException>(() => new Vegetable(name, price));
        }

        [Fact]
        public void ShouldUpdateName()
        {
            var name = "fakeVegetable";
            var price = 1.0M;

            var fakeVegetable = new Vegetable(name, price);
            Assert.NotNull(fakeVegetable);

            fakeVegetable.SetName("fakeVegetableAgain");
            Assert.Equal("fakeVegetableAgain", fakeVegetable.Name);
        }

        [Fact]
        public void ShouldUpdatePrice()
        {
            var name = "fakeVegetable";
            var price = 1.0M;

            var fakeVegetable = new Vegetable(name, price);
            Assert.NotNull(fakeVegetable);

            fakeVegetable.SetPrice(0.1M);
            Assert.Equal(0.1M, fakeVegetable.Price);
        }
    }
}
