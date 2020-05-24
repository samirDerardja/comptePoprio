using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using Entities;
using Entities.Models;

 
namespace Repository
{
    public class AccountRepository : RepositoryBase<Account> ,IAccountRepository
    {
        public AccountRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        {
        }

        public IEnumerable<Account> AccountsByOwner(Guid ownerId)
{
    return FindByCondition(a => a.idProprietaire.Equals(ownerId)).ToList();
} 

public IEnumerable<Account> GetAllAccounts()
        {
            return FindAll()
                .OrderBy(ow => ow.TypeDeCompte)
                .ToList();
        }

              
        public Account GetAccountById(Guid idAccount)
{
    return FindByCondition(account => account.id.Equals(idAccount)).FirstOrDefault();
}

 public void CreateAccount(Account account)
        {
            Create(account);
        }
         public void UpdateAccount(Account account)
        {
            Update(account);
        }
         public void DeleteAccount(Account account)
        {
            Delete(account);
        }
      
    }
} 