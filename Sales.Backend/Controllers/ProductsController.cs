using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sales.Backend.Models;
using Sales.Common.Models;
using Sales.Backend.Helpers;

namespace Sales.Backend.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Products
        public async Task<ActionResult> Index()
        {
            return View(await db.Products.OrderBy(p=>p.Description).ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductView view)/*[Bind(Include = "ProductId,Description,Remarks,Price,IsAvailable,PublishOn,ImagePath")]*/
        {

            var pic = string.Empty;
            var folder = "~/Content/Products";

            if (view.ImageFile != null)
            {
                pic = FileHelper.UploadPhoto(view.ImageFile, folder);
                pic = string.Format("{0}/{1}", folder, pic);
                view.ImagePath = pic;
            }

            var product = this.ToProduct(view);

            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(product);
        }


        /*Para el Create*/
        private Product ToProduct(ProductView view)
        {
           
                return new Product
                {
                    Description = view.Description,
                    Price = view.Price,
                    ImagePath = view.ImagePath,
                    Remarks = view.Remarks,
                    PublishOn = view.PublishOn,
                    ProductId = view.ProductId,
                    IsAvailable = view.IsAvailable,

                };


        }

        /*Para el Edit*/
        private ProductView ToView(Product product)
        {

            return new ProductView  
            {
                Description = product.Description,
                Price = product.Price,
                ImagePath = product.ImagePath,
                Remarks = product.Remarks,
                PublishOn = product.PublishOn,
                ProductId = product.ProductId,
                IsAvailable = product.IsAvailable,

            };


        }

        // GET: Products/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            /*Product product = await db.Products.FindAsync(id);*/
            var product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }


            var view = ToView(product);


            return View(view);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProductView view)
        /*([Bind(Include = "ProductId,Description,Remarks,Price,IsAvailable,PublishOn,ImagePath")]Product product)*/
        {

            if (ModelState.IsValid)
            {

                var pic = string.Empty;
                var folder = "~/Content/Products";

                if (view.ImageFile != null)
                {
                    pic = FileHelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                    view.ImagePath = pic;
                }

                var product = this.ToProduct(view);

                db.Entry(product).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(view);
        }

        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Product product = await db.Products.FindAsync(id);

            var product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Product product = await db.Products.FindAsync(id);
            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
