using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication3.Controllers
{
    public class VentasControllers : Controller
    {
        private readonly ApplicationDbContext _dbContex;

        public VentasControllers(ApplicationDbContext dbContext ){
            _dbContex = dbContext;
}
    }
}
