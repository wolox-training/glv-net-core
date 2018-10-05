using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TrainingNet.Models;
using TrainingNet.Repositories.Database;
using TrainingNet.Repositories.Interfaces;

namespace TrainingNet.Repositories
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(DataBaseContext context) : base(context) {}
    }
}
