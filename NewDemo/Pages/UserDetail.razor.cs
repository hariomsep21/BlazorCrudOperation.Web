using Microsoft.AspNetCore.Components;
using NewDemo.Models.Model;
using NewDemo.Services.Service;
using static System.Net.WebRequestMethods;

namespace NewDemo.Pages
{
    public partial class UserDetail
    {
        [Parameter]
        public int StudentId { get; set; }
        protected override async Task OnParametersSetAsync()
        {
            await StudentVMDelete.Initialize(StudentId);
        }
    }
}
