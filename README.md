# Ntech.CQRS - Triển khai CQRS với MediatR trên .NET 10

Dự án mẫu minh họa mô hình CQRS (Command and Query Responsibility Segregation) trong .NET 10 sử dụng thư viện MediatR, Entity Framework Core InMemory và Response Wrapper.

## Kiến Trúc Dự Án

Mô hình được tổ chức thành 3 dự án độc lập:

1. **`Ntech.CQRS.Core`**: Chứa Domain Entities (`Student`) và Data Access Layer (`SchoolContext` kế thừa `DbContext`).
2. **`Ntech.CQRS.Application`**: Chứa logic nghiệp vụ CQRS:
   - **Response Wrapper**: Lớp `Response<T>` chuẩn hóa định dạng kết quả trả về API.
   - **MediatR Wrappers**: `IRequestWrapper<TResponse>` và `IHandlerWrapper<TRequest, TResponse>`.
   - **Commands**: `CreateStudentCommand` và `CreateStudentCommandHandler`.
   - **Queries**: `GetStudentsByAgeQuery`, `GetAllStudentsQuery` và các Handler tương ứng.
   - **DTOs**: `StudentDto`.
3. **`Ntech.CQRS.Api`**: ASP.NET Core Web API controller (`StudentsController`) giao tiếp với client, đăng ký DI cho MediatR & EF Core.

---

## Hướng Dẫn Chạy Dự Án

### Yêu cầu hệ thống
- .NET 10 SDK (`dotnet --version` => `10.x`)

### Build dự án
```bash
dotnet build Ntech.CQRS.slnx
```

### Chạy API
```bash
dotnet run --project src/Ntech.CQRS.Api/Ntech.CQRS.Api.csproj
```

Sau khi ứng dụng khởi chạy, truy cập Swagger UI tại:
- `http://localhost:5000/swagger` hoặc `https://localhost:5001/swagger`

---

## API Endpoints

### 1. Lấy toàn bộ danh sách sinh viên
- **HTTP Method**: `GET`
- **URL**: `/api/students`

### 2. Lấy danh sách sinh viên theo độ tuổi
- **HTTP Method**: `GET`
- **URL**: `/api/students/age/{age}`
- **Ví dụ**: `/api/students/age/20`
- **Phản hồi**:
```json
{
  "success": true,
  "message": "Lấy danh sách sinh viên 20 tuổi thành công.",
  "data": [
    {
      "id": 1,
      "name": "Nguyen Van A",
      "age": 20
    },
    {
      "id": 2,
      "name": "Tran Thi B",
      "age": 20
    }
  ],
  "errorDetails": null
}
```

### 3. Tạo mới sinh viên
- **HTTP Method**: `POST`
- **URL**: `/api/students`
- **Body**:
```json
{
  "name": "Pham Van D",
  "age": 21
}
```
- **Phản hồi**:
```json
{
  "success": true,
  "message": "Tạo sinh viên mới thành công.",
  "data": {
    "id": 4,
    "name": "Pham Van D",
    "age": 21
  },
  "errorDetails": null
}
```
