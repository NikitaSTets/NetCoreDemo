﻿namespace NetCoreCheckDemo.University
{
    public class Student
    {
        public string FirstName { get; }

        public string LastName { get; }
        

        public Student(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
