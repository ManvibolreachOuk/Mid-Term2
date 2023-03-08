using System;

namespace Mid_Term_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Car car = new Car();
            Console.WriteLine("Current price: " + car.Price);
            decimal price;
            do
            {
                Console.Write("Enter car price: ");
                if (!decimal.TryParse(Console.ReadLine(), out price) || price < 0)
                {
                    Console.WriteLine("Invalid price. Please enter a positive decimal value.");
                    price = -1;
                }
                else
                {
                    car.Price = price;
                }
            } while (price < 0);
            Console.WriteLine("New price set: " + car.Price);

            while (true)
            {
                Console.WriteLine("Enter 1 to add a customer, 2 to remove a customer, or 0 to exit:");
                int choice = int.Parse(Console.ReadLine());

                if (choice == 1)
                {
                    Console.WriteLine("Enter customer name:");
                    string name = Console.ReadLine();
                    Customer customer = new Customer(name);
                    customer.RegisterObserver(car); // registers customer as an observer of the car
                    Console.WriteLine("Customer added to waiting list.");
                }
                else if (choice == 2)
                {
                    Console.WriteLine("Enter customer name:");
                    string name = Console.ReadLine();
                    Customer customer = car.FindCustomerInWaitingList(name);
                    if (customer != null)
                    {
                        car.RemoveFromWaitingList(customer); // removes customer from the waiting list
                        Console.WriteLine("Customer removed from waiting list.");
                    }
                    else
                    {
                        Console.WriteLine("Customer not found in waiting list.");
                    }
                }
                else if (choice == 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice.");
                }
            }
        }
    }
}
