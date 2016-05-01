﻿using Fulbert.Commons.Utils;
using System;

namespace Fulbert.Commons.Models.Entities
{
    public class AppointmentEntity : BaseEntity
    {
        public virtual DateTime Date { get; set; }
        public virtual PatientEntity Patient { get; set; }

        internal AppointmentEntity(Guid id)
        {
            Id = id;
        }

        public AppointmentEntity()
        {

        }

        public override string ToString()
        {
            return string.Format(Formatting.S0_1, Id, Date);
        }
    }
}