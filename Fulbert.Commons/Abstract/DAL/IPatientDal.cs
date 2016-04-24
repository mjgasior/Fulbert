using Fulbert.Commons.Models.Entities;
using System.Collections.Generic;
using System;

namespace Fulbert.Commons.Abstract.DAL
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
