using System;
using TrainingNet.Repositories.Database;

namespace TrainingNet.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        MovieRepository MovieRepository { get; }
        int Complete();
    }
}