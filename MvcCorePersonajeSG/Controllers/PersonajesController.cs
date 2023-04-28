using Microsoft.AspNetCore.Mvc;
using MvcCorePersonajeSG.Models;
using MvcCorePersonajeSG.Services;

namespace MvcCorePersonajeSG.Controllers
{
    public class PersonajesController : Controller
    {
        private ServicePersonajes service;

        public PersonajesController(ServicePersonajes service)
        {
            this.service = service;
        }


        public async Task<IActionResult> Index()
        {
            List<Personaje> personajes = await this.service.GetPersonajesAsync();

            return View(personajes);
        }

        public async Task<IActionResult> Details(int id)
        {
            Personaje personaje =
                await this.service.FindPersonajeAsync(id);
            return View(personaje);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Personaje personaje)
        {
            await this.service.InsertPersonajesAsync
                  (personaje.IdPersonaje, personaje.NombrePersonaje, personaje.Imagen, personaje.IdSerie);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Personaje personaje =
              await this.service.FindPersonajeAsync(id);
            return View(personaje);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Personaje personaje)
        {
            await this.service.UpdatePersonajeAsync
                 (personaje.IdPersonaje, personaje.NombrePersonaje, personaje.Imagen, personaje.IdSerie);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.service.DeletePersnajeAsync(id);
            return RedirectToAction("Index");
        }
    }
}
