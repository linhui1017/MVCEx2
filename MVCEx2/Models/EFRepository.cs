using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace MVCEx2.Models
{
	public class EFRepository<T> : IRepository<T> where T : class
	{
		public IUnitOfWork UnitOfWork { get; set; }

		/*
			delegate : this.UnitOfWork.Context	
		*/
		public CustomerEntities DBContex
		{
			get
			{
				return ((CustomerEntities)this.UnitOfWork.Context);
			}
			set { return; }
		}
		/*
			delegate : UnitOfWork.Commit()	
		*/
		public void Commit() 
		{
			this.UnitOfWork.Commit();
		}
		
		private IDbSet<T> _objectset;
		private IDbSet<T> ObjectSet
		{
			get
			{
				if (_objectset == null)
				{
					_objectset = UnitOfWork.Context.Set<T>();
				}
				return _objectset;
			}
		}

		public virtual IQueryable<T> All()
		{
			return ObjectSet.AsQueryable();
		}

		public IQueryable<T> Where(Expression<Func<T, bool>> expression)
		{
			return ObjectSet.Where(expression);
		}

		public void Add(T entity)
		{
			ObjectSet.Add(entity);
		}

		public virtual void Delete(T entity)
		{
			ObjectSet.Remove(entity);
		}

	}
}