using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExampleQuestions
{
    public class Program
    {
        //1. You are developing an application that will transmit large amounts of data between a client computer and a server.
        //You need to ensure the validity of the data by using a cryptographic hashing algorithm.
        //Which algorithm should you use?
        //A.RSA
        //B.HMACSHA256
        //C.AES
        //D.RNGCryptoServiceProvider
        #region Question 1
        // What is RSA: (algorithm + class)
        /// is an algorithm used to encrypt and decrypt messages. 
        /// It is an asymmetric cryptographic algorithm. 
        /// Asymmetric means that there are two different keys. This is also called public key cryptography, because one of the keys can be given to anyone

        // What is AES (algorithm + class)
        /// Advanced Encryption Standard
        /// It is an symmetric cryptographic algorithm
        /// Symmetric means both parties have a shared secret to encrypt with already

        // What is HMACSHA256 (algorithm + class)
        /// keyed hash algorithm that is constructed from the SHA-256 hash function and used as a Hash-based Message Authentication Code 
        /// You send message and hash of the message calucualted with a predefined key
        /// Reciever then can ensure that the message and hash match by re-hashing the message with the key
        /// Any change to the data or the hash value results in a mismatch.
        /// Knowledge of the secret key is required to change the message and reproduce the correct hash value.
        /// Therefore, if the original and computed hash values match, the message is authenticated.

        // What is RNGCryptoServiceProvider (class)
        /// Cryptographic Random Number Generator (RNG)
        /// A class that can generate a cryptographically secure random number
        /// Random numbers are hard
        #endregion

        //2. You are developing an application.The application contains the following code:
        //01 ArrayList array1 = new ArrayList();
        //02 int var1 = 10;
        //03 int var2;
        //04 array1.add(var1);
        //05 var2 = array1[0];

        //When you run the code, you receive the error message: 
        //“Cannot implicitly covert type ‘object’ to ‘int’. An explicit conversion exists”

        //You need to ensure the code can be compiled.
        //Which code should you use to replace at line 05?
        //A.var2 = array1[0] as int;
        //B.var2 = ((List<int>)array1[0];
        //C.Var 2 = array1[0].Equals(typeof(int));
        //D.var2 = (int) array1[0];
        #region Question 2
        public int ArrayList()
        {
            ArrayList array1 = new ArrayList();
            int var1 = 10;
            int var2 = 0;
            array1.Add(var1);

            //var2 = array1[0] as int;
            //var2 = (List<int>)array1[0];
            //var2 = array1[0].Equals(typeof(int));
            //var2 = (int) array1[0];

            return var2;
        }
        #endregion

        //3. You are writing the following method:
        //01 public T CreateObject();
        //02
        //03 {
        //04 	T obj = new T();
        //05	return obj;
        //06 }

        //You need to ensure the code can compile successfully.
        //What should you do?

        //A. Insert the following code at line 02: where T : new()
        //B.Replace line 01 with the following code: public void CreateObject<T>()
        //C.Replace line 01 with the following code: public Object CreateObject<T>()
        //D.Insert the following code at line 02: where T : Object
        #region Question 3
        // what does T represent in line 1?
        // where is a constraint for a generic method, what is the constraints enforcing?
        //public T CreateObject()
        //{
        //    T obj = new T();
        //    return obj;
        //}

        // What I would do...
        public T CreateObject<T>() where T : new()
        {
            T obj = new T();

            return obj;
        }
        #endregion

        //4.You are developing an application that will parse a large amount of text.
        //You need to parse the text into separate lines and minimize memory use while processing data. 
        //Which object type should you use?

        //A. StreamReader
        //B. StreamWriter
        //C. StringReader
        //D. StringBuilder
        #region Question 4
        public void Question4()
        {
            using (var sr = new StreamReader("TestFile.txt"))
            {
                string aLine = sr.ReadLine();
            }

            using (var sw = new StreamWriter("TestFile.txt"))
            {
                sw.WriteLine("A new line");
            }

            using (var sr = new StringReader("Hello, I'm a string"))
            {
                string aLine = sr.ReadLine();
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("hello");
            sb.Append(", hello again");
            sb.AppendFormat("{0}, {1}", "one", "two");

            sb.ToString();

        }
        #endregion
    }
}
