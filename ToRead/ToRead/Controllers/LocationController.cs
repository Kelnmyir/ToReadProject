using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using ToRead.MVC.Models;
using ToRead.Data;
using ToRead.Data.Models;

namespace ToRead.MVC.Controllers
{
    public class LocationController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILocationRepository _repository;

        public LocationController(ILocationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<LocationModel> locationModels = _mapper.Map<List<Location>, List<LocationModel>>(_repository.Get().ToList());
            return View(locationModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(LocationModel model)
        {
            Location location = _mapper.Map<Location>(model);
            _repository.Create(location);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Location location = _repository.GetOne(id);
            LocationModel model = _mapper.Map<LocationModel>(location);
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(LocationModel model)
        {
            Location location = _mapper.Map<Location>(model);
            _repository.Update(location);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Location location = _repository.GetLocationDetailed(id);
            LocationModel model = _mapper.Map<LocationModel>(location);
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Location location = _repository.GetOne(id);
            _repository.Delete(location);
            return RedirectToAction("Index");
        }
    }
}