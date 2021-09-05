using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MVCCrud.Data;
using MVCCrud.Models;

namespace MVCCrud.Controllers
{
    public class BookController : Controller
    {
        private readonly IConfiguration _configuration;

        public BookController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        // GET: Book
        public IActionResult Index()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sqlconn = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                sqlconn.Open();
                SqlDataAdapter sda = new SqlDataAdapter("BookViewAll",sqlconn);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.Fill(dt);
            }
            return View(dt);
        }

       
        // GET: Book/AddOrEdit/
        public IActionResult AddOrEdit(int? id)
        {
            BookViewModel bookViewModel = new BookViewModel();
            if (id > 0)
                bookViewModel = FetchBookByID(id);
            
            return View(bookViewModel);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult  AddOrEdit(int id, [Bind("ID,Name,Author,price,CategoryID")] BookViewModel bookViewModel)
        {
         
            if (ModelState.IsValid)
                using(SqlConnection sqlconn = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                    sqlconn.Open();
                    SqlCommand sqlcmd = new SqlCommand("BookAddOrEdit", sqlconn);
                    sqlcmd.CommandType =CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("ID", bookViewModel.ID);
                    sqlcmd.Parameters.AddWithValue("Name", bookViewModel.Name);
                    sqlcmd.Parameters.AddWithValue("Author", bookViewModel.Author);
                    sqlcmd.Parameters.AddWithValue("Price", bookViewModel.price);
                    sqlcmd.Parameters.AddWithValue("CategoryID", bookViewModel.CategoryID);
                    sqlcmd.ExecuteNonQuery();
                    return RedirectToAction(nameof(Index));
                }
            return View();
        }

        // GET: Book/Delete/5
        public IActionResult Delete(int? id)
        {
            BookViewModel bookViewModel = new BookViewModel();
            bookViewModel = FetchBookByID(id);
            return View(bookViewModel);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection sqlconn = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                sqlconn.Open();
                SqlCommand sqlcmd = new SqlCommand("BooksDeleteByID", sqlconn);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.AddWithValue("ID",id);
                
                sqlcmd.ExecuteNonQuery();
               
            }
            return RedirectToAction(nameof(Index));
        }
        [NonAction]
        public BookViewModel FetchBookByID(int? id)
        {
            BookViewModel bookViewModel = new BookViewModel();
            using (SqlConnection sqlconn = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                DataTable dt = new DataTable();
                sqlconn.Open();
                SqlDataAdapter sda = new SqlDataAdapter("BookViewByID", sqlconn);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("ID", id);
                sda.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    bookViewModel.ID = Convert.ToInt32(dt.Rows[0]["ID"].ToString());
                    bookViewModel.Name = dt.Rows[0]["Name"].ToString();
                    bookViewModel.Author = dt.Rows[0]["Author"].ToString();
                    bookViewModel.price = Convert.ToInt32(dt.Rows[0]["Price"].ToString());
                    bookViewModel.CategoryID = Convert.ToInt32(dt.Rows[0]["CategoryID"].ToString());
                }
                return bookViewModel;


            }

        }

  
    }
}
