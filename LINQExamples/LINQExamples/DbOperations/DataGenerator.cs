using LINQExamples.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQExamples.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize()
        {
            using (var context = new LinqDbContext())
            {
                if (context.Students.Any())
                {
                    return;
                }
                context.Students.AddRange(
                    new Student()
                    {
                        StudentId = 1,
                        Name = "Emre",
                        Surname = "Gelen",
                        ClassId = 1
                    }, 
                    new Student()
                    {
                        StudentId = 2,
                        Name = "Deniz",
                        Surname = "Arda",
                        ClassId = 1
                    }, 
                    new Student()
                    {
                        StudentId = 3,
                        Name = "Umut",
                        Surname = "Arda",
                        ClassId = 2
                    },
                    new Student()
                    {
                        StudentId = 4,
                        Name = "Ayşe",
                        Surname = "Fatma",
                        ClassId = 2
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
