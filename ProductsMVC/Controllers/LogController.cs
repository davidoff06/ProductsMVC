using AutoMapper;
using ProductDAL.Models;
using ProductDAL.Repos;
using ProductsMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductsMVC.Controllers
{
    public class LogController : Controller
    {
        private readonly LogRepo _repo = new LogRepo();

        private IMapper iMapper = new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<Log, LogViewModel>();
                cfg.CreateMap<LogViewModel, Log>();
            }
            ).CreateMapper();


        // GET: Product
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var logs = _repo.GetAll();
            return View(iMapper.Map<List<Log>, List<LogViewModel>>(logs));
        }

    }
}