﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLAPI;
namespace BO
{
    public class BUS
    {

        public int LicenseNum { get; set; }
        public DateTime FromDate { get; set; } = new DateTime(1, 1, 1);
        public DateTime lastime { get; set; } //last treatment
        public double TotalTrip { get; set; }
        public double ckm { get; set; } // km from last treatment
        public double FuelRemain { get; set; } = 1200;
        BusStatus status { set; get; }
    }
}
