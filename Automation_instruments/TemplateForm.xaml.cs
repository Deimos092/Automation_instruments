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
using System.Windows.Shapes;
using Automation_instruments.Model;

namespace Automation_instruments
{
    /// <summary>
    /// Interaction logic for TemplateForm.xaml
    /// </summary>
    public partial class TemplateForm : Window
    {
        internal TemplateForm(Collection database)
        {
            InitializeComponent();
            DB = database;
        }
        Collection DB { get; set; }
        Template Temp { get; set; }
        public int CountAdd { get; set; }
        void AddLine(string name)
        {
            Temp = new Template(name);
            DB.Templates.Add(Temp);
            DB.SaveChanges();
            CountAdd++;
        }
        private void Button_OK_Click(object sender, RoutedEventArgs e)
        {
            Task TaskAdd = new Task(() => AddLine(tb_name.Text));
            TaskAdd.RunSynchronously();
        }
    }
}
