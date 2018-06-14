using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Acme.SimpleTaskApp.Persons;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Acme.SimpleTaskApp.Common
{
    public class LookupAppService : SimpleTaskAppAppServiceBase, ILookupAppService
    {
        private readonly IRepository<Person, Guid> _personRepository;

        public LookupAppService(IRepository<Person, Guid> repository)
        {
            _personRepository = repository;
        }

        public async Task<ListResultDto<ComboboxItemDto>> GetPersonComboboxItemsAsync()
        {
            var person = await _personRepository.GetAllListAsync();

            return new ListResultDto<ComboboxItemDto>(person.Select(x => new ComboboxItemDto(x.Id.ToString("D"), x.Name)).ToList());
        }
    }
}
