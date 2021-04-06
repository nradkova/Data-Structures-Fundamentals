namespace _02.LegionSystem
{
    using System;
    using System.Collections.Generic;
    using _02.LegionSystem.Interfaces;
    using Wintellect.PowerCollections;

    public class Legion : IArmy
    {
        private OrderedSet<IEnemy> legion;
        public Legion()
        {
            this.legion = new OrderedSet<IEnemy>();
        }
        public int Size => this.legion.Count;

        public bool Contains(IEnemy enemy)
        {
            return this.legion.Contains(enemy);
        }

        public void Create(IEnemy enemy)
        {
            legion.Add(enemy);
        }

        public IEnemy GetByAttackSpeed(int speed)
        {
            foreach (var enemy in legion)
            {
                if (enemy.AttackSpeed == speed)
                {
                    return enemy;
                }
            }
            return null;
        }

        public List<IEnemy> GetFaster(int speed)
        {
            var result = new List<IEnemy>();
            foreach (var enemy in legion)
            {
                if (enemy.AttackSpeed > speed)
                {
                    result.Add(enemy);
                }
            }
            return result;
        }

        public IEnemy GetFastest()
        {
            this.EnsureNotEmpty();
            return this.legion.GetLast();
        }

        public IEnemy[] GetOrderedByHealth()
        {
            var newLegion = new OrderedBag<IEnemy>(CompareElements);
            newLegion.AddMany(this.legion);
            return newLegion.ToArray();
        }

        public List<IEnemy> GetSlower(int speed)
        {
            var result = new List<IEnemy>();
            foreach (var enemy in legion)
            {
                if (enemy.AttackSpeed<speed)
                {
                    result.Add(enemy);
                }
            }
            return result;
        }

        public IEnemy GetSlowest()
        {
            this.EnsureNotEmpty();
            return this.legion.GetFirst();
        }

        public void ShootFastest()
        {
            this.EnsureNotEmpty();
            this.legion.RemoveLast();
        }

        public void ShootSlowest()
        {
            this.EnsureNotEmpty();
            this.legion.RemoveFirst();
        }

        private void EnsureNotEmpty()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException
                    ("Legion has no enemies!");

            }
        }

        private int CompareElements(IEnemy x, IEnemy y)
        {
            return y.Health - x.Health;
        }

    }
}
