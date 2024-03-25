﻿using Infrastructure.Contexts;
using Infrastructure.Entities.AccountEntites;
using Microsoft.EntityFrameworkCore;
using Shared.Utilis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SqlRepositories
{
    public class AddressRepository : SqlBaseRepository<AddressEntity, AccountDbContext>
    {
        private readonly AccountDbContext _context;
        public AddressRepository(AccountDbContext context, ErrorLogger errorLogger) : base(context, errorLogger)
        {
            _context = context;
        }





    }
}