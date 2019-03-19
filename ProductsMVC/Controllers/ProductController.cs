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
using Microsoft.AspNet.Identity.Owin;
using ProductsMVC.Common.Enum;
using Microsoft.AspNet.Identity;

namespace ProductsMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepo _productRepo = new ProductRepo();
        private readonly LogRepo _logRepo = new LogRepo();

        // GET: Product
        public ActionResult Index()
        {
            var products = _productRepo.GetAll();
            return View(Mapper.Map<List<Product>, List<ProductViewModel>>(products));
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = _productRepo.GetOne(id);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<Product, ProductViewModel>(product));
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        //[ValidateAntiForgeryToken] 
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
                _productRepo.Add(Mapper.Map<ProductViewModel, Product>(product));
                LogProductCRUD(product, Actions.Create);
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
            var product = _productRepo.GetOne(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<Product, ProductViewModel>(product));
        }

        // POST: Product/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]  
        public ActionResult Edit([Bind(Include = "Id, Name, Price, Description")] ProductViewModel product)
        {
            if (!ModelState.IsValid) { return View(product); }
            try
            {
                var el = _productRepo.Save(Mapper.Map<ProductViewModel, Product>(product));
                LogProductCRUD(product, Actions.Edit);
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
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = _productRepo.GetOne(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<Product,ProductViewModel>(product));
        }

        // POST: Product/Delete/5
        // No longer need the [ActionName("Delete")] attribute
        [HttpPost]
        //[ValidateAntiForgeryToken] 
        public async Task<ActionResult> Delete(ProductViewModel product)
        {
            try
            {
                _productRepo.Delete(Mapper.Map<ProductViewModel, Product>(product));
                LogProductCRUD(product, Actions.Delete);
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

        private void LogProductCRUD(ProductViewModel product, Actions action)
        {
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var user = User.Identity.Name;
            user = string.IsNullOrWhiteSpace(user) ? "Anonymous" : user;
            var logId = new Random().Next(Int32.MinValue, Int32.MaxValue);// FIXME: Id generagion should be handled by EF instead
            LogViewModel logVM = new LogViewModel()
            {
                ActionDescription = $"User:{user ?? "Anonymous"} ; Action: {action}; Product name: {product.Name}; Product id: {product.Id}",
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                UserId = userId,
                Id = logId
            };

            _logRepo.Add(Mapper.Map<LogViewModel, Log>(logVM));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _productRepo.Dispose(); 

            }
            base.Dispose(disposing);
        }
    }
}