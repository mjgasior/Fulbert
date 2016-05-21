using System.Collections.Generic;
using System;
using Fulbert.DAL.RepositoryModels.Models;

namespace Fulbert.DAL.RepositoryModels.Abstract
{
    public interface IPatientDal
    {
        /// <summary>
        /// Allows to add a patient to the database.
        /// </summary>
        /// <param name="patient">Patient data model to be added to database.</param>
        void SaveOrUpdatePatient(PatientEntity patient);

        /// <summary>
        /// Get all patients from database.
        /// </summary>
        /// <returns>List of available patients in database.</returns>
        IList<PatientEntity> GetAllPatients();

        /// <summary>
        /// Returns the selected patient database model instance by his database id.
        /// </summary>
        /// <param name="patientId">Selected patient's database key (id).</param>
        /// <returns>Full database patient entity.</returns>
        PatientEntity GetPatientById(Guid patientId);

        /// <summary>
        /// Get all appointments from the database.
        /// </summary>
        /// <returns>List of every appointment of every patient in the database.</returns>
        IList<AppointmentEntity> GetAllAppointments();

        /// <summary>
        /// Delete selected patient from database.
        /// </summary>
        /// <param name="patient">Patient to be deleted with his appointments.</param>
        void DeletePatient(PatientEntity patient);
    }
}
