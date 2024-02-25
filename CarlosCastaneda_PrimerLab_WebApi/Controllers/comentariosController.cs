using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarlosCastaneda_PrimerLab_WebApi.Models;

namespace CarlosCastaneda_PrimerLab_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class comentariosController : ControllerBase
    {
        private readonly BlogContext _commentContext;

        public comentariosController(BlogContext commentContexto)
        {
            _commentContext = commentContexto;

        }


        ///Get
        [HttpGet]
        [Route("All Comments")]

        public IActionResult allcomments()
        {
            List<comments> list_comment = (from e in _commentContext.comentarios
                                              select e).ToList();
            if (list_comment.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(list_comment);

            }

        }
        //Peticion para agregar un equipo
        [HttpPost]
        [Route("Add Comment")]
        public IActionResult save_coments([FromBody] comments newcomment)
        {
            try
            {
                _commentContext.comentarios.Add(newcomment);
                _commentContext.SaveChanges();
                return Ok("creado correctamente\n" + newcomment);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        //update
        [HttpPut]
        [Route("Update Comment/{id}")]
        public IActionResult update_comments(int id, [FromBody] comments update_comment)
        {

            comments? comment_select = (from e in _commentContext.comentarios
                                          where e.cometarioId == id
                                          select e).FirstOrDefault();


            if (comment_select == null)
            {
                return NotFound();
            }
            else
            {
                comment_select.comentario = update_comment.comentario;
                comment_select.publicacionId = update_comment.publicacionId;
                comment_select.usuarioId = update_comment.usuarioId;

                //Mark the modified record and send it modified to the batabase :D

                _commentContext.Entry(comment_select).State = EntityState.Modified;
                _commentContext.SaveChanges();
                return Ok(update_comment);
            }

        }


        //Delete
        [HttpDelete]
        [Route("Delete Comment/{id}")]
        public IActionResult delete_comentario(int id)
        {
            comments? comment_select = (from e in _commentContext.comentarios
                                           where e.cometarioId == id
                                           select e).FirstOrDefault();

            if (comment_select == null)
            {
                return NotFound();
            }
            else
            {
               
                _commentContext.comentarios.Attach(comment_select);
                _commentContext.comentarios.Remove(comment_select);
                _commentContext.SaveChanges();
                return Ok("se elimino correctamente\n");


            }

        }

        //filter the record
        [HttpGet]
        [Route("Search Comment/{id}")]
        public IActionResult search_ref(int id)
        {

            comments? comments_select = (from e in _commentContext.comentarios
                                            where e.usuarioId == id
                                            select e).FirstOrDefault();

            if (comments_select == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(comments_select);
            }
        }
    }
}
