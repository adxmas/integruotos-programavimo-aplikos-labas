using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using static System.String;

namespace LAB3.Classes
{
    class Student
    {
        private const int MIN_GRADE = 1, MAX_GRADE = 10;

        public static Boolean isValidGrade(int grade)
        {
            if (Enumerable.Range(MIN_GRADE, MAX_GRADE).Contains(grade)) { return true; }
            else { return false; }
        }

        private string name;
        private string surname;
        private List<int> homeworkGrades;
        private int examGrade;

        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public List<int> HomeworkGrades { get => homeworkGrades; set => homeworkGrades = value; }
        public int ExamGrade { get => examGrade; set => examGrade = value; }

        private Random randomGenerator = new Random();

        public Student()
        {
            this.name = "randomName" + randomGenerator.Next(999999);
            this.surname = "randomSurname" + randomGenerator.Next(999999);
            this.homeworkGrades = new List<int>();
            for (int i = 0; i < 3; i++) this.homeworkGrades.Add(randomGenerator.Next(MIN_GRADE, MAX_GRADE));
            this.examGrade = randomGenerator.Next(MIN_GRADE, MAX_GRADE);
        }
        public Student(string name, string surname, List<int> homeworks, int exam)
        {
            this.name = name;
            this.surname = surname;
            this.homeworkGrades = homeworks;
            this.examGrade = exam;
        }
        public static decimal calculateMedian(IEnumerable<int> hw)
        {
            int[] hw_array = hw.ToArray();
            Array.Sort(hw_array);
            if (hw_array.Length == 0)
            {
                throw new Exception("List is empty");
            }
            else if (hw_array.Length % 2 == 0)
            {
                return (hw_array[hw_array.Length / 2 - 1] + hw_array[hw_array.Length / 2]) / 2m;
            }
            else
            {
                return hw_array[hw_array.Length / 2];
            }
        }

        public static List<int> createHWList(String name, String surname)
        {
            List<int> homeworkList = new List<int> { };
            int grade;
            Boolean working = true;
            while (working)
            {
                Console.WriteLine("If you want to stop entering grades, type '0' (zero) ");
            decideGrade:
                try
                {
                    Console.Write("Enter grade for student: " + name + " " + surname);
                    grade = Convert.ToInt32(Console.ReadLine());

                    if (grade == 0)
                    {
                        working = false;
                        break;
                    }

                    if (Student.isValidGrade(grade) == false)
                    {
                        Console.WriteLine("Grade is not valid, enter again!");
                        continue;
                    }
                    else
                    { homeworkList.Add(grade); }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    goto decideGrade;
                }
            }
            return homeworkList;
        }

        // Creating a student
        public static Student createStudent()
        {
            Student student = new Student();

        decideName:
            Console.Write("Enter student name: ");
            string name = Console.ReadLine();
            if (!IsNullOrEmpty(name) && name.Length < 15)
            {
                student.Name = name;
                goto decideSurname;
            }
            else
            {
                Console.Clear();
                goto decideName;
            }

        decideSurname:
            Console.Write("Enter student surname: ");
            string surname = Console.ReadLine();
            if (!IsNullOrEmpty(surname) && surname.Length < 30)
            {
                student.Surname = surname;
                goto decideHomework;
            }
            else
            {
                Console.Clear();
                goto decideSurname;
            }

        decideHomework:
            student.HomeworkGrades = createHWList(student.name, student.surname);
            goto decideExam;

        decideExam:
            try
            {
                Console.Write("Enter exam grade: ");
                int grade = Convert.ToInt32(Console.ReadLine());
                if (isValidGrade(grade) == true)
                {
                    student.ExamGrade = grade;
                    goto Finish;
                }
                else
                {
                    Console.Clear();
                    goto decideExam;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                goto decideExam;
            }

        Finish:
            return student;
        }


        public static void drawTableNoMedian(List<Student> students)
        {

            {
                Console.WriteLine("{0, -30} {1, -20} {2, -5}", "Surname", "Name", "Final points (Avg.)");

                for (int i = 0; i < 70; i++) { Console.Write("-"); }
                Console.Write('\n');

                foreach (Student student in students)
                {
                    Console.WriteLine("{0, -30} {1, -20} {2, -5}",
                        student.Surname, student.Name, (0.3 * student.HomeworkGrades.Average() + 0.7 * student.ExamGrade));
                }

            }
        }

        public static void drawTableWithMedian(List<Student> students)
        {

            Console.WriteLine("{0, -30} {1, -20} {2, -5}", "Surname", "Name", "Final points (Avg.) / Final points (Med.)");

            for (int i = 0; i < 100; i++) Console.Write("-");
            Console.Write('\n');

            foreach (Student student in students)
            {
                Console.WriteLine("{0, -30} {1, -20} {2, -21} {3, -20}",
                student.Surname, student.Name, (0.3 * student.HomeworkGrades.Average() + 0.7 * student.ExamGrade), calculateMedian(student.HomeworkGrades));
            }

        }

        public static List<Student> addStudentsFromFile()
        {
            // listai studentam is failo
            List<Student> studentsList = new List<Student>();
            List<int> studentGrades = new List<int>();
            try
            {
                // pakeiti path i failo, kuri norite kad uploadintu
                StreamReader fileReader = new StreamReader("C:\\Users\\adoma\\source\\repos\\LAB3\\LAB3\\classes\\students.csv");
                bool skipFirst = true;
                while (!fileReader.EndOfStream)
                {
                    var line = fileReader.ReadLine(); var splitOn = line.Split(',');

                    if (skipFirst){ 
                        skipFirst = false; 
                        continue;
                    }

                    for (int j = 2; j < 5; j++) { studentGrades.Add(int.Parse(splitOn[j])); }
                    
                    Student newStudent = new Student(splitOn[0], splitOn[1], studentGrades, int.Parse(splitOn[7]));
                    studentsList.Add(newStudent);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            studentsList.OrderBy(student => student.Name).ToList();
            return studentsList;

        }

        public double calculateFinalResult()
        {
            return (ExamGrade * 0.7) + (HomeworkGrades.Average() * 0.3);
        }


        public static void generateStudents(int amount)
        {

            Stopwatch timer = new Stopwatch();
            Stopwatch sortingTimer = new Stopwatch();
            timer.Restart();

            List<Student> students = new List<Student>();

       
            for (int i = 0; i < amount; i++)
            {
                students.Add(new Student());
            }

            StreamWriter passedList = new StreamWriter("C:\\Users\\adoma\\source\\repos\\LAB3\\LAB3\\results\\passed_students.csv");
            StreamWriter failedList = new StreamWriter("C:\\Users\\adoma\\source\\repos\\LAB3\\LAB3\\results\\failed_students.csv");

            passedList.WriteLine("Name, Surname, HW1, HW2, HW3, HW4, HW5, Exam");
            failedList.WriteLine("Name, Surname, HW1, HW2, HW3, HW4, HW5, Exam");

            sortingTimer.Restart();
            // decides if student is failed or not
            foreach (Student student in students)
            {
                if (student.calculateFinalResult() >= 5)
                {
                    passedList.WriteLine(student.csvToString(student));
                }
                else
                {
                    failedList.WriteLine(student.csvToString(student));
                }
            }
           

            passedList.Close();
            failedList.Close();
            sortingTimer.Stop();
            timer.Stop();

            Console.WriteLine();
            Console.WriteLine("Time it took to sort {0} students into passed/failed : {1}", amount, sortingTimer.Elapsed);
            Console.WriteLine("Generating {0} students, took: {1} .", amount, timer.Elapsed);

        }

        // converts csv to string so it can be easily written to file
        public string csvToString(Student student)
        {
            var lineToWrite = $"{student.Name},{student.Surname},";
            foreach (var item in student.HomeworkGrades){ lineToWrite += $"{item},";}
            lineToWrite += $"{student.ExamGrade}";
            return lineToWrite;
        }

    }
}





