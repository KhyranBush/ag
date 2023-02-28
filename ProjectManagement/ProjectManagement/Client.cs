using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement
{
    public class Client : IPeople
    {
        private static int counter = 500;
        public static int Counter { get { return counter; } }
        public int Identifier { get; }
        public String FirstName { set; get; }
        public String LastName { set; get; }
        public String FullName { get { return FirstName + " " + LastName; } }

        public Client() : this("_")
        {
        }
        public Client(String lastName) : this(lastName, "_")
        {
        }

        // Designated Constructor
        public Client(String lastName, String firstName)
        {
            Identifier = counter++;
            FirstName = firstName;
            LastName = lastName;
        }
        override public String ToString()
        {
            return Identifier + " - " + FullName;
        }
    }
}
