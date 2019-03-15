using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HR.Persistence;

namespace HR_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WebApiClient client;

        public MainWindow()
        {
            client = new WebApiClient();
            InitializeComponent();
            loadEmployeesDataGrid();
        }

        private void loadEmployeesDataGrid()
        {
            dtgEmployees.ItemsSource = client.GetAllEmployees();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtId.Text == string.Empty)
                client.AddEmployee(new Employee { FullName = txtFullname.Text, HireDate = DateTime.Parse(dtpHireDate.Text) });
            else
                client.EditEmployee(new Employee { Id = int.Parse(txtId.Text),  FullName = txtFullname.Text, HireDate = DateTime.Parse(dtpHireDate.Text) });

            loadEmployeesDataGrid();
        }

        private void HyperlinkDelete_Click(object sender, RoutedEventArgs e)
        {
            var link = (sender as Hyperlink);
            var employee = (link.DataContext as Employee);

            client.DeleteEmployee(employee.Id);
            loadEmployeesDataGrid();
        }

        private void HyperlinkEdit_Click(object sender, RoutedEventArgs e)
        {
            var link = (sender as Hyperlink);
            var employeeId = (link.DataContext as Employee).Id;
            var employee = client.GetEmployee(employeeId);

            txtId.Text = employee.Id.ToString();
            txtFullname.Text = employee.FullName;
            dtpHireDate.Text = employee.HireDate.Date.ToShortDateString();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            txtId.Text = string.Empty;
            txtFullname.Text = string.Empty;
            dtpHireDate.Text = string.Empty;
        }
    }
}
