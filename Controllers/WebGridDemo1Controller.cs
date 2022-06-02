using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MyMvc4App.Models;
using Newtonsoft.Json;

namespace MyMvc4App.Controllers
{
    public class FilteredSortedPagedLocation
    {
        public IEnumerable<Product> ProductList { get; set; }
        public FilterSortPageInfo FilterSortPageInfo { get; set; }
    }

    public class WebGridDemo1Controller : Controller
    {
        private AdventureWorksLT2008R2Context db = new AdventureWorksLT2008R2Context();

        private Action<FilterSortPageInfo> resetProductSortPage = delegate(FilterSortPageInfo fspInfo)
        {
            fspInfo.Sort = "ProductNumber";
            fspInfo.SortDir = "ASC";
            fspInfo.Page = 1;
        };

        //
        // GET: /WebGridDemo1/

        public ActionResult Index(FilterSortPageInfo fspInfo, string newFilter, string clearFilters)
        {
            fspInfo.Sort = fspInfo.Sort ?? "ProductNumber";
            fspInfo.SortDir = fspInfo.SortDir ?? "ASC";
            if (fspInfo.Page == 0) fspInfo.Page = 1;

            // only include the ones we need to show on the list
            var products = db.Products.Include(p => p.ProductCategory).Include(p => p.ProductModel);

            // apply filters
            // first get the filters, which include the new filter if any, only if we are not clearing them
            // use Newtonsoft.Json lib
            List<Filter> filters = JsonConvert.DeserializeObject<List<Filter>>(fspInfo.Filters);
            if (!String.IsNullOrEmpty(clearFilters) && clearFilters.ToUpper().Equals("TRUE"))
            {
                filters.Clear();

                // if we are clearing the filters, then we need to reset the sort and page settings
                // use delegate to reset the settings so the code can be reused
                fspInfo.ResetSettings(resetProductSortPage);
            }

            // no need to add the new filter because it is already included in the filters parameter
            // just reset the sort and page settings if we have a new filter
            if (!String.IsNullOrEmpty(newFilter) && newFilter.ToUpper().Equals("TRUE"))
            {
                // use delegate to reset the settings so the code can be reused
                fspInfo.ResetSettings(resetProductSortPage);
            }

            // apply each filter
            // no need to call ToUpper() on string since comparison is case insensitive,
            //  plus the SQL emitted uses LIKE instead of CHARINDEX
            foreach (Filter filter in filters)
            {
                // have to assign filter value to temp variable in the loop to avoid outer variable trap
                //  because the lambda expression we're using here is in a closure
                string filterVal = filter.Value;
                switch (filter.Field)
                {
                    case "Product Number":
                        products = products.Where(p => p.ProductNumber.Contains(filterVal));
                        break;
                    case "Name":
                        products = products.Where(p => p.Name.Contains(filterVal));
                        break;
                    case "Color":
                        products = products.Where(p => p.Color.Contains(filterVal));
                        break;
                    case "Size":
                        products = products.Where(p => p.Size.Contains(filterVal));
                        break;
                    case "Product Category":
                        products = products.Where(p => p.ProductCategory.Name.Contains(filterVal));
                        break;
                    case "Product Model":
                        products = products.Where(p => p.ProductModel.Name.Contains(filterVal));
                        break;
                }
            }

            // lastly, update the current filters
            // use Newtonsoft.Json lib
            fspInfo.Filters = JsonConvert.SerializeObject(filters);

            // pass total records to the view
            fspInfo.TotalRecords = products.Count();

            // apply sort
            switch (fspInfo.Sort)
            {
                case "Name":
                    products = products.OrderByWithDirection(p => p.Name, fspInfo.SortDir);
                    break;
                case "Color":
                    products = products.OrderByWithDirection(p => p.Color, fspInfo.SortDir);
                    break;
                case "StandardCost":
                    products = products.OrderByWithDirection(p => p.StandardCost, fspInfo.SortDir);
                    break;
                case "ListPrice":
                    products = products.OrderByWithDirection(p => p.ListPrice, fspInfo.SortDir);
                    break;
                case "Size":
                    products = products.OrderByWithDirection(p => p.Size, fspInfo.SortDir);
                    break;
                case "Weight":
                    products = products.OrderByWithDirection(p => p.Weight, fspInfo.SortDir);
                    break;
                case "ProductCategory.Name":
                    products = products.OrderByWithDirection(p => p.ProductCategory.Name, fspInfo.SortDir);
                    break;
                case "ProductModel.Name":
                    products = products.OrderByWithDirection(p => p.ProductModel.Name, fspInfo.SortDir);
                    break;
                default: // "ProductNumber"
                    products = products.OrderByWithDirection(p => p.ProductNumber, fspInfo.SortDir);
                    break;
            }

            // only load a page of data
            products = products.Skip(fspInfo.PageSize * (fspInfo.Page - 1)).Take(fspInfo.PageSize);

            return View(new FilteredSortedPagedLocation { ProductList = products.ToList(), FilterSortPageInfo = fspInfo });
        }

        //
        // GET: /WebGridDemo1/Details/5

        public ActionResult Details(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // GET: /WebGridDemo1/Create

        public ActionResult Create()
        {
            ViewBag.ProductCategoryID = new SelectList(db.ProductCategories, "ProductCategoryID", "Name");
            ViewBag.ProductModelID = new SelectList(db.ProductModels, "ProductModelID", "Name");
            return View();
        }

        //
        // POST: /WebGridDemo1/Create

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductCategoryID = new SelectList(db.ProductCategories, "ProductCategoryID", "Name", product.ProductCategoryID);
            ViewBag.ProductModelID = new SelectList(db.ProductModels, "ProductModelID", "Name", product.ProductModelID);
            return View(product);
        }

        //
        // GET: /WebGridDemo1/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductCategoryID = new SelectList(db.ProductCategories, "ProductCategoryID", "Name", product.ProductCategoryID);
            ViewBag.ProductModelID = new SelectList(db.ProductModels, "ProductModelID", "Name", product.ProductModelID);
            return View(product);
        }

        //
        // POST: /WebGridDemo1/Edit/5

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategoryID = new SelectList(db.ProductCategories, "ProductCategoryID", "Name", product.ProductCategoryID);
            ViewBag.ProductModelID = new SelectList(db.ProductModels, "ProductModelID", "Name", product.ProductModelID);
            return View(product);
        }

        //
        // GET: /WebGridDemo1/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // POST: /WebGridDemo1/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}