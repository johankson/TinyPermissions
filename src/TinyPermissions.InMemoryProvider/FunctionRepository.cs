﻿using System.Collections.Generic;
using System.Linq;

namespace TinyPermissionsLib.InMemoryProvider
{
    public class FunctionRepository : IFunctionRepository
    {
        public Dictionary<string, IFunction> _functions = new Dictionary<string, IFunction>();

        public void AddFunction(IFunction function)
        {
            if (!(function is InMemoryProvider.Function))
            {
                var f = new InMemoryProvider.Function()
                {
                    Id = function.Id,
                    Name = function.Name,
                    Description = function.Description
                };
            }

            _functions.Add(function.Id, function);
        }

        public IFunction GetFunction(string functionId)
        {
            return _functions[functionId];
        }

        public void AddUserToFunction(IUser user, IFunction function)
        {
            var f = GetFunction(function.Id) as Function;
            var u = user as User;

            if(f == null || u == null)
            {
                return;
            }

            f.Users.Add(u);
        }

        public bool UserHasAccessToFunction(IUser user, IFunction function)
        {
            var f = GetFunction(function.Id) as TinyPermissionsLib.InMemoryProvider.Function;

            if (f == null)
            {
                return false;
            }

            return f.Users.Any(x => x.Username == user.Username);
        }
    }
}