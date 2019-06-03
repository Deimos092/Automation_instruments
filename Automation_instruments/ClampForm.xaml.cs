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
    /// Interaction logic for ClampForm.xaml
    /// </summary>
    public partial class ClampForm : Window
    {
        Dictionary<int, Clamp.TypeS> TypeSize;
        internal ClampForm(Collection database)
        {
            InitializeComponent();
            DB = database;
            TypeSize = new Dictionary<int, Clamp.TypeS>();
            TypeSize.Add(0, Clamp.TypeS.S_0);
            TypeSize.Add(30, Clamp.TypeS.S_30);
            TypeSize.Add(60, Clamp.TypeS.S_60);
            TypeSize.Add(90, Clamp.TypeS.S_90);
            TypeSize.Add(120, Clamp.TypeS.S_120);
            TypeSize.Add(150, Clamp.TypeS.S_150);
        }
        Collection DB { get; set; }
        Clamp Clamp { get; set; }
        public int CountAdd { get; set; }
        void AddLine(string name, int typesize, bool diametr)
        {
            Clamp = new Clamp(name, TypeSize[typesize], diametr);
            DB.Clamps.Add(Clamp);
            DB.SaveChanges();
            CountAdd++;
        }
        private void Button_OK_Click(object sender, RoutedEventArgs e)
        {
            Task TaskAdd = new Task(() => AddLine(tb_name.Text,int.Parse(cb_type.Text),(bool)ChekBox_diametr.IsChecked));
            TaskAdd.RunSynchronously();
        }
    }
}
