using System.Net;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace st.Controllers;
public class HomeController : Controller
{
    static int count = 10;
    public static IList<ProductList> Carts = new List<ProductList>();
    static IList<Product> productList = new List<Product>{ 
        new Product() { Id = 1, Name = "Hamburger", Price = 20000 } ,
        new Product() { Id = 4, Name = "Somsa" , Price = 6000 } ,
        new Product() { Id = 5, Name = "Osh" , Price = 25000 } ,
        new Product() { Id = 6, Name = "Banan" , Price = 15000 } ,
        new Product() { Id = 7, Name = "Olma" , Price = 10000 } 
    };
            
    public ActionResult Index() 
    {
        return View(productList.OrderBy(s => s.Id).ToList());
    }

    public ActionResult Edit(int Id)
    { 
        var std = productList.Where(s => s.Id == Id).FirstOrDefault();

        return View(std);
    }

    [HttpPost]
    public ActionResult Edit(Product prd)
    {
        var product = productList.Where(s => s.Id == prd.Id).FirstOrDefault();
        productList.Remove(product);
        productList.Add(prd);

        return RedirectToAction("Index");
    }
    [HttpGet]
    public ActionResult Details(int Id)
    { 
        var std = productList.Where(s => s.Id == Id).FirstOrDefault();

        return View(std);
    }
    
    public ActionResult Create()
    {
        return View();
    }

    public ActionResult DeleteCart(int id)
    {
        var std = Carts.Where(s => s.Id == id).FirstOrDefault();
        Carts.Remove(std);
        return RedirectToAction("Cart");
    }

    public ActionResult Delete(int id)
    {
        var std = productList.Where(s => s.Id == id).FirstOrDefault();
        productList.Remove(std);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult Create(Product std)
    {
        std.Id = count++;
        productList.Add(std);
        return RedirectToAction("Index");
    }

    public ActionResult Cart()
    {
        return View(Carts);
    }

    [HttpPost]
    public ActionResult Cart(ProductList std)
    {
        var cart = Carts.Where(s => s.Id == std.Id).FirstOrDefault();
        Carts.Remove(cart);
        Carts.Add(cart);
        return RedirectToAction("Cart");
    }

    public ActionResult CartPost(int id)
    {
        if(Carts.Any(i => i.product.Id == id))
        {
            Carts.Where(s => s.product.Id == id).FirstOrDefault().Count+=1;
        }
        else
        {
            var std = productList.Where(s => s.Id == id).FirstOrDefault();
            var cartItem = new ProductList() { Count =1, product = std, Id = count++};
            Carts.Add(cartItem);
        }
        return RedirectToAction("");
    }

}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set;  }
    public int Price { get; set;  }
}

public class ProductList
{
    public int Id { get; set; }
    
    public int Count { get; set; }
    
    public Product product { get; set; }
}