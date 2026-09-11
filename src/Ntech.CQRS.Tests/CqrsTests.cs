using Microsoft.EntityFrameworkCore;
using Ntech.CQRS.Application.Students.Commands;
using Ntech.CQRS.Application.Students.Queries;
using Ntech.CQRS.Core.Context;
using Xunit;

namespace Ntech.CQRS.Tests;

public class CqrsTests
{
    private SchoolContext CreateInMemoryDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<SchoolContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;
        return new SchoolContext(options);
    }

    [Fact]
    public async Task CreateStudentCommand_ShouldAddStudentSuccessfully()
    {
        // Arrange
        using var context = CreateInMemoryDbContext(nameof(CreateStudentCommand_ShouldAddStudentSuccessfully));
        var handler = new CreateStudentCommandHandler(context);
        var command = new CreateStudentCommand("Nguyen Van A", 20);

        // Act
        var response = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(response.Success);
        Assert.NotNull(response.Data);
        Assert.Equal("Nguyen Van A", response.Data.Name);
        Assert.Equal(20, response.Data.Age);
        Assert.True(response.Data.Id > 0);

        var dbStudent = await context.Students.FirstOrDefaultAsync(s => s.Id == response.Data.Id);
        Assert.NotNull(dbStudent);
        Assert.Equal("Nguyen Van A", dbStudent.Name);
    }

    [Fact]
    public async Task CreateStudentCommand_WithEmptyName_ShouldReturnFailResponse()
    {
        // Arrange
        using var context = CreateInMemoryDbContext(nameof(CreateStudentCommand_WithEmptyName_ShouldReturnFailResponse));
        var handler = new CreateStudentCommandHandler(context);
        var command = new CreateStudentCommand("", 20);

        // Act
        var response = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(response.Success);
        Assert.Equal("Tên sinh viên không được để trống.", response.Message);
    }

    [Fact]
    public async Task GetStudentsByAgeQuery_ShouldReturnMatchingStudents()
    {
        // Arrange
        using var context = CreateInMemoryDbContext(nameof(GetStudentsByAgeQuery_ShouldReturnMatchingStudents));
        context.Students.AddRange(
            new Core.Entities.Student { Name = "Student 1", Age = 20 },
            new Core.Entities.Student { Name = "Student 2", Age = 20 },
            new Core.Entities.Student { Name = "Student 3", Age = 25 }
        );
        await context.SaveChangesAsync();

        var handler = new GetStudentsByAgeQueryHandler(context);
        var query = new GetStudentsByAgeQuery(20);

        // Act
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(response.Success);
        Assert.NotNull(response.Data);
        Assert.Equal(2, response.Data.Count);
        Assert.All(response.Data, s => Assert.Equal(20, s.Age));
    }
}
