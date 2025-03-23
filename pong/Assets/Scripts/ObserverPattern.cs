using System;
using System.Collections.Generic;

/// Interfaces for the Observer Pattern
namespace ObserverPattern
{
    public abstract class Observer
    {
        public abstract void Update();
    }

    public abstract class Subject
    {
        private List<Observer> observers = new List<Observer>();
        public void Attach(Observer observer)
        {
            observers.Add(observer);
        }
        public void Detach(Observer observer)
        {
            observers.Remove(observer);
        }
        public void Notify()
        {
            foreach (Observer o in observers)
            {
                o.Update();
            }
        }
    }
}