﻿using System;

namespace Fulbert.Commons.Models.Entities
{
    public class Appointment
    {
        public virtual Guid Id { get; protected set; }
        public virtual DateTime Date { get; set; }
        public virtual Patient Patient { get; set; }
    }
}