
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementAPI.Data;
using StudentManagementAPI.Models.Entity;
using StudentManagementAPI.Models.DTO;

namespace StudentManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentDbContext dbContext;

        public StudentsController(StudentDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: api/students
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await dbContext.Students.ToListAsync();

            return Ok(students);
        }

        // GET: api/students/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetStudentById(Guid id)
        {
            var student = await dbContext.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound("Student not found");
            }

            return Ok(student);
        }

        // POST: api/students
        [HttpPost]
        public async Task<IActionResult> AddStudent(AddStudentDto dto)
        {
            var student = new Student
            {
                StudentId = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Age = dto.Age,
                Course = dto.Course,
                Address = dto.Address
            };

            await dbContext.Students.AddAsync(student);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetStudentById),
                new { id = student.StudentId },
                student
            );
        }

        // PUT: api/students/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateStudent(
            Guid id, UpdateStudentDto dto)
        {
            var student = await dbContext.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound("Student not found");
            }

            student.Name = dto.Name;
            student.Email = dto.Email;
            student.Phone = dto.Phone;
            student.Age = dto.Age;
            student.Course = dto.Course;
            student.Address = dto.Address;

            await dbContext.SaveChangesAsync();

            return Ok(student);
        }

        // DELETE: api/students/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            var student = await dbContext.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound("Student not found");
            }

            dbContext.Students.Remove(student);
            await dbContext.SaveChangesAsync();

            return Ok("Student deleted successfully");
        }
    }
}
