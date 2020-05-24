using Contracts;
using Entities;
 
namespace Repository
{

    /*Comme vous pouvez le voir, nous créons des propriétés qui exposeront les référentiels
     concrets et nous avons également la méthode Save () à utiliser une fois toutes les modifications
      terminées sur un certain objet. Il s'agit d'une bonne pratique, car nous pouvons maintenant, par exemple, 
      ajouter deux propriétaires, modifier deux comptes et supprimer un propriétaire, le tout dans une seule méthode,
       puis appeler une seule fois la méthode Save. Toutes les modifications seront appliquées
        ou si quelque chose échoue, toutes les modifications seront annulées: */
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IOwnerRepository _owner;
        private IAccountRepository _account;
 
        public IOwnerRepository Owner {
            get {
                if(_owner == null)
                {
                    _owner = new OwnerRepository(_repoContext);
                }
 
                return _owner;
            }
        }
 
        public IAccountRepository Account {
            get {
                if(_account == null)
                {
                    _account = new AccountRepository(_repoContext);
                }
 
                return _account;
            }
        }
 
        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
 
        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}