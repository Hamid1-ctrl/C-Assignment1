using System;

namespace StudentResultsProcessingSystem
{
    class Student
    {
        public string FullName = "";
        public string StudentId = "";
        public string Programme = "";
        public string Level = "";
        public int[] Scores = new int[5];

        public int Total()
        {
            int sum = 0;
            for (int i = 0; i < Scores.Length; i++)
                sum += Scores[i];
            return sum;
        }

        public double Average()
        {
            return (double)Total() / Scores.Length;
        }

        public string Grade()
        {
            double avg = Average();
            if (avg >= 80) return "A";
            if (avg >= 70) return "B";
            if (avg >= 60) return "C";
            if (avg >= 50) return "D";
            return "F";
        }

        public string Status()
        {
            if (Average() >= 50) return "Passed";
            return "Failed";
        }
    }

    class Program
    {
        static string[] courseNames = new string[]
        {
            "Programming with C#",
            "Database Systems",
            "Computer Networks",
            "Web Development",
            "Mathematics for Computing"
        };

        static Student[]? students;
        static int numberOfStudents = 0;
        static bool dataEntered = false;

        static void Main(string[] args)
        {
            int choice;

            while (true)
            {
                DisplayMenu();

                while (true)
                {
                    Console.Write("Choose an option: ");
                    string input = Console.ReadLine()!;

                    if (string.IsNullOrEmpty(input))
                    {
                        Console.WriteLine("No input received. Exiting...");
                        return;
                    }

                    if (int.TryParse(input, out choice) && choice >= 1 && choice <= 3)
                        break;

                    Console.WriteLine("Invalid option. Please enter 1, 2, or 3.");
                }

                if (choice == 1)
                {
                    EnterStudentResults();
                }
                else if (choice == 2)
                {
                    ViewStudentReport();
                }
                else if (choice == 3)
                {
                    Console.WriteLine();
                    Console.WriteLine("Thank you for using the Student Results Processing System.");
                    return;
                }
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("===== STUDENT RESULTS PROCESSING SYSTEM =====");
            Console.WriteLine();
            Console.WriteLine("1. Enter Student Results");
            Console.WriteLine("2. View Student Report");
            Console.WriteLine("3. Exit");
            Console.WriteLine();
        }

        static void EnterStudentResults()
        {
            Console.WriteLine();
            Console.WriteLine("===== ENTER STUDENT RESULTS =====");
            Console.WriteLine();

            // Ask the user how many students they want to enter (minimum 3).
            while (true)
            {
                Console.Write("Enter the number of students (minimum 3): ");
                string input = ReadLineOrExit();

                if (int.TryParse(input, out int num) && num >= 3)
                {
                    numberOfStudents = num;
                    break;
                }

                Console.WriteLine("Invalid number. Please enter a number that is 3 or more.");
            }

            students = new Student[numberOfStudents];
            dataEntered = true;

            for (int i = 0; i < numberOfStudents; i++)
            {
                Student s = new Student();
                Console.WriteLine();
                Console.WriteLine("Enter details for Student {0}", i + 1);
                Console.WriteLine();

                Console.Write("Enter full name: ");
                s.FullName = ReadLineOrExit();

                Console.Write("Enter student ID: ");
                s.StudentId = ReadLineOrExit();

                Console.Write("Enter programme: ");
                s.Programme = ReadLineOrExit();

                Console.Write("Enter level: ");
                s.Level = ReadLineOrExit();

                Console.WriteLine();
                for (int j = 0; j < courseNames.Length; j++)
                {
                    s.Scores[j] = ReadValidScore(courseNames[j]);
                }

                students[i] = s;
            }

            Console.WriteLine();
            Console.WriteLine("Student results have been recorded successfully.");
            Console.WriteLine();
        }

        static string ReadLineOrExit()
        {
            string? input = Console.ReadLine();
            if (input == null)
            {
                Console.WriteLine();
                Console.WriteLine("Input ended unexpectedly. Exiting...");
                Environment.Exit(0);
            }
            return input;
        }

        static int ReadValidScore(string course)
        {
            int score;
            while (true)
            {
                Console.Write("Enter score for {0}: ", course);
                string input = ReadLineOrExit();

                if (int.TryParse(input, out score) && score >= 0 && score <= 100)
                    return score;

                Console.WriteLine("Invalid score. Score must be between 0 and 100.");
            }
        }

        static void ViewStudentReport()
        {
            if (!dataEntered || students == null)
            {
                Console.WriteLine();
                Console.WriteLine("No student data available. Please choose option 1 first.");
                Console.WriteLine();
                return;
            }

            Console.WriteLine();
            Console.WriteLine("===== STUDENT RESULTS REPORT =====");

            for (int i = 0; i < numberOfStudents; i++)
            {
                PrintStudent(students[i]);

                if (i < numberOfStudents - 1)
                {
                    Console.WriteLine("--------------------------------------------");
                }
            }

            DisplayStatistics();

            Console.WriteLine();
            Console.Write("Press Enter to return to the menu...");
            Console.ReadLine();
        }

        static void PrintStudent(Student s)
        {
            Console.WriteLine();
            Console.WriteLine("Student Name: {0}", s.FullName);
            Console.WriteLine("Student ID: {0}", s.StudentId);
            Console.WriteLine("Programme: {0}", s.Programme);
            Console.WriteLine("Level: {0}", s.Level);
            Console.WriteLine();

            for (int j = 0; j < courseNames.Length; j++)
                Console.WriteLine("{0}: {1}", courseNames[j], s.Scores[j]);

            Console.WriteLine();
            Console.WriteLine("Total Score: {0}", s.Total());
            Console.WriteLine("Average Score: {0:F1}", s.Average());
            Console.WriteLine("Grade: {0}", s.Grade());
            Console.WriteLine("Status: {0}", s.Status());
        }

        static void DisplayStatistics()
        {
            Student best = students![0];
            Student lowest = students[0];
            double sumOfAverages = 0;

            for (int i = 0; i < numberOfStudents; i++)
            {
                if (students[i].Average() > best.Average())
                    best = students[i];
                if (students[i].Average() < lowest.Average())
                    lowest = students[i];
                sumOfAverages += students[i].Average();
            }

            double classAverage = sumOfAverages / numberOfStudents;

            Console.WriteLine();
            Console.WriteLine("===== CLASS STATISTICS =====");
            Console.WriteLine("Best Student: {0} (Average: {1:F1})", best.FullName, best.Average());
            Console.WriteLine("Lowest Average: {0} (Average: {1:F1})", lowest.FullName, lowest.Average());
            Console.WriteLine("Class Average: {0:F1}", classAverage);
        }
    }
}
