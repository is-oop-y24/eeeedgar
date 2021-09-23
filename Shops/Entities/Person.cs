using System;
using System.Collections.Generic;

namespace Shops.Entities
{
    public class Person : BankClient, ISubject, IPerson
    {
        private List<IObserver> _observers;
        private Bank _bank;

        private Person(string name, Bank bank)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("wrong person name");
            Name = name;
            _bank = bank;
            _observers = new List<IObserver>();
        }

        public string Name { get; }
        public int SelectedShopId { get; private set; }

        public int SelectedProductId { get; private set; }
        public int SelectedProductAmount { get; private set; }

        public int Money => _bank.ProfileBalance(Id);

        public static Person CreateInstance(string name, Bank bank)
        {
            return new Person(name, bank);
        }

        public void MakePurchase(int shopId, int productId, int productAmount)
        {
            SelectedShopId = shopId;
            SelectedProductId = productId;
            SelectedProductAmount = productAmount;
            Notify();
        }

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (IObserver observer in _observers)
            {
                observer.Update(this);
            }
        }
    }
}