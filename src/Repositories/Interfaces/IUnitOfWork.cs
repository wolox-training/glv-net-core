using System;
using TrainingNet.Repositories.Database;

namespace TrainingNet.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IMovieRepository MovieRepository { get; }
        int Complete();
    }
}
