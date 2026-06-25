using System;

namespace StudentResultsProcessingSystem
{
    // Represents a single student and their results for 5 courses.
    class Student
    {
        public string FullName;
        public string StudentId;
        public string Programme;
        public string Level;
        public int[] Scores = new int[5];

        // Adds up all 5 course scores.
        public int Total()
        {
            int sum = 0;
            for (int i = 0; i < Scores.Length; i++)
            {
                sum += Scores[i];
            }
            return sum;
        }

        // Mean of the 5 course scores.
        public double Average()
        {
            return (double)Total() / Scores.Length;
        }

        // Converts the average into a letter grade.
        public string Grade()
        {
            double avg = Average();
            if (avg >= 80) return "A";   // 80 - 100
            if (avg >= 70) return "B";   // 70 - 79
            if (avg >= 60) return "C";   // 60 - 69
            if (avg >= 50) return "D";   // 50 - 59
            return "F";                  // Below 50
        }

        // Decides whether the student has passed or failed.
        public string Status()
        {
            if (Average() >= 50) return "Passed";
            return "Failed";
        }
    }

    class Program
    {
        // The 5 fixed courses every student is scored on.
        static string[] courseNames = new string[]
        {
            "Programming with C#",
            "Database Systems",
            "Computer Networks",
            "Web Development",
            "Mathematics for Computing"
        };

        const int NumberOfStudents = 3;
        static Student[] students = new Student[NumberOfStudents];
        static bool dataEntered = false;

        static void Main(string[] args)
        {
            int choice;

            // Main menu loop: keeps showing the menu until the user exits.
            while (true)
            {
                DisplayMenu();
                Console.Write("Choose an option: ");
                string input = Console.ReadLine();

                // Guard against non-numeric menu input.
                if (!int.TryParse(input, out choice))
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid option. Please enter 1, 2, or 3.");
                    Console.WriteLine();
                    continue;
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
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid option. Please enter 1, 2, or 3.");
                    Console.WriteLine();
                }
            }
        }

        // Prints the main menu.
        static void DisplayMenu()
        {
            Console.WriteLine("===== STUDENT RESULTS PROCESSING SYSTEM =====");
            Console.WriteLine();
            Console.WriteLine("1. Enter Student Results");
            Console.WriteLine("2. View Student Report");
            Console.WriteLine("3. Exit");
            Console.WriteLine();
        }

        // Collects details and scores for all students.
        static void EnterStudentResults()
        {
            Console.WriteLine();
            for (int i = 0; i < NumberOfStudents; i++)
            {
                Student s = new Student();
                Console.WriteLine("Enter details for Student {0}", i + 1);
                Console.WriteLine();

                Console.Write("Enter full name: ");
                s.FullName = Console.ReadLine();

                Console.Write("Enter student ID: ");
                s.StudentId = Console.ReadLine();

                Console.Write("Enter programme: ");
                s.Programme = Console.ReadLine();

                Console.Write("Enter level: ");
                s.Level = Console.ReadLine();

                Console.WriteLine();
                // Read a validated score for each of the 5 courses.
                for (int j = 0; j < courseNames.Length; j++)
                {
                    s.Scores[j] = ReadValidScore(courseNames[j]);
                }
                Console.WriteLine();

                students[i] = s;
            }

            dataEntered = true;
            Console.WriteLine("Student results have been recorded successfully.");
            Console.WriteLine();
        }

        // Keeps asking for a score until a valid value (0 - 100) is entered.
        static int ReadValidScore(string course)
        {
            int score;
            while (true)
            {
                Console.Write("Enter score for {0}: ", course);
                string input = Console.ReadLine();

                if (int.TryParse(input, out score) && score >= 0 && score <= 100)
                {
                    return score;
                }

                Console.WriteLine("Invalid score. Score must be between 0 and 100.");
            }
        }

        // Shows the full report for every student plus class statistics.
        static void ViewStudentReport()
        {
            if (!dataEntered)
            {
                Console.WriteLine();
                Console.WriteLine("No student data available. Please choose option 1 first.");
                Console.WriteLine();
                return;
            }

            Console.WriteLine();
            Console.WriteLine("===== STUDENT RESULTS REPORT =====");

            for (int i = 0; i < NumberOfStudents; i++)
            {
                PrintStudent(students[i]);

                // Separator between students (but not after the last one).
                if (i < NumberOfStudents - 1)
                {
                    Console.WriteLine("--------------------------------------------");
                }
            }

            DisplayStatistics();

            Console.WriteLine();
            Console.Write("Press Enter to return to the menu...");
            Console.ReadLine();
        }

        // Prints a single student's details, scores, total, average, grade and status.
        static void PrintStudent(Student s)
        {
            Console.WriteLine();
            Console.WriteLine("Student Name: {0}", s.FullName);
            Console.WriteLine("Student ID: {0}", s.StudentId);
            Console.WriteLine("Programme: {0}", s.Programme);
            Console.WriteLine("Level: {0}", s.Level);
            Console.WriteLine();

            for (int j = 0; j < courseNames.Length; j++)
            {
                Console.WriteLine("{0}: {1}", courseNames[j], s.Scores[j]);
            }

            Console.WriteLine();
            Console.WriteLine("Total Score: {0}", s.Total());
            Console.WriteLine("Average Score: {0:F1}", s.Average());
            Console.WriteLine("Grade: {0}", s.Grade());
            Console.WriteLine("Status: {0}", s.Status());
        }

        // Bonus: best student, lowest average and the class average.
        static void DisplayStatistics()
        {
            Student best = students[0];
            Student lowest = students[0];
            double sumOfAverages = 0;

            for (int i = 0; i < NumberOfStudents; i++)
            {
                if (students[i].Average() > best.Average())
                {
                    best = students[i];
                }
                if (students[i].Average() < lowest.Average())
                {
                    lowest = students[i];
                }
                sumOfAverages += students[i].Average();
            }

            double classAverage = sumOfAverages / NumberOfStudents;

            Console.WriteLine();
            Console.WriteLine("===== CLASS STATISTICS =====");
            Console.WriteLine("Best Student: {0} (Average: {1:F1})", best.FullName, best.Average());
            Console.WriteLine("Lowest Average: {0} (Average: {1:F1})", lowest.FullName, lowest.Average());
            Console.WriteLine("Class Average: {0:F1}", classAverage);
        }
    }
}
