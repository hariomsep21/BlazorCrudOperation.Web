using Microsoft.AspNetCore.Components;
using NewDemo.Models.Model;
using NewDemo.Services.Interface;
using NewDemo.Services.Service;

namespace NewDemo.ViewModel
{
    public class StudentListViewModel
    {
        private readonly IStudentInterface _studentService;
        

        public StudentListViewModel(IStudentInterface studentService)
        {
            _studentService = studentService ?? throw new ArgumentNullException(nameof(studentService));

        }

      public  IEnumerable<StudentModel> EmpObj;
        protected StudentModel student = new();

        public  async Task Initialize()
        {
            EmpObj = await _studentService.GetStudents();
        }

    }
}
