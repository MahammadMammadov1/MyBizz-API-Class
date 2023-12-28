using AutoMapper.Execution;
using Mamba_Class.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Member = Mamba_Class.Entites.Member;

namespace MyBizz.Core.Repositories.Interfaces
{
    public interface IMemberRepository : IGenericRepository<Member>
    {
    }
}
