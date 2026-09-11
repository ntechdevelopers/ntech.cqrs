using Microsoft.EntityFrameworkCore;
using Ntech.CQRS.Application.Common;
using Ntech.CQRS.Application.Common.Wrappers;
using Ntech.CQRS.Application.DTOs;
using Ntech.CQRS.Core.Context;

namespace Ntech.CQRS.Application.Students.Queries;

public class GetStudentsByAgeQueryHandler : IHandlerWrapper<GetStudentsByAgeQuery, List<StudentDto>>
{
    private readonly SchoolContext _context;

    public GetStudentsByAgeQueryHandler(SchoolContext context)
    {
        _context = context;
    }

    public async Task<Response<List<StudentDto>>> Handle(GetStudentsByAgeQuery request, CancellationToken cancellationToken)
    {
        var students = await _context.Students
            .Where(s => s.Age == request.Age)
            .Select(s => new StudentDto
            {
                Id = s.Id,
                Name = s.Name,
                Age = s.Age
            })
            .ToListAsync(cancellationToken);

        return Response<List<StudentDto>>.Ok(students, $"Lấy danh sách sinh viên {request.Age} tuổi thành công.");
    }
}
