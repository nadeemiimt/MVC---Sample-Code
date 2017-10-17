using System;
using System.Collections.Generic;
using System.Linq;
using EPDataLayer.Entities;

namespace EPDataLayer.Repository
{
    /// <summary>
    ///     Generic repository Class
    /// </summary>
    public class Repository : IRepository
    {
        //Private Variables
        private bool bDisposed;
        private EPTestEntities context;
        private IUnitOfWork unitOfWork;

        #region Contructor Logic

        /// <summary>
        ///     Initializes a new instance of the
        /// </summary>
        /// <param name="contextObj"></param>
        public Repository(EPTestEntities contextObj)
        {
            if (contextObj == null)
                this.context = new EPTestEntities();
            this.context = contextObj;
        }

        public void Dispose()
        {
            Close();
        }

        #endregion

        #region Properties

        //DbContext Property
        protected EPTestEntities DbContext
        {
            get
            {
                if (context == null)
                    throw new ArgumentNullException("context");

                return context;
            }
        }

        //Unit of Work Property
        public IUnitOfWork UnitOfWork
        {
            get
            {
                if (unitOfWork == null)
                {
                    unitOfWork = new UnitOfWork(DbContext);
                }
                return unitOfWork;
            }
        }

        #endregion

        #region Data Display Methods


        //All Readonly Display or fetch data methods.
        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return DbContext.Set<TEntity>().AsQueryable();
        }

        #endregion

        #region Data Transactional Methods

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            DbContext.Set<TEntity>().Add(entity);
        }

        public TEntity Save<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
           var data =  DbContext.Set<TEntity>().Add(entity);
            DbContext.SaveChanges();
            return data;
        }

        public void Add<TEntity>(List<TEntity> entities) where TEntity : class
        {
            if (entities.Count ==0)
            {
                throw new ArgumentNullException("entities");
            }
            DbContext.Set<TEntity>().AddRange(entities);
        }

        public void Attach<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            DbContext.Set<TEntity>().Attach(entity);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            DbContext.Set<TEntity>().Remove(entity);
        }

        #endregion

        #region Disposing Methods

        protected void Dispose(bool bDisposing)
        {
            if (!bDisposed)
            {
                if (bDisposing)
                {
                    if (null != context)
                    {
                        context.Dispose();
                    }
                }
                bDisposed = true;
            }
        }

        public void Close()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
