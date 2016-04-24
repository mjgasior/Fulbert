using Fulbert.Commons.Models.Entities;
using Fulbert.Tests.Common;
using NHibernate;
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
                return session.QueryOver<PatientEntity>().Fetch(x => x.Appointments).Eager
                    .Where(k => k.FirstName == firstName && k.LastName == lastName).List();
            }
        }

        public static void AddPatientToDatabase(string firstName, string lastName, DateTime appointmentDate)
        {
            var appointment = new AppointmentEntity
            {
                Date = appointmentDate
            };

            PatientEntity patient = new PatientEntity
            {
                FirstName = firstName,
                LastName = lastName
            };
            patient.AddAppointment(appointment);

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
