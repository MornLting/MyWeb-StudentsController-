namespace MyWeb.Models;

public class Major
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<Class> ClassList { get; set; } = new List<Class>();
}
