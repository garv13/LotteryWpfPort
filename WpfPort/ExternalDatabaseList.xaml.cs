﻿using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Forms;
using System.Data.SQLite;

namespace WpfPort
{
    /// <summary>
    /// Interaction logic for ExternalDatabaseList.xaml
    /// </summary>
    public partial class ExternalDatabaseList : Window
    {
        public ExternalDatabaseList()
        {
            InitializeComponent();

            List<IconTemplate> iconList = new List<IconTemplate>();
            iconList.Add(new IconTemplate()
            {
                ImageIconSource = "Sample-icon.png",
                Name = "External Games",
                ImageTag = "tag"
            });
            iconList.Add(new IconTemplate()
            {
                ImageIconSource = @"C:\Users\Varun Bhatia\Source\Repos\LotteryWpfPort\WpfPort\Sample-icon.png",
                Name = "Full Cell",
                ImageTag = "tag"
            });
            iconList.Add(new IconTemplate()
            {
                ImageIconSource = @"C:\Users\Varun Bhatia\Source\Repos\LotteryWpfPort\WpfPort\Sample-icon.png",
                Name = "Exact Cell",
                ImageTag = "tag"
            });
            System.Windows.Controls.Image ds1 = new System.Windows.Controls.Image();

            iconList.Add(new IconTemplate()
            {
                ImageIconSource = @"C:\Users\Varun Bhatia\Source\Repos\LotteryWpfPort\WpfPort\Sample-icon.png",
                Name = "Manual Code",
                ImageTag = "tag"
            });
            //Bind it with the ListBox  
            this.IconPanelTemplate1.ItemsSource = iconList;
            this.IconPanelTemplate2.ItemsSource = iconList;
        }

        static DataTable dt;


        private void ExternalDatabaseList_Load(object sender, EventArgs e)
        {
            fillExternalCheckListBox();
        }
        private void fillExternalCheckListBox()
        {
            //try
            //{
            //    dt = SqlClass.GetExternalDatabaseList();
            //    checkedListBox1.Items.Clear();
            //    foreach (DataRow row in dt.Rows)
            //    {
            //        checkedListBox1.Items.Add(row["DBName"].ToString());
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (checkedListBox1.CheckedItems.Count == 0)
            //    {
            //        MessageBox.Show("Select atleast one database");
            //        return;
            //    }
            //    string DBIdList = "";
            //    for (int i = 0; i < checkedListBox1.CheckedItems.Count; i++)
            //    {
            //        DataRow[] rows = dt.Select("DBName = '" + checkedListBox1.CheckedItems[i].ToString() + "'");
            //        DBIdList += "," + rows[0]["DBId"].ToString();
            //    }
            //    if (DBIdList != "")
            //    {
            //        DBIdList = DBIdList.Substring(1, DBIdList.Length - 1);
            //        SqlClass.DeleteDatabase(DBIdList);
            //    }
            //    fillExternalCheckListBox();
            //    MessageBox.Show("Deleted Successfully");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void btnSetInternalDatabase_Click(object sender, EventArgs e)
        {
            //    try
            //    {
            //        if (checkedListBox1.CheckedItems.Count == 0)
            //        {
            //            MessageBox.Show("Select atleast one database");
            //            return;
            //        }
            //        if (checkedListBox1.CheckedItems.Count > 1)
            //        {
            //            MessageBox.Show("Select a single database");
            //            return;
            //        }
            //        DataRow[] rows = dt.Select("DBName = '" + checkedListBox1.CheckedItems[0].ToString() + "'");
            //        SqlClass.SetInternalDatabase(Convert.ToInt32(rows[0]["DBId"]));
            //        MessageBox.Show(checkedListBox1.CheckedItems[0].ToString() + " is setted as internal database");

            //        EditDatabase oEditDatabase = new EditDatabase();
            //        oEditDatabase.cmbDatabaseList.Visible = false;
            //        oEditDatabase.btnLoad.Visible = false;
            //        oEditDatabase.lbl.Visible = false;
            //        oEditDatabase.MdiParent = this.ParentForm;
            //        oEditDatabase.btnSetAsExternal.Visible = true;
            //        this.Close();

            //        oEditDatabase.Show();
            //        oEditDatabase.cmbDatabaseList.DataSource = SqlClass.GetInternalDatabase();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
        }

        private void ExternalGamesButton_Click(object sender, RoutedEventArgs e)
        {
            // write here 
            using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=C:\Users\Varun Bhatia\Downloads\result\data.sqlite"))
            {
                conn.Open();

                //SQLiteCommand command = new SQLiteCommand("SELECT * FROM 'dbo.DBList' LIMIT 0,30", conn);
                //SQLiteDataReader reader = command.ExecuteReader();
                DataSet dataSet = new DataSet();
                SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter("SELECT * FROM 'dbo.DBList' LIMIT 0,30", conn);
                dataAdapter.Fill(dataSet);

                DataGrid.ItemsSource = dataSet.Tables[0].DefaultView;

                //while (reader.Read())
                //    Console.WriteLine(reader["DBName"]);
                //DataGrid.ItemsSource = reader;
                //reader.Close();
            }
        }

        private void IconButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button bs = sender as System.Windows.Controls.Button;
            string str = bs.Tag.ToString();
        }
    }
}
