﻿using System.ComponentModel.DataAnnotations;
// using Data\Bank_DB\Bank_Tabelle-w-PK.json
using System.Text.Json.Serialization;

namespace Bank_Database_MVC.Models.Bank_DB
{
    public class Bank_Tabelle
    {
        public string BLZ { get; set; }
        public int? Merkmal { get; set; }
        public string Bezeichnung { get; set; }
        public int? PLZ { get; set; }
        public string Ort { get; set; }
        public string Kurzbezeichnung { get; set; }
        public int? PAN { get; set; }
        public string BIC { get; set; }
        public string Pruefzifferberechnungsmethode { get; set; }
        public int? Datensatznummer { get; set; }
        public string Aenderungskennzeichen { get; set; }
        public int? Bankleitzahlloeschung { get; set; }
        public string NachfolgeBLZ { get; set; }

        // using Data\Bank_DB\Bank_Tabelle-w-PK.json
        //[Key][JsonPropertyName("ID")]
        //public int Key { get; set; }

        // using Data\Bank_DB\Bank_Tabelle-w-o-PK.json
        [Key]
        public int Key { get; set; }
    }
}
