using Microsoft.AspNetCore.Components;
using NewDemo.Models.Model;
using NewDemo.Services.Interface;

namespace NewDemo.ViewModel
{
    public class StudentDetailViewModel
    {
        private readonly IStudentInterface _studentService;
        private readonly NavigationManager _navigationManager;

        public StudentDetailViewModel(IStudentInterface studentService, NavigationManager navigationManager)
        {
            _studentService = studentService ?? throw new ArgumentNullException(nameof(studentService));
            _navigationManager = navigationManager ?? throw new ArgumentNullException(nameof(navigationManager));
        }

        public int StudentId { get; set; }


        public StudentModel student = new();
        public async Task Initialize(int studentId)
        {
            try
            {
                student = await _studentService.GetById(studentId);
                // Additional logic if needed...
            }
            catch (HttpRequestException ex)
            {
                // Handle other types of exceptions if needed
                Console.WriteLine($"HTTP request error: {ex.Message}");
            }
        }




        public void OK()
        {
            _navigationManager.NavigateTo("/");
        }


    }
}

