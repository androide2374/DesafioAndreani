﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Api.Geo.Models.Tables
{
    public class PedidoGeo
    {
        public PedidoGeo()
        {
            
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Calle { get; set; }
        public int Numero { get; set; }
        public string Ciudad { get; set; }
        public string CP { get; set; }
        public string Provincia { get; set; }
        public string Pais { get; set; }
    }
}
