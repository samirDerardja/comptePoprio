using System;
using System.Collections.Generic;
using Entities.Models;

namespace Contracts
{
    //* on fait heriter a l interface irepositorybase qui contiens uniquement les base du crud ()
  public interface IOwnerRepository  : IRepositoryBase<Owner>
{
    IEnumerable<Owner> GetAllOwners();
    Owner GetOwnerById(Guid idProprietaire);
    Owner GetOwnerWithDetails(Guid idProprietaire);
    void CreateOwner(Owner owner);
    void UpdateOwner(Owner owner);

    void DeleteOwner(Owner owner);
}
}
