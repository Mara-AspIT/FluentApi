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
using FluentApi.EF;
using System.ComponentModel;
namespace FluentApi.Gui
{
    /// <summary>
    /// Interaction logic for EmployeeUserControl.xaml
    /// </summary>
    public partial class EmployeeUserControl: UserControl
    {
        protected Model model;
        private Employee selectedEmployee;

        public EmployeeUserControl()
        {
            InitializeComponent();
            model = new Model();
            dataGridEmployees.ItemsSource = model.Employees.ToList();
            this.DataContext = selectedEmployee;
        }

        private void DataGrid_Employees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedEmployee = dataGridEmployees.SelectedItem as Employee;
            if(selectedEmployee != null)
            {
                buttonUpdateEmployee.IsEnabled = true;
                buttonSaveNewEmployee.IsEnabled = false;
                textBoxEmployeeName.Text = selectedEmployee.Name;
                if(selectedEmployee.ContactInfo != null)
                {
                    textBoxEmail.Text = selectedEmployee.ContactInfo.Email;
                    textBoxPhone.Text = selectedEmployee.ContactInfo.Phone;
                }
                else
                {
                    textBoxEmail.Text = String.Empty;
                    textBoxPhone.Text = String.Empty;
                }
            }
        }



        private void ReloadAllEmployees()
            => dataGridEmployees.ItemsSource = model.Employees.ToList();

        private void DataGrid_Employees_KeyDown(object sender, KeyEventArgs e)
        {
            if(dataGridEmployees.SelectedItem != null)
            {
                if(e.Key == Key.Escape)
                {
                    dataGridEmployees.SelectedItem = selectedEmployee = null;
                    buttonSaveNewEmployee.IsEnabled = true;
                    buttonUpdateEmployee.IsEnabled = false;
                    textBoxEmployeeName.Text = String.Empty;
                    textBoxEmployeeName.Focus();
                }
            }
        }

        private void Button_SaveNewEmployee_Click(object sender, RoutedEventArgs e)
        {
            string nameInput = textBoxEmployeeName.Text;
            if(!Validator.IsNameValid(nameInput))
            {
                MessageBox.Show("Det indtastede navn er ikke gyldigt. Må kun indeholde bogstaver og mellemrum. Prøv igen.", "Indtastningsfejl", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                try
                {
                    Employee employee = new Employee();
                    employee.Name = textBoxEmployeeName.Text;
                    model.Employees.Add(employee);
                    model.SaveChanges();
                    ReloadAllEmployees();
                }
                catch(Exception)
                {
                    MessageBox.Show("Der skete desværre en uventet fejl under forsøget på at gemme den nye ansatte. Prøv igen", "Uventet fejl", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            }
        }

        private void Button_UpdateEmployee_Click(object sender, RoutedEventArgs e)
        {
            if(selectedEmployee != null)
            {
                if(textBoxEmployeeName.Text != selectedEmployee.Name)
                {
                    selectedEmployee.Name = textBoxEmployeeName.Text;
                }
                model.SaveChanges();
                ReloadAllEmployees();
            }
        }

        private void TextBox_EmployeeName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(textBoxEmployeeName.Text == String.Empty)
            {
                buttonSaveNewEmployee.IsEnabled = false;
                buttonUpdateEmployee.IsEnabled = false;
            }
            else if(selectedEmployee == null)
            {
                buttonSaveNewEmployee.IsEnabled = true;
                buttonUpdateEmployee.IsEnabled = false;
            }
            else
            {
                buttonSaveNewEmployee.IsEnabled = false;
                buttonUpdateEmployee.IsEnabled = true;
            }
        }
    }
}