namespace Projeto_WEBAPI_CalebeBertoluci.Context;
using Microsoft.EntityFrameworkCore;
using Projeto_WEBAPI_CalebeBertoluci.Models;

public class InMemoryContext : DbContext
{
    public InMemoryContext(DbContextOptions<InMemoryContext> options) : base(options)
    {
        
    }
    public DbSet<Movies> Movies { get; set; }
    public DbSet<Users> Users { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

}