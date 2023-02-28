using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement
{
    public class Project : IProject
    {
        private static int counter = 1000;
        public static int Counter { get { return counter; } }
        public int ProjectID { get; }
        public String Name { set; get; }
        public String Description { set; get; }
        public Client Client { set; get; }
        public Employee Manager { set; get; }
        private List<Employee> employees;

        public Project() : this("NAMELESS")
        {

        }

        public Project(String name)
        {
            ProjectID = counter++;
            Name = name;
            Description = "Enter the project description here.";
            employees = new List<Employee>();
        }
        public bool AddEmployee(Employee newEmployee)
        {
            bool success = false;
            if(FindEmployee(newEmployee.LastName, newEmployee.FirstName) == null)
            {
                employees.Add(newEmployee);
                success = true;
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
        public bool RemoveEmployee(Employee oldEmployee)
        {
            return employees.Remove(oldEmployee);
        }

        public String EmployeeList
        {
            get
            {
                String output = "\n*** Employee List ***\n";
                foreach (Employee employee in employees)
                {
                    output += employee.FullName + "\n";
                }
                return output;
            }
        }

        override public String ToString()
        {
            String output = "ProjectID = " + ProjectID + "\n";
            output += "Project Name = " + Name + "\n";
            output += "Project Manager = " + (Manager != null?Manager.FullName : "NO MANAGER") + "\n";
            output += "Client = " + (Client != null?Client.FullName : "NO CLIENT") + "\n";
            output += "Description: " + Description;
            return output;
        }
    }
}
