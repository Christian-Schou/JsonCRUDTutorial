using System;

namespace JsonCRUDTutorial
{
    class Program
    {
        private static readonly string jsonDocument = @"C:\Users\chs\source\repos\JsonCRUDTutorial\JsonCRUDTutorial\users.json";
        static void Main(string[] args)
        {
            Console.WriteLine("What would you like to do? : 1 - Add Job, 2 - Update Job, 3 - Delete Job, 4 - Select \n");
            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    CRUD.AddJob(jsonDocument);
                    break;
                case "2":
                    CRUD.UpdateJob(jsonDocument);
                    break;
                case "3":
                    CRUD.DeleteJob(jsonDocument);
                    break;
                case "4":
                    CRUD.GetEmployeeDetails(jsonDocument);
                    break;
                default:
                    Main(null);
                    break;
            }
            Console.ReadLine();
        }
    }
}
