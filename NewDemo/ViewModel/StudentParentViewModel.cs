using Microsoft.AspNetCore.Components;
using NewDemo.Services.Interface;

namespace NewDemo.ViewModel
{
    public class StudentParentViewModel
    {
        private readonly IStudentInterface _studentService;


        public StudentParentViewModel(IStudentInterface studentService)
        {
            _studentService = studentService ?? throw new ArgumentNullException(nameof(studentService));

        }
        [Parameter]
        public int SelectedStudentCount {  get; set; }= 0;
        public bool ShowAction { get; set; } =true;
        public void StudentSelectionChanged(bool isSelected)
        {
            if(isSelected)
            {
                SelectedStudentCount++;
            }
            else
            {
                SelectedStudentCount--;
            }
        }
    }
}
