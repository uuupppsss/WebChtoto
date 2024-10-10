using Microsoft.AspNetCore.Mvc;
using WebChtoto.Model;

namespace WebChtoto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalData : ControllerBase
    {
        private static List<Passport> _passports = new List<Passport>();
        private static List<Snils> _snilses = new List<Snils>();

        public PersonalData()
        {
            
        }

        [HttpPost("CreatePassport")]
        public ActionResult CreatePassport(Passport passport)
        {
            if (passport == null)
                return BadRequest("Invalid passport");
            _passports.Add(passport);
            return Ok(passport);
        }

        [HttpPost("CreateSnils")]
        public ActionResult CreateSnils(Snils snils)
        {
            if(snils == null|| !IsSnilseValid(snils))
                return BadRequest("Invalid snils");
            _snilses.Add(snils);
            return Ok(snils);
        }

        [HttpPost("SearchHumanPassport")]
        public ActionResult<Passport> SearchHumanPassport(Search search)
        {
            if (string.IsNullOrEmpty(search.FirstName) ||
                string.IsNullOrEmpty(search.LastName))
                return BadRequest("Firstname or Lastname are empty");
            var find= _passports.FirstOrDefault(s=> s.FirstName.Equals(search.FirstName) &&
            s.LastName.Equals(search.LastName));
            if(find == null)
                return NotFound("We don't know this MC");
            return find;
        }

        [HttpPost("SendClaim")]
        public ActionResult SendClaim(Passport passport)
        {
            if (passport == null)
                return BadRequest("Invalid passport");
            _passports.Remove(passport);
            return Ok("Data deleted (no)");
        }

        [HttpPost("SearchSnils")]
        public ActionResult<Snils> SearchSnils(Search search)
        {
            if (string.IsNullOrEmpty(search.FirstName) ||
                string.IsNullOrEmpty(search.LastName))
                return BadRequest("Firstname or Lastname are empty");
            var find = _snilses.FirstOrDefault(s => s.FirstName.Equals(search.FirstName) &&
            s.LastName.Equals(search.LastName));
            if (find == null)
                return NotFound("We don't know this MC");
            return find;
        }

        private bool IsSnilseValid(Snils snils)
        {
            if (snils.Number.Length != 11||string.IsNullOrEmpty(snils.LastName) || string.IsNullOrEmpty(snils.FirstName)||!int.TryParse(snils.Number, out _))
                return false;
            return true;
        }
    }
}
