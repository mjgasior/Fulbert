using System.Collections.Generic;
using Fulbert.DAL.PatientDAL.Models;
using NHibernate;
using Fulbert.DAL.PatientDAL.Abstract;

namespace Fulbert.DAL.PatientDAL
{
    public class PatientDal : IPatientDal
    {
        // readme:
        // http://www.codeproject.com/Articles/13390/NHibernate-Best-Practices-with-ASP-NET-nd-Ed
        //
        private readonly ISessionFactory _sessionFactory;

        public PatientDal()
        {
            _sessionFactory = NHibernateConfig.CreateSessionFactory("fulbertOrigin");
        }

        internal PatientDal(string databaseName)
        {
            _sessionFactory = NHibernateConfig.CreateSessionFactoryWithDBReset(databaseName);
        }

        public void SaveOrUpdatePatient(Patient patient)
        {
            using (ISession session = _sessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(patient);
                    transaction.Commit();
                }
            }
        }

        public IList<Patient> GetAllPatients()
        {          
            using (ISession session = _sessionFactory.OpenSession())
            {
                return session.QueryOver<Patient>().Fetch(x => x.Appointments).Eager.List();
            }
        }

        public void DeletePatient(Patient patient)
        {
            using (ISession session = _sessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(patient);
                    transaction.Commit();
                }
            }
        }

        public IList<Appointment> GetAllAppointments()
        {
            using (ISession session = _sessionFactory.OpenSession())
            {
                return session.QueryOver<Appointment>().Fetch(x => x.Patient).Eager.List();
            }
        }
    }
}
