using Fulbert.DAL.PatientDAL.Models;
using NHibernate;

namespace Fulbert.DAL.PatientDAL
{
    public class PatientDal
    {
        // readme:
        // http://www.codeproject.com/Articles/13390/NHibernate-Best-Practices-with-ASP-NET-nd-Ed
        //
        private readonly ISessionFactory _sessionFactory;

        public PatientDal()
        {
            _sessionFactory = NHibernateConfig.CreateSessionFactory();
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
    }
}
