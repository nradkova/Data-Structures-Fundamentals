﻿namespace _02.LegionSystem.Models
{
    using System;
    using _02.LegionSystem.Interfaces;

    public class Enemy : IEnemy
    {
        public Enemy(int attackSpeed, int health)
        {
            this.AttackSpeed = attackSpeed;
            this.Health = health;
        }

        public int AttackSpeed { get; set; }

        public int Health { get; set; }

        public int CompareTo(object obj)
        {
            var current = (IEnemy)obj;
            return this.AttackSpeed - current.AttackSpeed;
        }
        public override bool Equals(object obj)
        {
            var current = (IEnemy)obj;
            return this.AttackSpeed ==current.AttackSpeed;
        }
    }
}
