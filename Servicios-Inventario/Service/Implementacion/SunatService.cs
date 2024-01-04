using Microsoft.Identity.Client;
using Model_Inventario.Entidades;
using Model_Inventario.InventarioDTO;
using Model_Inventario.SunatValidationDTO;
using Newtonsoft.Json;
using Repositorio_Inventario.UnitOfWork;
using RestSharp;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text.Json.Nodes;
using System.Threading;
using ValidarEstadoDocumentosSunat.Services.Interface;

namespace ValidarEstadoDocumentosSunat.Services.Implementacion
{
    public class SunatService : ISunatService
    {
        private readonly IUnitOfWork unitOfWork;
        private static SemaphoreSlim semaphore = new SemaphoreSlim(4, 4);
        public SunatService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public object ValidacionDelComprobante(DocumentDTO docuemnto, LogeoSunat log, string db)
        {
            try
            {
                var token = LoginSunat(log);
                if (token == null) { return null; }
                var estado = ValidarDocumento(docuemnto, token).data;
                if (estado != null)
                {
                    DocumentosAValidar d = new DocumentosAValidar();
                    d.numeroDocumentoRemision = docuemnto.numRuc;
                    d.tipoDocumento = docuemnto.codComp;
                    d.serieNumero=docuemnto.numeroSerie + "-"+ docuemnto.numero; 
                    d.tipoDocumentoRemision=docuemnto.numRuc.Length==8?"1":"6";
                    d.estadoRuc = estado.data?.estadoRuc == null ? null : estado.data.estadoRuc + " : " + ObtenerDescripcionestadoRuc(estado.data.estadoRuc);
                    d.EstadoCp = estado.data?.estadoCp == null ? null : estado.data.estadoCp + " : " + ObtenerDescripcionEstadoCp(estado.data.estadoCp);
                    d.condDomiRuc = estado.data?.condDomiRuc == null ? null : estado.data.condDomiRuc + " : " + ObtenerDescripcioncondDomiRuc(estado.data.condDomiRuc);
                    d.Observaciones = estado.data?.observaciones == null ? "" : string.Join(" ", estado.data.observaciones);
                    d.Mensaje = estado.message;
                    d.FechaDeConsulta = DateTime.Now;
                    unitOfWork.TDocumentosAValidarRepository.aActualziarData(d, db);
                }
                return estado;
            }
            catch (Exception ex) { throw ex; }
        }
        public dataObtencion ValidarDocumento(DocumentDTO doc, string token)
        {
            try
            {
                dataObtencion daata = new dataObtencion();
                var client = new RestClient("https://api.sunat.gob.pe/v1/contribuyente/");
                var request = new RestRequest($"contribuyentes/{doc.numRuc}/validarcomprobante", Method.Post);
                request.AddHeader("Authorization", $"Bearer {token}");

                request.AddJsonBody(doc);

                request.AddHeader("Content-Type", "application/json");

                var response = client.Execute<ValidationResponseSuant>(request);
                if (response.IsSuccessful)
                {
                    var contenidoRespuesta = response.Data;
                    daata.statusCode= (int)response.StatusCode;
                    daata.data = new ValidationResponseSuant();
                    daata.data = contenidoRespuesta;
                    return daata;
                }
                if (((int)response.StatusCode) == 401)
                {
                    daata.statusCode = (int)response.StatusCode;
                }
                return daata;
            }
            catch (Exception ex) { return null; }
        }
        public string? LoginSunat(LogeoSunat log)
        {
            try
            {
                var client = new RestClient($"https://api-seguridad.sunat.gob.pe/v1/");
                var request = new RestRequest($"clientesextranet/{log.client_id}/oauth2/token", Method.Post);
                request.AddParameter("grant_type", log.grant_type);
                request.AddParameter("scope", log.scope);
                request.AddParameter("client_id", log.client_id);
                request.AddParameter("client_secret", log.client_secret);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

                var response = client.Execute<responseLOGin>(request);
                if (response.IsSuccessful)
                {
                    var contenidoRespuesta = response.Data;
                    return contenidoRespuesta.access_token;
                }
                return null;
            }
            catch (Exception ex) { return null; }
        }

        public async Task<object> ValidacionDelComprobanteASync(LogeoSunat log, string db)
        {
            try
            {
                Console.WriteLine("Inicio");
                var token = LoginSunat(log);
                if (token == null) { return null; }
                List<object> list = new List<object>();
                var datosp = unitOfWork.TDocumentosAValidarRepository.ObtenerTodasLasValidacionesPendientes(db);
                for (var d = 0; d<datosp.Count;d++)
                {
                    //await semaphore.WaitAsync();
                    var docu = new DocumentDTO
                    {
                        codComp = datosp[d].tipoDocumento,
                        fechaEmision = datosp[d].FechaEmision.ToString("dd/MM/yyyy"),
                        monto = datosp[d].MontoTotal<0?(datosp[d].MontoTotal*-1).ToString() : datosp[d].MontoTotal.ToString(),
                        numRuc = datosp[d].numeroDocumentoRemision,
                        numero = ObtenerDatos(datosp[d].serieNumero, 1),
                        numeroSerie = ObtenerDatos(datosp[d].serieNumero, 0)
                    };
                    await Task.Run(() =>
                    {
                        var estat = ValidarDocumento(docu, token);
                        if (estat.statusCode==401)
                        {
                            token = LoginSunat(log);
                            d--;
                        }
                        var estado = estat.data;
                        if (estado != null)
                        {
                            list.Add(estado);
                            datosp[d].estadoRuc = estado.data?.estadoRuc == null ? null : estado.data.estadoRuc + " : " + ObtenerDescripcionestadoRuc(estado.data.estadoRuc);
                            datosp[d].EstadoCp = estado.data?.estadoCp == null ? null : estado.data.estadoCp + " : " + ObtenerDescripcionEstadoCp(estado.data.estadoCp);
                            datosp[d].condDomiRuc = estado.data?.condDomiRuc == null ? null : estado.data.condDomiRuc + " : " + ObtenerDescripcioncondDomiRuc(estado.data.condDomiRuc);
                            datosp[d].Observaciones = estado.data?.observaciones == null ? "" : string.Join(" ", estado.data.observaciones);
                            datosp[d].Mensaje = estado.message;
                            datosp[d].FechaDeConsulta = DateTime.Now;
                            datosp[d].EstadoDoc = estado.success;
                            unitOfWork.TDocumentosAValidarRepository.aActualziarData(datosp[d], db);
                        }
                    });
                    await Task.Delay(1);
                }
                return list;
            }catch(Exception ex) { Console.WriteLine("error: "+ex.Message); throw ex; }
            //finally { semaphore.Release(); }
        }
        public pagination ObtenerDocs(DesdeHastaDTO desdeshasta , string db)
        {
            try
            {
                return unitOfWork.TDocumentosAValidarRepository.obtenerReporte(desdeshasta, db);
            }catch(Exception ex) { throw ex; }
        }


        //metodos itnernos

        public string ObtenerDatos(string dato, int espacio)
        {
            return dato.Split('-')[espacio];
        }
        public static string ObtenerDescripcionestadoRuc(string codigo)
        {
            Dictionary<string, string> estados = new Dictionary<string, string>
        {
            { "00", "ACTIVO" },
            { "01", "BAJA PROVISIONAL" },
            { "02", "BAJA PROV. POR OFICIO" },
            { "03", "SUSPENSION TEMPORAL" },
            { "10", "BAJA DEFINITIVA" },
            { "11", "BAJA DE OFICIO" },
            { "22", "INHABILITADO-VENT.UNICA" }
        };

            if (estados.TryGetValue(codigo, out string descripcion))
            {
                return descripcion;
            }

            // Si el código no está en el diccionario, devuelve un mensaje predeterminado o lanza una excepción, según tus necesidades.
            return "Código no válido";
        }
        public static string ObtenerDescripcionEstadoCp(string codigo)
        {
            Dictionary<string, string> estados = new Dictionary<string, string>
        {
             { "0", "NO EXISTE (Comprobante no informado)" },
            { "1", "ACEPTADO (Comprobante aceptado)" },
            { "2", "ANULADO (Comunicado en una baja)" },
            { "3", "AUTORIZADO (con autorización de imprenta)" },
            { "4", "NO AUTORIZADO (no autorizado por imprenta)" }
            };

            if (estados.TryGetValue(codigo, out string descripcion))
            {
                return descripcion;
            }

            // Si el código no está en el diccionario, devuelve un mensaje predeterminado o lanza una excepción, según tus necesidades.
            return "Código no válido";
        }
        public static string ObtenerDescripcioncondDomiRuc(string codigo)
        {
            Dictionary<string, string> estados = new Dictionary<string, string>
            {
             { "00", "HABIDO" },
            { "09", "PENDIENTE" },
            { "11", "POR VERIFICAR" },
            { "12", "NO HABIDO" },
            { "20", "NO HALLADO" }
            };

            if (estados.TryGetValue(codigo, out string descripcion))
            {
                return descripcion;
            }

            // Si el código no está en el diccionario, devuelve un mensaje predeterminado o lanza una excepción, según tus necesidades.
            return "Código no válido";
        }
    }
}
