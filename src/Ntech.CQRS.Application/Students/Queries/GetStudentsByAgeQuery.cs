using Ntech.CQRS.Application.Common.Wrappers;
using Ntech.CQRS.Application.DTOs;

namespace Ntech.CQRS.Application.Students.Queries;

public record GetStudentsByAgeQuery(int Age) : IRequestWrapper<List<StudentDto>>;
