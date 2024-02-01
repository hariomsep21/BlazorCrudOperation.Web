using Microsoft.AspNetCore.Components.Forms;

using NewDemo.Models.Model;
using NewDemo.Services.Interface;
using static System.Net.WebRequestMethods;
using System.ComponentModel.DataAnnotations;
using NewDemo.Services.Service;

using NewDemo.ViewModel;
using Microsoft.AspNetCore.Components;
using Models.Model;

namespace  NewDemo.Pages

{

    public partial class EditStudent
    {
        [Parameter]
        public int studentId { get; set; }
        protected override async Task OnParametersSetAsync()
        {
            if (studentId != 0)
            {
                await StudentVM.Initialize(studentId);
            }
            else
            {
                // Create a new instance of the view model for "Add" operation
                StudentVM = new EditStudentViewModel(_studentService, NavigationManager);
            }
        }
        void UpdateStateName(ChangeEventArgs e)
        {
            var selectedStateId = int.Parse(e.Value.ToString());
            var selectedState = StudentVM.States.FirstOrDefault(s => s.StateID == selectedStateId);

            if (selectedState != null)
            {
                StudentVM.NewStudent.StateName = selectedState.StateName;
            }
            else
            {
                // Handle the case where the selected state is not found
                StudentVM.NewStudent.StateName = string.Empty;
            }
        }

    }
}

    

