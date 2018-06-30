using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfPort
{
    class SqlClass
    {
        private static SQLiteConnection con;
        private static SQLiteDataAdapter adp1;
        private static SQLiteDataAdapter adp2;
        private static readonly string sqlConn = @"Data Source=C:\Users\garvj\Source\Repos\WpfPort\WpfPort\data.sqlite";


        public static void ImportDatabase(DataSet dsExcell, string DatabaseName)
        {
            try
            {
                setConnectionString();
                con = new SQLiteConnection(sqlConn);
                SQLiteCommand cmd = new SQLiteCommand("insert into DBList(DBName,GameType,AllowNoFrom,AllowNoTo,DateOfCreation,IsInternalDatabase) values('" + DatabaseName + "','Weekly',1,90,getdate(),0) select Scope_Identity()");
                cmd.Connection = con;
                con.Open();
                int DBId = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                SQLiteDataAdapter adp = new SQLiteDataAdapter("select * from  Win_Machin_Table where DBId =" + DBId.ToString(), con);
                DataSet dsWin_Machin_Table = new DataSet();
                adp.Fill(dsWin_Machin_Table);
                int i = 1;
                DateTime dt = DateTime.Now;
                bool flag = true;
                foreach (DataRow rowFromExcell in dsExcell.Tables[0].Rows)
                {
                    DataRow rowSQL = dsWin_Machin_Table.Tables[0].NewRow();
                    rowSQL["DBId"] = DBId;
                    rowSQL["SNo"] = i;
                    if (flag)
                    {
                        try
                        {
                            if (rowFromExcell[0] != DBNull.Value)
                            {
                                string strDate = rowFromExcell[0].ToString();
                                int day = Convert.ToInt32(strDate.Split('/')[0]);
                                int month = Convert.ToInt32(strDate.Split('/')[1]);
                                int year = Convert.ToInt32(strDate.Split('/')[2]);
                                dt = new DateTime(year, month, day);
                                flag = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            flag = false;

                            string strDate = rowFromExcell[0].ToString();
                            int month = Convert.ToInt32(strDate.Split('/')[0]);
                            int day = Convert.ToInt32(strDate.Split('/')[1]);
                            int year = Convert.ToInt32(strDate.Split('/')[2]);
                            dt = new DateTime(year, month, day);

                            flag = true;
                        }
                    }
                    if (!flag)
                        rowSQL["Date"] = dt;
                    rowSQL["W1"] = rowFromExcell[1];
                    rowSQL["W2"] = rowFromExcell[2];
                    rowSQL["W3"] = rowFromExcell[3];
                    rowSQL["W4"] = rowFromExcell[4];
                    rowSQL["W5"] = rowFromExcell[5];
                    rowSQL["SUM_W"] = rowFromExcell[6];
                    if (dsExcell.Tables[0].Columns.Count > 7)
                    {
                        rowSQL["M1"] = rowFromExcell[8];
                        rowSQL["M2"] = rowFromExcell[9];
                        rowSQL["M3"] = rowFromExcell[10];
                        rowSQL["M4"] = rowFromExcell[11];
                        rowSQL["M5"] = rowFromExcell[12];
                        rowSQL["SUM_M"] = rowFromExcell[13];
                    }
                    dt = dt = dt.AddDays(7);
                    dsWin_Machin_Table.Tables[0].Rows.Add(rowSQL);
                    i++;
                }
                for (int j = 0; j < 5; j++)
                {
                    DataRow rowSQL = dsWin_Machin_Table.Tables[0].NewRow();
                    rowSQL["DBId"] = DBId;
                    rowSQL["SNo"] = i;
                    rowSQL["Date"] = dt;
                    dsWin_Machin_Table.Tables[0].Rows.Add(rowSQL);
                    dt = dt = dt.AddDays(7);
                    i++;
                }
                dsWin_Machin_Table.GetChanges();
                SQLiteCommandBuilder cb = new SQLiteCommandBuilder(adp);
                adp.Update(dsWin_Machin_Table);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        public static DataTable GetExternalDatabaseList()
        {
            DataSet dsDBList = new DataSet();
            try
            {
                setConnectionString();
                con = new SQLiteConnection(sqlConn);
                SQLiteDataAdapter adp = new SQLiteDataAdapter("select * from 'dbo.DBList' where IsInternalDatabase = '0'", con);

                adp.Fill(dsDBList);
                return dsDBList.Tables[0];

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return dsDBList.Tables[0];
        }

        public static DataTable GetInternalDatabase()
        {
            DataSet dsDBList = new DataSet();
            try
            {
                setConnectionString();
                con = new SQLiteConnection(sqlConn);
                SQLiteDataAdapter adp = new SQLiteDataAdapter("select * from 'dbo.DBList' where IsInternalDatabase = '1'", con);

                adp.Fill(dsDBList);
                return dsDBList.Tables[0];

            }
            catch (Exception ex)
            {
                throw ex;
            }
            //finally
            //{
            //    if (con.State == ConnectionState.Open)
            //        con.Close();
            //}
            
        }

        public static void GetWin_Machin_DataByDBId(int DBId, ref DataSet dsWin_Machin_Data)
        {

            //DataSet dsWin_Machin_Data = new DataSet();
            try
            {
                setConnectionString();
                con = new SQLiteConnection(sqlConn);
                adp1 = new SQLiteDataAdapter("select * from 'dbo.Win_Machin_Table' where DBId='" + DBId +"'", con);
                adp1.Fill(dsWin_Machin_Data);
                //return dsWin_Machin_Data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            //return dsWin_Machin_Data;
        }

        public static DataTable GetWin_Machin_DataByDBIdList(string DBIdList)
        {
            DataSet dsWin_Machin_Data = new DataSet();
            try
            {
                setConnectionString();
                con = new SQLiteConnection(sqlConn);
                adp1 = new SQLiteDataAdapter("select * from Win_Machin_Table where DBId IN (" + DBIdList + ")", con);
                adp1.Fill(dsWin_Machin_Data);
                return dsWin_Machin_Data.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return dsWin_Machin_Data.Tables[0];
        }

        public static void UpdateDatabase(ref DataSet dsWin_Machin_Data, int DBId)
        {
            try
            {
                //con = new SQLiteConnection(sqlConn);
                //SQLiteDataAdapter adp = new SQLiteDataAdapter("select * from  Win_Machin_Table where DBId =" + DBId.ToString(), con);
                //DataSet dsWin_Machin_Table = new DataSet();
                //adp.Fill(dsWin_Machin_Table);
                //dsWin_Machin_Table.Tables[0].Clear();
                //dsWin_Machin_Table.Tables[0].Merge(dsExcell.Tables[0]);
                //dsWin_Machin_Table.GetChanges();
                //DataRow[] rows = dsWin_Machin_Data.Tables[0].Select("DBId not in( " + DBId.ToString() + ")");

                SQLiteCommandBuilder cb = new SQLiteCommandBuilder(adp1);
                ////if (dsWin_Machin_Data.Tables[0].Columns.Contains("SNo"))
                ////    dsWin_Machin_Data.Tables[0].Columns.Remove("SNo");
                adp1.Update(dsWin_Machin_Data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        public static void DeleteDatabase(string DBIdList)
        {
            try
            {
                setConnectionString();
                SQLiteCommand cmd = new SQLiteCommand("delete from Win_Machin_Table where DBId IN (" + DBIdList + ")");
                con = new SQLiteConnection(sqlConn);
                con.Open();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                cmd.CommandText = "delete from DBList where DBId IN (" + DBIdList + ")";
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        public static void SetInternalDatabase(int DBId)
        {
            try
            {
                setConnectionString();
                SQLiteCommand cmd = new SQLiteCommand("update DBList set IsInternalDatabase = 0");
                con = new SQLiteConnection(sqlConn);
                con.Open();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                cmd.CommandText = "update DBList set IsInternalDatabase = 1 where DBId = " + DBId.ToString();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        public static void SetExternalDatabase(int DBId)
        {
            try
            {
                setConnectionString();
                SQLiteCommand cmd = new SQLiteCommand();
                con = new SQLiteConnection(sqlConn);
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "update DBList set IsInternalDatabase = 0 where DBId = " + DBId.ToString();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        public static void GetSearchPlaneData(ref DataSet dsSearchPlane)
        {
            try
            {
                setConnectionString();
                con = new SQLiteConnection(sqlConn);
                adp2 = new SQLiteDataAdapter("select * from 'dbo.SearchPlan'", con);
                adp2.Fill(dsSearchPlane);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        public static void UpdateSearchPlaneData(ref DataSet dsSearchPlane)
        {
            try
            {
                setConnectionString();
                SQLiteCommandBuilder cb = new SQLiteCommandBuilder(adp2);
                adp2.Update(dsSearchPlane);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        public static DataTable GetSearchNum()
        {
            DataSet dsSearchNumber = new DataSet();
            try
            {
                setConnectionString();
                con = new SQLiteConnection(sqlConn);
                SQLiteDataAdapter adp = new SQLiteDataAdapter("select * from 'dbo.SearchNumbers'", con);
                adp.Fill(dsSearchNumber);
                return dsSearchNumber.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return dsSearchNumber.Tables[0];
        }

        public static void SaveSearchNum(DataTable dtSearchNum)
        {

            try
            {
                setConnectionString();
                con = new SQLiteConnection(sqlConn);
                SQLiteCommand cmd = new SQLiteCommand("delete from SearchNumbers", con);
                con.Open();
                cmd.ExecuteNonQuery();
                foreach (DataRow row in dtSearchNum.Rows)
                {
                    cmd.CommandText = "insert into SearchNumbers(Row,W,M,dValue) values(" + row["Row"].ToString() + ",'" + row["W"].ToString() + "','" + row["M"].ToString() + "','" + row["dValue"].ToString() + "') ";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

        }

        public static string GetDBNameById(int DBId)
        {
            try
            {
                setConnectionString();
                con = new SQLiteConnection(sqlConn);
                con.Open();
                SQLiteCommand oCmd = new SQLiteCommand("select DBName from 'dbo.DBList' where DBId =" + DBId.ToString(), con);
                string DBName = Convert.ToString(oCmd.ExecuteScalar());
                return DBName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        public static void setConnectionString()
        {
            try
            {
                //string strFileName = @"\Connection_String.txt";
                //string strFileName1 = "Connection_String.txt";
                //if (!File.Exists(strFileName))
                //    strFileName = strFileName1;
                //FileStream myFileStream = new FileStream( strFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                //StreamReader rd = new StreamReader(myFileStream);
                //sqlConn = rd.ReadLine();
                //myFileStream.Close();
                //myFileStream.Dispose();
            }
            catch (Exception ex)
            {
            }
        }

        public static DataTable GetCodeList()
        {
            DataSet dsCodeList = new DataSet();
            try
            {
                setConnectionString();
                con = new SQLiteConnection(sqlConn);
                SQLiteDataAdapter adp = new SQLiteDataAdapter("select * from 'dbo.CodeTable'", con);

                adp.Fill(dsCodeList);
                return dsCodeList.Tables[0];

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return dsCodeList.Tables[0];
        }

        public static DataTable GetWin_Machin_Data()
        {
            DataSet dsWin_Machin_Data = new DataSet();
            try
            {
                setConnectionString();
                con = new SQLiteConnection(sqlConn);
                adp1 = new SQLiteDataAdapter("select * from Win_Machin_Table ", con);
                adp1.Fill(dsWin_Machin_Data);
                return dsWin_Machin_Data.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsWin_Machin_Data.Tables[0];
        }

        public static DataTable GetLastYearData(int DBId)
        {
            DataSet dsDBList = new DataSet();
            try
            {
                setConnectionString();
                con = new SQLiteConnection(sqlConn);
                SQLiteDataAdapter adp = new SQLiteDataAdapter(" select * from Win_Machin_Table as T where year(Date) = ( select Max(year(Date)) from Win_Machin_Table where dbid = " + DBId.ToString() + " )and dbid = " + DBId.ToString(), con);
                adp.Fill(dsDBList);
                return dsDBList.Tables[0];

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return dsDBList.Tables[0];
        }


        public static void SaveForCastResult(DataTable dtForcast)
        {
            try
            {
                setConnectionString();
                SQLiteCommand cmd = new SQLiteCommand();
                con = new SQLiteConnection(sqlConn);
                con.Open();
                cmd.Connection = con;
                foreach (DataRow rForcast in dtForcast.Rows)
                {
                    cmd.CommandText = "INSERT INTO ForcastTable (fDate ,DBName ,Numbers,Method)  VALUES ('" + rForcast["fDate"].ToString() + "','" + rForcast["DBName"].ToString() + "','" + rForcast["Numbers"].ToString() + "' ,'" + rForcast["Method"].ToString() + "')";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        public static DataTable GetForCastResult(bool All)
        {
            try
            {
                setConnectionString();
                SQLiteCommand cmd = new SQLiteCommand();
                con = new SQLiteConnection(sqlConn);
                con.Open();
                cmd.Connection = con;
                if (All)
                    cmd.CommandText = "Select * from ForcastTable";
                else
                    cmd.CommandText = "select * from ForcastTable where convert(varchar, fdate, 101) = convert(varchar, getdate(), 101)";
                SQLiteDataAdapter oSqlDataAdapter = new SQLiteDataAdapter(cmd);
                DataSet dsForcast = new DataSet();
                oSqlDataAdapter.Fill(dsForcast);
                return dsForcast.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        public static void DeleteForcastResults(string Ids)
        {
            try
            {
                setConnectionString();
                SQLiteCommand cmd = new SQLiteCommand("delete from ForcastTable where Id IN (" + Ids + ")");
                con = new SQLiteConnection(sqlConn);
                con.Open();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }
    }
}
