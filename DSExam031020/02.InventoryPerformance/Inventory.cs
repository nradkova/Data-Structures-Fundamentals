namespace _01.Inventory
{
    using _01.Inventory.Interfaces;
    using _01.Inventory.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Inventory : IHolder
    {
        private List<IWeapon> weapons;
        public Inventory()
        {
            weapons = new List<IWeapon>();
        }
        public int Capacity => this.weapons.Count;

        public void Add(IWeapon weapon)
        {
            weapons.Add(weapon);
        }

        public void Clear()
        {
            weapons.Clear();
        }

        public bool Contains(IWeapon weapon)
        {
            int index = weapons.IndexOf(weapon);
            return index > -1 ? true : false;
        }

        public void EmptyArsenal(Category category)
        {
            foreach (var weapon in weapons)
            {
                if (weapon.Category==(Category)category)
                {
                    weapon.Ammunition = 0;
                }
            }
        }

        public bool Fire(IWeapon weapon, int ammunition)
        {
           int index =this.ValidateWeapon(weapon);
            if (weapons[index].Ammunition>=ammunition)
            {
                weapons[index].Ammunition -= ammunition;
                return true;
            }
            return false;
        }

        public IWeapon GetById(int id)
        {
            foreach (var weapon in weapons)
            {
                if (weapon.Id==id)
                {
                    return weapon;
                }
            }
            return null;
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var weapon in weapons)
            {
                yield return weapon;
            }
        }

        public int Refill(IWeapon weapon, int ammunition)
        {
            this.ValidateWeapon(weapon);
            if (weapon.Ammunition-ammunition<=weapon.MaxCapacity)
            {
                weapon.Ammunition += ammunition;
            }
            return weapon.Ammunition;
        }

        public IWeapon RemoveById(int id)
        {
            var weapon = this.GetById(id);
            if (weapon==null)
            {
                throw new InvalidOperationException
                    ("Weapon does not exist in inventory!");
            }
            this.weapons.Remove(weapon);
            return weapon;
        }

        public int RemoveHeavy()
        {
            return this.weapons
                .RemoveAll(x=>x.Category==Category.Heavy);
        }

        public List<IWeapon> RetrieveAll()
        {
            return new List<IWeapon>(this.weapons);
        }

        public List<IWeapon> RetriveInRange
            (Category lower, Category upper)
        {
            int lowerBound = (int)(Category)lower;
            int upperBound = (int)(Category)upper;
            var result = new List<IWeapon>();
            foreach (var weapon in weapons)
            {
                int current = (int)weapon.Category;
                if (current>=lowerBound&&current<=upperBound)
                {
                    result.Add(weapon);
                }
            }
            return result;
        }

        public void Swap(IWeapon firstWeapon, IWeapon secondWeapon)
        {
            int firstInd = ValidateWeapon(firstWeapon);
            int secondInd = ValidateWeapon(secondWeapon);
            if (firstWeapon.Category==secondWeapon.Category)
            {
            this.weapons[firstInd] = secondWeapon;
            this.weapons[secondInd] = firstWeapon;
            }
        }

        private int ValidateWeapon(IWeapon weapon)
        {
            int index = weapons.IndexOf(weapon);
            if (index < 0)
            {
                throw new InvalidOperationException
                    ("Weapon does not exist in inventory!");
            }
            return index;
        }
    }
}
