using Xunit;
using System;
using System.Collections.Generic;

public class JsonServiceTests
{
    [Fact]
    public void Serialize_ReturnsJson()
    {
        var service = new JsonService();

        var student = new Student
        {
            FirstName = "Ivan",
            LastName = "Ivanov",
            BirthDate = new DateTime(2005, 5, 10),
            Grades = new List<Subject>
            {
                new Subject
                {
                    Name = "Math",
                    Grade = 5
                }
            }
        };

        var json = service.Serialize(student);

        Assert.Contains("Ivan", json);
    }

    [Fact]
    public void Deserialize_ReturnsStudent()
    {
        var service = new JsonService();

        var json =
@"{
    ""FirstName"":""Ivan"",
    ""LastName"":""Ivanov"",
    ""BirthDate"":""10.05.2005"",
    ""Grades"":[]
}";

        var student = service.Deserialize(json);

        Assert.Equal("Ivan", student.FirstName);
    }

    [Fact]
    public void SaveAndLoadFile()
    {
        var service = new JsonService();

        var student = new Student
        {
            FirstName = "Ivan",
            LastName = "Ivanov",
            BirthDate = new DateTime(2005, 5, 10),
            Grades = new List<Subject>()
        };

        service.SaveToFile(student, "student.json");

        var result = service.LoadFromFile("student.json");

        Assert.Equal("Ivan", result.FirstName);
    }

    [Fact]
    public void Deserialize_InvalidStudent_ReturnsNull()
    {
        var service = new JsonService();

        var json =
@"{
    ""FirstName"":"""",
    ""LastName"":""Ivanov"",
    ""BirthDate"":""10.05.2005"",
    ""Grades"":[]
}";

        var student = service.Deserialize(json);

        Assert.Null(student);
    }
}
