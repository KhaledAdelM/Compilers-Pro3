using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Com.Models;

namespace Com.Controllers
{
   public class Stack
    {
        private string[] ele;
        private int top;
        private int max;
        public Stack(int size)
        {
            ele = new string[size];
            top = -1;
            max = size;
        }

        public void push(string item)
        {
            if (top == max - 1)
            {
                Console.WriteLine("Stack Overflow");
                return;
            }
            else
            {
                ele[++top] = item;
            }
        }

        public void pop()
        {
            if (top == -1)
            {
                Console.WriteLine("Stack Underflow");
                return;
            }
            else
            {
                top--;
            }
        }
        public void Clear()
        {
            top = -1;
        }
        public bool isEmpty()
        {
            return top > -1;
        }
        public string retTop()
        {
            if (top == -1)
            {
                return "";
            }
            else
            {
                return ele[top];
            }
        }

    }


    public class Trie
    {

        
        static readonly int maxSize = 266;   
       
        public class TrieNode 
        {
            public TrieNode[] children = new TrieNode[maxSize];

            // null >>> Z 266 digit 
            // A == ascii 65 a == 97  { >> } [ ] null 32 >> 255  

            // isEndOfWord is true if the node represents  ipok >> ipof >> float 
            // end of a word
            // ipok ipokf
            public bool isEndOfWord;
            public string retToken;

            public TrieNode()
            {
                isEndOfWord = false;
                for (int i = 0; i < maxSize; i++)
                    children[i] = null;
            }
        };

       public  TrieNode root;
       public static Stack stk = new Stack(200);
        // If not present, inserts key into trie
        // If the key is prefix of trie node,
        // just marks leaf node
         public void insert(String key, String ret)
        {
            int level;
            int length = key.Length; // ipok f >> float
            int index;
            // a b c  >> 
            // 0 1 2 A >> 65 null 32 
            // A 65  >> 65-32 
            // B 66   66 - 32 

            TrieNode curNode = root;

            for (level = 0; level < length; level++)
            {
                index = key[level] - 32; // key[0] >> S key[1] = l  solw >> slowf // '0' == 48 

                if (curNode.children[index] == null)
                    curNode.children[index] = new TrieNode(); // create a node 

                curNode = curNode.children[index];
            }

            // mark last node as leaf
            curNode.isEndOfWord = true;
            curNode.retToken = ret;
        }

        // Returns true if key
        // presents in trie, else false
        public  bool ValidState(String key, int n)
        {
            int level;
            int length = key.Length;
            int index;
            TrieNode curNode = root;

            for (level = 0; level < length; level++)
            {
                index = key[level] - 32;

                if (curNode.children[index] == null)
                    return false;

                curNode = curNode.children[index];
            }
            if (curNode.isEndOfWord == true) output(key, curNode.retToken, n);   //Console.WriteLine(pCrawl.retToken);

            return (curNode.isEndOfWord);
        }
        public  bool onlySearch(String key)
        {
            int level;
            int length = key.Length;
            int index;
            TrieNode curNode = root;

            for (level = 0; level < length; level++)
            {
                index = key[level] - 32;

                if (curNode.children[index] == null)
                    return false;

                curNode = curNode.children[index];
            }
            // if (pCrawl.isEndOfWord == true) output(key, pCrawl.retToken, n);   //Console.WriteLine(pCrawl.retToken);

            return (curNode.isEndOfWord);
        }
        public  void output(string tokenText, string tokenType, int n)
        {

            string s = "Line" + " : " + n + " Token Text: " + tokenText + "\t" + "Token Type" + ": " + tokenType;
            //  Console.WriteLine(s);
            stk.push(s);
        }

        public  void takeKeyWords()
        {

            string filePath = @"C:\Users\20115\Desktop\in.txt";
            string[] lines = File.ReadAllLines(filePath);
            foreach (string curLine in lines)
            {
                int sizeOfCurLine = curLine.Length;
                string tokenTaken = "";
                string tokenReturn = "";
                bool flag = true;
                for (int i = 0; i < sizeOfCurLine; i++)
                {
                    if (curLine[i] == ' ') flag = false;
                    if (flag) tokenTaken += curLine[i];
                    else if (curLine[i] != ' ') tokenReturn += curLine[i];
                }
                insert(tokenTaken, tokenReturn);
            }

        }
        public  bool validString(string str)
        {
            //Console.WriteLine(str);
            if (str[0] == '\"' && str.EndsWith("\"") ) return true;

            else return false;
        }
        public bool validchar(string str)
        {
            //Console.WriteLine(str); 
            if (str[0] == '\'' && str.EndsWith("\'") && str.Length==3) return true;

            else return false;
        }
        public  bool validConstant(string str) // float x = 5.5 >>
        {

            bool isValid = true; int dotIndex = -1;
            for (int i = 0; i < str.Length; i++)
            {
                // check if the inString is float type or not 
                if (str[i] == '.') dotIndex = i;
                if ((str[i] < '0' && str[i] > '9' && str[i] != '.') || (str[i] == '.' && i == 0)) isValid = false;
            }
            if (str.Length > 18 && dotIndex == -1) return false;
            if (dotIndex > 16 || str.Length - dotIndex > 16) return false;
            return isValid;


        }

        public  int valid_number(string str)
        {
            int i = 0, j = str.Length - 1;


            while (i < str.Length && str[i] == ' ')
                i++;
            while (j >= 0 && str[j] == ' ')
                j--;

            if (i > j)
                return 0;


            if (i == j && !(str[i] >= '0' && str[i] <= '9'))
                return 0;


            if (str[i] != '.' && str[i] != '+'
                && str[i] != '-' && !(str[i] >= '0' && str[i] <= '9'))
                return 0;


            bool flagDotOrE = false;

            for (; i <= j; i++)
            {

                if (str[i] != 'e' && str[i] != '.'
                    && str[i] != '+' && str[i] != '-'
                    && !(str[i] >= '0' && str[i] <= '9'))
                    return 0;

                if (str[i] == '.')
                {

                    if (flagDotOrE == true)
                        return 0;

                    if (i + 1 > str.Length)
                        return 0;

                    if (!(str[i + 1] >= '0' && str[i + 1] <= '9'))
                        return 0;
                }

                else if (str[i] == 'e')
                {

                    flagDotOrE = true;


                    if (!(str[i - 1] >= '0' && str[i - 1] <= '9'))
                        return 0;

                    if (i + 1 > str.Length)
                        return 0;


                    if (str[i + 1] != '+' && str[i + 1] != '-'
                        && (str[i + 1] >= '0' && str[i] <= '9'))
                        return 0;
                }
            }

          
            return 1;
        }
        public  bool isValidID(String str)
        {

            if (onlySearch(str) || str == "") return false;

            if (!((str[0] >= 'a' && str[0] <= 'z')
                || (str[0] >= 'A' && str[0] <= 'Z')
                || str[0] == '_'))
                return false;

            for (int i = 1; i < str.Length; i++)
            {
                if (!((str[i] >= 'a' && str[i] <= 'z')
                    || (str[i] >= 'A' && str[i] <= 'Z')
                    || (str[i] >= '0' && str[i] <= '9')
                    || str[i] == '_'))
                    return false;
            }
            return true;
        }
        //static public string[] ids;
       
        static bool listContains(String str, List<string> Idslist )
        {
            foreach (string i in Idslist)
            {
                if (i == str) return true;
            }
            return false;

        }
        public  string PrintId(string str, int n)
        {
            return ("Line" + " : " + n + " Token Text: " + str + "\t" + "Token Type" + ": " + "Identifier");
           
        }
        public string PrintComment(string str, int n)
        {
            return ("Line" + " : " + n + " rest of the line is a " + str + "\t" );

        }
        public  string printError(string str, int n)
        {
            return ("Line" + " : " + n + " Invalid token Text or Identifier: " + str);
            
        }
        public  string Resize(string s)
        {
            int sz = s.Length;
            while (s[s.Length - 1] == ' ')
            {
                s = s.Remove(s.Length - 1);
            }

            return s + " ";
        }
         public String[] operators = { "+", "-", "*", "/", "&&", "||", "~", "==", ">", "<", "!=", "<=", ">=", "->", "=", ";" };
        public  bool isOperator(string s)
        {
            for (int i = 0; i < operators.Length; i++)
            {
                if (s == operators[i]) return true;
            }
            return false;
        }
        public List<string> OutPutList = new List<string>();
        public List<string> Scan(string filePath)
        {
           // string filePath = @"C:\Users\20115\Desktop\text.txt";
            OutPutList.Clear();
            string[] lines = File.ReadAllLines(filePath);
            int noCurline = 0; int noErrors = 0;
            foreach (string curLine in lines)
            {

                string scanedLine = Resize(curLine);
                noCurline++;   // 0>> false state , 1 Ac state        
                int sizeOfCurLine = scanedLine.Length;
                string curWord = "";
         
               
                for (int i = 0; i < sizeOfCurLine; i++)
                {
                    // if (noCurline == 1 && validStart == '@') continue;
                    
                    curWord += scanedLine[i];  // Type    
                    if (curWord == "***")
                    {
                        OutPutList.Add(PrintComment("comment", noCurline));
                        break;
                    }
                    ValidState(curWord, noCurline);
                    

                    if (scanedLine[i] == ' ') //ID " "  valid const ipokf (': *
                    {
                        
                        string output = stk.retTop();
                        if (curWord[curWord.Length - 1] == ' ') curWord = curWord.Remove(curWord.Length - 1);

                        if (output != "" && onlySearch(curWord)) // just to check last saved word (keyword)
                        {
                          
                            OutPutList.Add(output);
                            Console.WriteLine(output);
                        }
                        else if (isValidID(curWord))
                        {
                            OutPutList.Add(PrintId(curWord, noCurline));
                        }
                        else if (curWord == "***") continue;
                        else if (!isOperator(curWord) && !(valid_number(curWord) == 1) && !validString(curWord) &!validchar(curWord))
                        {
                            OutPutList.Add(printError(curWord, noCurline) );
                            noErrors++;
                        }
                        stk.Clear();
                        curWord = "";
                    }

                }

            }
            string LastLine = "Total NO of errors: " + noErrors.ToString();
            OutPutList.Add(LastLine);
            return OutPutList;
        }
    }

    
        public class HomeController : Controller
    {
       
      
    
        public void fun(string Text)
      {
            Trie trie = new Trie();
            trie.root = new Trie.TrieNode();
            trie.takeKeyWords();
            List<string> StList = new List<string>();
            string Line = ""; int i = 0;
            int Len = Text.Length;
            while (i < Len)
            {
                if (Text[i] == 13)
                {
                    StList.Add(Line);
                    i++;
                    Line = "";
                }
                else
                {
                    Line = Line + Text[i];
                }
                i++;
            }
            StList.Add(Line);
            System.IO.File.WriteAllLines(HostingEnvironment.MapPath(@"~/App_Data/scanedCode.txt"), StList);
            List<string> ScannerOutput = new List<string>();
            
            ScannerOutput = trie.Scan(@"C:\Users\20115\Desktop\Compiler project\Com\Com\App_Data\scanedCode.txt");
            Index(ScannerOutput);

      }
        public void funFromFile(string filePath)
        {
            Trie trie = new Trie();
            trie.root = new Trie.TrieNode();
            trie.takeKeyWords();

            List<string> ScannerOutput = new List<string>();
            
            ScannerOutput = trie.Scan(filePath);
            Index(ScannerOutput);

        }

        public void funPath(string path)
        {


            //Index(path);
        }
        public ActionResult Index(List<string> LST )
        {


            ViewBag.xc = LST;
            return View();
        }
        [HttpPost]
        public ActionResult Index(Script script)
        {
            try
            {
                 
                if (script.MyText != null)
                    fun(script.MyText);
                else if (script.MyText == null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(script.FilePost.FileName);
                    string extension = Path.GetExtension(script.FilePost.FileName);
                    script.PaperFile = @"C:\Users\20115\Desktop\Compiler project\Com\Com\File\" + fileName+extension;
                    funFromFile(script.PaperFile);
                }
                else
                    fun("Error");

                return View();
            }
            catch
            {

                return View();
            }
        }


        public ActionResult IndexFile(String tt)
        {


            ViewBag.xc = tt;
            return View();
        }
        [HttpPost]
        public ActionResult IndexFile(Script script)
        {
            try
            {
                string fileName = Path.GetFileNameWithoutExtension(script.FilePost.FileName);
                string extension = Path.GetExtension(script.FilePost.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                script.PaperFile = "~/File/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/File/"), fileName);
                script.FilePost.SaveAs(fileName);

                funPath(script.PaperFile);


                return View();
            }
            catch
            {

                return View();
            }
        }
    }
}