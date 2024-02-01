using Models.Model;
using NewDemo.Models.Model;
using NewDemo.Services.Interface;
using NewDemo.Services.Url;
using Newtonsoft.Json;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace NewDemo.Services.Service
{
    public class StudentService : IStudentInterface
    {
        private readonly HttpClient _httpClient;

        private readonly string _createUrl = ServiceUrl.CreateUrl;
        private readonly string _deleteUrl = ServiceUrl.DeleteUrl;
        private readonly string _getByIdUrl = ServiceUrl.GetByIdUrl;
        private readonly string _getStudentUrl = ServiceUrl.GetStudentUrl;
        private readonly string _updateUrl = ServiceUrl.UpdateUrl;
        private readonly string _getGenderUrl = ServiceUrl.GetGender;
        private readonly string _getStateUrl = ServiceUrl.GetStateUrl;
        public StudentService(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }
        public async Task<ServiceResponse> Create(StudentModel student)
        {
            // Send an HTTP POST request to the specified API endpoint with the provided StudentModel as JSON.
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(_createUrl, student);

            // Read the response content as a string.
            string responseBody = await response.Content.ReadAsStringAsync();

            // Check if the response is successful (status code 2xx).
            if (response.IsSuccessStatusCode)
            {
                // Deserialize the JSON response into a ServiceResponse object.
                ServiceResponse serviceResponse = JsonConvert.DeserializeObject<ServiceResponse>(responseBody);

                // Consider the operation successful, even if GenderName or StateName is not present in the response.
                serviceResponse.Success = true;

                return serviceResponse;
            }
            else
            {
                // Log the response content for debugging
                Console.WriteLine($"Response Content: {responseBody}");

                // Deserialize the JSON response into a ServiceResponse object for error cases.
                ServiceResponse errorResponse = JsonConvert.DeserializeObject<ServiceResponse>(responseBody);
                return errorResponse;
            }
        }




        public async Task<StudentModel> Delete(int id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_deleteUrl}Delete?Id={id}");

                // Check if the response indicates a failure (non-success status code)
                if (!response.IsSuccessStatusCode)
                {
                    // Handle 404 Not Found
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        // You might choose to throw an exception or return null here
                        // For now, let's throw an exception
                        throw new HttpRequestException($"HTTP request error: {response.StatusCode} - {response.ReasonPhrase}");
                    }

                    // Handle other non-success status codes if needed
                    // For example, you might throw a custom exception with details from the response
                    throw new HttpRequestException($"HTTP request error: {response.StatusCode} - {response.ReasonPhrase}");
                }

                // If the response was successful, return the deleted item
                // Assuming that the response body contains the deleted student model
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<StudentModel>(responseBody);
            }
            catch (HttpRequestException ex)
            {
                // Catch and rethrow any HttpRequestException, providing a more specific error message.
                throw new HttpRequestException($"HTTP request error: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Catch and rethrow any other exception type, providing a more generic error message.
                throw new Exception($"Exception: {ex.Message}");
            }
        }


        // Change the return type of GetById to directly return StudentModel
        public async Task<StudentModel> GetById(int id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_getByIdUrl}{id}");

                // Check if the response indicates a failure (non-success status code)
                if (!response.IsSuccessStatusCode)
                {
                    // Handle non-success status codes
                    string errorResponse = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Error: {response.StatusCode}. Response: {errorResponse}");
                }

                // Read the response content as a string.
                string responseBody = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON response into a StudentModel object.
                return JsonConvert.DeserializeObject<StudentModel>(responseBody);
            }
            catch (HttpRequestException ex)
            {
                // Catch and rethrow any HttpRequestException, providing a more specific error message.
                throw new HttpRequestException($"HTTP request error: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Catch and rethrow any other exception type, providing a more generic error message.
                throw new Exception($"Exception: {ex.Message}");
            }
        }

        public async Task<IEnumerable<StudentModel>> GetStudents()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_getStudentUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    try
                    {
                        // Deserialize the JSON array to a list of StudentModel
                        var dtos = JsonConvert.DeserializeObject<List<StudentModel>>(responseBody);
                        return dtos ?? new List<StudentModel>();
                    }
                    catch (JsonSerializationException)
                    {
                        // If deserialization as a list fails, try deserializing as a single object
                        var dto = JsonConvert.DeserializeObject<StudentModel>(responseBody);
                        return dto != null ? new List<StudentModel> { dto } : new List<StudentModel>();
                    }
                }
                else
                {
                    // Handle non-success status codes
                    string errorResponse = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Error: {response.StatusCode}. Response: {errorResponse}");
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle general HttpRequestException
                throw new HttpRequestException($"HTTP request error: {ex.Message}. URL: {_getStudentUrl}", ex);
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                throw new Exception($"Exception: {ex.Message}. URL: {_getStudentUrl}", ex);
            }
        }
        public async Task<List<StateModel>> GetState()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_getStateUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    try
                    {
                        // Deserialize the JSON array to a list of StudentModel
                        var dtos = JsonConvert.DeserializeObject<List<StateModel>>(responseBody);
                        return dtos ?? new List<StateModel>();
                    }
                    catch (JsonSerializationException)
                    {
                        // If deserialization as a list fails, try deserializing as a single object
                        var dto = JsonConvert.DeserializeObject<StateModel>(responseBody);
                        return dto != null ? new List<StateModel> { dto } : new List<StateModel>();
                    }
                }
                else
                {
                    // Handle non-success status codes
                    string errorResponse = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Error: {response.StatusCode}. Response: {errorResponse}");
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle general HttpRequestException
                throw new HttpRequestException($"HTTP request error: {ex.Message}. URL: {_getStateUrl}", ex);
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                throw new Exception($"Exception: {ex.Message}. URL: {_getStateUrl}", ex);
            }
        }
        public async Task<List<GenderModel>> GetGender()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_getGenderUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    try
                    {
                        // Deserialize the JSON array to a list of StudentModel
                        var dtos = JsonConvert.DeserializeObject<List<GenderModel>>(responseBody);
                        return dtos ?? new List<GenderModel>();
                    }
                    catch (JsonSerializationException)
                    {
                        // If deserialization as a list fails, try deserializing as a single object
                        var dto = JsonConvert.DeserializeObject<GenderModel>(responseBody);
                        return dto != null ? new List<GenderModel> { dto } : new List<GenderModel>();
                    }
                }
                else
                {
                    // Handle non-success status codes
                    string errorResponse = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Error: {response.StatusCode}. Response: {errorResponse}");
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle general HttpRequestException
                throw new HttpRequestException($"HTTP request error: {ex.Message}. URL: {_getGenderUrl}", ex);
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                throw new Exception($"Exception: {ex.Message}. URL: {_getGenderUrl}", ex);
            }
        }

        public async Task<ServiceResponse> Update(StudentModel student)
        {

            try
            {

                HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"{_updateUrl}", student);

                // Ensure that the HTTP request was successful (status code 2xx).
                response.EnsureSuccessStatusCode();

                // Read the response content as a string.
                string responseBody = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON response into a StateDto object.
                return JsonConvert.DeserializeObject<ServiceResponse>(responseBody);
            }
            catch (HttpRequestException ex)
            {
                // Catch and rethrow any HttpRequestException, providing a more specific error message.
                throw new HttpRequestException($"HTTP request error: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Catch and rethrow any other exception type, providing a more generic error message.
                throw new Exception($"Exception: {ex.Message}");
            }
        }


    }
}
