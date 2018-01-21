using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TinyPermissionsLib.Sample.Data;
using TinyPermissionsLib.EFCoreProvider;
using System.Security.Principal;
using System.Threading;

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
            var identity = new GenericIdentity("billybob");
            var principal = new GenericPrincipal(identity, null);
            Thread.CurrentPrincipal = principal;

            return _context.Ducks.WithRole("sales").Include(x => x.Owner).ToList();
        }
    }
}
