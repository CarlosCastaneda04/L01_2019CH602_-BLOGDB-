using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarlosCastaneda_PrimerLab_WebApi.Models;

namespace CarlosCastaneda_PrimerLab_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class calificacionesController : ControllerBase
    {
        private readonly BlogContext _calificacionesContexto;

        public calificacionesController(BlogContext calificacionesContex)
        {
            _calificacionesContexto = calificacionesContex;

        }


        ///GET
        [HttpGet]
        [Route("All Ratings")]

        public IActionResult allratings()
        {
            List<calificaciones> list_califi = (from e in _calificacionesContexto.calificaciones
                                                select e).ToList();
            if (list_califi.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(list_califi);

            }

        }

        //add ratings
        [HttpPost]
        [Route("Add Ratings")]
        public IActionResult save_grade([FromBody] calificaciones newrating)
        {
            try
            {
                _calificacionesContexto.calificaciones.Add(newrating);
                _calificacionesContexto.SaveChanges();
                return Ok("se agrego correctamente\n" + newrating);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        //update
        [HttpPut]
        [Route("Update Ratings/{id}")]
        public IActionResult update_ratings(int id, [FromBody] calificaciones rating_update)
        {

            calificaciones? rating_select = (from e in _calificacionesContexto.calificaciones
                                            where e.publicacionId == id
                                            select e).FirstOrDefault();

            if (rating_select == null)
            {
                return NotFound();
            }
            else
            {
                rating_select.calificacion = rating_update.calificacion;
                rating_select.publicacionId = rating_update.publicacionId;
                rating_select.usuarioId = rating_update.usuarioId;


                _calificacionesContexto.Entry(rating_select).State = EntityState.Modified;
                _calificacionesContexto.SaveChanges();
                return Ok(rating_update);


            }

        }


        //Delete
        [HttpDelete]
        [Route("Delete Ratings/{id}")]
        public IActionResult delete_calificacion(int id)
        {
            calificaciones? grade_select = (from e in _calificacionesContexto.calificaciones
                                            where e.calificacionId == id
                                            select e).FirstOrDefault();

            if (grade_select == null)
            {
                return NotFound();
            }
            else
            {
   
                _calificacionesContexto.calificaciones.Attach(grade_select);
                _calificacionesContexto.calificaciones.Remove(grade_select);
                _calificacionesContexto.SaveChanges();
                return Ok("Se a eliminado el calificación exitosamente \n");


            }

        }

        //search for publicacionId
        [HttpGet]
        [Route("Search/{id}")]
        public IActionResult search_ref(int id)
        {

            calificaciones? grade_select = (from e in _calificacionesContexto.calificaciones
                                            where e.publicacionId == id
                                            select e).FirstOrDefault();

            if (grade_select == null)
            {
                return NotFound();
            }
            else
            {
                return Ok("Busqueda realizada con exito\n " + "Publicacion ID: " + grade_select.publicacionId +
                    "\nID del la calificacio: " + grade_select.calificacionId + "\n Calificación: " + grade_select.calificacion);
            }
        }

    }
}
