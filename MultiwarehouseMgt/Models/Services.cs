using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MultiwarehouseMgt.Models
{
    public class Services
    {
        private Entities db = new Entities();


        public List<Book> GetBook()
        {
            var allbooks = db.Books.ToList();
            return allbooks;
        }

        public Book GetBooksByBookId( int id)
        {
            //db.Configuration.ProxyCreationEnabled = false;

            var allbooks = db.Books.Where(j=>j.Id==id).FirstOrDefault();
            return allbooks;
        }

        public List<StatesTable> GetStates()
        {
            //db.Configuration.ProxyCreationEnabled = false;
            List<StatesTable> allstate = db.StatesTables.ToList();
            return allstate;
        }

        internal Book GetBooksByBookName(string name)
        {
            var allbooks = db.Books.Where(j => j.Name == name).FirstOrDefault();
            return allbooks;
        }

        internal bool IsQuantityOk(string bookName, int? quantity)
        {
            bool result = false;
            try
            {
                var book = GetBooksByBookName(bookName);
                if (book != null && book.Quantity.HasValue)
                {
                    switch (quantity<book.Quantity)
                    {
                        case true :
                            result = true;
                           
                            break;

                        case false:
                            result = false;
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {

                var log = ex;
            }

            return result;
        }

        internal bool ReduceQuantity(string bookName, int? quantity)
        {
            bool result = false;
            try
            {
                var book = GetBooksByBookName(bookName);
                if (book != null && book.Quantity.HasValue)
                {
                    switch (quantity < book.Quantity)
                    {
                        case true:
                            result = true;
                            book.Quantity = (-quantity + (book.Quantity));
                            UpdateBook(book);
                            break;

                        case false:
                            result = false;
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {

                var log = ex;
            }

            return result;
        }



        public void UpdateBook(Book book)
        {
            db.Entry(book).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}