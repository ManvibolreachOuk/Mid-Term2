using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mid_Term_2
{
    class Car : IObservable<Car>
    {
        private decimal price;
        private int qty;
        private List<IObserver<Car>> observers = new List<IObserver<Car>>();
        private List<Customer> waitingList = new List<Customer>();

        public decimal Price
        {
            get { return price; }
            set
            {
                price = value;
                NotifyObservers();
            }
        }

        public int Quantity
        {
            get { return qty; }
            set
            {
                qty = value;
                NotifyCustomers();
            }
        }

        public IDisposable Subscribe(IObserver<Car> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }

            return new Unsubscriber(observers, observer);
        }

        public void AddToWaitingList(Customer customer)
        {
            waitingList.Add(customer);
        }

        public void RemoveFromWaitingList(Customer customer)
        {
            waitingList.Remove(customer);
        }

        public void NotifyCustomers()
        {
            if (qty > 0)
            {
                foreach (var customer in waitingList)
                {
                    customer.Notify($"The car is now available! Quantity: {qty}");
                    RemoveObserver(customer);
                }
            }
        }

        public Customer FindCustomerInWaitingList(string customerToRemove)
        {
            return waitingList.Find(c => c.Name == customerToRemove);
        }

        public void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.OnNext(this);
            }
        }

        private void RemoveObserver(Customer customer)
        {
            observers.Remove(customer);
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<Car>> _observers;
            private IObserver<Car> _observer;

            public Unsubscriber(List<IObserver<Car>> observers, IObserver<Car> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (_observers.Contains(_observer))
                {
                    _observers.Remove(_observer);
                }
            }
        }
    }
}

