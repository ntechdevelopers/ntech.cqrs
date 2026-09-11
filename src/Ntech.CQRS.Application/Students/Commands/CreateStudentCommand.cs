using Ntech.CQRS.Application.Common.Wrappers;
using Ntech.CQRS.Application.DTOs;

namespace Ntech.CQRS.Application.Students.Commands;

public record CreateStudentCommand(string Name, int Age) : IRequestWrapper<StudentDto>;
