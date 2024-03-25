using HelpersLibs.Excel;
using IBGEApiClient.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IBGEApiClient.Helpers {
    public class DDDFileHelper {
        private EnumerableRowCollection<DataRow> _base;

        public DDDFileHelper(string dddsFileLocation) {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var excelHelper = new ExcelHelper();
            _base = excelHelper.GetDataTableFromExcel(dddsFileLocation).AsEnumerable();
        }

        public List<int> GetDDS() {
            var dds = _base.Select(x => int.Parse(x.Field<string>("DDD"))).ToList();
            return dds;
        }

        public List<Cidade> GetDDDsFromUF(string UF) {
            var cidades = new List<Cidade>();
            var dds = _base.Where(x => x.Field<string>("UF") == UF).ToList();

            foreach (var ddd in dds) {
                cidades.Add(new Cidade {
                    cidade = ddd.Field<string>("MUNICiPIO"),
                    ddd = int.Parse(ddd.Field<string>("DDD")),
                    estado = ddd.Field<string>("UF"),
                    codigoIBGE = int.Parse(ddd.Field<string>("Código IBGE"))
                });
            }
            return cidades;
        }


        public Cidade GetDDDFromCidade(string cidade) {
            var ddd = _base.FirstOrDefault(x => x.Field<string>("MUNICiPIO").ToLower() == cidade.ToLower());

            var municipio = new Cidade {
                cidade = ddd.Field<string>("MUNICiPIO"),
                ddd = int.Parse(ddd.Field<string>("DDD")),
                estado = ddd.Field<string>("UF"),
                codigoIBGE = int.Parse(ddd.Field<string>("Código IBGE"))
            };

            return municipio;
        }

        public Cidade GetDDDFromIdCidade(int cidadeId) {
            var ddd = _base.FirstOrDefault(x => x.Field<string>("Código IBGE") == cidadeId.ToString());

            var municipio = new Cidade {
                cidade = ddd.Field<string>("MUNICiPIO"),
                ddd = int.Parse(ddd.Field<string>("DDD")),
                estado = ddd.Field<string>("UF"),
                codigoIBGE = int.Parse(ddd.Field<string>("Código IBGE"))
            };

            return municipio;
        }

    }
}
