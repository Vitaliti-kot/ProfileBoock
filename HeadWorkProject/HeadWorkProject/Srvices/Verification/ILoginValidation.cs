using HeadWorkProject.Srvices.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeadWorkProject.Srvices.Verification
{
    public interface ILoginValidation
    {
        IRepository _repository { get; set; }
        int Success(string login, string password);
    }
}
