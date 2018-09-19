using System.Collections.Generic;
using System.Linq;
<<<<<<< 2b195b81bbc5a1a4ab6ebb48595eebf4f873862b
using Microsoft.EntityFrameworkCore;
=======
>>>>>>> Repositories in process
using TrainingNet.Models;
using TrainingNet.Repositories.Database;
using TrainingNet.Repositories.Interfaces;

namespace TrainingNet.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
<<<<<<< 2b195b81bbc5a1a4ab6ebb48595eebf4f873862b
        public MovieRepository(DataBaseContext context) : base(context) {}
=======
        public MovieRepository(DataBaseContext context) 
            : base(context)
        {
        }
>>>>>>> Repositories in process
    }
}
