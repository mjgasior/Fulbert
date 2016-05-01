using Fulbert.Commons.Models.Entities;
using Fulbert.Tests.Common;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;

namespace Fulbert.DAL.PatientDAL.Tests
{
    public class DatabaseTools
    {
        #region Methods
        public static IList<AppointmentEntity> GetAllAppointments()
        {
            ISessionFactory sessionForTests = NHibernateConfig.CreateSessionFactory(Database.TEST_DB_NAME);
            using (ISession session = sessionForTests.OpenSession())
            {
                return session.QueryOver<AppointmentEntity>().List();
            }
        }

        public static IList<PatientEntity> GetPatientFromDatabase(string firstName, string lastName)
        {
            ISessionFactory sessionForTests = NHibernateConfig.CreateSessionFactory(Database.TEST_DB_NAME);
            using (ISession session = sessionForTests.OpenSession())
            {
                //return session.QueryOver<PatientEntity>().Where(k => k.FirstName == firstName && k.LastName == lastName).Fetch(x => x.Appointments).Eager.List();
                //return session.QueryOver<PatientEntity>()
                //  .WhereRestrictionOn(x => x.FirstName).IsLike(firstName)
                // .Fetch(x => x.Appointments).Eager.List();
                return session.CreateCriteria<PatientEntity>()
                    .Add(Expression.Eq("FirstName", firstName))
                    .Add(Expression.Eq("LastName", lastName))
                    .SetFetchMode("Appointments", FetchMode.Eager)
                    .List<PatientEntity>();
            }
        }

        public static void AddPatientToDatabase(string firstName, string lastName, params DateTime[] appointmentDate)
        {
            PatientEntity patient = new PatientEntity
            {
                FirstName = firstName,
                LastName = lastName
            };

            foreach (DateTime item in appointmentDate)
            {
                var appointment = new AppointmentEntity
                {
                    Date = item
                };
                patient.AddAppointment(appointment);
            }

            ISessionFactory sessionForTests = NHibernateConfig.CreateSessionFactory(Database.TEST_DB_NAME);
            using (ISession session = sessionForTests.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(patient);
                    transaction.Commit();
                }
            }
        }
        #endregion Methods
    }
}
