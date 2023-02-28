using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement
{
    public interface IPeople
    {
        int Identifier { get; }
        String FirstName { set; get; }
        String LastName { set; get; }
        String FullName { get; }
        String ToString();
    }

    public interface IProject
    {
        int ProjectID { get; }
        String Name { set; get; }
        String Description { set; get; }
        Client Client { set; get; }
        Employee Manager { set; get; }
        bool AddEmployee(Employee newEmployee);
        bool RemoveEmployee(Employee oldEmployee);
        Employee FindEmployee(String lastName, String firstName);
        String EmployeeList { get; }
        String ToString();
    }

    public interface IProjectDB
    {
        String Name { set; get; }
        bool CreateProject(String name);
        IProject FindProject(String name);
        bool CreateEmployee(String lastName, String firstName);
        bool AssignEmployee(String lastName, String firstName, String projectName);
        bool RemoveEmployee(String lastName, String firstName, String projectName);
        Employee FindEmployee(String lastName, String firstName);
        bool AssignManager(String lastName, String firstName, String projectName);
        bool RemoveManager(String projectName);
        bool CreateClient(String lastName, String firstName);
        bool AssignClient(String lastName, String firstName, String projectName);
        bool RemoveClient(String projectName);
        Client FindClient(String lastName, String firstName);
        String EmployeeList { get; }
        String ClientList { get; }
        String ProjectList { get; }
    }
}
