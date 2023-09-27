using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PROYECTO_LIBRARY_PRIMARY.Models;

namespace PROYECTO_LIBRARY_PRIMARY.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }


    public DbSet<Contacto> DataContactos {get; set;}

    public DbSet<Producto> DataProductos { get; set; }
     public DbSet<Proforma> DataCarrito { get; set; }


}
