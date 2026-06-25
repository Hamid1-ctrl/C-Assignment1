// ============================================================
// Mini Student Results Processing System
// C# Console Application — Individual Practical Assignment
// ============================================================

using System;

namespace C_Assignment1
{
    class Program
    {
        // ---------- constants ----------
        const int MAX_STUDENTS = 3;
        const int NUM_COURSES = 5;
        static readonly string[] CourseNames =
        {
            "Programming with C#",
            "Database Systems",
            "Computer Networks",
            "Web Development",
            "Mathematics for Computing"
        };

        // ---------- storage ----------
        static string[] names      = new string[MAX_STUDENTS];
        static string[] ids        = new string[MAX_STUDENTS];
        static string[] programmes = new string[MAX_STUDENTS];
        static string[] levels     = new string[MAX_STUDENTS];
        static int[,]   scores     = new int[MAX_STUDENTS, NUM_COURSES];

        static bool dataEntered = false;

        // --------------------------------------------------------
        //  Entry point
        // --------------------------------------------------------
        static void Main(string[] args)
        {
            RunMainMenu();
        }

        // --------------------------------------------------------
        //  Main menu loop
        // --------------------------------------------------------
        static void RunMainMenu()
        {
            int choice;

            do
            {
                Console.Clear();
                Console.WriteLine("===== STUDENT RESULTS PROCESSING SYSTEM =====");
                Console.WriteLine();
                Console.WriteLine("1. Enter Student Results");
                Console.WriteLine("2. View Student Report");
                Console.WriteLine("3. Exit");
                Console.WriteLine();
                Console.Write("Choose an option: ");

                string input = Console.ReadLine();

                // Validate menu choice
                if (!int.TryParse(input, out choice) || choice < 1 || choice > 3)
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid option. Please enter 1, 2, or 3.");
                    Pause();
                    continue;
                }

                Console.WriteLine();

                switch (choice)
                {
                    case 1:
                        EnterStudentResults();
                        break;
                    case 2:
                        ViewStudentReport();
                        break;
                    case 3:
                        Console.WriteLine("Thank you for using the Student Results Processing System.");
                        Pause();
                        break;
                }

            } while (choice != 3);
        }

        // --------------------------------------------------------
        //  Option 1 – collect data for all students
        // --------------------------------------------------------
        static void EnterStudentResults()
        {
            for (int i = 0; i < MAX_STUDENTS; i++)
            {
                Console.WriteLine($"Enter details for Student {i + 1}");
                Console.WriteLine();

                // Basic details
                Console.Write("Enter full name: ");
                names[i] = Console.ReadLine();

                Console.Write("Enter student ID: ");
                ids[i] = Console.ReadLine();

                Console.Write("Enter programme: ");
                programmes[i] = Console.ReadLine();

                Console.Write("Enter level: ");
                levels[i] = Console.ReadLine();

                Console.WriteLine();

                // Course scores with validation
                for (int j = 0; j < NUM_COURSES; j++)
                {
                    scores[i, j] = ReadValidScore(CourseNames[j]);
                }

                Console.WriteLine();
                Console.WriteLine($"Student {i + 1} results entered successfully.");
                Console.WriteLine();
            }

            dataEntered = true;
            Pause();
        }

        // --------------------------------------------------------
        //  Keep asking until the user enters a score in 0-100
        // --------------------------------------------------------
        static int ReadValidScore(string courseName)
        {
            int score;
            while (true)
            {
                Console.Write($"Enter score for {courseName}: ");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out score) || score < 0 || score > 100)
                {
                    Console.WriteLine("Invalid score. Score must be between 0 and 100.");
                    continue;
                }

                break;
            }
            return score;
        }

        // --------------------------------------------------------
        //  Option 2 – display full report
        // --------------------------------------------------------
        static void ViewStudentReport()
        {
            if (!dataEntered)
            {
                Console.WriteLine("No student data has been entered yet.");
                Console.WriteLine("Please select option 1 to enter student results first.");
                Pause();
                return;
            }

            Console.WriteLine("===== STUDENT RESULTS REPORT =====");
            Console.WriteLine();

            int bestStudent   = 0;
            int worstStudent  = 0;
            double classTotal = 0;

            for (int i = 0; i < MAX_STUDENTS; i++)
            {
                // --- header ---
                Console.WriteLine($"--- Student {i + 1} ---");
                Console.WriteLine($"Student Name  : {names[i]}");
                Console.WriteLine($"Student ID    : {ids[i]}");
                Console.WriteLine($"Programme     : {programmes[i]}");
                Console.WriteLine($"Level         : {levels[i]}");
                Console.WriteLine();

                // --- individual scores ---
                int    total   = 0;
                for (int j = 0; j < NUM_COURSES; j++)
                {
                    Console.WriteLine($"{CourseNames[j]}: {scores[i, j]}");
                    total += scores[i, j];
                }

                double average = (double)total / NUM_COURSES;
                string grade   = GetGrade(average);
                string status  = average >= 50 ? "Passed" : "Failed";

                Console.WriteLine();
                Console.WriteLine($"Total Score  : {total}");
                Console.WriteLine($"Average Score: {average:F1}");
                Console.WriteLine($"Grade        : {grade}");
                Console.WriteLine($"Status       : {status}");
                Console.WriteLine();

                classTotal += average;

                if (average > GetAverage(bestStudent))
                    bestStudent = i;
                if (average < GetAverage(worstStudent))
                    worstStudent = i;
            }

            // --- bonus: class summary ---
            double classAverage = classTotal / MAX_STUDENTS;
            Console.WriteLine("===== CLASS SUMMARY =====");
            Console.WriteLine($"Class Average        : {classAverage:F1}");
            Console.WriteLine($"Best Student         : {names[bestStudent]} ({GetAverage(bestStudent):F1})");
            Console.WriteLine($"Lowest Average Student: {names[worstStudent]} ({GetAverage(worstStudent):F1})");
            Console.WriteLine();

            Pause();
        }

        // --------------------------------------------------------
        //  Helpers
        // --------------------------------------------------------

        // Return the average score for a stored student index
        static double GetAverage(int index)
        {
            int total = 0;
            for (int j = 0; j < NUM_COURSES; j++)
                total += scores[index, j];
            return (double)total / NUM_COURSES;
        }

        // Map average to letter grade
        static string GetGrade(double average)
        {
            if (average >= 80) return "A";
            if (average >= 70) return "B";
            if (average >= 60) return "C";
            if (average >= 50) return "D";
            return "F";
        }

        // Press Enter to continue
        static void Pause()
        {
            Console.Write("Press Enter to continue...");
            Console.ReadLine();
        }
    }
}
