using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement
{
    public class ProjectDB : IProjectDB
    {
        private List<IProject> projects;
        private List<Employee> employees;
        private List<Client> clients;

        public String Name { set; get; }

        public String EmployeeList
        {
            get
            {
                String output = "\n*** Employee List ***\n";
                foreach (IPeople people in employees)
                {
                    output += people.ToString() + "\n";
                }

                return output;
            }
        }

        public String ClientList
        {
            get
            {
                String output = "\n $$$ Client List $$$\n";
                foreach(IPeople people in clients)
                {
                    output += people.ToString() + "\n";
                }

                return output;
            }
        }

        public String ProjectList
        {
            get
            {
                String output = "\n>>> Project List <<<\n";
                foreach (IProject project in projects)
                {
                    output += project.ProjectID + " - " + project.Name + "\n";
                }

                return output;
            }
        }

        public ProjectDB() : this("NAMELESS") { }

        // Designated Constructor
        public ProjectDB(String DBName)
        {
            Name = DBName;
            projects = new List<IProject>();
            employees = new List<Employee>();
            clients = new List<Client>();
        }
        public bool CreateProject(String name)
        {
            bool success = false;
            if(FindProject(name) == null)
            {
                projects.Add(new Project(name));
                success = true;
            }
            return success;
        }

        public IProject FindProject(String name)
        {
            Project tempProject = null;
            foreach (Project project in projects)
            {
                if (project.Name.Equals(name))
                {
                    tempProject = project;
                }
            }

            return tempProject;
        }
        public bool CreateEmployee(String lastName, String firstName)
        {
            bool success = false;
            if(FindEmployee(lastName, firstName) == null)
            {
                employees.Add(new Employee(lastName, firstName));
                success = true;
            }
            return success;
        }

        public bool AssignEmployee(String lastName, String firstName, String projectName)
        {
            bool success = false;
            IProject foundProject = FindProject(projectName);
            if(foundProject != null)
            {
                Employee foundEmployee = FindEmployee(lastName, firstName);
                if(foundEmployee != null)
                {
                    foundProject.AddEmployee(foundEmployee);
                    success = true;
                }
            }
            return success;
        }

        public bool RemoveEmployee(String lastName, String firstName, String projectName)
        {
            bool success = false;
            IProject foundProject = FindProject(projectName);
            if(foundProject != null)
            {
                Employee foundEmployee = FindEmployee(lastName, firstName);
                if(foundEmployee != null)
                {
                    success = foundProject.RemoveEmployee(foundEmployee);
                }
            }
            return success;
        }

        public Employee FindEmployee(String lastName, String firstName)
        {
            Employee tempEmployee = null;
            foreach (Employee people in employees)
            {
                if (people.LastName.Equals(lastName) && people.FirstName.Equals(firstName))
                {
                    tempEmployee = people;
                }
            }

            return tempEmployee;
        }


        public bool AssignManager(String lastName, String firstName, String projectName)
        {
            bool success = false;
            IProject project = FindProject(projectName);
            if (project != null)
            {
                Employee employee = FindEmployee(lastName, firstName);
                if (employee != null)
                {
                    project.Manager = employee;
                    success = true;
                }
            }
            return success;
        }

        public bool RemoveManager(String projectName)
        {
            bool success = false;
            IProject foundProject = FindProject(projectName);
            if(foundProject != null)
            {
                foundProject.Manager = null;
                success = true;
            }
            return success;
        }

        public bool CreateClient(String lastName, String firstName)
        {
            bool success = false;
            if(FindClient(lastName, firstName) == null)
            {
                clients.Add(new Client(lastName, firstName));
                success = true;
            }
            return success;
        }

        public bool AssignClient(String lastName, String firstName, String projectName)
        {
            bool success = false;
            IProject foundProject = FindProject(projectName);
            if(foundProject != null)
            {
                Client foundClient = FindClient(lastName, firstName);
                if(foundClient != null)
                {
                    foundProject.Client = foundClient;
                    success = true;
                }
            }

            return success;
        }

        public bool RemoveClient(String projectName)
        {
            bool success = false;
            IProject foundProject = FindProject(projectName);
            if(foundProject != null)
            {
                foundProject.Client = null;
                success = false;
            }
            return success;
        }

        public Client FindClient(String lastName, String firstName)
        {
            Client tempClient = null;
            foreach (Client people in clients)
            {
                if (people.LastName.Equals(lastName) && people.FirstName.Equals(firstName))
                {
                    tempClient = people;
                }
            }

            return tempClient;
        }
    }
}
