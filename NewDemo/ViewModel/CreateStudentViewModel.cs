// CreateStudentViewModel.cs
using Microsoft.AspNetCore.Components;
using Models.Model;
using NewDemo.Models.Model;
using NewDemo.Services.Interface;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace NewDemo.ViewModel
{
    public class CreateStudentViewModel
    {
        private readonly IStudentInterface _studentService;
        private readonly NavigationManager _navigationManager;

        public CreateStudentViewModel(IStudentInterface studentService, NavigationManager navigationManager)
        {
            _studentService = studentService ?? throw new ArgumentNullException(nameof(studentService));
            _navigationManager = navigationManager ?? throw new ArgumentNullException(nameof(navigationManager));
        }

        public string Title { get; set; } = "Create";
        public StudentModel NewStudent { get; set; } = new();
        public List<GenderModel> Genders { get; set; } = new List<GenderModel>();
        public List<StateModel> States { get; set; } = new List<StateModel>();

        public async Task Initialize()
        {
            try
            {
                States = await _studentService.GetState();
                Genders = await _studentService.GetGender();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request error: {ex.Message}");
            }
        }

        public async Task LoadGenders()
        {
            try
            {
                Genders = await _studentService.GetGender();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request error: {ex.Message}");
            }
        }

        public async Task LoadStates()
        {
            try
            {
                States = await _studentService.GetState();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request error: {ex.Message}");
            }
        }

        public async Task CreateStudent()
        {
            try
            {
                ServiceResponse res = await _studentService.Create(NewStudent);

                if (res.Success)
                {
                    NewStudent = new StudentModel();
                    _navigationManager.NavigateTo("/");
                }
                else
                {
                    Console.WriteLine($"Error: {res.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void Cancel()
        {
            _navigationManager.NavigateTo("/");
        }
    }
}
