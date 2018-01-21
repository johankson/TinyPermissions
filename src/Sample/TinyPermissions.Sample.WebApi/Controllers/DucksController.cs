using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
//using TinyPermissionsLib.Sample.Data;

namespace TinyPermissionsLib.Sample.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class DucksController
    {
        private TinyPermissionsLib.Sample.Data.DuckContext _context;

        public DucksController(TinyPermissionsLib.Sample.Data.DuckContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<TinyPermissionsLib.Sample.Data.Duck> GetDucks()
        {
            return _context.Ducks.ToList();
        }
    }
}
