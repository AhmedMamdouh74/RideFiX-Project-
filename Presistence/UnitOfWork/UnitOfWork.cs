using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Contracts.ReposatoriesContract;
using Domain.Entities;
using Presistence.Data;
using Presistence.Repositories;

namespace Presistence.unitofwork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IEmergencyRequestReposatory _emergencyRequestRepository;
        private IConnectionIdsRepository _connectionIdsRepository;

        readonly Dictionary<string, object> _repositories = new Dictionary<string, object>();

        public UnitOfWork(ApplicationDbContext context, IEmergencyRequestReposatory emergencyRequestRepository, IConnectionIdsRepository connectionIdsRepository)
        {
            _context = context;
            _emergencyRequestRepository = emergencyRequestRepository;
            _connectionIdsRepository = connectionIdsRepository;
        }

        public IEmergencyRequestReposatory EmergencyRequestRepository =>
            _emergencyRequestRepository ??= new EmergencyRequestReposatory(_context);

        public IConnectionIdsRepository ConnectionIdsRepository => _connectionIdsRepository ??= new ConnectionIdsRepository(_context);



        public IGenericRepository<T, TK> GetRepository<T, TK>() where T : BaseEntity<TK>
        {
            if (_repositories.ContainsKey(typeof(T).Name))
            {
                return (IGenericRepository<T, TK>)_repositories[typeof(T).Name];
            }
            var repository = new GenericRepository<T, TK>(_context);
            _repositories.Add(typeof(T).Name, repository);
            return repository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
