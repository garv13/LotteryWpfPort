using System;
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
using System.Collections;

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
        private static DataSet dsSearch;
        private static DataTable dtSearchNum;
        private static DataTable dtSerchRes;
        private static DataTable dtBaseTable;
        private static DataTable dtNumFound;
        public DataTable dtFullcellSearch;
        private static ArrayList arSymboles;
        private static int totalResFound;
        private static DataTable dtSummary;
        private static DataTable dtLastRows;
        private static DataTable dtCodeTable;

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
            //// write here 
            //using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=C:\Users\Varun Bhatia\Downloads\result\data.sqlite"))
            //{
            //    conn.Open();

            //    //SQLiteCommand command = new SQLiteCommand("SELECT * FROM 'dbo.DBList' LIMIT 0,30", conn);
            //    //SQLiteDataReader reader = command.ExecuteReader();
            //    DataSet dataSet = new DataSet();
            //    SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter("SELECT * FROM 'dbo.DBList' LIMIT 0,30", conn);
            //    dataAdapter.Fill(dataSet);

            //    DataGrid.ItemsSource = dataSet.Tables[0].DefaultView;

            //    //while (reader.Read())
            //    //    Console.WriteLine(reader["DBName"]);
            //    //DataGrid.ItemsSource = reader;
            //    //reader.Close();
            //}
        }

        private void IconButton_Click(object sender, RoutedEventArgs e)
        {
            PrepareSearchGrid();
            GetlastRowsMod();
            System.Windows.Controls.Button bs = sender as System.Windows.Controls.Button;
            string str = bs.Tag.ToString();
        }


        private void PrepareSearchGrid()
        {

            dsSearch = new DataSet();
            SqlClass.GetSearchPlaneData(ref dsSearch);
            //Date.DefaultCellStyle.Format = "dd/MM/yyyy";
            dataGrid.DataContext = dsSearch.Tables[0];
            dtFullcellSearch = dsSearch.Tables[0].Clone();
            ClearSearchPanel();
            dtNumFound = new DataTable();
            dtNumFound.Columns.Add("Id");
            dtNumFound.Columns.Add("col");
            dtNumFound.Columns.Add("RecNo");
        }

        private void SearchForm_Load(object sender, EventArgs e)
        {

            PrepareSearchGrid();
            fillExternalCheckListBox();
            if ((SqlClass.GetInternalDatabase()).Rows.Count > 0)
                DBNameTextBlock.Text = (SqlClass.GetInternalDatabase()).Rows[0]["DBName"].ToString();
            dtCodeTable = SqlClass.GetCodeList();
            CreateSummaryTable();
        }

        private void CreateSummaryTable()
        {
            try
            {
                dtSummary = new DataTable();
                dtSummary.Columns.Add("Id", Type.GetType("System.Int32"));
                dtSummary.Columns.Add("Number");
                dtSummary.Columns.Add("Description");
                dtSummary.Columns.Add("cnt", Type.GetType("System.Int32"));

                dtLastRows = new DataTable();
                dtLastRows.Columns.Add("Id");
                dtLastRows.Columns.Add("RecordId");
                dtLastRows.Columns.Add("DBId");
                dtLastRows.Columns.Add("RecNo");
                dtLastRows.Columns.Add("W1");
                dtLastRows.Columns.Add("W2");
                dtLastRows.Columns.Add("W3");
                dtLastRows.Columns.Add("W4");
                dtLastRows.Columns.Add("W5");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
        private static void ClearSearchPanel()
        {
            try
            {
                dsSearch.Tables[0].Rows.Clear();
                SqlClass.GetSearchPlaneData(ref dsSearch);
                
                dsSearch.GetChanges();
                if (dtSerchRes != null)
                    dtSerchRes.Rows.Clear();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
        private void GetlastRowsMod()
        {
            try
            {

                if (dsSearch.Tables[0].Rows[0]["W1"] == null || dsSearch.Tables[0].Rows[0]["W2"] == DBNull.Value || true)
                {
                    
                    DataTable dtInter = SqlClass.GetInternalDatabase();
                    int DBId;
                    if (dtInter.Rows.Count > 0)
                    {
                        DBId = Convert.ToInt32(dtInter.Rows[0]["DBId"]);
                        DataSet ds = new DataSet();

                        SqlClass.GetWin_Machin_DataByDBId(DBId, ref ds);

                        int rowCount = ds.Tables[0].Rows.Count;
                        int j = 1;
                        int k = 0;
                        while (k != 14)
                        {
                            if (ds.Tables[0].Rows[rowCount - j]["W1"] != DBNull.Value)
                            {
                                k++;
                            }
                            j++;
                        }
                        k = 1;
                        dsSearch.Tables[0].Rows.Clear();
                        for (int i = j; i > 0; i--)
                        {
                            DataRow rowSerch = dsSearch.Tables[0].NewRow();
                            rowSerch["Id"] = k;
                            rowSerch["Date"] = ds.Tables[0].Rows[rowCount - i]["Date"];
                            rowSerch["W1"] = ds.Tables[0].Rows[rowCount - i]["W1"];
                            rowSerch["W2"] = ds.Tables[0].Rows[rowCount - i]["W2"];
                            rowSerch["W3"] = ds.Tables[0].Rows[rowCount - i]["W3"];
                            rowSerch["W4"] = ds.Tables[0].Rows[rowCount - i]["W4"];
                            rowSerch["W5"] = ds.Tables[0].Rows[rowCount - i]["W5"];
                            rowSerch["SUM_W"] = ds.Tables[0].Rows[rowCount - i]["SUM_W"];
                            rowSerch["M1"] = ds.Tables[0].Rows[rowCount - i]["M1"];
                            rowSerch["M2"] = ds.Tables[0].Rows[rowCount - i]["M2"];
                            rowSerch["M3"] = ds.Tables[0].Rows[rowCount - i]["M3"];
                            rowSerch["M4"] = ds.Tables[0].Rows[rowCount - i]["M4"];
                            rowSerch["M5"] = ds.Tables[0].Rows[rowCount - i]["M5"];
                            rowSerch["SUM_M"] = ds.Tables[0].Rows[rowCount - i]["SUM_M"];
                            k++;
                            dsSearch.Tables[0].Rows.Add(rowSerch);
                        }
                        dataGrid.ItemsSource = dsSearch.Tables[0].DefaultView;
                        
                    }
                }//
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }

        }
    }
}
