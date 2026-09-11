using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ntech.CQRS.Application.Common;
using Ntech.CQRS.Application.DTOs;
using Ntech.CQRS.Application.Students.Commands;
using Ntech.CQRS.Application.Students.Queries;

namespace Ntech.CQRS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public StudentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Lấy danh sách toàn bộ sinh viên
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<Response<List<StudentDto>>>> GetAllStudents()
    {
        var result = await _mediator.Send(new GetAllStudentsQuery());
        return Ok(result);
    }

    /// <summary>
    /// Lấy danh sách sinh viên theo độ tuổi
    /// </summary>
    [HttpGet("age/{age:int}")]
    public async Task<ActionResult<Response<List<StudentDto>>>> GetStudentsByAge(int age)
    {
        var result = await _mediator.Send(new GetStudentsByAgeQuery(age));
        return Ok(result);
    }

    /// <summary>
    /// Tạo mới một sinh viên
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Response<StudentDto>>> CreateStudent([FromBody] CreateStudentCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
}
