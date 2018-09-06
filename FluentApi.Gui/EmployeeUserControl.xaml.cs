﻿using System;
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

        private void Button_SaveOrUpadteContactInfo_Click(object sender, RoutedEventArgs e)
        {
            
            

            model.SaveChanges();
            ReloadAllEmployees();
        }

        private void Button_SaveOrUpdateEmployee_Click(object sender, RoutedEventArgs e)
        {
            if(selectedEmployee.Name != textBoxEmployeeName.Text)
            {
                selectedEmployee.Name = textBoxEmployeeName.Text;
            }
            model.SaveChanges();
            ReloadAllEmployees();
        }

        private void ReloadAllEmployees()
            => dataGridEmployees.ItemsSource = model.Employees.ToList();
    }
}