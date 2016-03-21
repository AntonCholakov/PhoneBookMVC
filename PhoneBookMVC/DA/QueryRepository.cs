using PhoneBookMVC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.DA
{
    public class QueryRepository : BaseRepository<Query>
    {
        public QueryRepository(AppContext context) : base(context) { }
    }
}