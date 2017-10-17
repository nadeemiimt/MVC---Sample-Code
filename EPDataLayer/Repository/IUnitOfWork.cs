using System;

namespace EPDataLayer.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
    }
}
