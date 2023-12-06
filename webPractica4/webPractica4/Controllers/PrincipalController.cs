using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using webPractica4.Models;

namespace webPractica4.Controllers
{
    public class PrincipalController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44374/api");
        private readonly HttpClient _client;
        
        public PrincipalController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress; 
        }
        
        [HttpGet]
        public ActionResult Index()
        {
            List<PrincipalModel> list = new List<PrincipalModel>();
            HttpResponseMessage response = _client.GetAsync(baseAddress + "/Principal").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<PrincipalModel>>(data);

                list = list.OrderBy(item => item.estado != "Pendiente").ToList();
            }
            return View(list);
        }

        [HttpGet]

        public ActionResult Edit(int id)
        {
            try {
                PrincipalModel model = new PrincipalModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Principal/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    model = JsonConvert.DeserializeObject<PrincipalModel>(data);
                }

                return View(model);

            } catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();

            }

            
        }

        [HttpPost]
        public ActionResult Edit(PrincipalModel model)
        {
            try
            {
                string abonoValue = Request.Form["abono"];

                if (decimal.TryParse(abonoValue, out decimal abono))
                {
                    if (abono <= model.saldo)
                    {
                        model.saldo = model.saldo - abono;
                        if (model.saldo == 0)
                        {
                            model.estado = "Cancelado";
                        }
                    }
                    
                    
                }
                else
                {
                    // El valor no es un número válido
                    TempData["errorMessage"] = "El valor de abono no es válido.";
                }

                AbonoModel abonoObj = new AbonoModel();
                abonoObj.codCompra = model.codCompra;
                abonoObj.abono = abono;

                string dataAbono = JsonConvert.SerializeObject(abonoObj);
                StringContent contentAbono = new StringContent(dataAbono, Encoding.UTF8, "application/json");
                HttpResponseMessage response2 = _client.PostAsync(_client.BaseAddress + "/Abonos", contentAbono).Result;

                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/Principal/" + model.codCompra, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Abono Realizado";
                    return RedirectToAction("Index");
                }


                return View();

            } catch(Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            
        }
    }
}