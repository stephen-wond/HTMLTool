//using System;
//using System.Collections.Generic;
//using System.ComponentModel.Design;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using HTMLToolLogic;

//namespace HTMLTool
//{
//    class Program
//    {
//        public static Logic logic = new Logic();
//        public static List<string> files = new List<string>();

//        static void Main(string[] args)
//        {
//            files.Add(@"C:\Users\wonds\Desktop\PostilionTraceTimer\traces\IMovoSnk_1.html");
//            //files.Add(@"C:\Users\wonds\Desktop\PostilionTraceTimer\traces\IMovoSnk_56.html");
//            //files.Add(@"C:\Users\wonds\Desktop\PostilionTraceTimer\traces\IMovoSnk_57.html");
//            //files.Add(@"C:\Users\wonds\Desktop\PostilionTraceTimer\traces\IMovoSnk_58.html");
//            var abc = logic.GetResults(files);
//            foreach (var result in abc)
//            {
//                Console.WriteLine(result.Average);
//            }
//            Console.ReadKey();
//        }
//    }
//}
