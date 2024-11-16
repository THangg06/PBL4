using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebMVC.Models;
using System.Linq; 
using Microsoft.EntityFrameworkCore; 
using Microsoft.AspNetCore.Mvc.Rendering;

using WebMVC.Helper;

namespace WebMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context, ILogger<ProductController> logger)
        {
            _logger = logger;
            _context = context;
        }

       
       public async Task<IActionResult> Index(int CatID = 0)
        {
            IQueryable<Product> products = _context.Products;

            if (CatID != 0)
            {
                products = products.Where(x => x.CatID == CatID);
            }

            var sortedProducts = await products.OrderByDescending(x => x.ProductID).ToListAsync();

            return View(sortedProducts);
        }





       public IActionResult Add()
{
    var categories = _context.Categories.ToList();

    // Khởi tạo SelectList nếu danh sách danh mục hợp lệ
    ViewBag.CategoryList = new SelectList(categories, "CatID", "CatName");

    return View();
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Add([Bind("ProductID,ProductName,CatID,Price,Thumb")] Product product, IFormFile fThumb)
{
    if (ModelState.IsValid)
    {
        product.ProductName = MyUtil.ToTitleCase(product.ProductName);

        if (fThumb != null)
        {
            string image = Path.GetFileNameWithoutExtension(fThumb.FileName) + Path.GetExtension(fThumb.FileName);
            product.Thumb = await MyUtil.UploadFile(fThumb, image.ToLower());
        }

        if (string.IsNullOrEmpty(product.Thumb))
        {
            product.Thumb = "default.jpg";
        }
    
        _context.Add(product);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // Khởi tạo danh sách danh mục lại nếu ModelState không hợp lệ
    ViewBag.CategoryList = new SelectList(_context.Categories, "CatID", "CatName", product.CatID);

    return View(product);
}
public async Task<IActionResult> Edit(int id)
{
    if (id == 0)
    {
        return NotFound();
    }

    var product = await _context.Products.FindAsync(id);
    if (product == null)
    {
        return NotFound();
    }

    // Chuyển ViewBag thành ViewData
    ViewData["Danhmuc"] = new SelectList(_context.Categories, "CatID", "CatName", product.CatID);
    return View(product);
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(int id, [Bind("ProductID,ProductName,CatID,Price,Thumb")] Product product, IFormFile fThumb)
{
    if (id != product.ProductID)
    {
        return NotFound();
    }

    string tempThumb = product.Thumb;

    try
    {
        if (ModelState.IsValid)
        {
            product.ProductName = MyUtil.ToTitleCase(product.ProductName);

            if (fThumb != null)
            {
                string extension = Path.GetExtension(fThumb.FileName);
                string image = Path.GetFileNameWithoutExtension(fThumb.FileName) + extension;

                product.Thumb = await MyUtil.UploadFile(fThumb, image.ToLower());
            }
            else
            {
                product.Thumb = tempThumb;
            }

            if (string.IsNullOrEmpty(product.Thumb))
            {
                product.Thumb = "default.jpg";
            }

            _context.Update(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!ProductExists(product.ProductID))
        {
            return NotFound();
        }
        else
        {
            throw;
        }
    }

    ViewData["Danhmuc"] = new SelectList(_context.Categories, "CatID", "CatName", product.CatID);
    return View(product);
}

private bool ProductExists(int id)
{
    return _context.Products.Any(e => e.ProductID == id);
}



    }
}
