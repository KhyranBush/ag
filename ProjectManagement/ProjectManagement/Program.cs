using System;

namespace ProjectManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            IProjectDB projectDB = new ProjectDB("OOD Database");
            bool working = true;
            while(working)
            {
                Console.WriteLine(projectDB.Name);
                Console.WriteLine("Enter your choice.");
                Console.WriteLine("1. Employees");
                Console.WriteLine("2. Clients");
                Console.WriteLine("3. Projects");
                Console.WriteLine("9. Quit\n");
                String mainMenuInput = Console.ReadLine();
                switch(mainMenuInput)
                {
                    case "1":
                        Console.WriteLine("Now we deal with employees...");
                        bool employeesMenu = true;
                        while(employeesMenu)
                        {
                            String firstName, lastName, projectName;
                            Employee employee;
                            Console.WriteLine("\nEnter your choice:");
                            Console.WriteLine("1. Create Employee");
                            Console.WriteLine("2. Assign Employee");
                            Console.WriteLine("3. Remove Employee");
                            Console.WriteLine("4. Find Employee");
                            Console.WriteLine("5. List all employees");
                            Console.WriteLine("9. Return to Main Menu\n");
                            String employeeInput = Console.ReadLine();
                            switch(employeeInput)
                            {
                                case "1":
                                    Console.WriteLine("Let's create an employee\n");
                                    Console.WriteLine("Please, enter the first name");
                                    firstName = Console.ReadLine();
                                    Console.WriteLine("Please, enter the last name");
                                    lastName = Console.ReadLine();
                                    if(projectDB.CreateEmployee(lastName, firstName))
                                    {
                                        Console.WriteLine("\nThe employee was successfully created.\n");
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nThere was an error creating the employee.\n");
                                    }
                                    break;
                                case "2":
                                    Console.WriteLine("Let's assign an employee\n");
                                    Console.WriteLine("Please, enter the first name");
                                    firstName = Console.ReadLine();
                                    Console.WriteLine("Please, enter the last name");
                                    lastName = Console.ReadLine();
                                    Console.WriteLine("Please, enter the name of the project");
                                    projectName = Console.ReadLine();
                                    if(projectDB.AssignEmployee(lastName, firstName, projectName))
                                    {
                                        Console.WriteLine("The employee was successfully assigned.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("There was an error assigning the employee");
                                    }
                                    break;
                                case "3":
                                    Console.WriteLine("Let's remove an employee\n");
                                    Console.WriteLine("Please, enter the first name");
                                    firstName = Console.ReadLine();
                                    Console.WriteLine("Please, enter the last name");
                                    lastName = Console.ReadLine();
                                    Console.WriteLine("Please, enter the name of the project");
                                    projectName = Console.ReadLine();
                                    if (projectDB.RemoveEmployee(lastName, firstName, projectName))
                                    {
                                        Console.WriteLine("The employee was successfully removed.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("There was an error removing the employee");
                                    }
                                    break;
                                case "4":
                                    Console.WriteLine("Let's find an employee\n");
                                    Console.WriteLine("Please, enter the first name");
                                    firstName = Console.ReadLine();
                                    Console.WriteLine("Please, enter the last name");
                                    lastName = Console.ReadLine();
                                    employee = projectDB.FindEmployee(lastName, firstName);
                                    if (employee != null)
                                    {
                                        Console.WriteLine("\nThe employee was successfully found.\n");
                                        Console.WriteLine(employee);
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nThere was an error finding the employee.\n");
                                    }
                                    break;
                                case "5":
                                    Console.WriteLine("Let's list all the employees\n");
                                    Console.WriteLine(projectDB.EmployeeList);
                                    break;
                                case "9":
                                    Console.WriteLine("Returning to Main Menu");
                                    employeesMenu = false;
                                    break;
                                default:
                                    Console.WriteLine("I don't understand.");
                                    break;
                            }
                        }

                        break;
                    case "2":
                        Console.WriteLine("Now we deal with Clients...");
                        bool clientMenu = true;
                        while (clientMenu)
                        {
                            String firstName, lastName, projectName;
                           Client client;
                            Console.WriteLine("\nEnter your choice:");
                            Console.WriteLine("1. Create client");
                            Console.WriteLine("2. Assign client");
                            Console.WriteLine("3. Remove client");
                            Console.WriteLine("4. Find client");
                            Console.WriteLine("5. List all client");
                            Console.WriteLine("9. Return to Main Menu\n");
                            String clientInput = Console.ReadLine();
                            switch (clientInput)
                            {
                                case "1":
                                    Console.WriteLine("Let's create a client\n");
                                    Console.WriteLine("Please, enter the first name");
                                    firstName = Console.ReadLine();
                                    Console.WriteLine("Please, enter the last name");
                                    lastName = Console.ReadLine();
                                    if (projectDB.CreateClient(lastName, firstName))
                                    {
                                        Console.WriteLine("\nThe client was successfully created.\n");
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nThere was an error creating the client.\n");
                                    }
                                    break;
                                case "2":
                                    Console.WriteLine("Let's assign a client\n");
                                    Console.WriteLine("Please, enter the first name");
                                    firstName = Console.ReadLine();
                                    Console.WriteLine("Please, enter the last name");
                                    lastName = Console.ReadLine();
                                    Console.WriteLine("Please, enter the name of the project");
                                    projectName = Console.ReadLine();
                                    if (projectDB.AssignClient(lastName, firstName, projectName))
                                    {
                                        Console.WriteLine("The client was successfully assigned.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("There was an error assigning the client");
                                    }
                                    break;
                                case "3":
                                    Console.WriteLine("Let's remove a client\n");
                                    
                                    Console.WriteLine("Please, enter the name of the project");
                                    projectName = Console.ReadLine();
                                    if (projectDB.RemoveClient(projectName))
                                    {
                                        Console.WriteLine("The client was successfully removed.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("There was an error removing the client");
                                    }
                                    break;
                                case "4":
                                    Console.WriteLine("Let's find an client\n");
                                    Console.WriteLine("Please, enter the first name");
                                    firstName = Console.ReadLine();
                                    Console.WriteLine("Please, enter the last name");
                                    lastName = Console.ReadLine();
                                    client= projectDB.FindClient(lastName, firstName);
                                    if (client != null)
                                    {
                                        Console.WriteLine("\nThe client was successfully found.\n");
                                        Console.WriteLine(client);
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nThere was an error finding the client.\n");
                                    }
                                    break;
                                case "5":
                                    Console.WriteLine("Let's list all the client\n");
                                    Console.WriteLine(projectDB.ClientList);
                                    break;
                                case "9":
                                    Console.WriteLine("Returning to Main Menu");
                                    employeesMenu = false;
                                    break;
                                default:
                                    Console.WriteLine("I don't understand.");
                                    break;
                            }
                        }
                        break;
                   

                    case "3":
                        Console.WriteLine("Now we deal with Projects...");
                        bool ProjectMenu = true;
                        while (ProjectMenu)
                        {
                            String Name,  projectName;
                           IProject project ;
                            Console.WriteLine("\nEnter your choice:");
                            Console.WriteLine("1. Create project");
                            Console.WriteLine("2. Remove project");
                            Console.WriteLine("3. Find project");
                            Console.WriteLine("4. List all project");
                            Console.WriteLine("9. Return to Main Menu\n");
                            String projectInput = Console.ReadLine();
                            switch (projectInput)
                            {
                                case "1":
                                    Console.WriteLine("Let's create a project\n");
                                    Console.WriteLine("Please, enter the project name");
                                    Name = Console.ReadLine();
                                    
                                    if (projectDB.CreateProject(Name))
                                    {
                                        Console.WriteLine("\nThe project was successfully created.\n");
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nThere was an error creating the project.\n");
                                    }
                                    break;
                                
                                case "2":
                                    Console.WriteLine("Let's remove a project\n");

                                    Console.WriteLine("Please, enter the name of the project");
                                    projectName = Console.ReadLine();
                                    if (projectDB.RemoveClient(projectName))
                                    {
                                        Console.WriteLine("The project was successfully removed.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("There was an error removing the project");
                                    }
                                    break;
                                case "3":
                                    Console.WriteLine("Let's find a project \n");
                                    
                                    Console.WriteLine("Please, enter the Project name");
                                    Name = Console.ReadLine();
                                    
                                   project = (Project)projectDB.FindProject(Name);
                                    if (project != null)
                                    {
                                        Console.WriteLine("\nThe project was successfully found.\n");
                                        Console.WriteLine(project);
                                    }
                                    else
                                    
                                    {
                                        Console.WriteLine("\nThere was an error finding the project.\n");
                                    }
                                    break;
                                case "4":
                                    Console.WriteLine("Let's list all the projects\n");
                                    Console.WriteLine(projectDB.ProjectList);
                                    break;
                                case "9":
                                    Console.WriteLine("Returning to Main Menu");
                                    employeesMenu = false;
                                    break;
                                default:
                                    Console.WriteLine("I don't understand.");
                                    break;
                            }
                        }
                        break;

                    
                    case "9":
                     Console.WriteLine("Quitting");
                        working = false;
                        
                        break;
                       // break;
                    //default:
                       // Console.WriteLine("I don't understand.");
                       // break;
                }
            }
            Console.WriteLine(projectDB.Name + "\nThank for using our database system.\n");
        }
    }
}
