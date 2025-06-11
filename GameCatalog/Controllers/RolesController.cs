using GameCatalog.Data;
using GameCatalog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GameCatalog.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly GameCatalogContext _context;
        public RolesController(RoleManager<IdentityRole> roleManager, GameCatalogContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }
        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(IdentityRole role)
        {
            if (!_roleManager.RoleExistsAsync(role.Name).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(role.Name)).GetAwaiter().GetResult();
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> EditPermissions(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return NotFound();
            }

            var existingClaims = await _roleManager.GetClaimsAsync(role);
            var allPermissions = await _context.Permisos.ToListAsync();

            var model = new EditRolePermissionsViewModel
            {
                RoleId = role.Id,
                RoleName = role.Name,
                Permissions = allPermissions.Select(p => new PermissionCheckbox
                {
                    PermissionId = p.PermisosId,
                    NombrePermiso = p.NombrePermiso,
                    IsSelected = existingClaims.Any(c => c.Type == "Permission" && c.Value == p.NombrePermiso)
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditPermissions(EditRolePermissionsViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.RoleId);
            if (role == null)
            {
                return NotFound();
            }

            // Validación de modelo
            if (!ModelState.IsValid)
            {
                // Volver a cargar los permisos para que la vista no se rompa
                var existingClaims = await _roleManager.GetClaimsAsync(role);
                var allPermissions = await _context.Permisos.ToListAsync();

                model.Permissions = allPermissions.Select(p => new PermissionCheckbox
                {
                    PermissionId = p.PermisosId,
                    NombrePermiso = p.NombrePermiso,
                    IsSelected = existingClaims.Any(c => c.Type == "Permission" && c.Value == p.NombrePermiso)
                }).ToList();

                return View(model); // Regresa la vista con el modelo para corregir
            }

            var currentClaims = await _roleManager.GetClaimsAsync(role);

            // Eliminar todos los permisos actuales
            foreach (var claim in currentClaims.Where(c => c.Type == "Permission"))
            {
                await _roleManager.RemoveClaimAsync(role, claim);
            }

            // Agregar los nuevos permisos seleccionados
            foreach (var permiso in model.Permissions.Where(p => p.IsSelected))
            {
                await _roleManager.AddClaimAsync(role, new Claim("Permission", permiso.NombrePermiso));
            }

            await _roleManager.UpdateAsync(role);
            TempData["SuccessMessage"] = "Permisos actualizados correctamente.";

            return RedirectToAction("Index");
        }




    }
}
