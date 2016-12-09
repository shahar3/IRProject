using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.IO;
using System.Threading;
using System.Collections.Concurrent;

namespace IRProjectConsole
{
    /// <summary>
    /// This class reads the documents from the corpus. 
    /// The class gets a path and seperates the files
    /// </summary>
    class ReadFile
    {
        private string path;
        private ConcurrentQueue<string> fileNames;
        private const int WORKING_THREADS = 8, COMP_THREADS = 8;

        public ReadFile()
        {
            //1.get the path
            getPath();
            //2.create a queue of the file names
            fileNames = new ConcurrentQueue<string>();
            foreach (string file in Directory.GetFiles(path))
            {
                fileNames.Enqueue(file);
            }
            //3.iterate on the queue and create a thread for each one of the file names
            splitDocsThreadPool(fileNames);
        }

        private void splitDocsThreadPool(ConcurrentQueue<string> fileNames)
        {
            ThreadPool.SetMaxThreads(WORKING_THREADS, COMP_THREADS);
            for (int i = 0; i < fileNames.Count; i++)
            {
                string fileName;
                bool flag = fileNames.TryDequeue(out fileName);
                //create a thread for processing the file
                ThreadPool.QueueUserWorkItem((splitDocs) =>
                {
                    splitFile(fileName);
                });
            }
        }

        /// <summary>
        /// work in a thread
        /// </summary>
        /// <param name="fileName"></param>
        private void splitFile(string fileName)
        {
            HtmlDocument fileDoc = new HtmlDocument();
            //load the html from the file
            fileDoc.Load(fileName);
            HtmlNode[] docNodes = fileDoc.DocumentNode.SelectNodes("//doc").ToArray();
            foreach (HtmlNode docNode in docNodes)
            {
                //get the doc tag name
                Console.WriteLine(docNode.SelectNodes(".//docno")[0].InnerText);
                //send to the parser
            }
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
