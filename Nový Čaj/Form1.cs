using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Nový_Čaj
{
    public partial class Form1 : Form
    {
        Čajovač.TeaData teaData;
        public Form1()
        {
            InitializeComponent();
            string[] args = Environment.GetCommandLineArgs();            
            if (args.Length >= 2)
            {
                load(args[1]);
            }                         
        }
        void load(string filename)
        {
            loadFile = filename;
            string teasXML;
            try
            {
                teasXML = File.ReadAllText(filename);
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("Missing file");
                System.Windows.Forms.Application.Exit();
                return;
            }
            System.Xml.Serialization.XmlSerializer xml = new System.Xml.Serialization.XmlSerializer(typeof(Čajovač.TeaData));
            var dsr = new StringReader(teasXML);
            try
            {
                teaData = (Čajovač.TeaData)xml.Deserialize(dsr);
            }
            catch (System.InvalidOperationException)
            {
                MessageBox.Show("File "+filename+" corrupted");
                System.Windows.Forms.Application.Exit();
                return;
            }
            if (teaData != null)
                redraw();            
        }
        string loadFile = "";
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open file";
            theDialog.Filter = "Tea files (*.xml, *.tea)|*.xml;*.tea|All files (*.*)|*.*";            
            if (theDialog.ShowDialog() == DialogResult.OK)
            {                
                load(theDialog.FileName.ToString());
            }
        }
        void redraw()
        {
            seznam.Items.Clear();
            for(int i = 0;i<teaData.data.Count;i++)
            {
                string str;
                str = teaData.data[i].name;
                if (teaData.data[i].poznamka != "" && teaData.data[i].poznamka != null)
                {
                    str += " (" + teaData.data[i].poznamka + ")";
                }
                seznam.Items.Add(str, teaData.data[i].enabled);
            }
            numericUpDown1.Value = teaData.cajuNaSirku;
        }
        private void seznam_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckedListBox sen = (CheckedListBox)(sender);
            int i = sen.SelectedIndex;
            
            textBoxName.Text = teaData.data[i].name;
            textBoxGoal.Text = timeFormatter(teaData.data[i].goal);
            textBoxGoalWater.Text = timeFormatter(teaData.data[i].goalWater);
            textBoxImage.Text = teaData.data[i].imageFile;
            textBoxPozn.Text = teaData.data[i].poznamka;
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            if(seznam.SelectedItem!=null)
            {
                int i = seznam.SelectedIndex;
                if(i>0)
                {
                    Čajovač.TeaDataItem tdi = teaData.data[i - 1];
                    teaData.data[i - 1] = teaData.data[i];
                    teaData.data[i] = tdi;
                    redraw();
                    seznam.SelectedIndex = i - 1;
                }
            }
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            if (seznam.SelectedItem != null)
            {
                int i = seznam.SelectedIndex;
                if (i < seznam.Items.Count-1)
                {
                    Čajovač.TeaDataItem tdi = teaData.data[i + 1];
                    teaData.data[i + 1] = teaData.data[i];
                    teaData.data[i] = tdi;
                    redraw();
                    seznam.SelectedIndex = i + 1;
                }
                
            }
        }

        private void seznam_ItemCheck(object sender, ItemCheckEventArgs e)
        {            
                teaData.data[e.Index].enabled = (e.NewValue==CheckState.Checked);            
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (seznam.SelectedItem != null)
            {
                int i = seznam.SelectedIndex;
                teaData.data.RemoveAt(i);
                redraw();
            }
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (seznam.Items.Count == 1 && teaData.data[0].name == "New")
                seznam.SelectedItem = 0;

            if ((seznam.Items.Count == 1 && teaData.data[0].name == "New" && textBoxName.Text!="") || seznam.SelectedItem != null)
            {
                int g=0;
                int gw=0;
                string[] gs = textBoxGoal.Text.Split(':');
                string[] gws = textBoxGoalWater.Text.Split(':');
                bool gb = false; ;
                bool gwb=false;
                if (gs.Length == 1)
                {
                    gb = Int32.TryParse(gs[0], out g);
                    if(gs[0]=="")
                    {
                        gb = true;
                        g = 0;
                    }
                }
                else if (gs.Length == 2)
                {
                    int gmin = 0, gsec = 0;
                    gb = Int32.TryParse(gs[0], out gmin) && Int32.TryParse(gs[1], out gsec);
                    g = 60 * gmin + gsec;
                }
                if (gws.Length == 1)
                {
                    gwb = Int32.TryParse(gws[0], out gw);
                    if (gws[0] == "")
                    {
                        gwb = true;
                        gw = 0;
                    }
                }
                else if (gws.Length == 2)
                {
                    int gmin = 0, gsec = 0;
                    gwb = Int32.TryParse(gws[0], out gmin) && Int32.TryParse(gws[1], out gsec);
                    gw = 60 * gmin + gsec;
                }
                if(gb&&gwb)
                {
                    int i;
                    if (seznam.SelectedItem == null)
                        i = 0;
                    else
                        i = seznam.SelectedIndex;
                    teaData.data[i].name = textBoxName.Text;
                    teaData.data[i].goal = g;
                    teaData.data[i].goalWater = gw;
                    teaData.data[i].imageFile = textBoxImage.Text;
                    teaData.data[i].poznamka = textBoxPozn.Text;
                    redraw();
                    seznam.SelectedIndex = i;
                }                 
            }
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if(teaData == null)
            {
                teaData = new Čajovač.TeaData();
                teaData.data= new List<Čajovač.TeaDataItem>();
                teaData.data.Add(new Čajovač.TeaDataItem("New", 0, 0, ""));
                redraw();
                buttonApply_Click(null, null);
                return;
            }
            teaData.data.Add(new Čajovač.TeaDataItem("New", 0, 0, ""));
            redraw();
            seznam.SelectedIndex = seznam.Items.Count - 1;
           
            int i = seznam.Items.Count - 1;
            textBoxName.Text = teaData.data[i].name;
            textBoxGoal.Text = timeFormatter(teaData.data[i].goal);
            textBoxGoalWater.Text = timeFormatter(teaData.data[i].goalWater);
            textBoxImage.Text = teaData.data[i].imageFile;
            textBoxPozn.Text = teaData.data[i].poznamka;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if(teaData==null || teaData.data == null || teaData.data.Count==0)
            {
                return;
            }
            if(loadFile!="")
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to save it?", "Save?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    save(loadFile);
                }
            }
            else
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Tea file *.tea|*.tea|XML file *.xml|*.xml|All files (*.*)|*.*";  
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    save(saveFileDialog1.FileName.ToString());
                }

            }
        }
        string timeFormatter(int sec)
        {
            TimeSpan ts = new TimeSpan(0, 0, sec);
            return ts.ToString("m\\:ss");
        }
        void save(string filename)
        {                         
             TextWriter tw = new StreamWriter(filename);
             System.Xml.Serialization.XmlSerializer xml = new System.Xml.Serialization.XmlSerializer(typeof(Čajovač.TeaData));   
             xml.Serialize(tw, teaData);
             tw.Close();             
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (teaData == null)
            {
                teaData = new Čajovač.TeaData();
                teaData.data = new List<Čajovač.TeaDataItem>();
            }
            teaData.cajuNaSirku = (int)(numericUpDown1.Value);
        }
    }
}
