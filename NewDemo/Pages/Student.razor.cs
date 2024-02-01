using NewDemo.Models.Model;

namespace NewDemo.Pages
{
    public partial class Student
    {
        public bool ShowAction { get; set; } = true;
        protected  override void OnInitialized()
        {
            // Reset SelectedStudentCount to zero when the page is initialized
           parentVM.SelectedStudentCount = 0;
        }


    }
}
