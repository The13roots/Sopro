﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Uebungsprojekt.Models
{
    /// <summary>
    /// Model representing one specific booking 
    /// </summary>
    public class Booking
    {
        /// <summary>Current State of Charge</summary>
        [Required(ErrorMessage = "Bitte geben sie den Ladezustand ihres Autos an.")]
        [Range(0, 100)]
        public int Charge { get; set; }

        /// <summary>Distance needed before next charging</summary>
        [Required(ErrorMessage = "Bitte geben sie die benötigte Fahrtstrecke an.")]
        [Range(1, 1000)]
        public int Needed_distance { get; set; }

        /// <summary>Preferred start datetime</summary>
        [Required(ErrorMessage = "Bitte geben sie die Startzeit des Ladens an.")]
        public DateTime Start_time { get; set; }

        /// <summary>Preferred end datetime</summary>
        [Required(ErrorMessage = "Bitte geben sie die Endzeit des Ladens an.")]
        public DateTime End_time { get; set; }

        /// <summary>
        /// Empty constructor of booking model
        /// </summary>
        public Booking()
        { 

        }

        /// <summary> Connector Type</summary>
        [Required(ErrorMessage = "Bitte geben sie einen Steckertyp an.")]
        public Steckertyp s_type { get; set; }


        /// <summary> Enumeration of all available Connector Types</summary>
        public enum Steckertyp
        {
            Schuko_Socket,
            Type_1_Plug,
            Type_2_Plug,
            CHAdeMO_Plug,
            Tesla_Supercharger,
            CCS_Combo_2_Plug
        }
    }
}