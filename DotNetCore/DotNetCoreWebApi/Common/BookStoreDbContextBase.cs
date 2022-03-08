﻿using AutoMapper;
using DotNetCoreWebApi.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.Common
{
    public class BookStoreDbContextBase
    {
        protected readonly IBookStoreDbContext _dbContext;
        protected readonly IMapper _mapper;

        protected BookStoreDbContextBase(IBookStoreDbContext context,IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
    }
}
