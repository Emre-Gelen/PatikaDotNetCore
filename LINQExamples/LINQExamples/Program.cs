using LINQExamples.DbOperations;
using LINQExamples.Entities;
using System;
using System.Linq;

namespace LINQExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            DataGenerator.Initialize();
            LinqDbContext _context = new LinqDbContext();
            var students = _context.Students.ToList<Student>();

            //Find()
            Console.WriteLine("**** Find ****");
            var student = _context.Students.Find(2);
            Console.WriteLine(student.Name);

            //FirstOrDefault()
            Console.WriteLine("\n**** FirstOrDefault ****");
            student = _context.Students.FirstOrDefault(f => f.Surname == "Arda");
            Console.WriteLine(student.Name);

            //SingleOrDefault()
            Console.WriteLine("\n**** SingleOrDefault ****");
            student = _context.Students.SingleOrDefault(s => s.Name == "Deniz");
            Console.WriteLine(student.Name);
            /*student = _context.Students.SingleOrDefault(s => s.Surname == "Arda");*/ // This  line throws error.

            //ToList()
            Console.WriteLine("\n**** ToList ****");
            var studentList = _context.Students.Where(w => w.ClassId == 2).ToList();
            Console.WriteLine(studentList.Count);

            //OrderBy()
            Console.WriteLine("\n**** OrderBy ****");
            students = _context.Students.OrderBy(o => o.StudentId).ToList();
            foreach (var item in students)
            {
                Console.WriteLine(item.StudentId + " - " + item.Name + " - " + item.Surname);
            }

            //OrderByDescending()
            Console.WriteLine("\n**** OrderByDescending ****");
            students = _context.Students.OrderByDescending(o => o.StudentId).ToList();
            foreach (var item in students)
            {
                Console.WriteLine(item.StudentId + " - " + item.Name + " - " + item.Surname);
            }

            //Anonymous Object Result
            Console.WriteLine("\n**** Anonymous Object Result ****");

            var anonymousObject = _context.Students
                                .Where(w => w.ClassId == 2)
                                .Select(x => new
                                {
                                    Id = x.StudentId,
                                    FullName = x.Name + " " + x.Surname
                                });

            foreach (var item in anonymousObject)
            {
                Console.WriteLine(item.Id + " - " + item.FullName);
            }
        }
    }
}
