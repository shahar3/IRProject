using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRProjectConsole
{
    class Parse
    {
        string docText;
        string docNum;
        string[] unfilteredTerms;
        List<string> stopWords;
        Stemmer stemmer;
        public static int counter = 0;

        public Parse(List<string> stopWords)
        {
            this.stopWords = stopWords;
            stemmer = new Stemmer();
        }

        private void tokenize()
        {
            unfilteredTerms = docText.Split(' ');
            filterTokens();
        }

        private void filterTokens()
        {
            for(int i=0;i<unfilteredTerms.Length;i++)
            {
                //change all the terms to lowercase and trim the blank spaces
                unfilteredTerms[i] = unfilteredTerms[i].Trim().ToLower();
                //perform stemming
                unfilteredTerms[i] = stemmer.stemTerm(unfilteredTerms[i]);
                //check for the rules for each one of the tokens
                numParsing(unfilteredTerms[i], i);
                //check if the token is empty and pass it
                if (unfilteredTerms[i] == "")
                {
                    continue;
                }
                //filter stopWords
                removeStopWords();
                //send to the Indexer
            }
        }

        private void removeStopWords()
        {
            int i = 0;
            foreach (string word in unfilteredTerms)
            {
                if (stopWords.Contains(word))
                {
                    unfilteredTerms[i] = "";
                }
                i++;
            }
        }

        private void numParsing(string token, int i)
        {
            double res = 0;
            string trimmedToken = token.Trim(','); //clear all the ',' and spaces
            if(Double.TryParse(trimmedToken,out res))
            {
                //check the next term if its a "million", "billion","trillion", percentage or $ sign
                if (i < unfilteredTerms.Length)
                {
                    //check the next term
                    string nextTerm = unfilteredTerms[i + 1];
                    switch (nextTerm)
                    {
                        case "million":
                            unfilteredTerms[i] = unfilteredTerms[i] + "M";
                            break;
                        case "billion":
                            unfilteredTerms[i] = res*1000 + "M";
                            break;
                        case "trillion":
                            unfilteredTerms[i] = res * 1000000 + "M";
                            break;
                        default:
                            break;
                    }
                }
                if (res > 1000000)
                {
                    //big number
                    double divideRes = (double)res / 1000000;
                    unfilteredTerms[i] = divideRes.ToString()+"M"; //add M to the end
                }
            }
        }

        public void parseDoc(string docNum, string docText)
        {
            this.docNum = docNum;
            this.docText = docText;
            //extract the tokens from the text
            tokenize();
        }
    }
}
