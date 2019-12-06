using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc;
using mygarden.Models;

namespace mygarden.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search([FromForm] ProdutorModel produtor) {

            List<ProdutorModel> produtores = new List<ProdutorModel>();
            List<string> checkbox = new List<string>();

            string Frut = produtor.ProdFrut;
            string Leg  = produtor.ProdLeg;
            string Verd = produtor.ProdVerd;
            string Outr = produtor.ProdOutr;
            string Reg = produtor.ProdReg ;
            string chkbox = "";

            string cmd_string = "SELECT ProdId, ProdNom, ProdBairro, ProdEnd, ProdNum, ProdTel, ProdDesc, ProdReg FROM produtor WHERE";

            if (Reg != null)
            {
                checkbox.Add(Reg);
            }
            if (Frut != null) {
                checkbox.Add(Frut);
            }
            if (Leg != null) {
                checkbox.Add(Leg);
            }
            if (Verd != null)
            {
                checkbox.Add(Verd);
            }
            if (Outr != null)
            {
                checkbox.Add(Outr);
            }
            

            foreach(var item in checkbox)
            {
                if (chkbox == "")
                {
                    chkbox += " " + item + " ";
                }
                else
                {
                    chkbox += " AND " + item;
                }
            }

            cmd_string += chkbox;




            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=mygarden;Uid=root;Pwd=root;")) {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(cmd_string, conn)) {
                    using (MySqlDataReader dataReader = cmd.ExecuteReader()) {
                        while (dataReader.Read()) {
                        produtores.Add(new ProdutorModel
                        {
                            ProdId = dataReader.GetInt32(0),
                            ProdNom = dataReader.GetString(1),
                            ProdBairro = dataReader.GetString(2),
                            ProdEnd = dataReader.GetString(3),
                            ProdNum = dataReader.GetString(4),
                            ProdTel = dataReader.GetString(5),
                            ProdDesc = dataReader.GetString(6),
                            ProdReg = dataReader.GetString(7)
                        });
                        }
                    }
                   
                }
                ViewData["result"] = produtores;
            }

            return View("Filtered", produtores);

        }
       
    }
}