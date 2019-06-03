using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Automation_instruments.Model;
using System.Data.Entity;
using Microsoft.Win32;
using Automation_instruments.Properties;
using System.ComponentModel;
using AppExcel = Microsoft.Office.Interop.Excel.Application;
using WorkBook = Microsoft.Office.Interop.Excel.Workbook;
using WorkSheet = Microsoft.Office.Interop.Excel.Worksheet;
using CellRange = Microsoft.Office.Interop.Excel.Range;
using System.Data;

namespace Automation_instruments
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            Database.SetInitializer<Collection>(new DropCreateDatabaseIfModelChanges<Collection>());
            DB = new Collection();
        }


        Collection DB { get; set; }
        private void Btn_AddRow_Click(object sender, RoutedEventArgs e)
        {
            switch (Settings.Default.instrumentName)
            {
                case "Caliber":
                    CaliberForm caliberForm = new CaliberForm(DB);
                    caliberForm.ShowDialog();
                    ReloadDB(Settings.Default.instrumentName);
                    break;
                case "Clamp":
                    ClampForm clampform = new ClampForm(DB);
                    clampform.ShowDialog();
                    ReloadDB(Settings.Default.instrumentName);
                    break;
                case "Plug":
                    PlugForm plugForm = new PlugForm(DB);
                    plugForm.ShowDialog();
                    ReloadDB(Settings.Default.instrumentName);
                    break;
                case "Ring":
                    RingForm ringForm = new RingForm(DB);
                    ringForm.ShowDialog();
                    ReloadDB(Settings.Default.instrumentName);
                    break;
                case "Template":
                    TemplateForm tempform = new TemplateForm(DB);
                    tempform.ShowDialog();
                    ReloadDB(Settings.Default.instrumentName);
                    break;
            }
        }


        private void Btn_RemoveRow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (Settings.Default.instrumentName)
                {
                    case "Caliber":
                        DB.Calibers.Remove((Caliber)MainDataGrid.SelectedItem);
                        break;
                    case "Plug":
                        DB.Plugs.Remove((Plug)MainDataGrid.SelectedItem);
                        break;
                    case "Clamp":
                        DB.Clamps.Remove((Clamp)MainDataGrid.SelectedItem);
                        break;
                    case "Ring":
                        DB.Rings.Remove((Ring)MainDataGrid.SelectedItem);
                        break;
                    case "Template":
                        DB.Templates.Remove((Template)MainDataGrid.SelectedItem);
                        break;
                }
                DB.SaveChanges();
                ReloadDB(Settings.Default.instrumentName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            DB.SaveChanges();
            MessageBox.Show("База сохранена !");
        }


        private void Caliber_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                MainDataGrid.ItemsSource = DB.Calibers.ToList();
                BtnMenuClose_Click(sender, e);
                DisplayNormalColumn();


                ListViewItem listItem = (ListViewItem)ListMenu.SelectedItems[0];
                Settings.Default.instrumentName = listItem.Name;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Clamp_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {

                MainDataGrid.ItemsSource = DB.Clamps.ToList();
                BtnMenuClose_Click(sender, e);
                DisplayNormalColumn();

                ListViewItem listItem = (ListViewItem)ListMenu.SelectedItems[0];
                Settings.Default.instrumentName = listItem.Name;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Plug_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                MainDataGrid.ItemsSource = DB.Plugs.ToList();
                BtnMenuClose_Click(sender, e);
                DisplayNormalColumn();

                ListViewItem listItem = (ListViewItem)ListMenu.SelectedItems[0];
                Settings.Default.instrumentName = listItem.Name;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Ring_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                MainDataGrid.ItemsSource = DB.Rings.ToList();
                BtnMenuClose_Click(sender, e);
                DisplayNormalColumn();

                ListViewItem listItem = (ListViewItem)ListMenu.SelectedItems[0];
                Settings.Default.instrumentName = listItem.Name;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Template_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                MainDataGrid.ItemsSource = DB.Templates.ToList();
                BtnMenuClose_Click(sender, e);
                DisplayNormalColumn();

                ListViewItem listItem = (ListViewItem)ListMenu.SelectedItems[0];
                Settings.Default.instrumentName = listItem.Name;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btn_Print_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AppExcel App = new Microsoft.Office.Interop.Excel.Application();
                App.Visible = true;

                WorkBook Wbook = App.Workbooks.Add(Type.Missing);

                WorkSheet WSheet = (WorkSheet)App.ActiveSheet;

                WSheet.Name = this.Name + " " + Settings.Default.instrumentName;
                WSheet.Cells.Font.Size = 12;


                int Rowindex = MainDataGrid.Items.Count;
                int Columns = MainDataGrid.Columns.Count;
                for (int i = 1, j = 1; i <= Columns; i++, j++) //taking care of Headers.  
                {
                    int index = MainDataGrid.Columns[i - 1].DisplayIndex; //находим индекс колонки
                    WSheet.Cells[1, index + 1] = MainDataGrid.Columns[i - 1].Header; //закидываем по индексу название колонки как при отображении
                }

                AddDataToExcel(WSheet);
                CellRange Range = WSheet.Range[WSheet.Cells[1, 1], WSheet.Cells[Rowindex, Columns]];
                Range.EntireColumn.AutoFit();
            }
            catch (Exception)
            {
                throw;
            }
        }
        void AddDataToExcel(WorkSheet WSheet)
        {
            int Rowindex = 2;
            switch (Settings.Default.instrumentName)
            {
                case "Caliber":
                    foreach (Caliber item in MainDataGrid.Items)
                    {
                        WSheet.Cells[Rowindex, "A"] = item.Id;
                        WSheet.Cells[Rowindex, "B"] = item.Name;
                        WSheet.Cells[Rowindex++, "C"] = item.Type;
                    }
                    break;
                case "Clamp":
                    foreach (Clamp item in MainDataGrid.Items)
                    {
                        WSheet.Cells[Rowindex, "A"] = item.Id;
                        WSheet.Cells[Rowindex, "B"] = item.Name;
                        WSheet.Cells[Rowindex, "C"] = item.TypeSize;
                        WSheet.Cells[Rowindex++, "D"] = item.DiametrControl;
                    }
                    break;
                case "Plug":
                    foreach (Plug item in MainDataGrid.Items)
                    {
                        WSheet.Cells[Rowindex, "A"] = item.Id;
                        WSheet.Cells[Rowindex, "B"] = item.Name;
                        WSheet.Cells[Rowindex++, "C"] = item.Type;
                    }
                    break;
                case "Ring":
                    foreach (Ring item in MainDataGrid.Items)
                    {
                        WSheet.Cells[Rowindex, "A"] = item.Id;
                        WSheet.Cells[Rowindex, "B"] = item.Name;
                        WSheet.Cells[Rowindex, "C"] = item.Profile;
                        WSheet.Cells[Rowindex++, "D"] = item.TypeThread;
                    }
                    break;
                case "Template":
                    foreach (Template item in MainDataGrid.Items)
                    {
                        WSheet.Cells[Rowindex, "A"] = item.Id;
                        WSheet.Cells[Rowindex++, "B"] = item.Name;
                    }
                    break;
            }
        }
        private void Tb_SearchKey_TextChanged(object sender, TextChangedEventArgs e)
        {
            string collection = Settings.Default.instrumentName;
            switch (collection)
            {
                case "Caliber":
                    MainDataGrid.ItemsSource = DB.Calibers.Where(x => x.Type.Contains(Tb_SearchKey.Text)).ToList();
                    break;
                case "Clamp":
                    MainDataGrid.ItemsSource = DB.Clamps.Where(x => x.TypeSize.ToString().Contains(Tb_SearchKey.Text)).ToList();
                    break;
                case "Plug":
                    MainDataGrid.ItemsSource = DB.Plugs.Where(x => x.Type.Contains(Tb_SearchKey.Text)).ToList();
                    break;
                case "Ring":
                    MainDataGrid.ItemsSource = DB.Rings.Where(x => x.Profile.Contains(Tb_SearchKey.Text)).ToList();
                    break;
                case "Template":
                    MainDataGrid.ItemsSource = DB.Templates.Where(x => x.Name.Contains(Tb_SearchKey.Text)).ToList();
                    break;
                default:
                    ReloadDB(collection);
                    break;
            }
            DisplayNormalColumn();
        }
        void ReloadDB(string CollectionName)
        {
            switch (CollectionName)
            {
                case "Caliber":
                    MainDataGrid.ItemsSource = DB.Calibers.ToList();
                    break;
                case "Plug":
                    MainDataGrid.ItemsSource = DB.Plugs.ToList();
                    break;
                case "Clamp":
                    MainDataGrid.ItemsSource = DB.Clamps.ToList();
                    break;
                case "Ring":
                    MainDataGrid.ItemsSource = DB.Rings.ToList();
                    break;
                case "Template":
                    MainDataGrid.ItemsSource = DB.Templates.ToList();
                    break;
            }
            DisplayNormalColumn();
        }
        void DisplayNormalColumn()
        {
            foreach (var column in MainDataGrid.Columns)
            {
                if (column.Header.ToString() == "Id") { column.DisplayIndex = 0; }
                if (column.Header.ToString() == "Name") { column.DisplayIndex = 1; }
            }
        }


        //------------ Мелкие задачи --------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------
        private void BtnMenuOpen_Click(object sender, RoutedEventArgs e)
        {
            BtnMenuOpen.Visibility = Visibility.Collapsed;
            BtnMenuClose.Visibility = Visibility.Visible;
        }
        private void BtnMenuClose_Click(object sender, RoutedEventArgs e)
        {
            BtnMenuOpen.Visibility = Visibility.Visible;
            BtnMenuClose.Visibility = Visibility.Collapsed;
        }
        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in Process.GetProcessesByName("EXCEL.EXE"))
                item.Kill();

            Environment.Exit(0);
        }

        private bool clicked = false;
        private Point lmAbs = new Point();
        void PnMouseMove(object sender, MouseEventArgs e)
        {
            if (clicked)
            {
                Point MousePosition = e.GetPosition(this);
                Point MousePositionAbs = new Point();
                MousePositionAbs.X = Convert.ToInt16(this.Left) + MousePosition.X;
                MousePositionAbs.Y = Convert.ToInt16(this.Top) + MousePosition.Y;
                this.Left = this.Left + (MousePositionAbs.X - this.lmAbs.X);
                this.Top = this.Top + (MousePositionAbs.Y - this.lmAbs.Y);
                this.lmAbs = MousePositionAbs;
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            clicked = true;
            this.lmAbs = e.GetPosition(this);
            this.lmAbs.Y = Convert.ToInt16(this.Top) + this.lmAbs.Y;
            this.lmAbs.X = Convert.ToInt16(this.Left) + this.lmAbs.X;
        }
        private void Btn_About_Click(object sender, RoutedEventArgs e)
        {
            Information informationForm = new Information();
            informationForm.ShowDialog();
        }
        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            clicked = false;
        }
        private void MainDataGrid_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {

        }

        //-----------------------------------------------------------------------------------------------------
    }
}
