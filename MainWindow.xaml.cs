//using System.Windows.Forms;
using System;
using System.Windows;
using DevExpress.Xpf.Core;

namespace patients
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ThemedWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            GetCurId();
            while (MainFrame2.NavigationService.RemoveBackEntry() != null) ;
            MainFrame2.NavigationService.Navigate(new Uri("patientcards.xaml", UriKind.RelativeOrAbsolute));
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            mainrib.CollapseMinimizedRibbon();
        }
        public void GetCurId()
        {
            try
            {
                IniFile file = new IniFile();
                var MyIni = new IniFile("CurID.ini");
                var DefaultVolume = MyIni.Read("Curid", "info");
                MeClass.CurId = Convert.ToInt32(DefaultVolume);
                //var defauval = MyIni.Read("CurAuid", "info");
                //Globalvar.CurauId = Convert.ToInt32(defauval);
                //var bookarch = MyIni.Read("BookArchPath", "info");
                //Globalvar.bookarchivepath = bookarch;
            }
            catch
            {
                MessageBox.Show("خطأ في قراءة ملف  ini");
            }
        }
        private void newItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            while (MainFrame2.NavigationService.RemoveBackEntry() != null) ;
            MainFrame2.NavigationService.Navigate(new Uri("patientcards.xaml", UriKind.RelativeOrAbsolute));
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void saveAsItem2_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            while (MainFrame2.NavigationService.RemoveBackEntry() != null) ;
            MainFrame2.NavigationService.Navigate(new Uri("patientcards.xaml", UriKind.RelativeOrAbsolute));
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void newItems_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            while (MainFrame2.NavigationService.RemoveBackEntry() != null) ;
            MainFrame2.NavigationService.Navigate(new Uri("patientcard.xaml", UriKind.RelativeOrAbsolute));
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void patientshow_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            while (MainFrame2.NavigationService.RemoveBackEntry() != null) ;
            MainFrame2.NavigationService.Navigate(new Uri("patientshow.xaml", UriKind.RelativeOrAbsolute));
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }
    }
}
