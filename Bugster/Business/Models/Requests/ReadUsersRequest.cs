using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bugster.Business.Models
{
    public class ReadUsersRequest
    {
        public IEnumerable<string> Roles { get; set; }

        internal List<SelectListItem> Select(Func<object, SelectListItem> p)
        {
            throw new NotImplementedException();
        }
    }
}
