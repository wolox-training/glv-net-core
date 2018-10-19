using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TrainingNet.Models;
using TrainingNet.Repositories.Database;
using TrainingNet.Repositories.Interfaces;

namespace TrainingNet.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(DataBaseContext context) : base(context) {}
        public Movie GetMovieWithComments( int id)
        {
            return Context.Movies.Where(m => m.Id == id).Include(m => m.Comments).FirstOrDefault();
        }
    }
}
