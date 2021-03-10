using System;
using System.Collections.Generic;
using System.Text;

namespace HeadWorkProject.Model
{
    public interface IEntityBaseProfile
    {
        int Id { get; set; }
        int UserId { get; set; }
    }
}
