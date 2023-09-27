using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PROYECTO_LIBRARY_PRIMARY.Data;
using PROYECTO_LIBRARY_PRIMARY.Models;
using Microsoft.AspNetCore.Identity;


namespace PROYECTO_LIBRARY_PRIMARY.Controllers
{
  
    public class CatalogoController : Controller
    {
        private readonly ILogger<CatalogoController> _logger;

        private readonly ApplicationDbContext _context;


    private readonly UserManager<IdentityUser> _userManager;


        public CatalogoController(ILogger<CatalogoController> logger,
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser>signInManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            
        }

        public IActionResult Index()
        {
            var productos = from o in _context.DataProductos select o;
            return View(productos.ToList());
        }

         public async Task<IActionResult> Details(int? id){
            Producto? objProduct = await _context.DataProductos.FindAsync(id);
            if(objProduct == null){
                return NotFound();
            }
            return View(objProduct);
        }

         public async Task<IActionResult> Add(int? id){
            var userID = _userManager.GetUserName(User); //sesion
            if(userID == null){
                ViewData["Message"] = "Por favor debe loguearse antes de agregar un producto";
                List<Producto> productos = new List<Producto>();
                return  View("Index",productos);
            }else{
                var producto = await _context.DataProductos.FindAsync(id);
                ViewData["Message"] = "productos añadidos";
                Proforma proforma = new Proforma();
                proforma.Producto = producto;
                proforma.Precio = producto.Precio; //precio del producto en ese momento
                proforma.Cantidad = 1;
                proforma.UserID = userID;
                _context.Add(proforma);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}