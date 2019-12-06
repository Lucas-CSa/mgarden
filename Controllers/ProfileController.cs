using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using mygarden.Models;
using Microsoft.AspNetCore.Mvc;

namespace mygarden.Controllers
{
    public class ProfileController : Controller
    {

        public IActionResult GoIndex() {
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Indique()
        {
            return View();
        }

        [HttpPost]  
        public IActionResult CreateProd([FromForm] ProdutorModel produtor) {
            int prodReg;
            int prodFrut;
            int prodLeg;
            int prodVerd;
            int prodOutr;

            if (produtor.ProdReg != null) {
                prodReg = Int32.Parse(produtor.ProdReg);
            }
            if (produtor.ProdFrut != null)
            {
                prodFrut = 1;
            }
            else {
                prodFrut = 0;
            }
            if (produtor.ProdLeg != null) {
                prodLeg = 1;
            }
            else
            {
                prodLeg = 0;
            }
            if (produtor.ProdVerd != null)
            {
                prodVerd = 1;
            }
            else {
                prodVerd = 0;
            }
            if (produtor.ProdOutr != null)
            {
                prodOutr = 1;
            }
            else {
                prodOutr = 0;
            }
            
           
            
            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=mygarden;Uid=root;Pwd=root;"))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("INSERT INTO produtor (ProdNom, ProdBairro, ProdEnd, ProdNum, ProdReg, ProdTel, ProdDesc, ProdFrut, ProdLeg, ProdVerd, ProdOutr) VALUES (@ProdNom, @ProdBairro, @ProdEnd, @ProdNum, @ProdReg, @ProdTel, @ProdDesc, @ProdFrut, @ProdLeg, @ProdVerd, @ProdOutr)", conn))
                {
                    cmd.Parameters.AddWithValue("@ProdNom", produtor.ProdNom);
                    cmd.Parameters.AddWithValue("@ProdBairro", produtor.ProdBairro);
                    cmd.Parameters.AddWithValue("@ProdEnd", produtor.ProdEnd);
                    cmd.Parameters.AddWithValue("@ProdNum", produtor.ProdNum);
                    cmd.Parameters.AddWithValue("@ProdTel", produtor.ProdTel);
                    cmd.Parameters.AddWithValue("@ProdReg", produtor.ProdReg);
                    cmd.Parameters.AddWithValue("@ProdDesc", produtor.ProdDesc);
                    cmd.Parameters.AddWithValue("@ProdFrut", prodFrut);
                    cmd.Parameters.AddWithValue("@ProdLeg", prodLeg);
                    cmd.Parameters.AddWithValue("@ProdVerd", prodVerd);
                    cmd.Parameters.AddWithValue("@ProdOutr", prodOutr);

                    cmd.ExecuteNonQuery();
                }
            }

            ViewBag.Mensagem = "Sucesso!";

            return View("Indique");
        }

        public IActionResult perfil(int id)
        {
            ProdutorModel produtor;

            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=mygarden;Uid=root;Pwd=root;"))
            {
                conn.Open();
                using(MySqlCommand cmd = new MySqlCommand("SELECT ProdNom, ProdEnd, ProdBairro, ProdNum, ProdReg, ProdTel, ProdDesc, ProdFrut, ProdVerd, ProdLeg, ProdOutr FROM produtor WHERE ProdId = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using(MySqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        if (!dataReader.Read())
                        {
                            throw new Exception("Produtor não encontrado");
                        }
                        produtor = new ProdutorModel();
                        produtor.ProdId = id;
                        produtor.ProdNom = dataReader.GetString(0);
                        produtor.ProdEnd = dataReader.GetString(1);
                        produtor.ProdBairro = dataReader.GetString(2);
                        produtor.ProdNum = dataReader.GetString(3);
                        produtor.ProdReg = dataReader.GetString(4);
                        produtor.ProdTel = dataReader.GetString(5);
                        produtor.ProdDesc = dataReader.GetString(6);
                        produtor.ProdFrut = dataReader.GetString(7);
                        produtor.ProdVerd = dataReader.GetString(8);
                        produtor.ProdLeg = dataReader.GetString(9);
                        produtor.ProdOutr = dataReader.GetString(10);
                    }
                }
            }
            return View(produtor);
        }

    }
}