using ChequesApi.Models.Entities;
using ChequesApi.Repositories;
using ChequesApi.ViewModels.Proveedor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChequesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProveedorController : ControllerBase
    {
        private readonly IRepository<Proveedor> _repo;
        public ProveedorController(IRepository<Proveedor> repo)
        {
            _repo = repo;
        }

        [HttpGet("{id}")]
        public ActionResult<ProveedorViewModel> GetById(int id)
        {
            try
            {
                var proveedor = _repo.Find(id);

                if (proveedor == null) return NotFound();

                ProveedorViewModel viewModel = new ProveedorViewModel()
                {
                    Id = proveedor.Id,
                    Nombre = proveedor.Nombre,
                    TipoPersona= proveedor.TipoPersona,
                    Cedula= proveedor.Cedula,
                    CuentaContable= proveedor.CuentaContable,
                    Balance = proveedor.Balance,
                    Estado = proveedor.Estado,
                };
                return Ok(viewModel);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public ActionResult<List<ProveedorViewModel>> GetAll()
        {
            var proveedores = _repo.FindAll();
            List<ProveedorViewModel> result = new List<ProveedorViewModel>();
            foreach (var item in proveedores)
            {
                result.Add(new ProveedorViewModel()
                {
                    Id = item.Id,
                    Nombre = item.Nombre,
                    TipoPersona = item.TipoPersona,
                    Cedula = item.Cedula,
                    CuentaContable = item.CuentaContable,
                    Balance = item.Balance,
                    Estado = item.Estado,
                });
            }

            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<int>> CreateProveedor(ProveedorViewModel viewModel)
        {
            try
            {
                var proveedor = new Proveedor()
                {
                    Id = viewModel.Id,
                    Nombre = viewModel.Nombre,
                    TipoPersona = viewModel.TipoPersona,
                    Cedula = viewModel.Cedula,
                    CuentaContable = viewModel.CuentaContable,
                    Balance = viewModel.Balance,
                    Estado = true,
                };

                await _repo.InsertAsync(proveedor);
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpPut]
        public ActionResult UpdateProveedor(ProveedorViewModel viewModel)
        {
            var existing = _repo.Find(viewModel.Id);

            if (existing == null)
                return NotFound();

            existing.Estado = viewModel.Estado;
            existing.Nombre = viewModel.Nombre;
            existing.Balance = viewModel.Balance;
            existing.CuentaContable = viewModel.CuentaContable;
            existing.Cedula = viewModel.Cedula;
            existing.TipoPersona = viewModel.TipoPersona;
            _repo.Update(existing);
            return Ok();
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var proveedor = _repo.Find(id);
            proveedor.Estado = false;
            _repo.Update(proveedor);
            return Ok();
        }
    }
}
