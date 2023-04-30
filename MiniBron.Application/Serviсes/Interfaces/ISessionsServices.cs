using MiniBron.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBron.Application.Serviсes.Interfaces
{
    public interface ISessionsServices
    {
        public IEnumerable<SessionDTO> GetAllSessions(int hotelId);
        public IEnumerable<SessionDTO> GetActualSessions(int hotelId);
        public SessionDTO GetSessionsById(int sessionId, int hotelId);
        public int CreateSession(SessionCreateDTO session, int hotelId);
        public bool ChangeSession(SessionChangeDTO session, int hotelId);
        public bool DeleteSessionById(SessionDeleteDTO sessionDeleteDTO, int hotelId);
        public int AddServiceInSession(ServicesForSessionCreateDTO servicesForSession, int hotelId);
        public bool DeleteServisForSessionById(ServicesForSessionDeleteDTO servicesForSessionDeleteDTO, int hotelId);
    }
}
