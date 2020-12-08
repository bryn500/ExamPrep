using Microsoft.EntityFrameworkCore;
using System;

namespace Exceptions
{
    class Program
    {
        static void Main(string[] args)
        {
            var exception = new CustomException("My Message");
            //var inner = new Exception("Inner Exception");
            //var exception = new NotImplementedException("My Message", inner);
            var exception2 = new Exception("My Message");

            try
            {
                throw exception;
            }
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}           
            catch (CustomException ex)
            {
                Console.WriteLine("CustomException " + ex.Message);                
            }
            catch (NotImplementedException ex)
            {
                Console.WriteLine("NotImplemented:" + ex.Message);
                Console.WriteLine("NotImplemented Inner:" + ex.InnerException.Message);
            }
            //catch(DbUpdateException ex)
            //{
            //    Console.WriteLine("DbUpdateException " + ex.Message);
            //}
            catch (Exception ex)
            {
                Console.WriteLine("Exception " + ex.Message);
            }
        }
    }


    public class CustomException : NotImplementedException
    {
        public CustomException()
        {
        }

        public CustomException(string message)
            : base(message)
        {
        }

        public CustomException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
