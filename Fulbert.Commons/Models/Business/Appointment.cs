﻿using System;

namespace Fulbert.Commons.Models.Business
{
    public class Appointment
    {
        public Guid Id { get; private set; }
        public DateTime Date { get; set; }

        public Appointment(Guid id)
        {
            Id = id;
        }

        public Appointment()
        {

        }
    }
}