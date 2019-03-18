using AutoMapper;
using ProductDAL.Repos;
using ProductDAL.Models;
using ProductsMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace ProductsMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepo _repo = new ProductRepo();

        private IMapper iMapper = new MapperConfiguration(
            cfg => 
            {
                cfg.CreateMap<Product, ProductViewModel>();
                cfg.CreateMap<ProductViewModel, Product>();
            }
            ).CreateMapper();

       
        // GET: Product
        public ActionResult Index()
        {
            var products = _repo.GetAll();
            return View(iMapper.Map<List<Product>, List<ProductViewModel>>(products));
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = _repo.GetOne(id);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(iMapper.Map<Product, ProductViewModel>(product));
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        //[ValidateAntiForgeryToken] //TODO : add validation
        [HttpPost]
        public ActionResult Create([Bind(Include = "Name,Price,Description")] ProductViewModel product)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "An error occurred in the data.  Please check all values and try again.");
                return View(product);
            }
            try
            {
                product.Id = 100;
                _repo.Add(iMapper.Map<ProductViewModel, Product>(product));
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to create record: {ex.Message}");
                return View(product);
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = _repo.GetOne(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(iMapper.Map<Product, ProductViewModel>(product));
        }

        // POST: Product/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken] //TODO : add validation
        public ActionResult Edit([Bind(Include = "Id, Name, Price, Description")] ProductViewModel product)
        {
            if (!ModelState.IsValid) { return View(product); }
            try
            {
                var el = _repo.Save(iMapper.Map<ProductViewModel, Product>(product));
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError(string.Empty, "Unable to save record. Another user updated the record.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to save record: {ex.Message}");
            }
            return View(product);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = _repo.GetOne(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(iMapper.Map<Product,ProductViewModel>(product));
        }

        // POST: Product/Delete/5
        // No longer need the [ActionName("Delete")] attribute
        [HttpPost]
        //[ValidateAntiForgeryToken] //TODO
        public async Task<ActionResult> Delete(ProductViewModel product)
        {
            try
            {
                _repo.Delete(iMapper.Map<ProductViewModel, Product>(product));
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError(string.Empty, "Unable to delete record. Another user updated the record.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to create record: {ex.Message}");
            }
            return View(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose(); 

            }
            base.Dispose(disposing);
        }
    }
}