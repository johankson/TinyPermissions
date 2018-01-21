using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TinyPermissionsLib.Sample.Data;

namespace TinyPermissionsLib.Sample.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class DucksController
    {
        private DuckContext _context;

        public DucksController(DuckContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Duck> GetDucks()
        {
            return _context.Ducks.Include(x => x.Owner).ToList();
        }
    }
}
