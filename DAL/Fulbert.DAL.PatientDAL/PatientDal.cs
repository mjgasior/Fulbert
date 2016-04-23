using System;
using System.Collections.Generic;
using Fulbert.DAL.PatientDAL.Models;
using NHibernate;
using Fulbert.DAL.PatientDAL.Abstract;
using NHibernate.Linq;
using System.Linq;

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
            _sessionFactory = NHibernateConfig.CreateSessionFactory("AAV");
        }

        internal PatientDal(string databaseName)
        {
            _sessionFactory = NHibernateConfig.CreateSessionFactoryWithDBReset(databaseName);
        }

        public void AddPatient(Patient patient)
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

        public IEnumerable<Patient> GetAllPatients()
        {          
            using (ISession session = _sessionFactory.OpenSession())
            {
                return session.Query<Patient>().ToList();
            }
        }

        internal void DeletePatient(Patient patient)
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
    }
}
