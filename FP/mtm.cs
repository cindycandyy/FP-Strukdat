using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP.MiniTaskManager
{
    class Task : IComparable<Task>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public Task(string name, string description, int priority, DateTime dueDate)
        {
            Name = name;
            Description = description;
            Priority = priority;
            DueDate = dueDate;
            IsCompleted = false;
        }

        public override string ToString()
        {
            string completionStatus = IsCompleted ? "[√]" : "[ ]";
            return $"[{Priority}] {Name} - {Description} ({DueDate.ToShortDateString()}) {completionStatus}";
        }

        public int CompareTo(Task other)
        {
            if (other == null) return 1;
            return Priority.CompareTo(other.Priority);
        }
    }

    class TaskManager
    {
        private SortedSet<Task> tasks = new SortedSet<Task>();

        public void AddTask(Task task)
        {
            tasks.Add(task);
            Console.WriteLine("Task added successfully!");
        }

        public void RemoveTask(Task task)
        {
            if (tasks.Remove(task))
            {
                Console.WriteLine("Task removed successfully!");
            }
            else
            {
                Console.WriteLine("Task not found.");
            }
        }

        public void EditTask()
        {
            Console.Write("Enter the name of the task to edit: ");
            string taskName = Console.ReadLine();

            Task foundTask = FindTask(taskName);
            if (foundTask != null)
            {
                Console.Write("Enter new name (or press Enter to skip): ");
                string newName = Console.ReadLine();
                if (!string.IsNullOrEmpty(newName))
                {
                    foundTask.Name = newName;
                }

                Console.Write("Enter new description (or press Enter to skip): ");
                string newDescription = Console.ReadLine();
                if (!string.IsNullOrEmpty(newDescription))
                {
                    foundTask.Description = newDescription;
                }

                Console.Write("Enter new priority (or press Enter to skip): ");
                string newPriorityStr = Console.ReadLine();
                if (!string.IsNullOrEmpty(newPriorityStr))
                {
                    int newPriority;
                    if (int.TryParse(newPriorityStr, out newPriority))
                    {
                        foundTask.Priority = newPriority;
                    }
                    else
                    {
                        Console.WriteLine("Invalid priority.");
                    }
                }

                Console.Write("Enter new due date (or press Enter to skip): ");
                string newDueDateStr = Console.ReadLine();
                if (!string.IsNullOrEmpty(newDueDateStr))
                {
                    DateTime newDueDate;
                    if (DateTime.TryParse(newDueDateStr, out newDueDate))
                    {
                        foundTask.DueDate = newDueDate;
                    }
                    else
                    {
                        Console.WriteLine("Invalid due date.");
                    }
                }

                Console.WriteLine("Task edited successfully!");
            }
            else
            {
                Console.WriteLine("Task not found.");
            }
        }

        public void MarkTaskComplete()
        {
            Console.Write("Enter the name of the task to mark as complete: ");
            string taskName = Console.ReadLine();

            Task foundTask = FindTask(taskName);
            if (foundTask != null)
            {
                foundTask.IsCompleted = true;
                Console.WriteLine("Task marked as complete successfully!");
            }
            else
            {
                Console.WriteLine("Task not found.");
            }
        }

        public void DisplayTasks()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("Task list is empty.");
            }
            else
            {
                Console.WriteLine("Task List:");
                foreach (Task task in tasks)
                {
                    Console.WriteLine(task);
                }
            }
        }

        public Task FindTask(string taskName)
        {
            foreach (var task in tasks)
            {
                if (task.Name.Equals(taskName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return task;
                }
            }
            return null;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            TaskManager taskManager = new TaskManager();

            while (true)
            {
                Console.WriteLine("Task Manager Menu:");
                Console.WriteLine("1. Add Task");
                Console.WriteLine("2. Remove Task");
                Console.WriteLine("3. Edit Task");
                Console.WriteLine("4. Mark Task Complete");
                Console.WriteLine("5. Display Tasks");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option: ");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        AddTaskMenu(taskManager);
                        break;
                    case "2":
                        RemoveTaskMenu(taskManager);
                        break;
                    case "3":
                        taskManager.EditTask();
                        break;
                    case "4":
                        taskManager.MarkTaskComplete();
                        break;
                    case "5":
                        taskManager.DisplayTasks();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private static void AddTaskMenu(TaskManager taskManager)
        {
            Console.Write("Enter task name: ");
            string name = Console.ReadLine();

            Console.Write("Enter task description: ");
            string description = Console.ReadLine();

            Console.Write("Enter task priority: ");
            int priority = int.Parse(Console.ReadLine());

            Console.Write("Enter task due date (yyyy-mm-dd): ");
            DateTime dueDate = DateTime.Parse(Console.ReadLine());

            Task task = new Task(name, description, priority, dueDate);
            taskManager.AddTask(task);
        }

        private static void RemoveTaskMenu(TaskManager taskManager)
        {
            Console.Write("Enter the name of the task to remove: ");
            string taskName = Console.ReadLine();

            Task task = taskManager.FindTask(taskName);
            if (task != null)
            {
                taskManager.RemoveTask(task);
            }
            else
            {
                Console.WriteLine("Task not found.");
            }
        }
    }
}