using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RewatchIt.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        #region Constructors

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        #endregion
    }
}