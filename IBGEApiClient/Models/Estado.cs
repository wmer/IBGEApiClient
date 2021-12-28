using System;
using System.Collections.Generic;
using System.Text;

namespace IBGEApiClient.Models {
    public class Estado {
        public int id { get; set; }
        public string sigla { get; set; }
        public string nome { get; set; }
        public Regiao regiao { get; set; }
    }
}
