using System;
using System.Collections.Generic;
using System.Text;

namespace IBGEApiClient.Models {
    public class Municipio {
        public int id { get; set; }
        public string nome { get; set; }
        public Microrregiao microrregiao { get; set; }
    }
}
