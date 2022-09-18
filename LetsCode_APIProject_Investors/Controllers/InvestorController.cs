using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using LetsCode_APIProject_Investors;
using Microsoft.AspNetCore.Mvc;

namespace Projeto_CalebeBertoluci.Controllers
{
    [Route("Investors/[controller]")]
    [ApiController]
    
    public class APIController : Controller
    {
        public static List<Investors> investors = new List<Investors>();
        
        //BUSCA INVESTORES POR ID
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Investors>>> Get(int id)
        {
            var investor = investors.Find(x => x.Id == id);
            if (investor == null)
                return BadRequest("Investor not found.");
            
            return Ok(investor);
        }
        
        //PROCURA INVESTIDORES COM SALDO MAIOR QUE X (balanceHigherThan)
        [HttpGet("balance")]
        public async Task<ActionResult<IEnumerable<List<Investors>>>> Get(decimal balanceHigherThan)
        {
            var investor = investors
                .Where(x => x.Balance > balanceHigherThan)
                .ToList();
            
            if (investor == null)
                return BadRequest("Investor not found.");
            
            return Ok(investor);
        }
        
        //CADASTRA INVESTIDOR
        [HttpPost]
        public async Task<ActionResult<List<Investors>>> AddInvestor(Investors investor)
        {
            investors.Add(investor);
            System.IO.File.WriteAllText(Database.dbPath,JsonSerializer.Serialize(investors));
            
            return Ok(investors);
        }
        
        //ATUALIZA INVESTIDOR
        [HttpPut]
        public async Task<ActionResult<List<Investors>>> UpdateInvestor(Investors request)
        {            
            
            var investor = investors.Find(x => x.Id == request.Id);
            if (investor == null)
                return BadRequest("Investor not found.");

            investor.Name = request.Name;
            investor.Balance = request.Balance;
            investor.InvestorProfile = request.InvestorProfile;
            investor.PreferredStock = request.PreferredStock;
            
            System.IO.File.WriteAllText(Database.dbPath,JsonSerializer.Serialize(investors));
            
            return Ok(investors);
        }
        
        //DELETA INVESTIDOR
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Investors>>> Delete(int id)
        {
            var investor = investors.Find(x => x.Id == id);
            if (investor == null)
                return BadRequest("Investor not found.");
            
            investors.Remove(investor);
            System.IO.File.WriteAllText(Database.dbPath,JsonSerializer.Serialize(investors));
            
            return Ok(investors);
        }
    }
}