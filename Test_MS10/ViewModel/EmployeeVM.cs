using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Test_MS10.Common;

namespace Test_MS10.ViewModel;

public class EmployeeVM
{
    public string? Name { get; set; }
    public DateOnly? DoB { get; set; }
    public int? Position { get; set; }
}
