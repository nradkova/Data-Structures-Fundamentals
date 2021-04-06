namespace _01.Loader
{
    using _01.Loader.Interfaces;
    using _01.Loader.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Loader : IBuffer
    {
        private List<IEntity> entities;
        public Loader()
        {
            this.entities = new List<IEntity>();
        }
        public int EntitiesCount => this.entities.Count;

        //O(1)amortized
        public void Add(IEntity entity)
        {
            this.entities.Add(entity);
        }

        public void Clear()
        {
            this.entities.Clear();
        }

        public bool Contains(IEntity entity)
        {
            return this.GetById(entity.Id) != null;
        }

        public IEntity Extract(int id)
        {
            var current = this.GetById(id);
            if (current!=null)
            {
                this.entities.Remove(current);
            }
            return current;
        }

        public IEntity Find(IEntity entity)
        {
            return this.GetById(entity.Id);
        }

        public List<IEntity> GetAll()
        {
            return new List<IEntity>(this.entities);
        }

        public IEnumerator<IEntity> GetEnumerator()
        {
            foreach (var entity in entities)
            {
                yield return entity;
            }
        }

        public void RemoveSold()
        {
            var temp = new List<IEntity>();
            foreach (var item in temp)
            {
                if (!item.Status.Equals(BaseEntityStatus.Sold))
                {
                    temp.Add(item);
                }
            }
            this.entities = temp;
        }

        public void Replace(IEntity oldEntity, IEntity newEntity)
        {
           int index= this.ValidateEntityIndex(oldEntity);
            entities[index] = newEntity;
        }

        public List<IEntity> RetainAllFromTo
            (BaseEntityStatus lowerBound, BaseEntityStatus upperBound)
        {
            int lower =(int) lowerBound;
            int upper = (int)upperBound;
            var list = new List<IEntity>(this.EntitiesCount);
            foreach (var entity in entities)
            {
                if (IsStatusInRange(entity.Status,lower,upper))
                {
                    list.Add(entity);
                }
            }
            return list;
        }

        public void Swap(IEntity first, IEntity second)
        {
            int firstIndex = this.ValidateEntityIndex(first);
            int secondIndex = this.ValidateEntityIndex(second);
            entities[firstIndex] = second;
            entities[secondIndex] = first;
        }

        public IEntity[] ToArray()
        {
            return this.entities.ToArray();
        }

        public void UpdateAll(BaseEntityStatus oldStatus, BaseEntityStatus newStatus)
        {
            foreach (var item in this.entities)
            {
                if ((BaseEntityStatus)item.Status==(BaseEntityStatus)oldStatus)
                {
                    item.Status=(BaseEntityStatus)newStatus;
                }
            }
        }

        private bool IsStatusInRange(BaseEntityStatus status,
            int lower, int upper)
        {
            if ((int)status < lower)
            {
                return false;
            }
            if ((int)status > upper)
            {
                return false;
            }
            return true;
        }

        private int ValidateEntityIndex(IEntity oldEntity)
        {
            int index = this.entities.IndexOf(this.GetById(oldEntity.Id));
            if (index == -1)
            {
                throw new InvalidOperationException("Entity not found");
            }
            return index;
        }
        IEnumerator IEnumerable.GetEnumerator()
        => this.GetEnumerator();

        private IEntity GetById(int id)
        {
            foreach (var entity in entities)
            {
                if (entity.Id==id)
                {
                    return entity;
                }
            }
            return null;
        }
    }
}
