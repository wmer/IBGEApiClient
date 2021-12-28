using System;
using System.Collections.Generic;
using System.Text;

namespace IBGEApiClient.Models {
    public class Distrito {
        public int id { get; set; }
        public string nome { get; set; }
        public Municipio municipio { get; set; }
    }
}
