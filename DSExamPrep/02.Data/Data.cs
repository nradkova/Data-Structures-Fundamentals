namespace _02.Data
{
    using _02.Data.Interfaces;
    using _02.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    public class Data : IRepository
    {
        private OrderedBag<IEntity> entities;

        public Data()
        {
            entities = new OrderedBag<IEntity>();
        }
        public Data(Data copy)
        {
            this.entities = copy.entities;
        }

        public int Size => this.entities.Count;
       
        public void Add(IEntity entity)
        {
            entities.Add(entity);
            var parent = this.GetById((int)entity.ParentId);
            if (parent!=null)
            {
                parent.AddChild(entity);
            }
        }

        public IRepository Copy()
        {
            Data result =(Data)this.MemberwiseClone();
            return new Data( result);
        }

        public IEntity DequeueMostRecent()
        {
            this.EnsureNotEmpty();
            return this.entities.RemoveFirst();
        }

        public List<IEntity> GetAll()
        {
            return new List<IEntity>(this.entities);
        }

        public List<IEntity> GetAllByType(string type)
        {
            if (type!= typeof(Invoice).Name
                && type != typeof(StoreClient).Name
                  && type != typeof(User).Name)
            {
                throw new InvalidOperationException
                    ($"Invalid type: {type}");
            }
            var result = new List<IEntity>();
            foreach (var entity in entities)
            {
                if (entity.GetType().Name==type)
                {
                    result.Add(entity);
                }
            }
            return result;
        }

        public IEntity GetById(int id)
        {
            if (id<0||id>=this.Size)
            {
                return null;
            }
            return entities[this.Size-1-id];
        }

        public List<IEntity> GetByParentId(int parentId)
        {
            var parent = this.GetById(parentId);
            return parent != null ? parent.Children : new List<IEntity>();
        }

        public IEntity PeekMostRecent()
        {
            this.EnsureNotEmpty();
            return this.entities.GetFirst();
        }

        private int CompareElements(IEntity x, IEntity y)
        {
            return y.Id - x.Id;
        }

        private void EnsureNotEmpty()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException
                    ("Operation on empty Data");
            }
        }
        
        private List<string> GetCustomTypes()
        {
            var result = new HashSet<string>();
            foreach (var entity in entities)
            {
                result.Add(entity.GetType().Name);
            }
            return result.ToList();
        }
    }
}
