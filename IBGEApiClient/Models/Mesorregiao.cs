using System;
using System.Collections.Generic;
using System.Text;

namespace IBGEApiClient.Models {
    public class Mesorregiao {
        public int id { get; set; }
        public string nome { get; set; }
        public Estado UF { get; set; }
    }
}
