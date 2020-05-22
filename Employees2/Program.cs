using Employees2.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Employees2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Enter full file path: ");
                string path = Console.ReadLine();

                Console.Write("Enter minimum salary: ");
                double minSalary = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

                List<Employee> list = new List<Employee>();
                using (StreamReader sr = File.OpenText(path))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] fields = sr.ReadLine().Split(',');

                        string name = fields[0];
                        string email = fields[1];
                        double salary = double.Parse(fields[2], CultureInfo.InvariantCulture);
                        list.Add(new Employee(name, email, salary));
                    }
                }

                Console.WriteLine($"Full list:");
                foreach (var item in list)
                {
                    Console.WriteLine(item);
                }

                Console.WriteLine();

                var emails = from x in list
                             where x.Salary > minSalary
                             orderby x.Email
                             select x.Email;

                Console.WriteLine($"Email of people whose salary is more than {minSalary.ToString("F2", CultureInfo.InvariantCulture)} ordered by email:");
                foreach (var item in emails)
                {
                    Console.WriteLine(item);
                }

                Console.WriteLine();

                var sum = (from x in list
                           where x.Name[0].Equals('M')
                           select x.Salary)
                              .DefaultIfEmpty(0.0)
                              .Sum();

                Console.WriteLine($"Sum of salary of people whose name starts with 'M': {sum.ToString("F2", CultureInfo.InvariantCulture)}");
            }
            catch (FormatException e)
            {
                Console.WriteLine($"Formatting error: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }

        }
    }
}
