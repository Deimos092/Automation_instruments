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
    /// Interaction logic for RingForm.xaml
    /// </summary>
    public partial class RingForm : Window
    {
        internal RingForm(Collection database)
        {
            InitializeComponent();
            DB = database;
        }

        Collection DB { get; set; }
        Ring Ring { get; set; }
        public int CountAdd { get; set; }
        void AddLine(string name, string type, string profile)
        {
            Ring = new Ring(name, type, profile);
            DB.Rings.Add(Ring);
            DB.SaveChanges();
            CountAdd++;
        }
        private void Button_OK_Click(object sender, RoutedEventArgs e)
        {
            Task TaskAdd = new Task(() => AddLine(tb_name.Text, tb_type.Text, tb_profile.Text));
            TaskAdd.RunSynchronously();
        }
    }
}
