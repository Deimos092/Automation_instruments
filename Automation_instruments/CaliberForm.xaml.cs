using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading;
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
    /// Interaction logic for CaliberForm.xaml
    /// </summary>
    public partial class CaliberForm : Window
    {
        internal CaliberForm(Collection database)
        {
            InitializeComponent();
            DB = database;
        }
        Collection DB { get; set; }
        Caliber Caliber { get; set; }
        public int CountAdd { get; set; }
        void AddLine(string name, string type)
        {
            Caliber = new Caliber(name, type);
            DB.Calibers.Add(Caliber);
            DB.SaveChanges();
            CountAdd++;
        }
        private void Button_OK_Click(object sender, RoutedEventArgs e)
        {
            Task TaskAdd = new Task(() => AddLine(tb_name.Text, cb_type.Text));
            TaskAdd.RunSynchronously();
        }
    }
}
