using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace dataApp.API
{
 public class MappingProfile : Profile
{
    public MappingProfile()
    {

        // ! le mapping vas du premier instancié vers le second ex: du Owner --> OwnerDto
        //*  en regle generale, on map d abord notre model(entities ou objet) avec notre model Dto crée qui vas communqiuer avec la BDD
        CreateMap<Owner, OwnerDto>();
        CreateMap<Account, AccountDto>();
        CreateMap<OwnerForCreationDto, Owner>();
        CreateMap<OwnerForUpdateDto, Owner>();
        CreateMap<AccountForCreationDto, Account>();
         CreateMap<AccountForUpdateDto, Account>();
    }
}

}
