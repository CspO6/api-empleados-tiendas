
using Backend.Application.DTOs;
using Backend.Domain.Entities;
using Backend.Infrastructure.Persistence;
using Backend.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

public class EmpleadoServiceTests
{
    private readonly AppDbContext _context;
    private readonly EmpleadoService _service;

    public EmpleadoServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Base de datos aislada por test
            .Options;

        _context = new AppDbContext(options);
        _service = new EmpleadoService(_context);
    }

    [Fact]
    public async Task ObtenerTodos_DeberiaRetornarListaDeEmpleados()
    {
        // Arrange
        _context.Empleados.Add(new Empleado { Nombre = "Juan", Apellido = "Perez", Correo = "jp@correo.com", Cargo = "Dev", FechaIngreso = DateTime.Today, EstaActivo = true });
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.Single(result);
        Assert.Equal("Juan", result[0].Nombre);
    }
    [Fact]
    public async Task GetByIdAsync_DeberiaRetornarEmpleadoSiExiste()
    {
        // Arrange
        var empleado = new Empleado
        {
            Nombre = "Pedro",
            Apellido = "Gomez",
            Correo = "pg@correo.com",
            Cargo = "QA",
            FechaIngreso = DateTime.Today,
            EstaActivo = true
        };
        _context.Empleados.Add(empleado);
        await _context.SaveChangesAsync();

        // Act
        var resultado = await _service.GetByIdAsync(empleado.Id);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal("Pedro", resultado.Nombre);
    }

    [Fact]
    public async Task CreateAsync_DeberiaCrearNuevoEmpleado()
    {
        // Arrange
        var dto = new EmpleadoDto
        {
            Nombre = "Luis",
            Apellido = "Martinez",
            Correo = "lm@correo.com",
            Cargo = "Analista",
            FechaIngreso = DateTime.Today,
            EstaActivo = true
        };

        // Act
        var resultado = await _service.CreateAsync(dto);

        // Assert
        var creado = await _context.Empleados.FindAsync(resultado.Id);
        Assert.NotNull(creado);
        Assert.Equal("Luis", creado.Nombre);
    }

    [Fact]
    public async Task UpdateAsync_DeberiaActualizarEmpleadoExistente()
    {
        // Arrange
        var empleado = new Empleado
        {
            Nombre = "Ana",
            Apellido = "Lopez",
            Correo = "al@correo.com",
            Cargo = "Admin",
            FechaIngreso = DateTime.Today,
            EstaActivo = true
        };
        _context.Empleados.Add(empleado);
        await _context.SaveChangesAsync();

        var dto = new EmpleadoDto
        {
            Id = empleado.Id,
            Nombre = "AnaActualizada",
            Apellido = "Lopez",
            Correo = "al@correo.com",
            Cargo = "Admin",
            FechaIngreso = DateTime.Today,
            EstaActivo = true
        };

        // Act
        var actualizado = await _service.UpdateAsync(dto);

        // Assert
        Assert.True(actualizado);
        var actualizadoDb = await _context.Empleados.FindAsync(empleado.Id);
        Assert.Equal("AnaActualizada", actualizadoDb.Nombre);
    }

    [Fact]
    public async Task DeleteAsync_DeberiaEliminarEmpleadoSinUsuario()
    {
        // Arrange
        var empleado = new Empleado
        {
            Nombre = "Carlos",
            EstaActivo = true
        };
        _context.Empleados.Add(empleado);
        await _context.SaveChangesAsync();

        // Act
        var eliminado = await _service.DeleteAsync(empleado.Id);

        // Assert
        Assert.True(eliminado);
        var eliminadoDb = await _context.Empleados.FindAsync(empleado.Id);
        Assert.Null(eliminadoDb);
    }

    [Fact]
    public async Task DeleteAsync_DeberiaLanzarExcepcionSiUsuarioEsAdmin()
    {
        // Arrange
        var empleado = new Empleado
        {
            Nombre = "Admin",
            Usuario = new Usuario { Rol = "Admin" }
        };
        _context.Empleados.Add(empleado);
        await _context.SaveChangesAsync();

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _service.DeleteAsync(empleado.Id));
    }


    [Fact]
    public async Task DeleteAsync_DeberiaLanzarExcepcionSiEmpleadoTieneUsuario()
    {
        // Arrange
        var empleado = new Empleado
        {
            Nombre = "EmpleadoNormal",
            Usuario = new Usuario { Rol = "User" }
        };
        _context.Empleados.Add(empleado);
        await _context.SaveChangesAsync();

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _service.DeleteAsync(empleado.Id));
    }

}
