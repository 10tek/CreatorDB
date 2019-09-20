using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace СreatorDB
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly assembly = Assembly.LoadFile(@"C:\Users\ОралбаевГ\source\repos\TestApp\TestApp\bin\Debug\TestApp.exe");
            //Assembly assembly = Assembly.LoadFile(Console.ReadLine());
            var script = "CREATE Database asd;";
            var types = new Dictionary<string, string>();
            types.Add("System.String", "nvarchar(max)");
            types.Add("System.Guid", "uniqueidentifier");
            types.Add("System.DateTime", "date");
            types.Add("System.Boolean", "BIT");
            types.Add("System.Int64", "bigint");
            types.Add("System.Byte", "byte");

            foreach (var type in assembly.GetTypes())
            {
                if (type.Name == "Program")
                {
                    break;
                }
                script += $"\nCREATE Table {type.Name}\n(";
                foreach (var memberInfo in type.GetMembers())
                {
                    Console.WriteLine($"{memberInfo.Name} ----------- {memberInfo.GetType()} ");

                    if (memberInfo is PropertyInfo)
                    {
                        script += $"\n  {memberInfo.Name} ";
                        var propertyInfo = memberInfo as PropertyInfo;
                        if (!propertyInfo.PropertyType.ToString().Contains("Nullable"))
                        {
                            script += $"{types[propertyInfo.PropertyType.ToString()]} not null";
                        }
                        else
                        {
                            script += $"{types[DeleteNullable(propertyInfo.PropertyType.ToString())]}";
                        }
                        Console.WriteLine(script);
                        //Console.WriteLine(propertyInfo.PropertyType + "123123123123123123123");
                    }
                }
            }
            Console.ReadKey();
        }

        public static string DeleteNullable(string text)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var beginIndex = text.LastIndexOf('[');
            var endIndex = text.LastIndexOf(']') - 1;
            while (beginIndex < endIndex)
            {
                beginIndex++;
                stringBuilder.Append(text[beginIndex]);
            }
            var txt = stringBuilder.ToString();
            return txt;
        }
    }
}
