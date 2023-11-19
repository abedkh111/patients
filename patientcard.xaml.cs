using System;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;


namespace patients
{
    /// <summary>
    /// Interaction logic for patientcards.xaml
    /// </summary>
    public partial class patientcard : Page
    {
        SQLiteConnection sqlconn = new SQLiteConnection(MeClass.subconectionstr);
        bool selectindex = true;
        bool selectimg = true;
        //      bool selmodh = true;
        bool shohidim = true;
        int mgsize = 120;
        int filecount = 1;
        int filecounts = 1;
        int imgid = 0;
        //   int selmod = 0;
        public patientcard()
        {
            InitializeComponent();
            getpage(0);

            getimagetemp(mgsize, filecount);
        }
        private void getpage(int selmod = 0)
        {



            string stm = string.Empty;
            if (selmod == 0)
            {
                stm = "SELECT * FROM `users` WHERE ID=" + MeClass.CurId + " ORDER BY ID LIMIT 0,1;";
            }
            else if (selmod == 1)
            {
                stm = "SELECT * FROM `users`  ORDER BY ID LIMIT 0,1;";
            }
            else if (selmod == 2)
            {
                stm = "SELECT * FROM `users` WHERE ID<" + MeClass.CurId + "  ORDER BY ID DESC LIMIT 0,1;";
            }
            else if (selmod == 3)
            {
                stm = "SELECT * FROM `users`  WHERE ID>" + MeClass.CurId + "  ORDER BY ID ASC LIMIT 0,1;";
            }
            else if (selmod == 4)
            {
                stm = "SELECT * FROM `users`  ORDER BY ID DESC LIMIT 0,1";
            }
            //  MessageBox.Show(Convert.ToString(MeClass.CurId));
            String varname, varvalue;
            SQLiteCommand commmvis = new SQLiteCommand();
            SQLiteConnection connvis1 = new SQLiteConnection(sqlconn);
            connvis1.Open();
            String commvistxt = stm;
            commmvis.Connection = connvis1;
            commmvis.CommandText = commvistxt;
            SQLiteDataReader visallarry = commmvis.ExecuteReader();

            if (visallarry.HasRows)
            {
                while (visallarry.Read())
                {
                    DateTime date = new DateTime();
                    int year = DateTime.Now.Year;
                    txtnumber.Text = Convert.ToString(visallarry["ID"]);
                    Name.Text = Convert.ToString(visallarry["Name"]);
                    Name.Text = Convert.ToString(visallarry["Name"]);
                    Birthday.Text = Convert.ToString(visallarry["Birthday"]);
                    age.Text = Convert.ToString(year - Convert.ToInt32(visallarry["Birthday"]));
                    CardDat.Text = visallarry.GetString(visallarry.GetOrdinal("CardDat"));
                    Address.Text = Convert.ToString(visallarry["Address"]);
                    MobilePhone.Text = Convert.ToString(visallarry["MobilePhone"]);
                    HomePhone.Text = Convert.ToString(visallarry["HomePhone"]);
                    SickStory.Document.Blocks.Clear();
                    MainComplaint.Document.Blocks.Clear();
                    FirstDiagnosis.Document.Blocks.Clear();
                    SickStory.AppendText(Convert.ToString(visallarry["SickStory"]));
                    MainComplaint.AppendText(Convert.ToString(visallarry["MainComplaint"]));
                    FirstDiagnosis.AppendText(Convert.ToString(visallarry["FirstDiagnosis"]));
                    MeClass.CurId = Convert.ToInt32(visallarry["ID"]);
                }
            }

            getimage(mgsize, 0);
        }
        public void Linq40()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var numberGroups =
                from n in numbers
                group n by n % 5 into g
                select new { Remainder = g.Key, Numbers = g };
            foreach (var g in numberGroups)
            {
                MessageBox.Show(Convert.ToString("Numbers with a remainder of " + g.Remainder + " when divided by 5:"));
                foreach (var n in g.Numbers)
                {
                    MessageBox.Show(Convert.ToString(n));
                }
            }
        }
        private void showbookcardn_Click(object sender, RoutedEventArgs e)
        {
        }
        private void textnumpage_KeyDown(object sender, KeyEventArgs e)
        {
        }
        private void firstpageButtonDown(object sender, RoutedEventArgs e)
        {
            getpage(1);
        }
        private void prevpageButtonDown(object sender, RoutedEventArgs e)
        {
            getpage(2);
        }
        private void nextpageButtonDown(object sender, RoutedEventArgs e)
        {
            getpage(3);
        }
        private void lastpageButtonDown(object sender, RoutedEventArgs e)
        {
            getpage(4);
        }
        private void textsearchh_KeyDown(object sender, KeyEventArgs e)
        {
            selectindex = false;
            if (e.Key == Key.Enter)
            {
                //if (searchtext.IsChecked == true)
                //{
                //    textparasearch(txtsearchh.Text);
                //}
                //else if (searchindex.IsChecked == true)
                //{
                //    textparasearchinindex(txtsearchh.Text);
                //}



            }

        }
        private void hideimg(object sender, RoutedEventArgs e)
        {
            if (shohidim)
            {
                hidimg();
                shohidim = false;
            }
            else
            {
                showimg();
                shohidim = true;
            }
        }
        private void visimg(object sender, RoutedEventArgs e)
        {
            //      showimg();


        }
        private void hidimg()
        {
            imgborder.Visibility = Visibility.Hidden;


            treeborder2.SetValue(Grid.ColumnSpanProperty, 2);
            initiateGrid();
            Grid.SetColumn(storylabal, 5);
            Grid.SetRow(storylabal, 0);
            //   storylabal.SetValue(Grid.SetColumn, 5);
        }
        private void initiateGrid()
        {
            //   Grid grid = new Grid();
            ColumnDefinition c1 = new ColumnDefinition();
            c1.Width = new GridLength(100, GridUnitType.Star);

            infogrid.ColumnDefinitions.Add(c1);

        }
        private void removeCol()
        {
            if (infogrid.ColumnDefinitions.Count >= 0)
            {
                //  MessageBox.Show(Convert.ToString(infogrid.ColumnDefinitions.Count));
                infogrid.ColumnDefinitions.RemoveAt(0);
            }

        }
        private void showimg()
        {
            removeCol();

            imgborder.Visibility = Visibility.Visible;
            treeborder2.SetValue(Grid.ColumnSpanProperty, 1);
            Grid.SetColumn(storylabal, 0);
            Grid.SetRow(storylabal, 3);
        }
        //private void hidimg()
        //{
        //    imgborder.Visibility = Visibility.Hidden;
        //    gridmain12.HorizontalAlignment = HorizontalAlignment.Stretch;
        //    gridmain12.Width = (MeClass.allwidth * 68) / 100;
        //      wbpageview.Width = (MeClass.allwidth * 66) / 100;
        //    imgscrall.Width = (MeClass.allwidth * 33) / 100;
        //      treeborder.Width = (MeClass.allwidth * 23) / 100;
        //    colw.Width = new GridLength(0, GridUnitType.Auto);
        //    imgborder.Width = (MeClass.allwidth * 34) / 100;
        //     tv.Width = (MeClass.allwidth * 22) / 100;

        //}
        //private void showimg()
        //{
        //          gridmain.Height = (MeClass.allheight * 87) / 100;
        //    imgborder.Visibility = Visibility.Visible;
        //          //   brborder.HorizontalAlignment = HorizontalAlignment.Stretch;
        //        //  brborder.Width = (MeClass.allwidth * 34) / 100;
        //        //  wbpageview.Width = (MeClass.allwidth * 33) / 100;
        //          imgborder.Width = (MeClass.allwidth * 34) / 100;
        //          //  imgscrall.Width = (MeClass.allwidth * 30) / 100;
        //          treeborder.Width = (MeClass.allwidth * 23) / 100;

        //}


        private void txtsearch_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void zoomin_Click(object sender, RoutedEventArgs e)
        {
            zoomweb(1);
        }

        private void zoomout_Click(object sender, RoutedEventArgs e)
        {
            zoomweb(0);
        }
        private void zoomtxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {


                zoomweb(2);



            }
        }
        private void zoomweb(int zoomper)
        {
            if (zoomper == 2)
            {

                mgsize = Convert.ToInt32(zoomtxt.Text);
                if (mgsize > 200)
                {
                    mgsize = 200;
                }
                else if (mgsize < 25)
                {
                    mgsize = 25;
                }

                getimage(mgsize, filecount);
            }
            else if (zoomper == 1)
            {
                mgsize = mgsize + 10;
                if (mgsize > 200)
                {
                    mgsize = 200;
                }
                getimage(mgsize, filecount);
            }
            else if (zoomper == 0)
            {
                if (mgsize >= 20)
                {
                    mgsize = mgsize - 10;
                    if (mgsize < 25)
                    {
                        mgsize = 25;
                    }

                    getimage(mgsize, filecount);
                }
            }
            zoomtxt.Text = Convert.ToString(mgsize);
            // selcetpoints();
            //  selcetpointone();

            try
            {

                if (MeClass.selectmod == 2)
                {
                    //  selcetpoints();
                }
                else if (MeClass.selectmod == 1)
                {
                    //       selcetpointone();

                }
            }
            catch
            {

            }
        }
        private void getimage(double imgsize, int selmod = 0)
        {

            Cnv.Width = (459 * imgsize) / 100;
            Cnv.Height = (579 * imgsize) / 100;
            imgpreview.Width = (459 * imgsize) / 100;
            imgpreview.Height = (579 * imgsize) / 100;
            //  MessageBox.Show(Convert.ToString(MeClass.CurId));
            String varname, varvalue;
            SQLiteCommand commmvis = new SQLiteCommand();
            SQLiteConnection connvis1 = new SQLiteConnection(sqlconn);
            connvis1.Open();

            string stm = string.Empty;
            if (selmod == 0)
            {
                stm = "SELECT ID,title FROM `images` WHERE userid=" + MeClass.CurId + "  ORDER BY ID LIMIT 0,1;";
            }
            else if (selmod == 1)
            {
                stm = "SELECT ID,title FROM `images` WHERE userid=" + MeClass.CurId + "  ORDER BY ID LIMIT 0,1;";
            }
            else if (selmod == 2)
            {
                stm = "SELECT ID,title FROM `images` WHERE userid=" + MeClass.CurId + "  and ID<" + imgid + "  ORDER BY ID DESC LIMIT 0,1;";
            }
            else if (selmod == 3)
            {
                stm = "SELECT ID,title FROM `images` WHERE userid=" + MeClass.CurId + "  and ID>" + imgid + "  ORDER BY ID ASC LIMIT 0,1;";
            }
            else if (selmod == 4)
            {
                stm = "SELECT  ID,title FROM `images` WHERE userid=" + MeClass.CurId + "  ORDER BY ID DESC LIMIT 0,1";
            }

            commmvis.Connection = connvis1;
            commmvis.CommandText = stm;
            SQLiteDataReader visallarry = commmvis.ExecuteReader();

            if (visallarry.HasRows)
            {
                while (visallarry.Read())
                {
                    string imgtemppath2 = $@"{ Environment.CurrentDirectory}\\Images\\{Path.GetFileName(Convert.ToString(visallarry["title"]))}";
                    if (File.Exists(imgtemppath2))
                    //for (int ii = 0; ii < filenms.Length; ii++)
                    {
                        BitmapImage logo = new BitmapImage();
                        logo.BeginInit();
                        logo.UriSource = new Uri(imgtemppath2);
                        logo.EndInit();
                        //    con.Close();
                        imgpreview.Source = logo;
                        imgid = Convert.ToInt32(visallarry["ID"]);

                    }
                }
            }

        }
        private void getimagetemp(double imgsize, int filemod)
        {
            //try
            //{


         

            Cnv.Width = (459 * imgsize) / 100;
            Cnv.Height = (579 * imgsize) / 100;
            imgpreview.Width = (459 * imgsize) / 100;
            imgpreview.Height = (579 * imgsize) / 100;
            string imgtemppath = $@"{ Environment.CurrentDirectory}\\tempimages\\";
            string[] files = Directory.GetFiles(imgtemppath);
            string[] filenms = new String[files.Length];
            filenms = Directory.GetFiles(imgtemppath);


            if (filemod == 1)
            {
                filecount = 1;

            }
            else if (filemod == 2)
            {
                if (filecount > 1)

                {
                    filecount = filecount - 1;

                }
                else
                {
                    filecount = 1;

                }
            }
            else if (filemod == 3)
            {
                if (filecount < filecounts && filecount < files.Length - 1)
                {
                    filecount = filecount + 1;

                }
            
            }
            else if (filemod == 4)
            {
               
                    filecount = files.Length - 1;
            

            }
            else
            {
                filecount = 1;

            }






            if (files.Length > 2)
            {

                int i = 0;
                foreach (string file in files)
                {
                    filenms[i] = Path.GetFileName(file);
                    i += 1;


                }
                filecounts = filenms.Length;
               // if(filecount<= filecounts)
               // { 
                string imgtemppath2 = $@"{ Environment.CurrentDirectory}\\tempimages\\{Path.GetFileName(filenms[filecount])}";
                if ((filecount > 0 && filecount < filenms.Length) && File.Exists(imgtemppath2))
           
                {
                    BitmapImage logo = new BitmapImage();
                    logo.BeginInit();
                    logo.UriSource = new Uri(imgtemppath2);
                    logo.EndInit();
                                imgpreview.Source = logo;

                }
               // }
            }
        }

        private void firstpageButtonDown2(object sender, RoutedEventArgs e)
        {
           

            getimagetemp(mgsize, 1);
        }

        private void prevpageButtonDown2(object sender, RoutedEventArgs e)
        {

            getimagetemp(mgsize, 2);

        

        }

        private void nextpageButtonDown2(object sender, RoutedEventArgs e)
        {

            getimagetemp(mgsize, 3);

         

        }

        private void lastpageButtonDown2(object sender, RoutedEventArgs e)
        {
            getimagetemp(mgsize, 4);
          
           
        }

        private void cardnumber_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void savecard(object sender, RoutedEventArgs e)
        {

        }






    }
}
