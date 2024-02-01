using Microsoft.AspNetCore.Components;
using NewDemo.Models.Model;
using NewDemo.Services.Interface;

namespace NewDemo.ViewModel
{
    public class StudentDeleteViewModel
    {
        private readonly IStudentInterface _studentService;
        private readonly NavigationManager _navigationManager;

        public StudentDeleteViewModel(IStudentInterface studentService, NavigationManager navigationManager)
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

        public async Task RemoveUser(int userID)
        {
            try
            {
                var deletedStudent = await _studentService.Delete(userID);

                if (deletedStudent != null)
                {
                    // Handle success (e.g., navigate to another page)
                    _navigationManager.NavigateTo("/");
                }
                else
                {
                    // Handle error (e.g., display error message)
                    Console.WriteLine("Student not found or deletion failed.");
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle other types of exceptions if needed
                Console.WriteLine($"HTTP request error: {ex.Message}");
            }
        }


        public void Cancel()
        {
            _navigationManager.NavigateTo("/");
        }


    }
}
