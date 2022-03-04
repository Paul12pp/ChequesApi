using ChequesApi.Models.Entities;
using ChequesApi.Repositories;
using ChequesApi.ViewModels.Concepto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChequesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConceptoController : ControllerBase
    {
        private readonly IRepository<Concepto> _repo;
        public ConceptoController(IRepository<Concepto> repo)
        {
            _repo = repo;
        }
        [HttpGet("{id}")]
        public ActionResult<ConceptoViewModel> GetById(int id)
        {
            try
            {
                var concepto = _repo.Find(id);

                if (concepto == null) return NotFound();

                ConceptoViewModel viewModel = new ConceptoViewModel()
                {
                    Id = concepto.Id,
                    Descripcion = concepto.Descripcion,
                    Estado = concepto.Estado,
                };
                return Ok(viewModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult<List<ConceptoViewModel>> GetAll()
        {
            var articulos = _repo.FindAll();
            List<ConceptoViewModel> result = new List<ConceptoViewModel>();
            foreach (var item in articulos)
            {
                result.Add(new ConceptoViewModel()
                {
                    Descripcion = item.Descripcion,
                    Estado = item.Estado,
                    Id = item.Id,
                });
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateConcepto(ConceptoViewModel viewModel)
        {
            try
            {
                var concepto = new Concepto()
                {
                    Descripcion = viewModel.Descripcion,
                    Estado = viewModel.Estado,
                };

                await _repo.InsertAsync(concepto);
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPut]
        public ActionResult UpdateConcepto(ConceptoViewModel viewModel)
        {
            var existing = _repo.Find(viewModel.Id);

            if (existing == null)
                return NotFound();

            existing.Estado = viewModel.Estado;
            existing.Descripcion = viewModel.Descripcion;
            _repo.Update(existing);
            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var concepto = _repo.Find(id);
            concepto.Estado = false;
            _repo.Update(concepto);
            return Ok();
        }
    }
}
