﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraAcmeApp.Models
{
    public class Carro
    {
        public int CarroId { get; set; }

        public string Modelo { get; set; }

        public string Marca { get; set; }

        public int PrecoDiaria { get; set; }
    }
}