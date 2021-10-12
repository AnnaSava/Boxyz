using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Proto.Data.Services
{
    public abstract class BaseService
    {
        protected readonly BoxContext _dbContext;
        protected readonly IMapper _mapper;

        public BaseService(BoxContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
    }
}
