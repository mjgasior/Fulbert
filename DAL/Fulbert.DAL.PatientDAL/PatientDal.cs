using System.Collections.Generic;
using NHibernate;
using Fulbert.Commons.Abstract.DAL;
using Fulbert.Commons.Models.Entities;
using System;
using NHibernate.Transform;

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
            _sessionFactory = NHibernateConfig.CreateSessionFactory("fulbertTests");
        }

        internal PatientDal(string databaseName)
        {
            _sessionFactory = NHibernateConfig.CreateSessionFactoryWithDBReset(databaseName);
        }

        public void SaveOrUpdatePatient(PatientEntity patient)
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

        public IList<PatientEntity> GetAllPatients()
        {          
            using (ISession session = _sessionFactory.OpenSession())
            {
                return session.QueryOver<PatientEntity>().Fetch(x => x.Appointments).Eager.TransformUsing(Transformers.DistinctRootEntity).List();
            }
        }

        public void DeletePatient(PatientEntity patient)
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

        public IList<AppointmentEntity> GetAllAppointments()
        {
            using (ISession session = _sessionFactory.OpenSession())
            {
                return session.QueryOver<AppointmentEntity>().Fetch(x => x.Patient).Eager.List();
            }
        }

        public PatientEntity GetPatientById(Guid patientId)
        {
            using (ISession session = _sessionFactory.OpenSession())
            {
                var entity = session.Get<PatientEntity>(patientId);
                NHibernateUtil.Initialize(entity.Appointments);
                return entity;
            }
        }
    }
}
