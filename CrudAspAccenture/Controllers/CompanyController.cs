using CrudAspAccenture.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Net;

namespace CrudAspAccenture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;

        public CompanyController(DatabaseContext databaseContext) 
        {            
            _databaseContext = databaseContext;
        }

        [HttpGet]
        [Route("GetEmpresa")]
        public async Task<IEnumerable<Company>> GetCompanies()
        {
            return await _databaseContext.Company.ToListAsync();
        }
        [HttpPost]
        [Route("PostEmpresa")]

        public async Task<IActionResult> AddCompany(Company objCompany)
        {
            var objetoExistente = this.GetCompanies().Result.Where(x => x.Cnpj == objCompany.Cnpj).FirstOrDefault();

                if (objetoExistente is null)
            {
                _databaseContext.Company.Add(objCompany);
                await _databaseContext.SaveChangesAsync();
                return Ok("Empresa Cadastrada com Sucesso");              
            } 
            else
            {
                return BadRequest("Já existe um cadastro para esse CNPJ");
            }
                
        }

        [HttpPatch]
        [Route("UpdateEmpresa/{id}")]
        public async Task<IActionResult> UpdateCompany(Company objCompany)
        {
            _databaseContext.Entry(objCompany).State = EntityState.Modified;
            var objetoExistente = this.GetCompanies().Result.Where(x => x.Cnpj == objCompany.Cnpj && x.Id != objCompany.Id).FirstOrDefault();

            if (objetoExistente is not null)
            {
                if (objetoExistente.Cnpj != objCompany.Cnpj)
                {
                    await _databaseContext.SaveChangesAsync();
                    return Ok("O novo cadastro foi salvo!");
                }
                else
                {
                    return BadRequest("Já existe um cadastro com este CNPJ");
                }
            }
            else
            {

                _databaseContext.Company.Update(objCompany);
                await _databaseContext.SaveChangesAsync();
                return Ok("Cadastro Alterado com sucesso!");
            }

        }
        [HttpDelete]
        [Route("DeleteEmpresa/{id}")]
        public bool DeleteCompany(int id) 
        {
            bool a = false;
            var company = _databaseContext.Company.Find(id);
            if (company != null) 
            {
                a = true;
                _databaseContext.Entry(company).State = EntityState.Deleted;
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
