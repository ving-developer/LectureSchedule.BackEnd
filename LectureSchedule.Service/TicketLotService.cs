using AutoMapper;
using LectureSchedule.Data.Persistence.Interface;
using LectureSchedule.Domain;
using LectureSchedule.Service.DTO;
using LectureSchedule.Service.Exceptions;
using LectureSchedule.Service.Interface;
using System.Linq;
using System.Threading.Tasks;

namespace LectureSchedule.Service
{
    public class TicketLotService : ITicketLotService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public TicketLotService(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<bool> DeleteTicketLot(int ticketLotId, int lectureId)
        {
            try
            {
                var ticketLot = await _unit.TicketLotRepository.GetTicketLotById(ticketLotId,lectureId);
                if (ticketLot is null) throw new NotFoundException(ticketLotId);
                _unit.TicketLotRepository.Delete(ticketLot);

                return await _unit.CommitAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<TicketLotDTO[]> SaveTicketLot(int lectureId, TicketLotDTO[] models)
        {
            try
            {
                var currentTicketLots = await _unit.TicketLotRepository.GetTicketLotsByLectureId(lectureId);
                foreach (TicketLotDTO model in models)
                {
                    if (model.Id == 0)
                    {
                        var ticketLot = _mapper.Map<TicketLot>(model);
                        ticketLot.LectureId = lectureId;
                        _unit.TicketLotRepository.Add(ticketLot);
                    }
                    else
                    {
                        var ticketLot = currentTicketLots?.FirstOrDefault(lot => lot.Id == model.Id);
                        model.LectureId = lectureId;
                        _mapper.Map(model, ticketLot);
                        _unit.TicketLotRepository.Update(ticketLot);
                    }
                }
                
                if (await _unit.CommitAsync())
                {
                    var updatedTicketsLots = await _unit.TicketLotRepository.GetTicketLotsByLectureId(lectureId);
                    return _mapper.Map<TicketLotDTO[]>(updatedTicketsLots);
                }
                return null;
            }
            catch
            {
                throw;
            }
        }

        public async Task<TicketLotDTO> GetTicketLotById(int ticketLotId, int lectureId)
        {
            try
            {
                var ticketLot = await _unit.TicketLotRepository.GetTicketLotById(ticketLotId, lectureId);
                return _mapper.Map<TicketLotDTO>(ticketLot);
            }
            catch
            {
                throw;
            }
        }

        public async Task<TicketLotDTO[]> GetTicketLotsByLectureId(int lectureId)
        {
            try
            {
                var ticketLot = await _unit.TicketLotRepository.GetTicketLotsByLectureId(lectureId);
                return _mapper.Map<TicketLotDTO[]>(ticketLot);
            }
            catch
            {
                throw;
            }
        }

    }
}
