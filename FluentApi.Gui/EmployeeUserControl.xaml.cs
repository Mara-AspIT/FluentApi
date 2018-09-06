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
    public partial class EmployeeUserControl: UserControl, INotifyPropertyChanged
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

        public Employee SelectedEmployee
        {
            get
            {
                return selectedEmployee;
            }

            set
            {
                if(value != selectedEmployee)
                {
                    selectedEmployee = value;
                    Notify(nameof(SelectedEmployee));
                }
            }
        }
        protected virtual void Notify(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void DataGrid_Employees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedEmployee = dataGridEmployees.SelectedItem as Employee;
            
        }
    }
}