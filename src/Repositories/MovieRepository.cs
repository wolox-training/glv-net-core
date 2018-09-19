using System.Collections.Generic;
using System.Linq;
using TrainingNet.Models;
using TrainingNet.Repositories.Database;
using TrainingNet.Repositories.Interfaces;

namespace TrainingNet.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(DataBaseContext context) 
            : base(context)
        {
        }
    }
}
