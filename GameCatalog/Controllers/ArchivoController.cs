using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameCatalog.Controllers
{
    [Authorize]
    public class ArchivoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Index(IFormFile file)
        {
            if(file != null && file.Length > 0)
            {
                var ruta = Path.Combine(Directory.GetCurrentDirectory(), "Upload", file.FileName);
                using(var stream = new FileStream(ruta, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    ViewData["Subida"] = "El archivo se ha subido correctamente";
                }
            }
            else
            {
                ViewData["Subida"] = "Ingresa un archivo";
            }
            return View();
        }
        public IActionResult Download()
        {
            var files = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "Upload"))
                .Select(Path.GetFileName);
            return View(files);
        }
        public IActionResult DownloadFile(string fileName)
        {
            var fileRuta = Path.Combine(Directory.GetCurrentDirectory(), "Upload", fileName);
            var memoria = new MemoryStream();
            using(var stream = new FileStream(fileRuta, FileMode.Open))
            {
                stream.CopyTo(memoria);
            }
            memoria.Position = 0;
            return File(memoria, "application/octet-stream", fileName);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete()
        {
            var files = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "Upload"))
                .Select(Path.GetFileName);
            return View(files);
        }
        public IActionResult DeleteFile(string fileName)
        {
            try
            {
                var fileRuta = Path.Combine(Directory.GetCurrentDirectory(), "Upload", fileName);
                if(System.IO.File.Exists(fileRuta))
                {
                    System.IO.File.Delete(fileRuta);
                    ViewData["Mensaje"] = "El archivo se borro exitosamente";
                }
                else
                {
                    ViewData["Mensaje"] = "El archivo no existe";
                }
            }
            catch (Exception)
            {
                ViewData["Mensaje"] = "No se pudo borrar el archivo";
            }
            return RedirectToAction("Delete");
        }
    }
}
