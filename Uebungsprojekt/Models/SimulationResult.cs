﻿using System;
using System.Collections.Generic;

namespace Uebungsprojekt.Models
{
    public class SimulationResult
    {
        public int id { get; set; }
        public int config_id { get; set; }
        public int infrastructure_id { get; set; }
        public List<double> total_workload { get; set; }
        public List<int> num_generated_bookings { get; set; }
        public List<int> num_unsatisfiable_bookings { get; set; }
        public bool done { get; set; }
        public List<Tuple<Booking, Booking>> unsatisfiable_bookings_with_suggestion { get; set; }
        
        public SimulationResult()
        {
            total_workload = new List<double>();
            num_generated_bookings = new List<int>();
            num_unsatisfiable_bookings = new List<int>();
            
            unsatisfiable_bookings_with_suggestion = new List<Tuple<Booking, Booking>>();
        }
    }
}