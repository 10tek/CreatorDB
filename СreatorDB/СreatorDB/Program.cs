using System;
using System.Collections.Generic;
using System.Reflection;

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

            foreach (var type in assembly.GetTypes())
            {
                if(type.Name == "Program")
                {
                    break;
                }
                script += $"\nCREATE Table {type.Name}\n(\n  ";
                foreach (var memberInfo in type.GetMembers())
                {
                    Console.WriteLine($"{memberInfo.Name} ----------- {memberInfo.GetType()} ");

                    if (memberInfo is PropertyInfo)
                    {
                        script += $@"{memberInfo.Name} ";
                        var propertyInfo = memberInfo as PropertyInfo;
                        script += $"{types[propertyInfo.PropertyType.ToString()]}";
                        //Console.WriteLine(propertyInfo.PropertyType + "123123123123123123123");
                    }
                }
            }
            Console.WriteLine(script);
            Console.ReadKey();
        }
    }
}
