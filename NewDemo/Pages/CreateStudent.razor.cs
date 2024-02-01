// CreateStudent.razor.cs
using Microsoft.AspNetCore.Components;

namespace NewDemo.Pages
{
    public partial class CreateStudent
    {
        [Parameter]
        public int studentId { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            await StudentVM.Initialize();
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
                StudentVM.NewStudent.StateName = string.Empty;
            }
        }
    }
}
