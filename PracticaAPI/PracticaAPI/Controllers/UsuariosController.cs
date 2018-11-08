using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticaAPI.Models;

namespace PracticaAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public UsuariosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        //GET/users
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return context.Users.ToList();
        }
        //GET/users/id
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = context.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Post([FromBody]User usuarioNuevo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.Users.Add(usuarioNuevo);
                    context.SaveChanges();
                    return new CreatedAtRouteResult("Usuario agregado: ",
                        new { Name = usuarioNuevo.Name, UserName = usuarioNuevo.UserName, Email = usuarioNuevo.Email });
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = context.Users.FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                try
                {

                    context.Users.Remove(user);
                    context.SaveChanges();
                    return Ok("Usuario eliminado correctamente :" + user.UserName);
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPut("{id}")]
        public IActionResult Put([FromBody]User usuarioBusqueda, int id)
        {
            if (ModelState.IsValid)
            {
                if (usuarioBusqueda.Id == id)
                {
                    try
                    {
                        var usuario = context.Users.AsNoTracking().FirstOrDefault(x => x.Id == usuarioBusqueda.Id);
                        if (usuario != null)
                        {
                            context.Entry(usuarioBusqueda).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            context.SaveChanges();
                            return new CreatedAtRouteResult("Usuario modificado: ",
                                new { Name = usuarioBusqueda.Name, UserName = usuarioBusqueda.UserName, Email = usuarioBusqueda.Email });
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
                else
                    return BadRequest();
            }
            else
            return BadRequest(ModelState);
        }
    }
}