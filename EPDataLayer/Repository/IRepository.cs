using System;
using System.Collections.Generic;

namespace EPDataLayer.Repository
{
    public interface IRepository : IDisposable
    {
        /// <summary>
        ///     Gets the unit of work.
        /// </summary>
        /// <value>The unit of work.</value>
        IUnitOfWork UnitOfWork { get; }


        /// <summary>
        ///     Gets all.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class;



        /// <summary>
        ///     Adds the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        void Add<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Explicitily Adds and Return Entity
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Save<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        ///     Adds the specified entity.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <TEntity></TEntity>">The type of the entity.
        void Add<TEntity>(List<TEntity> entities) where TEntity : class;

        /// <summary>
        ///     Attaches the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        void Attach<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        ///     Deletes the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        void Delete<TEntity>(TEntity entity) where TEntity : class;
    }
}
