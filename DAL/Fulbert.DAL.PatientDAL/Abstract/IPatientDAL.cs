using Fulbert.DAL.PatientDAL.Models;
using System.Collections.Generic;

namespace Fulbert.DAL.PatientDAL.Abstract
{
    public interface IPatientDal
    {
        /// <summary>
        /// Allows to add a patient to the database.
        /// </summary>
        /// <param name="patient">Patient data model to be added to database.</param>
        void AddPatient(Patient patient);

        /// <summary>
        /// Get all patients from database.
        /// </summary>
        /// <returns>List of available patients in database.</returns>
        IList<Patient> GetAllPatients();
    }
}
