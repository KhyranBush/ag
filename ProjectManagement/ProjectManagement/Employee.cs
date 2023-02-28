using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement
{
    public class Employee : IPeople
    {
        private static int counter = 100;
        public static int Counter { get { return counter; } }
        public int Identifier { get; }
        public String FirstName { set; get; }
        public String LastName { set; get; }
        public String FullName { get { return FirstName + " " + LastName; } }

        public Employee() : this("_")
        {
        }
        public Employee(String lastName) : this(lastName, "_")
        {
        }

        // Designated Constructor
        public Employee(String lastName, String firstName)
        {
            Identifier = counter++;
            FirstName = firstName;
            LastName = lastName;        }
        override public String ToString()
        {
            return Identifier + " - " + FullName;
        }

    }
}
