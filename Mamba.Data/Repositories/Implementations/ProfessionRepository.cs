using Mamba.Core.Models;
using Mamba.Core.Repositories.Interfaces;
using Mamba.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamba.Data.Repositories.Implementations
{
    public class ProfessionRepository : GenericRepository<Profession>, IProfessionRepository
    {
        public ProfessionRepository(AppDbContext appDb) : base(appDb)
        {
        }
    }
}
