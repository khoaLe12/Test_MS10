using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_MS10.Common;
using Test_MS10.Entity;
using Test_MS10.Repository;
using Test_MS10.ViewModel;

namespace Test_MS10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IValidateGet _validateGet;

        public EmployeeController(IEmployeeRepository employeeRepository, IMapper mapper, IValidateGet validateGet)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _validateGet = validateGet;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees([FromQuery] int startPage, [FromQuery] int endPage, [FromQuery] int? quantity)
        {
            if (ModelState.IsValid)
            {
                int quantityResult = 0;
                _validateGet.ValidateGetRequest(ref startPage, ref endPage, quantity, ref quantityResult);
                if (quantityResult == 0)
                {
                    return BadRequest(new
                    {
                        Title = "Get employees failed",
                        Errors = new string[1] { "Invalid get quantity" }
                    });
                }

                var result = await _employeeRepository
                    .Get(_ => true)
                    .Skip((startPage - 1) * quantityResult)
                    .Take((endPage - startPage + 1) * quantityResult)
                    .ToListAsync();

                return Ok(new
                {
                    Title = "Get successfully",
                    Result = _mapper.Map<IEnumerable<EmployeeResponseVM>>(result)
                });
            }

            return BadRequest(new
            {
                Title = "Get employees failed",
                Errors = new string[1] { "Invalid input" }
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewEmployee([FromBody]EmployeeVM employee)
        {
            if (ModelState.IsValid && employee.Name != string.Empty && employee.DoB != null && employee.Position != null)
            {
                int order = await _employeeRepository.GetLatestOrder();
                var newEmployee = new Employee
                {
                    Code = "NV_" + employee.DoB.GetValueOrDefault().ToString("yyyy_MM_dd") + "_" + order.ToString(),
                    Name = employee.Name!,
                    DoB = employee.DoB.GetValueOrDefault(),
                    Position = employee.Position ?? 0
                };
                await _employeeRepository.AddAsync(newEmployee);
                var result = await _employeeRepository.SaveChangesAsync();
                if (result)
                {
                    return Ok(new
                    {
                        Title = "Create successfully",
                        Result = _mapper.Map<EmployeeResponseVM>(newEmployee)
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        Title = "Create employee failed",
                        Errors = new string[1] { "Invalid input" }
                    });
                }
            }

            return BadRequest(new
            {
                Title = "Create employee failed",
                Errors = new string[1] { "Invalid input" }
            });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(EmployeeVM employee, string code)
        {
            if (ModelState.IsValid && code != string.Empty)
            {
                var existedEmployee = _employeeRepository
                    .Get(e => e.Code.Equals(code))
                    .FirstOrDefault();
                if (existedEmployee == null)
                {
                    return BadRequest(new
                    {
                        Title = "Update employee failed",
                        Errors = new string[1] { "Employee not found" }
                    });
                }

                if(employee.Name != null)
                {
                    existedEmployee.Name = employee.Name;
                }

                if(employee.DoB != null)
                {
                    var oldDob = existedEmployee.DoB;
                    var newDob = employee.DoB.GetValueOrDefault();

                    existedEmployee.DoB = newDob;
                    existedEmployee.Code = existedEmployee.Code.Replace(oldDob.ToString("yyyy_MM_dd"), newDob.ToString("yyyy_MM_dd"));
                }

                if(employee.Position != null)
                {
                    existedEmployee.Position = employee.Position ?? 0;
                }

                _employeeRepository.Update(existedEmployee);
                var result = await _employeeRepository.SaveChangesAsync();

                if (result)
                {
                    return Ok(new
                    {
                        Title = "Update successfully",
                        Result = _mapper.Map<EmployeeResponseVM>(existedEmployee)
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        Title = "Update employee failed",
                        Errors = new string[1] { "Invalid input" }
                    });
                }
            }

            return BadRequest(new
            {
                Title = "Update employee failed",
                Errors = new string[1] { "Invalid input" }
            });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(string code)
        {
            if(ModelState.IsValid && code != string.Empty)
            {
                var existedEmployee = _employeeRepository
                    .Get(e => e.Code.Equals(code))
                    .FirstOrDefault();
                if (existedEmployee == null)
                {
                    return BadRequest(new
                    {
                        Title = "Delete employee failed",
                        Errors = new string[1] { "Employee not found" }
                    });
                }

                _employeeRepository.Remove(existedEmployee);
                var result = await _employeeRepository.SaveChangesAsync();

                if (result)
                {
                    return Ok(new
                    {
                        Title = "Delete successfully"
                    });
                }

                return BadRequest(new
                {
                    Title = "Delete employee failed",
                    Errors = new string[1] { "Invalid input" }
                });
            }

            return BadRequest(new
            {
                Title = "Delete employee failed",
                Errors = new string[1] { "Invalid input" }
            });
        }
    }
}
