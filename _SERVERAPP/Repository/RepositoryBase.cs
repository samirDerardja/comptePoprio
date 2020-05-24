using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
 
namespace Repository
{

    //*Cette classe abstraite, ainsi que l'interface IRepositoryBase ( dans le dossier contrat), utilise le type générique T pour travailler avec.
    //* Ce type T donne encore plus de réutilisabilité à la classe RepositoryBase. Cela signifie que nous n'avons pas besoin
    //* de spécifier le modèle (classe) exact pour le moment pour que RepositoryBase fonctionne.
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext RepositoryContext { get; set; }
 
        public RepositoryBase(RepositoryContext repositoryContext)
        {
            this.RepositoryContext = repositoryContext;
        }
 
        public IQueryable<T> FindAll()
        {
            return this.RepositoryContext.Set<T>().AsNoTracking();
        }
 
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.RepositoryContext.Set<T>().Where(expression).AsNoTracking();
        }
 
        public void Create(T entity)
        {
            this.RepositoryContext.Set<T>().Add(entity);
        }
 
        public void Update(T entity)
        {
            this.RepositoryContext.Set<T>().Update(entity);
        }
 
        public void Delete(T entity)
        {
            this.RepositoryContext.Set<T>().Remove(entity);
        }
    }
}