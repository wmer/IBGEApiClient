﻿using IBGEApiClient.Helpers;
using IBGEApiClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBGEApiClient {
    public class IBGEApi {
        private readonly ApiConsumingHelper _api;
        private List<Estado> _estados;
        private List<int> _dds;
        private readonly Dictionary<string, List<Municipio>> _estadoCidade;
        private readonly Dictionary<string, List<Distrito>> _cidadeeDistrito;

        public IBGEApi() {
            _api = new ApiConsumingHelper("https://servicodados.ibge.gov.br/api/v1/localidades");
            _dds = new List<int>();
            _estadoCidade = new Dictionary<string, List<Municipio>>();
            _cidadeeDistrito = new Dictionary<string, List<Distrito>>();
        }
        
        public async Task<List<Estado>> GetEstadosAsync() {
            if (_estados == null) {
                _estados = new List<Estado>();
                var result = await _api.GetAssync<List<Estado>>($"/estados");

                if (result.result != null) {
                    _estados = result.result;
                }
            }

            return _estados;
        }

        public async Task<List<Municipio>> CidadesPorEstadoAsync(string uf) {
            var cidades = new List<Municipio>();

            if (_estadoCidade.ContainsKey(uf)) {
                cidades = _estadoCidade[uf];
            } else {
                var result = await _api.GetAssync<List<Municipio>>($"/estados/{uf}/municipios");

                if (result.result != null) {
                    cidades = result.result;
                    _estadoCidade[uf] = cidades;
                }
            }

            return cidades;
        }

        public async Task<List<Distrito>> DistritoPorMunicipioAsync(string municipio) {
            var cidades = new List<Distrito>();

            if (_cidadeeDistrito.ContainsKey(municipio)) {
                cidades = _cidadeeDistrito[municipio];
            } else {
                var result = await _api.GetAssync<List<Distrito>>($"/municipios/{municipio}/distritos");

                if (result.result != null) {
                    cidades = result.result;
                    _cidadeeDistrito[municipio] = cidades;
                }
            }

            return cidades;
        }

        public async Task<Distrito> DistritoPorIdentificadorAsync(string id) {
            var distrito = new Distrito();
            var result = await _api.GetAssync<List<Distrito>>($"/distritos/{id}");

            if (result.result != null && result.result.Count() > 0) {
                distrito = result.result.FirstOrDefault();
            }

            return distrito;
        }

        public async Task<Municipio> MunicipioPorIdentificadorAsync(string id) {
            var municipio = new Municipio();
            var result = await _api.GetAssync<Municipio>($"/municipios/{id}");

            if (result.result != null) {
                municipio = result.result;
            }

            return municipio;
        }

        public async Task<Estado> EstadoPorIdentificadorAsync(string id) {
            var municipio = new Estado();
            var result = await _api.GetAssync<Estado>($"/estados/{id}");

            if (result.result != null) {
                municipio = result.result;
            }

            return municipio;
        }
    }
}
