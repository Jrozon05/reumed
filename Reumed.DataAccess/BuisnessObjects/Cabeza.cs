﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reumed.DataAccess.BusinessObjects
{
    public class Cabeza : RecordBase<Cabeza>
    {
        #region Constructor
        public Cabeza()
        {
            SetInitialState();
        }
        #endregion

        #region Properties

        [JsonProperty("cabezaid")]
        public int CabezaId { get; set; }

        [JsonProperty("detalle")]
        public string Detalle { get; set; }

        [JsonProperty("categoria")]
        public string Categoria { get; set; }

        [JsonProperty("fisico")]
        public Fisico Fisico { get; set; }
        #endregion
    }
}
