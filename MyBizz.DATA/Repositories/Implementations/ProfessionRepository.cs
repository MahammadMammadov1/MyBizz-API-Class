using Mamba_Class.DAL;
using Mamba_Class.Entites;
using MyBizz.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBizz.Data.Repositories.Implementations
{
    public class ProfessionRepository : GenericRepository<Profession>, IProfessionRepository
    {
        public ProfessionRepository(AppDbContext appDb) : base(appDb)
        {
        }
    }
}
