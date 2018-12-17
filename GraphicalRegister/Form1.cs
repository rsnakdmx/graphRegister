using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;

namespace GraphicalRegister
{
    public partial class frmRegView : Form
    {
        private string[][] regEntries;

        public frmRegView()
        {
            InitializeComponent();
        }

        private void iniciarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string regAddr = "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\UserAssist\\{F4E57C4B-2036-45F0-A9AB-443BCFE33D9F}\\Count";
            regEntries = new Registro(@regAddr).getRegistryContent();
            regEntries = regEntries.OrderBy(inner => inner[3]).ToArray();

            lstVw.View = View.Details;
            lstVw.Columns.Add("Path key", 240);
            lstVw.Columns.Add("Encrypted key", 300);
            lstVw.Columns.Add("Decrypted key", 300);
            lstVw.Columns.Add("Counter", 50);
            lstVw.Columns.Add("Last execution", 150);


            //MessageBox.Show(regEntries.Length.ToString());
            for (int i = 0; i < regEntries.Length; i++)
            {
                ListViewItem item = new ListViewItem(regEntries[0][0]);

                for (int j = 1; j < regEntries[0].Length; j++)
                    item.SubItems.Add(regEntries[i][j]);

                lstVw.Items.Add(item);
            }
        }
    }
}
