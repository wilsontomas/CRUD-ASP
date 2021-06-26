using CRUDASP.Model;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDASP.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private SqlConnection DB;
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            this.DB = new SqlConnection("Data Source=DESKTOP-V32QJTJ\\SQLEXPRESS;Initial Catalog=CRUD;Integrated Security=True");
        }


        public PersonaProvinciaDTO PersonasProvinciasDto = new PersonaProvinciaDTO();
        public void OnGet()
        {
            //cargamos la informacion de la base de datos
            List<Provincia> listaProvincias = this.DB.Query<Provincia>("ObtenerProvincias", null, commandType: CommandType.StoredProcedure).ToList();
            List<PersonaProvincia> listaPersonas = this.DB.Query<PersonaProvincia>("ObtenerPersonas", null, commandType: CommandType.StoredProcedure).ToList();

            this.PersonasProvinciasDto.Provincias =listaProvincias;
            this.PersonasProvinciasDto.Personas =listaPersonas;
           
        }


        public void OnPostAgregar(Persona persona) 
        {
            //Insertamos la nueva persona
            var parametros = new { @nombre=persona.Nombre, @apellido=persona.Apellido, @Idprovincia=persona.IdProvincia };
            this.DB.Query("InsertarPersona", parametros,commandType: CommandType.StoredProcedure);

            //cargamos la informacion de la base de datos
            List<Provincia> listaProvincias = this.DB.Query<Provincia>("ObtenerProvincias", null, commandType: CommandType.StoredProcedure).ToList();
            List<PersonaProvincia> listaPersonas = this.DB.Query<PersonaProvincia>("ObtenerPersonas", null, commandType: CommandType.StoredProcedure).ToList();

            this.PersonasProvinciasDto.Provincias = listaProvincias;
            this.PersonasProvinciasDto.Personas = listaPersonas;

        }

        public void OnPostEliminar(int Eliminar) {
            
            var parametros = new { @IdPersona=Eliminar };
            this.DB.Query("EliminarPersona", parametros, commandType: CommandType.StoredProcedure);

            //cargamos la informacion de la base de datos
            List<Provincia> listaProvincias = this.DB.Query<Provincia>("ObtenerProvincias", null, commandType: CommandType.StoredProcedure).ToList();
            List<PersonaProvincia> listaPersonas = this.DB.Query<PersonaProvincia>("ObtenerPersonas", null, commandType: CommandType.StoredProcedure).ToList();

            this.PersonasProvinciasDto.Provincias = listaProvincias;
            this.PersonasProvinciasDto.Personas = listaPersonas;
        }
    }
}
