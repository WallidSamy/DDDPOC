using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDPOC.Application.ViewModels.Response
{
    public class GetAllUsersResponse
    {
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public bool enabled { get; set; }
        public bool emailVerified { get; set; }
    }
}
