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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfPort
{
    /// <summary>
    /// Interaction logic for NumberTrace.xaml
    /// </summary>
    public partial class NumberTrace : Window
    {
        public NumberTrace()
        {
            InitializeComponent();
        }

        private static int intRN = 0;
        private static int intCN = 0;
        private static int intTCN = 0;
        private static int intTN = 0;
        private static DataSet ds;
        private static DataTable dtNumFound;
        private static DataTable dtSummary;
        private static bool flag = false;

        private void txtFirstNum_Validated(object sender, EventArgs e)
        {

            try
            {
                if (txtFirstNum.Text != "")
                {
                    intRN = Convert.ToInt32(txtFirstNum.Text);
                    intCN = GetCounter(intRN);
                    intTN = GetTurning(intRN);
                    intTCN = GetTurning(intCN);

                    txtSecondNumber.Text = intCN.ToString();
                    txtThirdNumber.Text = intTN.ToString();
                    txtForthNum.Text = intTCN.ToString();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                flag = true;
                NumberTraceSearch();
                LoadDataSet();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
        private int GetTurning(int RN)
        {
            int TN = RN;
            TN = (TN % 10) * 10 + TN / 10;
            if (TN > 90)
                TN = RN;
            return TN;
        }

        private int GetCounter(int RN)
        {
            if (RN <= 45)
                return RN + 45;
            else
                return RN - 45;
        }
        private void LoadDataSet()
        {
            try
            {
                if ((SqlClass.GetInternalDatabase()).Rows.Count > 0)
                {
                    lblInternalDatabaseName.DataContext = (SqlClass.GetInternalDatabase()).Rows[0]["DBName"].ToString();
                    int DBId = Convert.ToInt32((SqlClass.GetInternalDatabase()).Rows[0]["DBId"]);
                    ds = new DataSet();
                    SqlClass.GetWin_Machin_DataByDBId(DBId, ref ds);

                    //Date.DefaultCellStyle.Format = "dd/MM/yyyy";
                    searchGrid.ItemsSource = ds.Tables[0].DefaultView;
                    searchGrid.Visibility = Visibility.Visible;
                    //searchGrid.Columns["Id"].Visibility = Visibility.Collapsed;
                    //searchGrid.Columns["DBId"].Visibility = Visibility.Collapsed;
                }
                else
                {
                    System.Windows.MessageBox.Show("Internal database is not seted");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void NumberTrace_Load(object sender, EventArgs e)
        {
            LoadDataSet();
        }

        private void NumberTraceSearch()
        {
            try
            {
                dtNumFound = new DataTable();
                dtNumFound.Columns.Add("Id", Type.GetType("System.Int32"));
                dtNumFound.Columns.Add("SNo", Type.GetType("System.Int32"));
                dtNumFound.Columns.Add("Col");
                dtNumFound.Columns.Add("Code");

                dtSummary = new DataTable();
                dtSummary.Columns.Add("SearchCode");
                dtSummary.Columns.Add("Events");
                dtSummary.Columns.Add("WinningMachine");

                string strCol = "W";
                if (rdMachineNumbersOnly.IsChecked.Value)
                    strCol = "M";
                for (int cnt = 0; cnt < ds.Tables[0].Rows.Count; cnt++)
                {
                    if (!(ds.Tables[0].Rows[cnt]["W1"] == null || ds.Tables[0].Rows[cnt]["W1"] == DBNull.Value))
                    {
                        foreach (DataColumn dc in ds.Tables[0].Columns)
                        {
                            if (dc.Caption.StartsWith("M") || dc.Caption.StartsWith("W"))
                                if (dc.Caption.StartsWith(strCol) || rdWinningAndMachineNumbers.IsChecked.Value)
                                {
                                    if (intRN == Convert.ToInt32(ds.Tables[0].Rows[cnt][dc]))
                                    {
                                        DataRow row = dtNumFound.NewRow();
                                        row["Id"] = ds.Tables[0].Rows.Count + 1;
                                        row["SNo"] = ds.Tables[0].Rows[cnt]["SNo"];
                                        row["Col"] = dc.Caption;
                                        row["Code"] = "RN";
                                        dtNumFound.Rows.Add(row);
                                    }
                                    if (intRN != intCN && intCN == Convert.ToInt32(ds.Tables[0].Rows[cnt][dc]))
                                    {
                                        DataRow row = dtNumFound.NewRow();
                                        row["Id"] = ds.Tables[0].Rows.Count + 1;
                                        row["SNo"] = ds.Tables[0].Rows[cnt]["SNo"];
                                        row["Col"] = dc.Caption;
                                        row["Code"] = "CN";
                                        dtNumFound.Rows.Add(row);
                                    }
                                    if (intRN != intTN && intTN == Convert.ToInt32(ds.Tables[0].Rows[cnt][dc]))
                                    {
                                        DataRow row = dtNumFound.NewRow();
                                        row["Id"] = ds.Tables[0].Rows.Count + 1;
                                        row["SNo"] = ds.Tables[0].Rows[cnt]["SNo"];
                                        row["Col"] = dc.Caption;
                                        row["Code"] = "TN";
                                        dtNumFound.Rows.Add(row);
                                    }
                                    if (intRN != intTCN && intTCN != intCN && intTCN == Convert.ToInt32(ds.Tables[0].Rows[cnt][dc]))
                                    {
                                        DataRow row = dtNumFound.NewRow();
                                        row["Id"] = ds.Tables[0].Rows.Count + 1;
                                        row["SNo"] = ds.Tables[0].Rows[cnt]["SNo"];
                                        row["Col"] = dc.Caption;
                                        row["Code"] = "TCN";
                                        dtNumFound.Rows.Add(row);
                                    }
                                }
                        }
                    }
                }
                DataRow[] dtNums = dtNumFound.Select("Code = 'RN'");
                if (dtNums.Length > 0)
                {
                    DataRow rowSum1 = dtSummary.NewRow();
                    DataRow rowSum2 = dtSummary.NewRow();
                    DataRow rowSum3 = dtSummary.NewRow();

                    rowSum1["SearchCode"] = "RN";
                    foreach (DataRow rowNum in dtNums)
                    {
                        if (rdWinningNumbersOnly.IsChecked.Value)
                        {
                            rowSum3["Events"] += rowNum["SNo"].ToString() + ",";
                        }
                        if (rdMachineNumbersOnly.IsChecked.Value)
                        {
                            rowSum2["Events"] += rowNum["SNo"].ToString() + ",";
                        }
                        if (rdWinningAndMachineNumbers.IsChecked.Value)
                        {
                            if (rowNum["Col"].ToString().StartsWith("W"))
                                rowSum3["Events"] += rowNum["SNo"].ToString() + ",";
                            else
                                rowSum2["Events"] += rowNum["SNo"].ToString() + ",";
                        }
                    }
                    if (rdMachineNumbersOnly.IsChecked.Value)
                    {
                        rowSum1["Events"] = rowSum2["Events"];
                        rowSum1["WinningMachine"] = "Find in machine no only";
                        dtSummary.Rows.Add(rowSum1);
                    }
                    else if (rdWinningNumbersOnly.IsChecked.Value)
                    {
                        rowSum1["Events"] = rowSum3["Events"];
                        rowSum1["WinningMachine"] = "Find in winning no only";
                        dtSummary.Rows.Add(rowSum1);
                    }
                    else
                    {
                        rowSum1["Events"] = rowSum3["Events"].ToString() + rowSum2["Events"].ToString();
                        rowSum1["WinningMachine"] = "Both in winning and machine";
                        rowSum3["WinningMachine"] = "Find in winning no only";
                        rowSum2["WinningMachine"] = "Find in machine no only";
                        dtSummary.Rows.Add(rowSum1);
                        dtSummary.Rows.Add(rowSum3);
                        dtSummary.Rows.Add(rowSum2);
                    }
                }


                DataRow[] dtNumsCN = dtNumFound.Select("Code = 'CN'");
                if (dtNumsCN.Length > 0)
                {
                    DataRow rowSum1 = dtSummary.NewRow();
                    DataRow rowSum2 = dtSummary.NewRow();
                    DataRow rowSum3 = dtSummary.NewRow();

                    rowSum1["SearchCode"] = "CN";
                    foreach (DataRow rowNum in dtNumsCN)
                    {
                        if (rdWinningNumbersOnly.IsChecked.Value)
                        {
                            rowSum3["Events"] += rowNum["SNo"].ToString() + ",";
                        }
                        if (rdMachineNumbersOnly.IsChecked.Value)
                        {
                            rowSum2["Events"] += rowNum["SNo"].ToString() + ",";
                        }
                        if (rdWinningAndMachineNumbers.IsChecked.Value)
                        {
                            if (rowNum["Col"].ToString().StartsWith("W"))
                                rowSum3["Events"] += rowNum["SNo"].ToString() + ",";
                            else
                                rowSum2["Events"] += rowNum["SNo"].ToString() + ",";
                        }
                    }
                    if (rdMachineNumbersOnly.IsChecked.Value)
                    {
                        rowSum1["Events"] = rowSum2["Events"];
                        rowSum1["WinningMachine"] = "Find in machine no only";
                        dtSummary.Rows.Add(rowSum1);
                    }
                    else if (rdWinningNumbersOnly.IsChecked.Value)
                    {
                        rowSum1["Events"] = rowSum3["Events"];
                        rowSum1["WinningMachine"] = "Find in winning no only";
                        dtSummary.Rows.Add(rowSum1);
                    }
                    else
                    {
                        rowSum1["Events"] = rowSum3["Events"].ToString() + rowSum2["Events"].ToString();
                        rowSum1["WinningMachine"] = "Both in winning and machine";
                        rowSum3["WinningMachine"] = "Find in winning no only";
                        rowSum2["WinningMachine"] = "Find in machine no only";
                        dtSummary.Rows.Add(rowSum1);
                        dtSummary.Rows.Add(rowSum3);
                        dtSummary.Rows.Add(rowSum2);
                    }
                }



                DataRow[] dtNumsTN = dtNumFound.Select("Code = 'TN'");
                if (dtNumsTN.Length > 0)
                {
                    DataRow rowSum1 = dtSummary.NewRow();
                    DataRow rowSum2 = dtSummary.NewRow();
                    DataRow rowSum3 = dtSummary.NewRow();

                    rowSum1["SearchCode"] = "TN";
                    foreach (DataRow rowNum in dtNumsTN)
                    {
                        if (rdWinningNumbersOnly.IsChecked.Value)
                        {
                            rowSum3["Events"] += rowNum["SNo"].ToString() + ",";
                        }
                        if (rdMachineNumbersOnly.IsChecked.Value)
                        {
                            rowSum2["Events"] += rowNum["SNo"].ToString() + ",";
                        }
                        if (rdWinningAndMachineNumbers.IsChecked.Value)
                        {
                            if (rowNum["Col"].ToString().StartsWith("W"))
                                rowSum3["Events"] += rowNum["SNo"].ToString() + ",";
                            else
                                rowSum2["Events"] += rowNum["SNo"].ToString() + ",";
                        }
                    }
                    if (rdMachineNumbersOnly.IsChecked.Value)
                    {
                        rowSum1["Events"] = rowSum2["Events"];
                        rowSum1["WinningMachine"] = "Find in machine no only";
                        dtSummary.Rows.Add(rowSum1);
                    }
                    else if (rdWinningNumbersOnly.IsChecked.Value)
                    {
                        rowSum1["Events"] = rowSum3["Events"];
                        rowSum1["WinningMachine"] = "Find in winning no only";
                        dtSummary.Rows.Add(rowSum1);
                    }
                    else
                    {
                        rowSum1["Events"] = rowSum3["Events"].ToString() + rowSum2["Events"].ToString();
                        rowSum1["WinningMachine"] = "Both in winning and machine";
                        rowSum3["WinningMachine"] = "Find in winning no only";
                        rowSum2["WinningMachine"] = "Find in machine no only";
                        dtSummary.Rows.Add(rowSum1);
                        dtSummary.Rows.Add(rowSum3);
                        dtSummary.Rows.Add(rowSum2);
                    }
                }


                DataRow[] dtNumsTCN = dtNumFound.Select("Code = 'TCN'");
                if (dtNumsTCN.Length > 0)
                {
                    DataRow rowSum1 = dtSummary.NewRow();
                    DataRow rowSum2 = dtSummary.NewRow();
                    DataRow rowSum3 = dtSummary.NewRow();

                    rowSum1["SearchCode"] = "TCN";
                    foreach (DataRow rowNum in dtNumsTCN)
                    {
                        if (rdWinningNumbersOnly.IsChecked.Value)
                        {
                            rowSum3["Events"] += rowNum["SNo"].ToString() + ",";
                        }
                        if (rdMachineNumbersOnly.IsChecked.Value)
                        {
                            rowSum2["Events"] += rowNum["SNo"].ToString() + ",";
                        }
                        if (rdWinningAndMachineNumbers.IsChecked.Value)
                        {
                            if (rowNum["Col"].ToString().StartsWith("W"))
                                rowSum3["Events"] += rowNum["SNo"].ToString() + ",";
                            else
                                rowSum2["Events"] += rowNum["SNo"].ToString() + ",";
                        }
                    }
                    if (rdMachineNumbersOnly.IsChecked.Value)
                    {
                        rowSum1["Events"] = rowSum2["Events"];
                        rowSum1["WinningMachine"] = "Find in machine no only";
                        dtSummary.Rows.Add(rowSum1);
                    }
                    else if (rdWinningNumbersOnly.IsChecked.Value)
                    {
                        rowSum1["Events"] = rowSum3["Events"];
                        rowSum1["WinningMachine"] = "Find in winning no only";
                        dtSummary.Rows.Add(rowSum1);
                    }
                    else
                    {
                        rowSum1["Events"] = rowSum3["Events"].ToString() + rowSum2["Events"].ToString();
                        rowSum1["WinningMachine"] = "Both in winning and machine";
                        rowSum3["WinningMachine"] = "Find in winning no only";
                        rowSum2["WinningMachine"] = "Find in machine no only";
                        dtSummary.Rows.Add(rowSum1);
                        dtSummary.Rows.Add(rowSum3);
                        dtSummary.Rows.Add(rowSum2);
                    }
                }

                ResultGrid.DataContext = dtSummary;
            }
            catch (Exception ex)
            {
            }
        }

        private void searchGrid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                    if (flag)
                    {
                        foreach (DataRow row in dtNumFound.Rows)
                        {
                            if (searchGrid.Columns[e.ColumnIndex].Header.ToString() == row["Col"].ToString())
                            {
                                //if (Convert.ToInt32(searchGrid.Rows[e.RowIndex].Cells["SNo"].Value) == Convert.ToInt32(row["SNo"]))
                                //{
                                //    if (row["Code"].ToString() == "RN")
                                //        e.CellStyle.BackColor = System.Drawing.Color.FromArgb(Colors.Red.A, Colors.Red.R, Colors.Red.G, Colors.Red.B);
                                //    else if (row["Code"].ToString() == "CN")
                                //        e.CellStyle.BackColor = System.Drawing.Color.FromArgb(Colors.Blue.A, Colors.Blue.R, Colors.Blue.G, Colors.Blue.B);
                                //    else if (row["Code"].ToString() == "TN")
                                //        e.CellStyle.BackColor = System.Drawing.Color.FromArgb(Colors.Green.A, Colors.Green.R, Colors.Green.G, Colors.Green.B);
                                //    else
                                //        e.CellStyle.BackColor = System.Drawing.Color.FromArgb(Colors.Brown.A, Colors.Brown.R, Colors.Brown.G, Colors.Brown.B);


                                //}

                            }
                        }
                    }
            }
            catch (Exception ex)
            {
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                Refresh();
            }
            catch (Exception ex)
            {
            }
        }
        private void Refresh()
        {
            try
            {
                //saveResult();
                flag = false;
                LoadDataSet();

                if (dtSummary != null)
                    dtSummary.Rows.Clear();

                txtFirstNum.Text = "";
                txtSecondNumber.Text = "";
                txtThirdNumber.Text = "";
                txtForthNum.Text = "";
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
    }
}
