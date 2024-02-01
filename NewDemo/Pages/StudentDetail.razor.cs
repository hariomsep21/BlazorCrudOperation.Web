using Microsoft.AspNetCore.Components;

namespace NewDemo.Pages
{
    public partial class StudentDetail
    {
        [Parameter]
        public int StudentId { get; set; }
        protected override async Task OnParametersSetAsync()
        {
            await StudentVMDetail.Initialize(StudentId);
        }
    }
}

