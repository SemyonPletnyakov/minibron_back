using MiniBron.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBron.EntityFramework.Repository.Interfaces
{
    public interface ISessionsSelects
    {
        public IEnumerable<Session> GetAllSessions(int hotelId);
        public IEnumerable<Session> GetActualSessions(int hotelId);
        public Session GetSessionsById(int sessionId, int hotelId);
        public int CreateSession(Session session, int hotelId);
        public bool ChangeSession(Session session, int hotelId);
        public bool DeleteSessionById(int sessionId, int hotelId);
        public int AddServiceInSession(ServicesForSession servicesForSession, int hotelId);
        public bool DeleteServisForSessionById(int serviceForSessionId, int hotelId);
        public IEnumerable<Session> GetSessionsByDate(DateTime startDate, DateTime endDate, int hotelId);
    }
}
