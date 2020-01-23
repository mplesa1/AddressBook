using AddressBook.Model;
using AddressBook.Shared.Contracts.Business;
using AddressBook.Shared.Contracts.DataAccess;
using AddressBook.Shared.DataTransferObjects.Settlement;
using AddressBook.Shared.Infrastructure.Exceptions;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddressBook.BusinessLayer.Services
{
    public class SettlementService : BaseService, ISettlementService
    {
        private readonly ISettlementRepository _settlementRepository;

        public SettlementService(ISettlementRepository settlementRepository,
            IMapper mapper) : base(mapper)
        {
            _settlementRepository = settlementRepository;
        }

        public async Task<ICollection<SettlementDto>> FindAllAsync()
        {
            var settlements = await _settlementRepository.FindAllAsync();

            var settlementDtos = Map<ICollection<Settlement>, ICollection<SettlementDto>>(settlements);

            return settlementDtos;
        }

        public async Task CreateAsync(CreateSettlementDto dto)
        {
            var settlement = Map<CreateSettlementDto, Settlement>(dto);
            await _settlementRepository.CreateAsync(settlement);
        }

        public async Task UpdateAsync(int settlementId, CreateSettlementDto dto)
        {
            var settlement = await _settlementRepository.FindByIdAsync(settlementId);

            if (settlement == null)
            {
                throw new ResourceNotFoundException();
            }

            MapToInstance(dto, settlement);

            await _settlementRepository.UpdateAsync(settlement);
        }

        public async Task DeleteAsync(int settlementId)
        {
            Settlement settlement = await _settlementRepository.FindByIdAsync(settlementId);

            if (settlement == null)
            {
                throw new ResourceNotFoundException();
            }

            await _settlementRepository.DeleteAsync(settlement);
        }
    }
}
