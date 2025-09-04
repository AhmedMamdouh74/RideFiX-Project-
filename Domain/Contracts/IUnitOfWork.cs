using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts.ReposatoriesContract;
using Domain.Entities;

namespace Domain.Contracts
{
    public interface IUnitOfWork
    {
        IGenericRepository<T, TK> GetRepository<T, TK>() where T : BaseEntity<TK>;
        IEmergencyRequestReposatory EmergencyRequestRepository { get; }
        
        IConnectionIdsRepository ConnectionIdsRepository { get; }
        public Task<int> SaveChangesAsync();
    }
}
