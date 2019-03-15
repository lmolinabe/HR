using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HR.Persistence;
using Newtonsoft.Json;

namespace HR_WPF
{
    public class WebApiClient
    {
        private static HttpClient httpClient = new HttpClient();

        public WebApiClient()
        {
            httpClient.BaseAddress = new Uri("http://localhost:50153/api/");
        }

        public List<Employee> GetAllEmployees()
        {
            var result = httpClient.GetAsync("Employee").Result;
            var employees = new List<Employee>();

            if (result.IsSuccessStatusCode)
            {
                employees = result.Content.ReadAsAsync<List<Employee>>().Result;
            }

            return employees;
        }

        public Employee GetEmployee(int employeeId)
        {
            var result = httpClient.GetAsync($"Employee/{employeeId}").Result;
            var employee = new Employee();

            if (result.IsSuccessStatusCode)
            {
                employee = result.Content.ReadAsAsync<Employee>().Result;
            }

            return employee;
        }

        public void AddEmployee(Employee employee) {
            var json = JsonConvert.SerializeObject(employee);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var result = httpClient.PostAsync("Employee", stringContent).Result;

            if (result.IsSuccessStatusCode)
            {
                //Log something
            }
        }

        public void EditEmployee(Employee employee) {
            var json = JsonConvert.SerializeObject(employee);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var result = httpClient.PutAsync($"Employee/{employee.Id}", stringContent).Result;

            if (result.IsSuccessStatusCode)
            {
                //Log something
            }
        }

        public void DeleteEmployee(int employeeId)
        {
            var result = httpClient.DeleteAsync($"Employee/{employeeId}").Result;

            if (result.IsSuccessStatusCode)
            {
                //Log something
            }
        }
    }
}
