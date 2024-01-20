using FDBlog.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDBlog.Entity.Entities
{
    public class AppUserRole:IdentityUserRole<int>, IEntityBase
    {
    }
}
