using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mid_Term_2
{
    class Customer : IObserver<Car>
    {
        private string name;

        public Customer(string name)
        {
            this.name = name;
        }

        public string Name { get; internal set; }

        public void Notify(string message)
        {
            Console.WriteLine($"{name}, {message}");
        }

        public void OnCompleted()
        {
            Console.WriteLine($"Thank you for your purchase, {name}!");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine($"An error occurred: {error.Message}");
        }

        public void OnNext(Car value)
        {
            Console.WriteLine($"{name}, the price of the car has been updated to: {value.Price:C}");
        }

        public void RegisterObserver(Car car)
        {
            car.Subscribe(this);
        }
    }
}
