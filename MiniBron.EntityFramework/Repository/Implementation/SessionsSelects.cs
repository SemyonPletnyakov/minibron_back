using Microsoft.EntityFrameworkCore;
using MiniBron.Domain;
using MiniBron.EntityFramework.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBron.EntityFramework.Repository.Implementation
{
    public class SessionsSelects : ISessionsSelects
    {
        public IEnumerable<Session> GetAllSessions(int hotelId)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    List<Session> result = db.Sessions.Where(r => r.Room.HotelId == hotelId)
                        .Include(r => r.Room)
                        .Include(r => r.ServicesForSessions)?.ThenInclude(s => s.AdditionalService).ToList();
                    return result;
                }
            }
            catch
            {
                return null;
            }
        }
        public IEnumerable<Session> GetActualSessions(int hotelId)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {

                    List<Session> result = db.Sessions.Where(r => r.Room.HotelId == hotelId && (r.EndDateTime > DateTime.Now || r.EndDateTime==null)).Include(r => r.Room).Include(r => r.ServicesForSessions).ThenInclude(s => s.AdditionalService).ToList();
                    return result;
                }
            }
            catch
            {
                return null;
            }
        }
        public Session GetSessionsById(int sessionId, int hotelId)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {

                    Session result = db.Sessions.Where(r => r.Id == sessionId && r.Room.HotelId == hotelId).Include(r => r.Room).Include(r => r.ServicesForSessions).ThenInclude(s => s.AdditionalService).FirstOrDefault();
                    return result;
                }
            }
            catch
            {
                return null;
            }
        }

        
        public int CreateSession(Session session, int hotelId)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Room room = db.Rooms.FirstOrDefault(b => b.Id == session.RoomId);
                    if (room == null || room.HotelId != hotelId) return -3;
                    db.Sessions.Add(session);
                    db.SaveChanges();
                    UpdateTotalPriceForSession(db, session.Id);
                    return session.Id;
                }
            }
            catch
            {
                return -1;
            }
        }
        public bool ChangeSession(Session session, int hotelId)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {

                    Session mainSession = db.Sessions.Include(b=>b.Room).FirstOrDefault(b => b.Id == session.Id && b.Room.Hotel.Id == hotelId);
                    if (mainSession == null) return false;
                    mainSession.RoomId = session.RoomId;
                    mainSession.StartDateTime = session.StartDateTime;
                    mainSession.EndDateTime = session.EndDateTime;
                    mainSession.FIO = session.FIO;
                    mainSession.Phone = session.Phone;
                    mainSession.Email = session.Email;
                    mainSession.ActualPriceForRoom = session.ActualPriceForRoom;

                    db.SaveChanges();
                    UpdateTotalPriceForSession(db, mainSession.Id);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        private void UpdateTotalPriceForSession(ApplicationContext db, int sessionId)
        {
            try
            {
                Session session = db.Sessions.FirstOrDefault(s => s.Id == sessionId);
                int dayCount = ((session.EndDateTime ?? DateTime.Now) - session.StartDateTime).Days;
                session.TotalPrice = session.ActualPriceForRoom*(dayCount>0?dayCount:0) + db.ServicesForSessions.Where(sv => sv.SessionsId == sessionId).Sum(sv => sv.ActualPriceForService);
                db.SaveChanges();
            }
            catch { }
        }
        public bool DeleteSessionById(int sessionId, int hotelId)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {

                    Session mainSession = db.Sessions.Include(b => b.Room).FirstOrDefault(b => b.Id == sessionId && b.Room.Hotel.Id == hotelId);
                    if (mainSession == null) return false;
                    db.Remove(mainSession);

                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public int AddServiceInSession(ServicesForSession servicesForSession, int hotelId)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Session session = db.Sessions.Where(b => b.Id == servicesForSession.SessionsId).Include(b => b.Room).FirstOrDefault();
                    if (session == null || session.Room == null || session.Room.HotelId != hotelId) return -3;

                    db.ServicesForSessions.Add(servicesForSession);
                    db.SaveChanges();

                    UpdateTotalPriceForSession(db, session.Id);
                    return servicesForSession.Id;
                }
            }
            catch
            {
                return -1;
            }
        }
        public bool DeleteServisForSessionById(int serviceForSessionId, int hotelId)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {

                    ServicesForSession service = db.ServicesForSessions.Where(b => b.Id == serviceForSessionId && b.Sessions.Room.HotelId == hotelId).Include(s => s.Sessions).ThenInclude(s => s.Room).FirstOrDefault();
                    if (service == null) return false;
                    int sessionId = service.SessionsId;
                    db.Remove(service);

                    db.SaveChanges();

                    UpdateTotalPriceForSession(db, sessionId);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}

