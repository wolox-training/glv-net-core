
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TrainingNet.Repositories.Interfaces;

namespace TrainingNet.Repositories.Database

{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataBaseContext _context;

        public UnitOfWork(DataBaseContext context)
        {
            this._context = context;
            MovieRepository = new MovieRepository(_context);
            ApplicationUserRepository = new ApplicationUserRepository(_context);
        }
        public IMovieRepository MovieRepository { get; private set; }
        public IApplicationUserRepository ApplicationUserRepository { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
