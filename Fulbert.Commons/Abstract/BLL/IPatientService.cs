﻿using System;
using Fulbert.Commons.Models.Business;

namespace Fulbert.Commons.Abstract.BLL
{
    public interface IPatientService
    {
        void AddNewPatient(Patient patient);
        void AddAppointmentToPatient(Guid patientId, Appointment appointment);
        void UpdatePatient(Patient patient);
    }
}
