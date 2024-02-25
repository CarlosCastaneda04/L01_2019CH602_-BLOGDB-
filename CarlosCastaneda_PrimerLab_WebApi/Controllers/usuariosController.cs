using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarlosCastaneda_PrimerLab_WebApi.Models;

namespace CarlosCastaneda_PrimerLab_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuariosController : ControllerBase
    {
        private readonly BlogContext _userContext;

        public usuariosController(BlogContext userContext)
        {
            _userContext = userContext;

        }

        //GET
        [HttpGet]
        [Route("All Users")]

        public IActionResult allusers()
        {
            List<usuarios> listUsers = (from e in _userContext.usuarios
                                             select e).ToList();
            if (listUsers.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(listUsers);

            }

        }
        //add user
        [HttpPost]
        [Route("Add USer")]
        public IActionResult save_usuario([FromBody] usuarios newuser)
        {
            try
            {
                _userContext.usuarios.Add(newuser);
                _userContext.SaveChanges();
                return Ok("se agrego correctamente\n" + newuser);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }

        //update a user
        [HttpPut]
        [Route("Update User/{id}")]
        public IActionResult update_user(int id, [FromBody] usuarios userUpdate)
        {

            usuarios? userselect = (from e in _userContext.usuarios
                                       where e.usuarioId == id
                                       select e).FirstOrDefault();


            if (userselect == null)
            {
                return NotFound();
            }
            else
            {
                userselect.nombreUsuario = userUpdate.nombreUsuario;
                userselect.nombre = userUpdate.nombre;
                userselect.apellido = userUpdate.apellido;



                _userContext.Entry(userselect).State = EntityState.Modified;
                _userContext.SaveChanges();
                return Ok(userUpdate);


            }

        }


        //Delete User
        [HttpDelete]
        [Route("Delete User/{id}")]
        public IActionResult delete_user(int id)
        {
            usuarios? userselect = (from e in _userContext.usuarios
                                       where e.usuarioId == id
                                       select e).FirstOrDefault();
            if (userselect == null)
            {
                return NotFound();
            }
            else
            {
                _userContext.usuarios.Attach(userselect);
                _userContext.usuarios.Remove(userselect);
                _userContext.SaveChanges();
                return Ok(userselect);


            }

        }

        //filter user
        [HttpGet]
        [Route("Filter User for Username/{nombreusuario}")]
        public IActionResult search_ref(string nombreusuario)
        {

            usuarios? usuarioselect = (from e in _userContext.usuarios
                                       where e.nombreUsuario == nombreusuario
                                       select e).FirstOrDefault();

            if (usuarioselect == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(usuarioselect);
            }
        }
        //filter user
        [HttpGet]
        [Route("Filter User for Name/{nombre}")]
        public IActionResult search_name(string nombre)
        {

            usuarios? usuarioselect = (from e in _userContext.usuarios
                                       where e.nombre == nombre
                                       select e).FirstOrDefault();

            if (usuarioselect == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(usuarioselect);
            }
        }

        //filter user
        [HttpGet]
        [Route("Filter User for Lastname/{apellido}")]
        public IActionResult search_apellido(string apellido)
        {

            usuarios? usuarioselect = (from e in _userContext.usuarios
                                       where e.apellido == apellido
                                       select e).FirstOrDefault();

            if (usuarioselect == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(usuarioselect);
            }
        }
    }
}
