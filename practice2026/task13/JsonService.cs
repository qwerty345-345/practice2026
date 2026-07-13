using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

public class JsonService
{
    private JsonSerializerOptions options;

    public JsonService()
    {
        options = new JsonSerializerOptions();

        options.WriteIndented = true;

        options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

        options.Converters.Add(new DateConverter());
    }

    public string Serialize(Student student)
    {
        return JsonSerializer.Serialize(student, options);
    }

    public Student Deserialize(string json)
    {
        var student = JsonSerializer.Deserialize<Student>(json, options);

        if (student == null)
            return null;

        if (string.IsNullOrWhiteSpace(student.FirstName))
            return null;

        if (string.IsNullOrWhiteSpace(student.LastName))
            return null;

        return student;
    }

    public void SaveToFile(Student student, string path)
    {
        File.WriteAllText(path, Serialize(student));
    }

    public Student LoadFromFile(string path)
    {
        var json = File.ReadAllText(path);

        return Deserialize(json);
    }
}
