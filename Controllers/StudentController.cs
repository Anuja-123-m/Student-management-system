using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentRecordManagementSystem.Repository;
using System.Security.Claims;

namespace StudentRecordManagementSystem.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _students;
        public StudentController(IStudentRepository students)
        {
            _students = students;
        }

        public async Task<IActionResult> Index(string? roll = null)
        {
            // Prefer explicit student_id claim if present (Option A schema)
            var studentIdClaim = User.FindFirstValue("student_id");
            if (!string.IsNullOrWhiteSpace(studentIdClaim) && int.TryParse(studentIdClaim, out var studentId))
            {
                var s = await _students.GetByIdAsync(studentId);
                if (s != null) return View(s);
            }

            // Fallback: assume Username equals RollNumber (Option B)
            var usernameRoll = User.Identity?.Name;
            if (!string.IsNullOrWhiteSpace(usernameRoll))
            {
                var student = await _students.GetByRollAsync(usernameRoll);
                if (student != null) return View(student);
            }

            // If user supplied a roll number via querystring/form, try that too
            if (!string.IsNullOrWhiteSpace(roll))
            {
                var byInput = await _students.GetByRollAsync(roll);
                if (byInput != null) return View(byInput);
            }

            // Graceful fallback: render view with an empty model so page loads instead of 404
            ViewBag.Message = "Your record is not linked yet. Once invigilator adds your marks, they will appear here.";
            return View(new StudentRecordManagementSystem.Models.Student());
        }
    }
}
