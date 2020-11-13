using GoGreen.Domain.Common;
using System;

namespace GoGreen.Domain.Aggregates.VegetableAggregate
{
    public class Vegetable : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public Vegetable(string name, decimal price)
        {
            Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name));
            Price = price >= 0 ? price : throw new ArgumentException("Price must not be lesser than 0", nameof(price));
            CreatedTime = DateTime.UtcNow;
            LastUpdatedTime = DateTime.UtcNow;
        }

        public void SetName(string name)
        {
            Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name));
        }

        public void SetPrice(decimal price)
        {
            Price = price >= 0 ? price : throw new ArgumentException("Price must not be lesser than 0", nameof(price));
        }
    }
}
