using LAB3.Classes;
using System;
using System.Collections.Generic;

namespace LAB3
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = new List<Student> { };
            int choice;

            do
            {
                Console.Write("\n");
                Console.WriteLine(" 1 - create new student.\n" +" 2 - show avg. final score.\n" +
                                " 3 - show median final score.\n" + " 4 - create random student.\n" + 
                                " 5 - load students from file.\n"  + " 6 - test students with different data.\n" +  " 0 - Exit.\n");
                Console.Write("What your choice: ");

                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 0:{
                            break;
                        }
                    case 1:
                        {
                            students.Add(Student.createStudent());
                            break;
                        }
                    case 2:
                        {
                            Student.drawTableNoMedian(students);
                            break;
                        }
                    case 3:
                        {
                            Student.drawTableWithMedian(students);
                            break;
                        }
                    case 4:
                        {
                            Student student = new Student();
                            students.Add(student);
                            break;
                        }
                    case 5:
                        {
                            List<Student> studentsFromFile = Student.addStudentsFromFile();
                            students.AddRange(studentsFromFile);
                            break;
                        }
                    case 6:
                        {
                            Student.generateStudents(1000);
                            Student.generateStudents(10000);
                            Student.generateStudents(100000);
                            Student.generateStudents(1000000);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Wrong choice");
                            break;
                        }
                }
            } while (choice != 0);


        }
    }
}
