using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Student
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime BirthDate { get; set; }

    public List<Subject> Grades { get; set; }
}
