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
        [HttpGet("GetById")]
        public ActionResult<ConceptoViewModel> GetById(int id)
        {
            try
            {

                //using var _dbContext = new FacturacionDbContext();
                //var _repo = new ArticuloRepository(_dbContext);
                //var test = _repo.Find(id);
                //var test2 = _repo.SumTwoDBNumbers();

                var concepto = _repo.Find(id);//_dbContext.Articulos.FirstOrDefault(x => x.Id == id);

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

        [HttpGet("GetAll")]
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
        public ActionResult UpdateArticulo(ConceptoViewModel viewModel)
        {
            // using var _dbContext = new FacturacionDbContext();

            var existing = _repo.Find(viewModel.Id); //_dbContext.Articulos.FirstOrDefault(x => x.Id == viewModel.Id);

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
            //using var _dbContext = new FacturacionDbContext();
            
            var concepto = _repo.Find(id);//_dbContext.Articulos.FirstOrDefault(x => x.Id == id);
            concepto.Estado = false;
            _repo.Update(concepto);
            return Ok();
        }
    }
}
