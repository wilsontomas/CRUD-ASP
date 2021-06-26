using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CRUDASP.Model;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CRUDASP.Pages
{
    public class EditarModel : PageModel
    {
         
        public List<Provincia> provincias = new List<Provincia>();
        public Persona personaBuscada = new Persona();
        private SqlConnection DB;
        public EditarModel() { 
        this.DB = new SqlConnection("Data Source=DESKTOP-V32QJTJ\\SQLEXPRESS;Initial Catalog=CRUD;Integrated Security=True");
      
        }
        public void OnGet(int Editar)
        {
            var parametros = new { @IdPersona=Editar };

            this.personaBuscada = this.DB.QueryFirst<Persona>("ObtenerPersonaPorId", parametros, commandType: CommandType.StoredProcedure);
            this.provincias = this.DB.Query<Provincia>("ObtenerProvincias", null, commandType: CommandType.StoredProcedure).ToList();
           
        }

        public ActionResult OnPostEditar(Persona persona) {
            var parametros = new { @IdPersona = persona.PersonAId,@nombre=persona.Nombre, @Apellido = persona.Apellido, @provincia=persona.IdProvincia };
            this.DB.Query("EditarPersona", parametros, commandType: CommandType.StoredProcedure);
            
            return RedirectToPage("Index");
        }
       
    }
}
