# Student Results Processing System

A C# console application that allows users to enter student details, record scores for 5 courses, and generate a complete academic report — including total scores, averages, letter grades, and pass/fail status.

---

## How to Run

1. Open a terminal in the project folder.
2. Run:
   ```bash
   dotnet run
   ```

---

## Main Menu

The program displays a menu with 3 options:

| Option | Description |
|--------|-------------|
| 1 | Enter Student Results |
| 2 | View Student Report |
| 3 | Exit |

---

## How It Works

### Option 1 — Enter Student Results

- The program asks how many students you want to process (minimum **3**).
- For each student, it collects:
  - **Full name**
  - **Student ID**
  - **Programme**
  - **Level**
  - **Scores for 5 courses:**
    1. Programming with C#
    2. Database Systems
    3. Computer Networks
    4. Web Development
    5. Mathematics for Computing
- Each score is validated — only values between **0 and 100** are accepted. Invalid entries are rejected with a message and the user is prompted again.

### Option 2 — View Student Report

- Displays a neatly formatted report for every student showing:
  - Personal details (name, ID, programme, level)
  - All 5 course scores
  - **Total score** (sum of 5 courses)
  - **Average score** (total ÷ 5)
  - **Grade** (based on the grading table below)
  - **Academic status** (Passed or Failed)
- Also displays **class statistics** at the end:
  - Best performing student
  - Student with the lowest average
  - Overall class average

### Option 3 — Exit

- Displays a thank-you message and closes the program.

---

## Grading System

| Average Score | Grade |
|---------------|-------|
| 80 – 100      | A     |
| 70 – 79       | B     |
| 60 – 69       | C     |
| 50 – 59       | D     |
| Below 50      | F     |

## Academic Status

| Average Score | Status  |
|---------------|---------|
| 50 and above  | Passed  |
| Below 50      | Failed  |

---

## Code Structure

The program is organised into two classes:

- **`Student`** — Holds a student's personal details and 5 course scores. Contains methods to calculate:
  - `Total()` — sum of all scores
  - `Average()` — mean of all scores
  - `Grade()` — letter grade based on average
  - `Status()` — Passed or Failed

- **`Program`** — Contains the main menu loop and helper methods:
  - `DisplayMenu()` — prints the main menu
  - `EnterStudentResults()` — collects student data with validation
  - `ReadValidScore()` — ensures scores are between 0 and 100
  - `ReadLineOrExit()` — safely reads input and handles end-of-input
  - `ViewStudentReport()` — displays the full report for all students
  - `PrintStudent()` — prints one student's details and results
  - `DisplayStatistics()` — shows class-wide statistics (best, lowest, class average)

---

## Technologies

- **Language:** C#
- **Framework:** .NET 10
- **Type:** Console Application

---

## Author

Built for C# Console Advanced Project Assignment.
