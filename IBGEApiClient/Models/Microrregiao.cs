using System;
using System.Collections.Generic;
using System.Text;

namespace IBGEApiClient.Models {
    public class Microrregiao {
        public int id { get; set; }
        public string nome { get; set; }
        public Mesorregiao mesorregiao { get; set; }
    }
}
