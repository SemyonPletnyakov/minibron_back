using MiniBron.Application.DTO;
using MiniBron.Application.Serviсes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniBron.Domain;
using MiniBron.EntityFramework.Repository.Implementation;
using MiniBron.EntityFramework.Repository.Interfaces;

namespace MiniBron.Application.Serviсes.Implementation
{
    public class SessionsServices : ISessionsServices
    {
        ISessionsSelects sessionsSelects;
        public SessionsServices()
        {
            sessionsSelects = new SessionsSelects();
        }
        public IEnumerable<SessionDTO> GetAllSessions(int hotelId)
        {
            return sessionsSelects.GetAllSessions(hotelId)?.Select(b => new SessionDTO()
            {
                Id = b.Id,
                RoomId = b.RoomId,
                RoomName = b.Room.Title,
                StartDateTime = b.StartDateTime,
                EndDateTime = b.EndDateTime,
                FIO = b.FIO,
                Phone = b.Phone,
                Email = b.Email,
                ActualPriceForRoom = b.ActualPriceForRoom,
                TotalPrice = b.TotalPrice,
                ServicesForSessions = b.ServicesForSessions?.Select(s => new ServicesForSessionDTO()
                {
                    Id = s.Id,
                    SessionId = s.SessionsId,
                    AdditionalServiceId = s.AdditionalServiceId,
                    ServiceName = s.AdditionalService.Title,
                    ActualPrice = s.ActualPriceForService
                })
            });
        }
        public IEnumerable<SessionDTO> GetActualSessions(int hotelId)
        {
            return sessionsSelects.GetActualSessions(hotelId)?.Select(b => new SessionDTO()
            {
                Id = b.Id,
                RoomId = b.RoomId,
                RoomName = b.Room.Title,
                StartDateTime = b.StartDateTime,
                EndDateTime = b.EndDateTime,
                FIO = b.FIO,
                Phone = b.Phone,
                Email = b.Email,
                ActualPriceForRoom = b.ActualPriceForRoom,
                TotalPrice = b.TotalPrice,
                ServicesForSessions = b.ServicesForSessions?.Select(s => new ServicesForSessionDTO()
                {
                    Id = s.Id,
                    SessionId = s.SessionsId,
                    AdditionalServiceId = s.AdditionalServiceId,
                    ServiceName = s.AdditionalService.Title,
                    ActualPrice = s.ActualPriceForService
                })
            });
        }
        public SessionDTO GetSessionsById(int sessionId, int hotelId)
        {
            Session b = sessionsSelects.GetSessionsById(sessionId, hotelId);
            if (b == null) return null;
            return new SessionDTO()
            {
                Id = b.Id,
                RoomId = b.RoomId,
                RoomName = b.Room.Title,
                StartDateTime = b.StartDateTime,
                EndDateTime = b.EndDateTime,
                FIO = b.FIO,
                Phone = b.Phone,
                Email = b.Email,
                ActualPriceForRoom = b.ActualPriceForRoom,
                TotalPrice = b.TotalPrice,
                ServicesForSessions = b.ServicesForSessions?.Select(s => new ServicesForSessionDTO()
                {
                    Id = s.Id,
                    SessionId = s.SessionsId,
                    AdditionalServiceId = s.AdditionalServiceId,
                    ServiceName = s.AdditionalService.Title,
                    ActualPrice = s.ActualPriceForService
                })
            };
        }
        public int CreateSession(SessionCreateDTO session, int hotelId)
        {
            return sessionsSelects.CreateSession(new Session()
            {
                RoomId = session.RoomId,
                StartDateTime = session.StartDateTime,
                EndDateTime = session.EndDateTime,
                FIO = session.FIO,
                Phone = session.Phone,
                Email = session.Email,
                ActualPriceForRoom = session.ActualPriceForRoom,
                TotalPrice = session.TotalPrice
            }, hotelId);
        }
        public bool ChangeSession(SessionChangeDTO session, int hotelId)
        {
            return sessionsSelects.ChangeSession(new Session()
            {
                Id = session.Id,
                RoomId = session.RoomId,
                StartDateTime = session.StartDateTime,
                EndDateTime = session.EndDateTime,
                FIO = session.FIO,
                Phone = session.Phone,
                Email = session.Email,
                ActualPriceForRoom = session.ActualPriceForRoom
            }, hotelId);
        }
        public bool DeleteSessionById(SessionDeleteDTO sessionDeleteDTO, int hotelId)
        {
            return sessionsSelects.DeleteSessionById(sessionDeleteDTO.Id, hotelId);
        }
        public int AddServiceInSession(ServicesForSessionCreateDTO servicesForSession, int hotelId)
        {
            return sessionsSelects.AddServiceInSession(new ServicesForSession() { SessionsId = servicesForSession.SessionsId, AdditionalServiceId = servicesForSession.AdditionalServiceId, ActualPriceForService = servicesForSession.ActualPrice }, hotelId);
        }
        public bool DeleteServisForSessionById(ServicesForSessionDeleteDTO servicesForSessionDeleteDTO, int hotelId)
        {
            return sessionsSelects.DeleteServisForSessionById(servicesForSessionDeleteDTO.Id, hotelId);
        }
    }
}
