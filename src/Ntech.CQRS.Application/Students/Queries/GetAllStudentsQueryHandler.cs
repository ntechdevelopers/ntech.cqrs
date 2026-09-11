using Microsoft.EntityFrameworkCore;
using Ntech.CQRS.Application.Common;
using Ntech.CQRS.Application.Common.Wrappers;
using Ntech.CQRS.Application.DTOs;
using Ntech.CQRS.Core.Context;

namespace Ntech.CQRS.Application.Students.Queries;

public class GetAllStudentsQueryHandler : IHandlerWrapper<GetAllStudentsQuery, List<StudentDto>>
{
    private readonly SchoolContext _context;

    public GetAllStudentsQueryHandler(SchoolContext context)
    {
        _context = context;
    }

    public async Task<Response<List<StudentDto>>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
        var students = await _context.Students
            .Select(s => new StudentDto
            {
                Id = s.Id,
                Name = s.Name,
                Age = s.Age
            })
            .ToListAsync(cancellationToken);

        return Response<List<StudentDto>>.Ok(students, "Lấy toàn bộ danh sách sinh viên thành công.");
    }
}
