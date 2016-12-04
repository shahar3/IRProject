using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRProject
{
    /// <summary>
    /// This class reads the documents from the corpus. 
    /// The class gets a path and seperates the files
    /// </summary>
    class ReadFile
    {
        private string path;

        public ReadFile()
        {
            //1.get the path
            Console.WriteLine("Enter the files path");
            path = Console.ReadLine();
            //2.create a queue of the file names
            //3.iterate on the queue and create a thread for each one of the file names
        }

    }
}
