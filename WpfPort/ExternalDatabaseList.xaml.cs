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
            Loaded += ExternalDatabaseList_Loaded;
        }

        private void ExternalDatabaseList_Loaded(object sender, RoutedEventArgs e)
        {
            SearchForm_Load(sender, e);
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
                        //dataGrid.ItemsSource = dsSearch.Tables[0].DefaultView;
                        
                    }
                }//
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }

        }

        #region Exact Search

        private void ExectSearch(DataTable dtTargetdt)
        {
            try
            {
                DataTable dtRankFound = new DataTable();
                dtRankFound.Columns.Add("startIndex");
                dtRankFound.Columns.Add("endIndex");
                dtRankFound.Columns.Add("numFound");
                int RecNo = 0;
                //if (rdInternalDB.Checked)
                //    progressBar1.Maximum = dtTargetdt.Rows.Count;
                int numFound = 0;
                for (int cnt = 0; cnt < dtTargetdt.Rows.Count; cnt++)
                {
                    //if (rdInternalDB.Checked)
                    //    progressBar1.Value = cnt;
                    DataRow[] rowMaxRecNo = dtNumFound.Select("RecNo = MAX(RecNo)");
                    if (rowMaxRecNo.Length > 0)
                        RecNo = Convert.ToInt32(rowMaxRecNo[0]["RecNo"]) + 1;

                    numFound = 0;
                    for (int i = 0; i < dtSearchNum.Rows.Count; i++)
                    {
                        int rowNo = (int)dtSearchNum.Rows[i]["Row"] - 1;
                        if (cnt + rowNo < dtTargetdt.Rows.Count)
                            if (!(dtTargetdt.Rows[cnt + rowNo][dtSearchNum.Rows[i]["W"].ToString()] == null || dtTargetdt.Rows[cnt + rowNo][dtSearchNum.Rows[i]["W"].ToString()] == DBNull.Value))
                                if (Convert.ToInt32(dtSearchNum.Rows[i]["dValue"]) == (int)dtTargetdt.Rows[cnt + rowNo][dtSearchNum.Rows[i]["W"].ToString()])
                                {
                                    numFound = numFound + 1;
                                    DataRow rowNumFound = dtNumFound.NewRow();
                                    rowNumFound["Id"] = (int)dtTargetdt.Rows[cnt + rowNo]["Id"];
                                    rowNumFound["col"] = dtSearchNum.Rows[i]["W"].ToString();
                                    rowNumFound["RecNo"] = RecNo;
                                    dtNumFound.Rows.Add(rowNumFound);
                                }
                    }
                    if (numFound > 1 || (dtSearchNum.Rows.Count == 1 && numFound == 1))
                    {
                        totalResFound += 1;
                        int startIndex = cnt;
                        int endIndex = startIndex + 14;
                        if (endIndex >= dtTargetdt.Rows.Count)
                        {
                            endIndex = dtTargetdt.Rows.Count - 1;
                        }
                        int lastIndex = startIndex + 14;
                        if (startIndex + 16 < dtTargetdt.Rows.Count)
                            lastIndex = startIndex + 16;
                        else if (startIndex + 15 < dtTargetdt.Rows.Count)
                            lastIndex = startIndex + 15;
                        else
                            lastIndex = dtTargetdt.Rows.Count - 1;

                        int startIndexId = (int)dtTargetdt.Rows[startIndex]["Id"];
                        int endIndexId = (int)dtTargetdt.Rows[endIndex]["Id"];
                        lastIndex = (int)dtTargetdt.Rows[lastIndex]["Id"];
                        ImportToResultM(startIndexId, endIndexId, numFound, RecNo, lastIndex);
                    }
                    else
                    {
                        if (dtSearchNum.Rows.Count > 1 && numFound == 1)
                        {
                            dtNumFound.Rows.RemoveAt(dtNumFound.Rows.Count - 1);
                            dtNumFound.AcceptChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void ImportToResultM(int startIndex, int endIndex, int Hits, int RecNo, int lastIndex)
        {
            try
            {
                int id = dtSerchRes.Rows.Count;
                DataRow row1 = dtSerchRes.NewRow();
                id = id + 1;
                row1["Id"] = id;
                row1["MatchType"] = "";
                row1["NumHits"] = "";
                row1["NosFound"] = Hits;
                dtSerchRes.Rows.Add(row1);

                DataRow row2 = dtSerchRes.NewRow();
                id = id + 1;
                row2["Id"] = id;
                row2["MatchType"] = "Total No Found ";
                row2["NumHits"] = Hits.ToString();
                row2["NosFound"] = Hits;

                dtSerchRes.Rows.Add(row2);
                int lastId = endIndex;

                DataRow[] rows = dtBaseTable.Select("Id > " + (startIndex - 1).ToString() + " and Id < " + (lastIndex + 1).ToString());
                //if (rdBottomToTop.Checked)
                //{
                //    rows = dtBaseTable.Select("Id > " + (lastIndex - 1).ToString() + " and Id < " + (startIndex + 1).ToString());
                //    lastId = startIndex;
                //}
                DataRow row3 = dtSerchRes.NewRow();
                id = id + 1;
                row3["Id"] = id;
                int DBId = Convert.ToInt32(rows[0]["DBId"]);
                row3["MatchType"] = "Database :";
                row3["NumHits"] = SqlClass.GetDBNameById(DBId);
                row3["NosFound"] = Hits;
                dtSerchRes.Rows.Add(row3);

                for (int j = 0; j < rows.Length; j++)
                {
                    int i = j;
                    //if (rdBottomToTop.Checked)
                    //    i = rows.Length - 1 - j;

                    DataRow row = dtSerchRes.NewRow();
                    row["Date"] = rows[i]["Date"];
                    row["W1"] = rows[i]["W1"];
                    row["W2"] = rows[i]["W2"];
                    row["W3"] = rows[i]["W3"];
                    row["W4"] = rows[i]["W4"];
                    row["W5"] = rows[i]["W5"];
                    row["SUM_W"] = rows[i]["SUM_W"];
                    row["M1"] = rows[i]["M1"];
                    row["M2"] = rows[i]["M2"];
                    row["M3"] = rows[i]["M3"];
                    row["M4"] = rows[i]["M4"];
                    row["M5"] = rows[i]["M5"];
                    row["SUM_M"] = rows[i]["SUM_M"];
                    row["SNo"] = rows[i]["SNo"];
                    row["RecordId"] = rows[i]["Id"].ToString();
                    row["RecNo"] = RecNo;
                    if (false)//rdTopToBottom.Checked
                    {
                        if ((int)rows[i]["Id"] != lastId && (int)rows[i]["Id"] != lastId - 1)
                        {
                            if (i == rows.Length - 1 || i == rows.Length - 2)
                            {
                                row["RecordId"] = 0;
                                if (i == rows.Length - 2)
                                {
                                    DataRow rowLast = dtLastRows.NewRow();
                                    rowLast["Id"] = id;
                                    rowLast["RecordId"] = rows[i]["Id"].ToString();
                                    rowLast["DBId"] = rows[i]["DBId"].ToString();
                                    rowLast["RecNo"] = RecNo;
                                    rowLast["W1"] = rows[i]["W1"].ToString();
                                    rowLast["W2"] = rows[i]["W2"].ToString();
                                    rowLast["W3"] = rows[i]["W3"].ToString();
                                    rowLast["W4"] = rows[i]["W4"].ToString();
                                    rowLast["W5"] = rows[i]["W5"].ToString();
                                    dtLastRows.Rows.Add(rowLast);
                                }
                            }
                        }
                    }
                    else
                    {
                        if ((int)rows[i]["Id"] != endIndex && (int)rows[i]["Id"] != endIndex + 1)
                        {
                            if (i == 0 || i == 1)
                            {
                                row["RecordId"] = 0;
                                if (i == 1)
                                {
                                    DataRow rowLast = dtLastRows.NewRow();
                                    rowLast["Id"] = id;
                                    rowLast["RecordId"] = rows[i]["Id"].ToString();
                                    rowLast["DBId"] = rows[i]["DBId"].ToString();
                                    rowLast["RecNo"] = RecNo;
                                    rowLast["W1"] = rows[i]["W1"].ToString();
                                    rowLast["W2"] = rows[i]["W2"].ToString();
                                    rowLast["W3"] = rows[i]["W3"].ToString();
                                    rowLast["W4"] = rows[i]["W4"].ToString();
                                    rowLast["W5"] = rows[i]["W5"].ToString();
                                    dtLastRows.Rows.Add(rowLast);
                                }
                            }
                        }
                    }
                    row["NosFound"] = Hits;

                    id = id + 1;

                    row["Id"] = id;

                    dtSerchRes.Rows.Add(row);


                }

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Fullcell Search

        private void FullCellSearchMOD(DataTable dtTargetdt)
        {
            try
            {

                int numFound = 0;
                int totalNumFound = 0;
                DataTable dtRank = new DataTable();
                dtRank.Columns.Add("RecNo", Type.GetType("System.Int32"));
                dtRank.Columns.Add("startId", Type.GetType("System.Int32"));
                dtRank.Columns.Add("endId", Type.GetType("System.Int32"));
                dtRank.Columns.Add("lastId", Type.GetType("System.Int32"));
                dtRank.Columns.Add("numFound", Type.GetType("System.Int32"));
                dtRank.Columns.Add("totalNumFound", Type.GetType("System.Int32"));

                DataTable dtSearchTemp = dsSearch.Tables[0].Copy();
                //if (rdBottomToTop.Checked)
                //    dtSearchTemp = ReversreSearchTable();
                int count = dtSearchTemp.Rows.Count;
                for (int cnt = 0; cnt < count; cnt++)
                {
                    while ((cnt < count) && (dtSearchTemp.Rows[cnt]["W1"] == null || dtSearchTemp.Rows[cnt]["W1"] == DBNull.Value))
                    {
                        dtSearchTemp.Rows[cnt].Delete();
                        dtSearchTemp.AcceptChanges();
                        count = count - 1;
                    }
                }
                //if (rdInternalDB.Checked)
                //{
                //    progressBar1.Maximum = dtTargetdt.Rows.Count;

                //}
                for (int cnt = 14; cnt < dtTargetdt.Rows.Count;)
                {
                    //if (rdInternalDB.Checked)
                    //    progressBar1.Value = cnt;

                    numFound = 0;
                    totalNumFound = 0;
                    foreach (DataColumn dc in dtSearchTemp.Columns)
                    {
                        if (dc.Caption != "Id" && dc.Caption != "Date" && dc.Caption != "SUM_W" && dc.Caption != "SUM_M")
                        {
                            if (dtSearchTemp.Rows[14][dc] != null && dtSearchTemp.Rows[14][dc] != DBNull.Value && dtTargetdt.Rows[cnt][dc.Caption] != null && dtTargetdt.Rows[cnt][dc.Caption] != DBNull.Value)
                            {
                                if (Convert.ToInt32(dtSearchTemp.Rows[14][dc]) == Convert.ToInt32(dtTargetdt.Rows[cnt][dc.Caption]))
                                {
                                    numFound += 1;
                                    totalNumFound += 1;
                                    DataRow rowNumFound = dtNumFound.NewRow();
                                    rowNumFound["Id"] = Convert.ToInt32(dtTargetdt.Rows[cnt]["Id"]);
                                    rowNumFound["col"] = dc.Caption;
                                    rowNumFound["RecNo"] = dtRank.Rows.Count + 1;
                                    dtNumFound.Rows.Add(rowNumFound);
                                }
                                for (int i = 0; i < 14; i++)
                                {
                                    if (!(dtSearchTemp.Rows[i][dc] == null || dtSearchTemp.Rows[i][dc] == DBNull.Value || dtTargetdt.Rows[cnt - 14 + i][dc.Caption] == DBNull.Value || dtTargetdt.Rows[cnt - 14 + i][dc.Caption] == null))

                                        if (Convert.ToInt32(dtSearchTemp.Rows[i][dc]) == Convert.ToInt32(dtTargetdt.Rows[cnt - 14 + i][dc.Caption]))
                                        {
                                            totalNumFound += 1;
                                            DataRow rowNumFound = dtNumFound.NewRow();
                                            rowNumFound["Id"] = Convert.ToInt32(dtTargetdt.Rows[cnt - 14 + i]["Id"]);
                                            rowNumFound["col"] = dc.Caption;
                                            rowNumFound["RecNo"] = dtRank.Rows.Count + 1;
                                            dtNumFound.Rows.Add(rowNumFound);
                                        }
                                }
                            }
                        }
                    }
                    if (numFound == 0 || totalNumFound <= 2)
                    {
                        for (int i = 1; i <= totalNumFound; i++)
                        {
                            dtNumFound.Rows[dtNumFound.Rows.Count - 1].Delete();
                            dtNumFound.AcceptChanges();
                        }
                    }
                    if (numFound > 0 && totalNumFound > 2)
                    {
                        totalResFound += 1;
                        DataRow rowRank = dtRank.NewRow();
                        rowRank["startId"] = Convert.ToInt32(dtTargetdt.Rows[cnt - 14]["Id"]);
                        if (cnt >= dtTargetdt.Rows.Count)
                        {
                            cnt = dtTargetdt.Rows.Count - 1;
                        }
                        rowRank["endId"] = Convert.ToInt32(dtTargetdt.Rows[cnt]["Id"]);

                        if (dtTargetdt.Rows.Count > cnt + 2)
                            rowRank["lastId"] = Convert.ToInt32(dtTargetdt.Rows[cnt + 2]["Id"]);
                        else if (dtTargetdt.Rows.Count > cnt + 1)
                            rowRank["lastId"] = Convert.ToInt32(dtTargetdt.Rows[cnt + 1]["Id"]);
                        else
                            rowRank["lastId"] = Convert.ToInt32(dtTargetdt.Rows[cnt]["Id"]);
                        rowRank["numFound"] = numFound;
                        rowRank["totalNumFound"] = totalNumFound;
                        rowRank["RecNo"] = dtRank.Rows.Count + 1;
                        dtRank.Rows.Add(rowRank);
                        //cnt += 15;
                    }
                    //else
                    cnt++;
                }
                ImportToResultFullCellSearch(dtRank);

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void FullCellSearchBottomUp(DataTable dtTargetdt)
        {
            try
            {

                int numFound = 0;
                int totalNumFound = 0;
                DataTable dtRank = new DataTable();
                dtRank.Columns.Add("RecNo", Type.GetType("System.Int32"));
                dtRank.Columns.Add("startId", Type.GetType("System.Int32"));
                dtRank.Columns.Add("endId", Type.GetType("System.Int32"));
                dtRank.Columns.Add("lastId", Type.GetType("System.Int32"));
                dtRank.Columns.Add("numFound", Type.GetType("System.Int32"));
                dtRank.Columns.Add("totalNumFound", Type.GetType("System.Int32"));

                DataTable dtSearchTemp = dsSearch.Tables[0].Copy();
                //if (rdBottomToTop.Checked)
                //    dtSearchTemp = ReversreSearchTable();
                int count = dtSearchTemp.Rows.Count;
                for (int cnt = 0; cnt < count; cnt++)
                {
                    while ((cnt < count) && (dtSearchTemp.Rows[cnt]["W1"] == null || dtSearchTemp.Rows[cnt]["W1"] == DBNull.Value))
                    {
                        dtSearchTemp.Rows[cnt].Delete();
                        dtSearchTemp.AcceptChanges();
                        count = count - 1;
                    }
                }
                //if (rdInternalDB.Checked)
                //{
                //    progressBar1.Maximum = dtTargetdt.Rows.Count;

                //}
                for (int cnt = 14; cnt < dtTargetdt.Rows.Count;)
                {
                    //if (rdInternalDB.Checked)
                    //    progressBar1.Value = cnt;

                    numFound = 0;
                    totalNumFound = 0;
                    foreach (DataColumn dc in dtSearchTemp.Columns)
                    {
                        if (dc.Caption != "Id" && dc.Caption != "Date" && dc.Caption != "SUM_W" && dc.Caption != "SUM_M")
                        {
                            if (dtSearchTemp.Rows[0][dc] != null && dtSearchTemp.Rows[0][dc] != DBNull.Value && dtTargetdt.Rows[cnt - 14][dc.Caption] != null && dtTargetdt.Rows[cnt - 14][dc.Caption] != DBNull.Value)
                            {
                                if (Convert.ToInt32(dtSearchTemp.Rows[0][dc]) == (int)dtTargetdt.Rows[cnt - 14][dc.Caption])
                                {
                                    numFound += 1;
                                    totalNumFound += 1;
                                    DataRow rowNumFound = dtNumFound.NewRow();
                                    rowNumFound["Id"] = (int)dtTargetdt.Rows[cnt - 14]["Id"];
                                    rowNumFound["col"] = dc.Caption;
                                    rowNumFound["RecNo"] = dtRank.Rows.Count + 1;
                                    dtNumFound.Rows.Add(rowNumFound);
                                }
                                for (int i = 1; i <= 14; i++)
                                {
                                    if (cnt - 14 + i < dtTargetdt.Rows.Count)
                                    {
                                        if (!(dtSearchTemp.Rows[i][dc] == null || dtSearchTemp.Rows[i][dc] == DBNull.Value || dtTargetdt.Rows[cnt - 14 + i][dc.Caption] == DBNull.Value || dtTargetdt.Rows[cnt - 14 + i][dc.Caption] == null))
                                        {
                                            if (Convert.ToInt32(dtSearchTemp.Rows[i][dc]) == (int)dtTargetdt.Rows[cnt - 14 + i][dc.Caption])
                                            {
                                                totalNumFound += 1;
                                                DataRow rowNumFound = dtNumFound.NewRow();
                                                rowNumFound["Id"] = (int)dtTargetdt.Rows[cnt - 14 + i]["Id"];
                                                rowNumFound["col"] = dc.Caption;
                                                rowNumFound["RecNo"] = dtRank.Rows.Count + 1;
                                                dtNumFound.Rows.Add(rowNumFound);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (numFound == 0 || totalNumFound <= 2)
                    {
                        for (int i = 1; i <= totalNumFound; i++)
                        {
                            dtNumFound.Rows[dtNumFound.Rows.Count - 1].Delete();
                            dtNumFound.AcceptChanges();
                        }
                    }
                    if (numFound > 0 && totalNumFound > 2)
                    {
                        totalResFound += 1;
                        DataRow rowRank = dtRank.NewRow();
                        rowRank["startId"] = (int)dtTargetdt.Rows[cnt - 14]["Id"];
                        if (cnt >= dtTargetdt.Rows.Count)
                        {
                            cnt = dtTargetdt.Rows.Count - 1;
                        }
                        if (cnt - 16 >= 0)
                            rowRank["lastId"] = (int)dtTargetdt.Rows[cnt - 16]["Id"];
                        else if (cnt - 15 >= 0)
                            rowRank["lastId"] = (int)dtTargetdt.Rows[cnt - 15]["Id"];
                        else
                            rowRank["lastId"] = (int)dtTargetdt.Rows[cnt - 14]["Id"];
                        rowRank["endId"] = (int)dtTargetdt.Rows[cnt]["Id"];
                        rowRank["numFound"] = numFound;
                        rowRank["totalNumFound"] = totalNumFound;
                        rowRank["RecNo"] = dtRank.Rows.Count + 1;
                        dtRank.Rows.Add(rowRank);
                        //cnt += 15;
                    }
                    //else
                    cnt++;
                }
                ImportToResultFullCellSearch(dtRank);

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void ImportToResultFullCellSearch(DataTable dtRank)
        {
            try
            {
                int id = dtSerchRes.Rows.Count;
                dtRank.DefaultView.Sort = "numFound DESC,totalNumFound DESC";
                foreach (DataRowView rowView in dtRank.DefaultView)
                {

                    DataRow[] rows = dtBaseTable.Select("Id > " + ((int)rowView["startId"] - 1).ToString() + " and Id < " + ((int)rowView["lastId"] + 1).ToString());
                    int LastId = Convert.ToInt32(rowView["endId"]);
                    //if (rdBottomToTop.Checked)
                    //{
                    //    rows = dtBaseTable.Select("Id > " + ((int)rowView["lastId"] - 1).ToString() + " and Id < " + ((int)rowView["endId"] + 1).ToString());
                    //    LastId = (int)rowView["startId"];
                    //}
                    DataRow row4 = dtSerchRes.NewRow();
                    id = id + 1;
                    row4["Id"] = id;
                    row4["MatchType"] = "";
                    row4["NumHits"] = "";
                    row4["NosFound"] = rowView["numFound"];
                    row4["tNosFound"] = rowView["totalNumFound"];

                    dtSerchRes.Rows.Add(row4);

                    DataRow row3 = dtSerchRes.NewRow();
                    id = id + 1;
                    row3["Id"] = id;
                    int DBId = Convert.ToInt32(rows[0]["DBId"]);
                    row3["MatchType"] = "Database :";
                    row3["NumHits"] = SqlClass.GetDBNameById(DBId);
                    row3["NosFound"] = rowView["numFound"];
                    row3["tNosFound"] = rowView["totalNumFound"];
                    dtSerchRes.Rows.Add(row3);





                    DataRow row2 = dtSerchRes.NewRow();
                    id = id + 1;
                    row2["Id"] = id;
                    row2["MatchType"] = "Totle Num Found ";
                    row2["NumHits"] = rowView["totalNumFound"].ToString();
                    row2["NosFound"] = rowView["numFound"];
                    row2["tNosFound"] = rowView["totalNumFound"];
                    dtSerchRes.Rows.Add(row2);

                    DataRow row1 = dtSerchRes.NewRow();
                    id = id + 1;
                    row1["Id"] = id;
                    row1["MatchType"] = "Num Found in Forcast Line ";
                    row1["NumHits"] = rowView["numFound"].ToString();
                    row1["NosFound"] = rowView["numFound"];
                    row1["tNosFound"] = rowView["totalNumFound"];
                    dtSerchRes.Rows.Add(row1);

                    for (int j = 0; j < rows.Length; j++)
                    {
                        int i = j;
                        //if (rdBottomToTop.Checked)
                        //    i = rows.Length - 1 - j;

                        DataRow row = dtSerchRes.NewRow();
                        row["Date"] = rows[i]["Date"];
                        row["W1"] = rows[i]["W1"];
                        row["W2"] = rows[i]["W2"];
                        row["W3"] = rows[i]["W3"];
                        row["W4"] = rows[i]["W4"];
                        row["W5"] = rows[i]["W5"];
                        row["SUM_W"] = rows[i]["SUM_W"];
                        row["M1"] = rows[i]["M1"];
                        row["M2"] = rows[i]["M2"];
                        row["M3"] = rows[i]["M3"];
                        row["M4"] = rows[i]["M4"];
                        row["M5"] = rows[i]["M5"];
                        row["SUM_M"] = rows[i]["SUM_M"];
                        row["SNo"] = rows[i]["SNo"];
                        row["RecordId"] = rows[i]["Id"].ToString();
                        if (false)//rdTopToBottom.Checked
                        {
                            if (Convert.ToInt32(rows[i]["Id"]) != LastId && Convert.ToInt32(rows[i]["Id"]) != LastId - 1)
                            {
                                if (i == rows.Length - 1 || i == rows.Length - 2)
                                    row["RecordId"] = 0;
                                if (i == rows.Length - 2)
                                {
                                    DataRow rowLast = dtLastRows.NewRow();
                                    rowLast["Id"] = id;
                                    rowLast["RecordId"] = rows[i]["Id"].ToString();
                                    rowLast["DBId"] = rows[i]["DBId"].ToString();
                                    rowLast["RecNo"] = rowView["RecNo"];
                                    rowLast["W1"] = rows[i]["W1"].ToString();
                                    rowLast["W2"] = rows[i]["W2"].ToString();
                                    rowLast["W3"] = rows[i]["W3"].ToString();
                                    rowLast["W4"] = rows[i]["W4"].ToString();
                                    rowLast["W5"] = rows[i]["W5"].ToString();
                                    dtLastRows.Rows.Add(rowLast);
                                }
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(rows[i]["Id"]) != Convert.ToInt32(rowView["endId"]) && Convert.ToInt32(rows[i]["Id"]) != Convert.ToInt32(rowView["endId"]) + 1)
                            {
                                if (i == 0 || i == 1)
                                    row["RecordId"] = 0;
                                if (i == 1)
                                {
                                    DataRow rowLast = dtLastRows.NewRow();
                                    rowLast["Id"] = id;
                                    rowLast["RecordId"] = rows[i]["Id"].ToString();
                                    rowLast["DBId"] = rows[i]["DBId"].ToString();
                                    rowLast["RecNo"] = rowView["RecNo"];
                                    rowLast["W1"] = rows[i]["W1"].ToString();
                                    rowLast["W2"] = rows[i]["W2"].ToString();
                                    rowLast["W3"] = rows[i]["W3"].ToString();
                                    rowLast["W4"] = rows[i]["W4"].ToString();
                                    rowLast["W5"] = rows[i]["W5"].ToString();
                                    dtLastRows.Rows.Add(rowLast);
                                }
                            }
                        }
                        id = id + 1;
                        row["Id"] = id;
                        row["RecNo"] = rowView["RecNo"];

                        row["NosFound"] = rowView["numFound"];
                        row["tNosFound"] = rowView["totalNumFound"];
                        dtSerchRes.Rows.Add(row);
                    }

                }


            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
        #endregion

        private void MakeSearch(int DBId)
        {
            DataSet ds = new DataSet();

            SqlClass.GetWin_Machin_DataByDBId(DBId, ref ds);
            dtBaseTable = ds.Tables[0].Copy();


            DataTable dtTargetdt = dtBaseTable.Copy();
            //if (rdBottomToTop.Checked)
            //{
            //    if (rdExectSearch12.Checked || rdCodeSearch.Checked)
            //        ReversreSearchTable(ref dtTargetdt);
            //}
            int count = dtBaseTable.Rows.Count;
            for (int cnt = 0; cnt < count; cnt++)
            {
                while ((cnt < count) && (dtTargetdt.Rows[cnt]["W1"] == null || dtTargetdt.Rows[cnt]["W1"] == DBNull.Value))
                {
                    dtTargetdt.Rows[cnt].Delete();
                    dtTargetdt.AcceptChanges();
                    count = count - 1;
                }
            }
            FullCellSearchMOD(dtTargetdt);
            //if (rdExectSearch12.Checked)
            //    ExectSearch(dtTargetdt);
            //else if (rdFullCellSearch.Checked)
            //{
            //    if (rdTopToBottom.Checked)
            //        FullCellSearchMOD(dtTargetdt);
            //    else
            //        FullCellSearchBottomUp(dtTargetdt);
            //}
            ////FullCellSearch(dtTargetdt);
            //else if (rdCodeSearch.Checked)
            //    //ExectSearchM(dtTargetdt);
            //    GeneralCodeSearch(dtTargetdt);
            //else
            //{
            //    if (rdTopToBottom.Checked)
            //        MachineTailSearchMOD(dtTargetdt);
            //    else
            //        MachineTailSearchBottomUp(dtTargetdt);
            //}
            //MachineTailSearch(dtTargetdt);

        }

        private void PreSettingSearch()
        {
            try
            {
                //progressBar1.Value = 0;
                dtNumFound.Rows.Clear();
                totalResFound = 0;

                dsSearch.GetChanges();
                dtSummary.Rows.Clear();
                dtLastRows.Rows.Clear();

                dtSearchNum = SqlClass.GetSearchNum().Clone();
                //if (rdCodeSearch.Checked)
                //{
                //    if (arSymboles == null)
                //    {
                //        arSymboles = new ArrayList();
                //    }
                //    arSymboles.Clear();

                //    if (!dtSearchNum.Columns.Contains("Sym"))
                //        dtSearchNum.Columns.Add("Sym");
                //    if (!dtNumFound.Columns.Contains("Sym"))
                //        dtNumFound.Columns.Add("Sym");
                //}
                //if (rdMachineTailSearch.Checked)
                //{
                //    if (!dtNumFound.Columns.Contains("Sym"))
                //        dtNumFound.Columns.Add("Sym");
                //    if (!dtNumFound.Columns.Contains("RecNo"))
                //        dtNumFound.Columns.Add("RecNo");
                //}
                int Sym = 0;
                DataTable dtTemp = dsSearch.Tables[0].Copy();
                //if (rdBottomToTop.Checked && (rdFullCellSearch.Checked || rdMachineTailSearch.Checked))
                //    dtTemp = ReversreSearchTable();

                foreach (DataRow row in dtTemp.Rows)
                {
                    foreach (DataColumn col in dtTemp.Columns)
                    {
                        if (col.Caption != "Id" && col.Caption != "Date" && col.Caption != "SUM_W" && col.Caption != "SUM_M")
                            if (!(row[col] == DBNull.Value || row[col] == null))
                            {
                                DataRow rowSerNum = dtSearchNum.NewRow();
                                rowSerNum["Row"] = row["Id"];
                                //if (col.Caption.Contains("W"))
                                rowSerNum["W"] = col.Caption;
                                if (col.Caption.Contains("M"))
                                    rowSerNum["M"] = col.Caption;
                                rowSerNum["dValue"] = row[col].ToString().Trim();
                                //if (rdCodeSearch.Checked)
                                //{
                                //    rowSerNum["Sym"] = row[col].ToString().Trim().Split('N')[1];
                                //    bool flag = true;
                                //    for (int i = 0; i < arSymboles.Count; i++)
                                //    {
                                //        if (arSymboles[i].ToString() == row[col].ToString().Trim().Split('N')[1])
                                //        {
                                //            flag = false;
                                //        }
                                //    }
                                //    if (flag)
                                //    {
                                //        arSymboles.Add(row[col].ToString().Trim().Split('N')[1]);
                                //        Sym++;
                                //    }
                                //    //rowSerNum["Sym"] = Sym;
                                //}

                                dtSearchNum.Rows.Add(rowSerNum);
                            }
                    }
                }
                //SqlClass.SaveSearchNum(dtSearchNum);
                //SqlClass.UpdateSearchPlaneData(ref dsSearch);
                if (dtSearchNum.Rows.Count == 0)
                    return;
                DataSet ds = new DataSet();
                SqlClass.GetWin_Machin_DataByDBId(0, ref ds);
                dtSerchRes = ds.Tables[0].Clone();

                dtSerchRes.Columns.Add("RecNo", Type.GetType("System.Int32"));
                dtSerchRes.Columns.Add("NumHits");
                dtSerchRes.Columns.Add("MatchType");
                dtSerchRes.Columns.Add("RecordId");
                dtSerchRes.Columns.Add("NosFound", Type.GetType("System.Int32"));
                dtSerchRes.Columns.Add("tNosFound", Type.GetType("System.Int32"));
                int DBId = 0;
                if (true)//rdInternalDB.Checked
                {
                    DataTable dtInter = SqlClass.GetInternalDatabase();

                    if (dtInter.Rows.Count > 0)
                    {
                        DBId = Convert.ToInt32(dtInter.Rows[0]["DBId"]);
                    }
                    MakeSearch(DBId);
                }
                else
                {
                    //DataTable dt = SqlClass.GetExternalDatabaseList();
                    //string DBIdList = "";
                    //for (int i = 0; i < checkedListBox1.CheckedItems.Count; i++)
                    //{
                    //    progressBar1.Value += 100 / checkedListBox1.CheckedItems.Count;
                    //    DataRow[] rows = dt.Select("DBName = '" + checkedListBox1.CheckedItems[i].ToString() + "'");
                    //    DBIdList += "," + rows[0]["DBId"].ToString();
                    //    DBId = Convert.ToInt32(rows[0]["DBId"]);
                    //    MakeSearch(DBId);
                    //}
                    //progressBar1.Value = 100;
                    //if (DBIdList != "")
                    //{
                    //    DBIdList = DBIdList.Substring(1, DBIdList.Length - 1);

                    //}
                }

                //timer1.Enabled = true;
                PrepareSummary();
                //Date1.DefaultCellStyle.Format = "dd/MM/yyyy";
                dtSerchRes.DefaultView.Sort = "NosFound desc,tNosFound desc";
                dataGrid.ItemsSource = dtSerchRes.DefaultView;
                //ResultGrid.Columns["DBId"].Visible = false;
                //ResultGrid.Columns["NosFound"].Visible = false;
                //ResultGrid.Columns["RecNo"].Visible = false;
                //lblResultFound.Text = "Total Result Found : " + totalResFound.ToString();


            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void PrepareSummary()
        {
            int b = 0;
            try
            {
                int MaxCount = 1;
                for (int i = 0; i < dtLastRows.Rows.Count; i++)
                {

                    foreach (DataColumn dc in dtLastRows.Columns)
                    {
                        if (dc.Caption.StartsWith("W"))
                        {
                            if (!(dtLastRows.Rows[i][dc] == null || dtLastRows.Rows[i][dc] == DBNull.Value))
                            {
                                if (dtLastRows.Rows[i][dc].ToString().Trim() != "")
                                {
                                    string num = Convert.ToString(dtLastRows.Rows[i][dc]);
                                    DataRow[] rowSum = dtSummary.Select("Number = '" + num + "'");
                                    if (rowSum.Length > 0)
                                    {
                                        rowSum[0]["cnt"] = (Convert.ToInt32(rowSum[0]["cnt"]) + 1).ToString();
                                        rowSum[0]["Description"] = "Appears " + rowSum[0]["cnt"].ToString() + " times";
                                        if (MaxCount < Convert.ToInt32(rowSum[0]["cnt"]))
                                            MaxCount = Convert.ToInt32(rowSum[0]["cnt"]);
                                    }
                                    else
                                    {
                                        DataRow newSum = dtSummary.NewRow();
                                        newSum["Id"] = dtSummary.Rows.Count + 1;
                                        newSum["Number"] = dtLastRows.Rows[i][dc];
                                        newSum["cnt"] = 1;
                                        newSum["Description"] = "Appers 1 time";
                                        dtSummary.Rows.Add(newSum);
                                    }
                                }
                            }
                        }
                    }
                }
                DataTable dtPairs = new DataTable();
                dtPairs.Columns.Add("p1", Type.GetType("System.Int32"));
                dtPairs.Columns.Add("p2", Type.GetType("System.Int32"));
                for (int i = 0; i < dtSummary.Rows.Count - 1; i++)
                {
                    for (int j = i + 1; j < dtSummary.Rows.Count - 1; j++)
                    {
                        DataRow row = dtPairs.NewRow();
                        row["p1"] = dtSummary.Rows[i]["Number"];
                        row["p2"] = dtSummary.Rows[j]["Number"];
                        dtPairs.Rows.Add(row);
                    }
                }

                foreach (DataRow rPair in dtPairs.Rows)
                {
                    int CountPair = 0;
                    //break;
                    foreach (DataRow rLast in dtLastRows.Rows)
                    {
                        bool bBreak = false;
                        foreach (DataColumn dc in dtLastRows.Columns)
                        {
                            if (dc.Caption.StartsWith("W") && (!(rLast[dc] == null || rLast[dc] == DBNull.Value)))
                            {
                                if (rLast[dc].ToString().Trim() != "")
                                    foreach (DataColumn dc1 in dtLastRows.Columns)
                                    {

                                        if (dc1.Caption.StartsWith("W"))
                                        {
                                            if (!(rLast[dc1] == null || rLast[dc1] == DBNull.Value))
                                            {
                                                if (rLast[dc1].ToString().Trim() != "")
                                                    if ((Convert.ToInt32(rPair["p1"]) == Convert.ToInt32(rLast[dc]) && Convert.ToInt32(rPair["p2"]) == Convert.ToInt32(rLast[dc1])) || (Convert.ToInt32(rPair["p2"]) == Convert.ToInt32(rLast[dc]) && Convert.ToInt32(rPair["p1"]) == Convert.ToInt32(rLast[dc1])))
                                                    {
                                                        CountPair++;
                                                        bBreak = true;
                                                        break;
                                                    }
                                            }
                                        }
                                    }
                                if (bBreak)
                                    break;
                            }
                        }
                    }
                    DataRow newSum = dtSummary.NewRow();
                    newSum["Id"] = dtSummary.Rows.Count + 1;
                    newSum["Number"] = rPair["p1"].ToString() + " , " + rPair["p2"].ToString();
                    newSum["cnt"] = CountPair;
                    newSum["Description"] = "Appers " + CountPair.ToString() + " times";
                    dtSummary.Rows.Add(newSum);

                }

                dtSummary.DefaultView.RowFilter = "cnt > 1";
                dtSummary.DefaultView.Sort = "cnt desc";
                dataGrid.DataContext = dtSummary.DefaultView;

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void FullCellButton_Click(object sender, RoutedEventArgs e)
        {
            GetlastRowsMod();// called on radio button changed
            PreSettingSearch();
            //PrepareSearchGrid();// called on search button click
        }
    }
}
