using System;
using System.Windows;
using System.Windows.Forms;


namespace WpfPort
{
    /// <summary>
    /// Interaction logic for MDIParent1.xaml
    /// </summary>
    public partial class MDIParent1 : Window
    {
        private int childFormNumber = 0;
        public MDIParent1()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Window childForm = new Window();
            childForm.Owner = this;
            childForm.Content = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void MDIParent1_Load(object sender, EventArgs e)
        {
            //DateTime dtClosing = new DateTime(2009, 4, 25);
            //if (DateTime.Now > dtClosing)
            //{
            //    //MessageBox.Show("Trial period is expired");
            //    this.Close();
            //}
        }

        private void inToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchForm oSearchForm = new SearchForm();
            oSearchForm.Owner = this;
            oSearchForm.Show();
        }


        private void importNewDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewDatabase oNewDatabase = new NewDatabase();
            oNewDatabase.Owner = this;
            oNewDatabase.Show();
        }

        private void listToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExternalDatabaseList oExternalDatabaseList = new ExternalDatabaseList();
            oExternalDatabaseList.Owner = this;
            //oExternalDatabaseList.btnDelete.Visibility = Visibility.Visible;
            oExternalDatabaseList.Show();
        }

        private void editDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                EditDatabase oEditDatabase = new EditDatabase();
                //oEditDatabase.btnLoad.Visible = false;
                oEditDatabase.Owner = this;
                //oEditDatabase.cmbDatabaseList.DataSource = SqlClass.GetExternalDatabaseList();
                oEditDatabase.Show();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void editDatabaseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                EditDatabase oEditDatabase = new EditDatabase();
                //oEditDatabase.cmbDatabaseList.Visible = false;
                //oEditDatabase.btnLoad.Visible = false;
                //oEditDatabase.lbl.Visible = false;
                oEditDatabase.Owner = this;
                //oEditDatabase.btnSetAsExternal.Visible = true;
                //oEditDatabase.cmbDatabaseList.DataSource = SqlClass.GetInternalDatabase();
                oEditDatabase.Show();

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void selectInternalDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExternalDatabaseList oExternalDatabaseList = new ExternalDatabaseList();
            oExternalDatabaseList.Content = "Select  Internal Database";
            //oExternalDatabaseList.btnSetInternalDatabase.Visibility = Visibility.Visible;
            oExternalDatabaseList.Owner = this;
            oExternalDatabaseList.Show();
        }

        private void numberHistorySearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                NumberHistoryForm oNumberHistoryForm = new NumberHistoryForm();
                oNumberHistoryForm.Owner = this;
                oNumberHistoryForm.Show();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void numberTraceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                NumberTrace oNumberTrace = new NumberTrace();
                oNumberTrace.Owner = this;
                oNumberTrace.Show();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void oddEvenSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OddEvenSearch oOddEvenSearch = new OddEvenSearch();
                oOddEvenSearch.Owner = this;
                oOddEvenSearch.Show();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void sumBlindSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SumBlindArithmatic oSumBlindArithmatic = new SumBlindArithmatic();
                oSumBlindArithmatic.Owner = this;
                oSumBlindArithmatic.Show();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void yearPatternSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                YearPattern oYearPattern = new YearPattern();
                oYearPattern.Owner = this;
                oYearPattern.Show();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void keysSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                KeysSearch oKeysSearch = new KeysSearch();
                oKeysSearch.Owner = this;
                oKeysSearch.Show();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void withoutAutocalculationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            HorizontalArithmatic oHorizontalArithmatic = new HorizontalArithmatic();
            oHorizontalArithmatic.Owner = this;
            oHorizontalArithmatic.Show();
        }

        private void withAutocalculationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                HorizontalArithmatic oHorizontalArithmatic = new HorizontalArithmatic();
                oHorizontalArithmatic.Owner = this;
                //oHorizontalArithmatic.lblHeading.Text = "Horizontal Arithmatic With Autocaculation";
                //for (int i = 1; i <= 80; i++)
                //{
                //    (oHorizontalArithmatic.Controls.Find("btnSearch" + i.ToString(), true))[0].Enabled = false;
                //}
                oHorizontalArithmatic.Show();
            }
            catch (Exception ex)
            {
            }

        }

        private void withoutAutocalculationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VerticalArithmatic oVerticalArithmatic = new VerticalArithmatic();
            oVerticalArithmatic.Owner = this;
            oVerticalArithmatic.Show();
        }

        private void withAutocalculationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                VerticalArithmatic oVerticalArithmatic = new VerticalArithmatic();
                oVerticalArithmatic.Owner = this;
                //oVerticalArithmatic.lblHeading.Text = "Vertical Arithmatic With Autocaculation";
                //for (int i = 1; i <= 30; i++)
                //{
                //    (oVerticalArithmatic.Controls.Find("btnSearch" + i.ToString(), true))[0].Enabled = false;
                //}
                oVerticalArithmatic.Show();
            }
            catch (Exception ex)
            {
            }
        }

        private void forcastNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ResultForm oResultForm = new ResultForm();
                oResultForm.Owner = this;
                oResultForm.Show();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
