using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LarpQuestSystem.Data
{
    public class LarpIdentityContext : IdentityDbContext
    {
        public LarpIdentityContext(DbContextOptions<LarpIdentityContext> options) : base(options)
        {
        }
    }
}
