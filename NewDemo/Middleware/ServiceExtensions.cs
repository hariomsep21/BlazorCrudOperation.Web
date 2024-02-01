


using NewDemo.Services.Interface;
using NewDemo.Services.Service;
using NewDemo.ViewModel;

namespace NewDemo.Middleware
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7276") });
            services.AddScoped<IStudentInterface, StudentService>();
            services.AddScoped<CreateStudentViewModel>();
            services.AddScoped<EditStudentViewModel>();
            services.AddScoped<StudentDeleteViewModel>();
            services.AddScoped<StudentListViewModel>();
            services.AddScoped<StudentParentViewModel>();
            services.AddScoped<StudentDetailViewModel>();

            // Add other services here...

            return services;
        }
    }
}