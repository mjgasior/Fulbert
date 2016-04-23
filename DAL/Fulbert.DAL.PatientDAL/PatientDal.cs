using System.Collections.Generic;
using NHibernate;
using Fulbert.Commons.Abstract.DAL;
using Fulbert.Commons.Models.Entities;
using System;

namespace Fulbert.DAL.PatientDAL
{
    public class PatientDal : IPatientDal
    {
        // readme:
        // http://www.codeproject.com/Articles/13390/NHibernate-Best-Practices-with-ASP-NET-nd-Ed
        // http://nhibernate.info/blog/2008/08/31/data-access-with-nhibernate.html
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

        public Patient GetPatientById(Guid patientId)
        {
            using (ISession session = _sessionFactory.OpenSession())
            {
                //return session.QueryOver<Patient>().Fetch(x => x.Appointments).Eager
                //    .Where(k => k.Id == patientId).List().FirstOrDefault();
                var entity = session.Get<Patient>(patientId);
                NHibernateUtil.Initialize(entity.Appointments);
                return entity;
            }
        }
    }
}
