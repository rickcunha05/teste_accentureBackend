using CrudAspAccenture.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudAspAccenture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;

        public SuppliersController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        [HttpGet]
        [Route("GetFornecedor")]
        public async Task<IEnumerable<Suppliers>> GetSupliers()
        {
            return await _databaseContext.Suppliers.ToListAsync();
        }

        [HttpPost]
        [Route("PostFornecedor")]
        public async Task<IActionResult> AddSuppliers(Suppliers objSuppliers)
        {
            var objetoExistente = this.GetSupliers().Result.Where(x => x.Cnpj == objSuppliers.Cnpj ||  x.Cpf == objSuppliers.Cpf).FirstOrDefault();
                                                                 
            if (objetoExistente is null)
            {
                _databaseContext.Suppliers.Add(objSuppliers);
                await _databaseContext.SaveChangesAsync();
                return Ok("Registro salvo com Sucesso");                
            }
            else
            {
                return BadRequest("Já existe um registro com este CNPJ.");
            }
        }

        [HttpPatch]
        [Route("UpdateFornecedor/{id}")]
        public async Task<IActionResult> UpdateSuppliers(Suppliers objSuppliers)
        {

            _databaseContext.Entry(objSuppliers).State = EntityState.Modified;
            var objetoExistente = this.GetSupliers().Result.Where(x => x.Cnpj == objSuppliers.Cnpj && x.Id != objSuppliers.Id).FirstOrDefault();
            
            if (objetoExistente is not null)
            {
                if (objetoExistente.Cnpj != objSuppliers.Cnpj)
                {                    
                    await _databaseContext.SaveChangesAsync();
                    return Ok("O novo cadastro foi salvo!");
                }
                else
                {
                    return BadRequest("Já existe um cadastro com este CNPJ ou CPF cadastrado.");
                }
            }
            else
            {

                _databaseContext.Suppliers.Update(objSuppliers);
                await _databaseContext.SaveChangesAsync();
                return Ok("Cadastro Alterado com sucesso!");
            }
        }
        [HttpDelete]
        [Route("DeleteFornecedor/{id}")]
        public bool DeleteCompany(int id)
        {
            bool a = false;
            var suppliers = _databaseContext.Suppliers.Find(id);
            if (suppliers != null)
            {
                a = true;
                _databaseContext.Entry(suppliers).State = EntityState.Deleted;
                _databaseContext.SaveChanges();
            }
            else
            {
                a = false;
            }
            return a;
        }
    }
}
