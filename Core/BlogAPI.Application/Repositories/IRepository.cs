using BlogAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity 
    {
       DbSet<T> Table { get; }
    }
}
