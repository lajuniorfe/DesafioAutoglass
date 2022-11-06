using Desafio.Domain.Entities;
using Desafio.Domain.Interfaces;
using Desafio.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Infra.Data.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly SqliteContext _sqliteContext;

        public BaseRepository(SqliteContext sqliteContext)
        {
            _sqliteContext = sqliteContext;
        }


        public void Insert(TEntity obj)
        {
            _sqliteContext.Set<TEntity>().Add(obj);
            _sqliteContext.SaveChanges();
        }

        public IList<TEntity> Select(int NumeroPagina, int quantidadeRegistro)
        {
            return _sqliteContext.Set<TEntity>()
                .Skip(NumeroPagina - 1)
                .Take(quantidadeRegistro)
                .ToList();
        }

        public TEntity Select(int Codigo) => _sqliteContext.Set<TEntity>().Find(Codigo);

        public void Update(TEntity obj)
        {
            _sqliteContext.Entry(obj).Property(p => p.Codigo).IsModified = false; 
            _sqliteContext.Entry(obj).State = EntityState.Modified;
            _sqliteContext.SaveChanges();
        }


        
      
    }
}
