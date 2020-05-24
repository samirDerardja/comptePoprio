
using System;
using System.Collections.Generic; 
using Entities.Models;

namespace Contracts 
{

    /*Si vous essayez de supprimer le propriétaire qui a des comptes, vous obtiendrez 500 erreurs internes, 
    car nous n'avons pas autorisé la suppression en cascade dans notre configuration de base de données.
     Ce que nous voulons, c'est renvoyer une BadRequest. Donc, pour ce faire, apportons quelques modifications.*/
    public interface IAccountRepository : IRepositoryBase<Account>
    {
         IEnumerable<Account> AccountsByOwner(Guid ownerId);
          IEnumerable<Account> GetAllAccounts();
         
         Account GetAccountById(Guid idAccount);

         void CreateAccount(Account account);
          void UpdateAccount(Account account);
          void DeleteAccount(Account account);
          
    }
}