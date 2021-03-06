using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationResult.Tests.Mocks
{
    public class FooUser
    {
        public FooUser(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
        public FooUser() {}

        public string UserName { get; set; }
        public string Password { get; set; }

        public bool EqulaInner(FooUser foo)
        {
            return foo.UserName.Equals(UserName) && foo.Password.Equals(Password);
        }
    }

    public class FooInto
    {
        public FooUser User { get; set; }
        public IEnumerable<FooUser> OtherUsers { get; set; }        
        public int StatusCode { get; set; }
    }
}
