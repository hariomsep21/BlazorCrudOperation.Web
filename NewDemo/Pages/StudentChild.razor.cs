using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace NewDemo.Pages
{
    public partial class StudentChild
    {

        [Parameter]
        public bool ShowAction { get; set; }
        [Parameter]
        public EventCallback<bool> OnStudentSelection { get; set; }
        public async Task CheckBoxChanged(ChangeEventArgs e)
        {
            await OnStudentSelection.InvokeAsync((bool)e.Value);


        }

        protected override async Task OnParametersSetAsync()
        {
            await listVM.Initialize();
        }
    }
}
