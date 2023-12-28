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
    public class MemberRepository : GenericRepository<Member>, IMemberRepository
    {
        public MemberRepository(AppDbContext appDb) : base(appDb)
        {
        }
    }
}
