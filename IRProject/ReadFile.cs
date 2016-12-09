using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.IO;

namespace IRProject
{
    /// <summary>
    /// This class reads the documents from the corpus. 
    /// The class gets a path and seperates the files
    /// </summary>
    class ReadFile
    {
        private string path;
        private Queue<string> fileNames;

        public ReadFile()
        {
            //1.get the path
            getPath();
            //2.create a queue of the file names
            fileNames = new Queue<string>();
            foreach (string file in Directory.GetFiles(path))
            {
                Console.WriteLine(file);
                fileNames.Enqueue(file);
            }
            //3.iterate on the queue and create a thread for each one of the file names

        }

        /// <summary>
        /// Get the absulute path of the corpus
        /// </summary>
        private void getPath()
        {
            do
            {
                Console.WriteLine("Enter the files path");
                path = Console.ReadLine();
            } while (!Directory.Exists(path));
        }
    }
}
