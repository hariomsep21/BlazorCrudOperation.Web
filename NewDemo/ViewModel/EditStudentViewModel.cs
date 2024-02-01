// EditStudentViewModel.cs
using Microsoft.AspNetCore.Components;
using Models.Model;
using NewDemo.Models.Model;
using NewDemo.Services.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;

namespace NewDemo.ViewModel
{
    public class EditStudentViewModel
    {
        private readonly IStudentInterface _studentService;
        private readonly NavigationManager _navigationManager;

        public EditStudentViewModel(IStudentInterface studentService, NavigationManager navigationManager)
        {
            _studentService = studentService ?? throw new ArgumentNullException(nameof(studentService));
            _navigationManager = navigationManager ?? throw new ArgumentNullException(nameof(navigationManager));
        }

        public string Title { get; set; } = "Edit";
        public StudentModel NewStudent { get; set; } = new();
        public List<GenderModel> Genders { get; set; } = new List<GenderModel>();
        public List<StateModel> States { get; set; } = new List<StateModel>();
        public string? ExistingGenderId { get; set; }  // Store original GenderID
        public string? ExistingStateId { get; set; }  // Store original StateID

        public async Task Initialize(int studentId)
        {
            try
            {
                NewStudent = await _studentService.GetById(studentId);
                ExistingGenderId = NewStudent.GenderID.ToString();
                ExistingStateId = NewStudent.StateID.ToString();
                States = await _studentService.GetState();
                Genders = await _studentService.GetGender();

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request error: {ex.Message}");
            }
        }

        public async Task UpdateStudent()
        {
            if (NewStudent.Id != 0)
            {
                NewStudent.GenderID = Convert.ToInt32( ExistingGenderId.ToString());  // Assign default if needed
                NewStudent.StateID = Convert.ToInt32(ExistingStateId.ToString());   // Assign default if needed

                ServiceResponse res = await _studentService.Update(NewStudent);

                if (res.Success)
                {
                    _navigationManager.NavigateTo("/");
                }
                else
                {
                    _navigationManager.NavigateTo("/");
                    Console.WriteLine($"Error: {res.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Error: ");
            }
        }

        // ... (other methods remain unchanged)



        public void ValidateField(object model, string propertyName)
        {
            var validationContext = new ValidationContext(model, null, null) { MemberName = propertyName };
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateProperty(model.GetType().GetProperty(propertyName)?.GetValue(model), validationContext, validationResults))
            {
                // Handle validation errors or update UI as needed
            }
        }

        public void ClearValidationMessage(object model, string propertyName)
        {
            // Clear validation error message when the user focuses on the input field
            typeof(StudentModel).GetProperty(propertyName)?.SetValue(model, null);
        }

        public void Cancel()
        {
            _navigationManager.NavigateTo("/");
        }
    }
}
