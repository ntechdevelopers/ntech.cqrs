using Ntech.CQRS.Application.Common;
using Ntech.CQRS.Application.Common.Wrappers;
using Ntech.CQRS.Application.DTOs;
using Ntech.CQRS.Core.Context;
using Ntech.CQRS.Core.Entities;

namespace Ntech.CQRS.Application.Students.Commands;

public class CreateStudentCommandHandler : IHandlerWrapper<CreateStudentCommand, StudentDto>
{
    private readonly SchoolContext _context;

    public CreateStudentCommandHandler(SchoolContext context)
    {
        _context = context;
    }

    public async Task<Response<StudentDto>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return Response<StudentDto>.Fail("Tên sinh viên không được để trống.");
        }

        if (request.Age <= 0)
        {
            return Response<StudentDto>.Fail("Tuổi sinh viên không hợp lệ.");
        }

        var student = new Student
        {
            Name = request.Name,
            Age = request.Age
        };

        _context.Students.Add(student);
        await _context.SaveChangesAsync(cancellationToken);

        var dto = new StudentDto
        {
            Id = student.Id,
            Name = student.Name,
            Age = student.Age
        };

        return Response<StudentDto>.Ok(dto, "Tạo sinh viên mới thành công.");
    }
}
