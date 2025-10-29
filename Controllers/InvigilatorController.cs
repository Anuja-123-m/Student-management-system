using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StudentRecordManagementSystem.Repository;
using StudentRecordManagementSystem.Models;

namespace StudentRecordManagementSystem.Controllers
{
    [Authorize(Roles = "Invigilator")]
    public class InvigilatorController : Controller
    {
        private readonly IStudentRepository _students;

        public InvigilatorController(IStudentRepository students)
        {
            _students = students;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _students.GetAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Details(string rollNumber)
        {
            if (string.IsNullOrWhiteSpace(rollNumber)) return NotFound();
            var student = await _students.GetByRollAsync(rollNumber);
            if (student == null) return NotFound();
            return View(student);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Student());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            if (!ModelState.IsValid) return View(student);
            await _students.AddStudentAsync(student);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string rollNumber)
        {
            if (string.IsNullOrWhiteSpace(rollNumber)) return NotFound();
            var student = await _students.GetByRollAsync(rollNumber);
            if (student == null) return NotFound();
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string rollNumber, Student student)
        {
            if (string.IsNullOrWhiteSpace(rollNumber)) return NotFound();
            if (!ModelState.IsValid)
            {
                student.RollNumber = rollNumber;
                return View(student);
            }
            await _students.UpdateMarksAsync(rollNumber, student);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Disable(string rollNumber)
        {
            if (string.IsNullOrWhiteSpace(rollNumber)) return NotFound();
            await _students.DisableStudentAsync(rollNumber);
            return RedirectToAction(nameof(Index));
        }
    }
}
