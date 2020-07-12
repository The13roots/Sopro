﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Uebungsprojekt.Models;

namespace Uebungsprojekt.Models
{
    /// <summary>
    /// Model representing one specific booking 
    /// </summary>
    public class Booking
    {
        public int id { get; set; }

        /// <summary>Current State of Charge</summary>
        [Required(ErrorMessage = "Please specify the current state of charge.")]
        [Range(0, 100)]
        public int start_state_of_charge { get; set; }

        /// <summary>Distance needed before next charging</summary>
        [Required(ErrorMessage = "Please specify the distance needed.")]
        [Range(1, 100)]
        public int target_state_of_charge { get; set; }

        /// <summary>Preferred start datetime</summary>
        [Required(ErrorMessage = "Please specify the wanted start time.")]
        public DateTime start_time { get; set; }

        /// <summary>Preferred end datetime</summary>
        [Required(ErrorMessage = "Please specify the wanted end time.")]
        public DateTime end_time { get; set; }

        /// <summary>Indicates if the booking is allready accepted or still just a booking wish</summary>
        public Boolean accepted { get; set; }

        // TODO: Need custom JSONConverter for Vehicle etc.
        /// <summary>Connector type for charging</summary>
        [Required(ErrorMessage = "Please select at least one of the plug types.")]
        public Vehicle vehicle { get; set; }
        
        public User user { get; set; }

        // Frage: muss hier noch eine Variable für ChargingColumn rein?
        public ChargingColumn charging_column { get; set; }

        public Location location { get; set; }

        public List<ConnectorType> connectorTypes { get; set; }

        /// <summary>
        /// Empty constructor of booking model
        /// </summary>
        public Booking()
        { 

        }
        public void Accept()
        {
            accepted = true;
        }
    }
}
