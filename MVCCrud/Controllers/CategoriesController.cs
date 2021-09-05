using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCCrud.Data;
using MVCCrud.Models;

namespace MVCCrud.Controllers
{
    public class CategoriesController : Controller
    {
        

        public CategoriesController()
        {
            
        }

        // GET: Categories
        public IActionResult Index()
        {
            return View();
        }

   

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ID,Name")] CategoriesViewModel categoriesViewModel)
        {
            if (ModelState.IsValid)
            {
                
                return RedirectToAction();
            }
            return View(categoriesViewModel);
        }

        

        // GET: Categories/Delete/5
        public IActionResult Delete(int? id)
        {
           

            return View();
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            
            return RedirectToAction();
        }
    }
}
