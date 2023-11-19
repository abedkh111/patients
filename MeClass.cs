using System;
using System.Data.SQLite;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;


namespace patients
{
    class MeClass
    {
        public static double allheight = System.Windows.SystemParameters.PrimaryScreenHeight;
        public static double allwidth = System.Windows.SystemParameters.PrimaryScreenWidth;
        public static int pageid = 0;
        public static int CurIdd = 1;
        public static int paraid = 0;
        public static int selectmod = 0;
        //   string masterconectionstr = "Data Source=" + Environment.CurrentDirectory + "\\BooksData\\BookMaster.sqlite;Version=3;";
        public static string subconectionstr = "Data Source=" + Environment.CurrentDirectory + "\\db\\patients.sqlite;Version=3;";
        // public static string subconectionstr = "Data Source=" + Environment.CurrentDirectory + "\\BooksData\\testbook.sqlite;Version=3;;Password=WsxWsx123@@;";
        public static int CurId
        {
            get { return CurIdd; }
            set { CurIdd = value; }
        }
        public static int insertdata(string datatxt)
        {
            int lastId = 0;
            //try
            //{

            SQLiteConnection con = new SQLiteConnection(MeClass.subconectionstr);
            con.Open();
            SQLiteCommand insertSQL = new SQLiteCommand(datatxt + "; select last_insert_rowid();", con);
            //  insertSQL.ExecuteNonQuery();
            lastId = Convert.ToInt32(insertSQL.ExecuteScalar());
            con.Close();

            //}
            //catch
            //{

            //}



            return lastId;

        }
        public static int lastcat(int parent)
        {
            int lastId = 0;
            SQLiteConnection con = new SQLiteConnection(MeClass.subconectionstr);
            con.Open();
            SQLiteCommand insertSQL = new SQLiteCommand("select parent from bookindex where INID=" + (parent) + " order by ID DESC limit 0,1", con);
            //  insertSQL.ExecuteNonQuery();
            lastId = Convert.ToInt32(insertSQL.ExecuteScalar());
            con.Close();
            return lastId;
        }


    }
    public static class MyExtensions
    {
        /// <summary>
        /// Returns the left part of this string instance.
        /// </summary>
        /// <param name="count">Number of characters to return.</param>
        public static string Left(this string input, int count)
        {
            return input.Substring(0, Math.Min(input.Length, count));
        }
        public static string Right(this string input, int count)
        {
            return input.Substring(Math.Max(input.Length - count, 0), Math.Min(count, input.Length));
        }
        public static string Mid(this string input, int start)
        {
            return input.Substring(Math.Min(start, input.Length));
        }
        public static string Mid(this string input, int start, int count)
        {
            return input.Substring(Math.Min(start, input.Length), Math.Min(count, Math.Max(input.Length - start, 0)));
        }

    }

    //class TreeViewLineConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    //    {
    //        TreeViewItem item = (TreeViewItem)value;
    //        ItemsControl ic = ItemsControl.ItemsControlFromItemContainer(item);
    //        return ic.ItemContainerGenerator.IndexFromContainer(item) == ic.Items.Count - 1;
    //    }
    //    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    //    {
    //        return false;
    //    }
    //}
    class MyMenuItem
    {
        internal int ID
        { private set; get; }
        internal string Header
        { private set; get; }
        internal int ParentID
        { private set; get; }
        internal int LinkID
        { private set; get; }
        internal MyMenuItem(int id, string header, int parentId, int linkid)
        {
            ID = id;
            Header = header;
            ParentID = parentId;
            LinkID = linkid;
        }
    }

    class myvar
    {

        internal int pageid
        { private set; get; }
        internal int parentid
        { private set; get; }
        internal int curid
        { private set; get; }
        // static string txtdata;
        //  { private set; get; }

    }
    class goimge
    {

    }
    class IniFile   // revision 11
    {
        string Path;
        string EXE = Assembly.GetExecutingAssembly().GetName().Name;

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        public IniFile(string IniPath = null)
        {
            Path = new FileInfo(IniPath ?? EXE + ".ini").FullName;
        }

        public string Read(string Key, string Section = null)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, Path);
            return RetVal.ToString();
        }

        public void Write(string Key, string Value, string Section = null)
        {
            WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
        }

        public void DeleteKey(string Key, string Section = null)
        {
            Write(Key, null, Section ?? EXE);
        }

        public void DeleteSection(string Section = null)
        {
            Write(null, null, Section ?? EXE);
        }

        public bool KeyExists(string Key, string Section = null)
        {
            return Read(Key, Section).Length > 0;
        }
    }
}
