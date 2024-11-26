namespace Test_MS10.ViewModel;

public class EmployeeResponseVM
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateOnly DoB { get; set; }
    public int Age { get; set; }
    public string Position { get; set; } = string.Empty;
}
