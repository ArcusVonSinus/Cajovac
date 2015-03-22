using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Media;
using System.IO;
using System.Xml;

namespace Čajovač
{
    //TTEESSTT
    public partial class Form1 : Form
    {
        /*
         * //Times of teas
        int goalOolong = 2 * 60;        
        int goalGen = 1 * 60 + 30;        
        int goalRooibos = 5 * 60;        
        int goalVR = 2*60+30;
        int goalDS = 2*60+30;
        int goalMate = 4*60;
        //Water---       
        int goalWaterOolong = 10 * 60 + 21;
        int goalWaterGen = 10 * 60 + 21;
        int goalWaterRooibos = 0;
        int goalWaterVR = 29*60+8;
        int goalWaterDS = 29*60+8;
        int goalWaterMate = 22*60+41;
        //WaterFromBoil
        int goalWaterOolong = 6 * 60 + 43;
        int goalWaterGen = 6 * 60 + 43;
        int goalWaterRooibos = 0;
        int goalWaterVR = 25 * 60 + 30;
        int goalWaterDS = 25 * 60 + 30;
        int goalWaterMate = 18 * 60 + 41;
        */
        SoundPlayer gongSound;
        private System.Windows.Forms.ContextMenu contextMenu;
        private System.Windows.Forms.MenuItem menuItemExit;
        private Tea[] teas;
        public int cajuNaSirku = 3;
        public Form1()
        {
            InitializeComponent();
            loadTeas();

            this.ClientSize = new System.Drawing.Size(12 + (256+12)*cajuNaSirku, 12 + (256+12)*(((teas.Length-1) / cajuNaSirku)+1));
           // this.MaximumSize = new System.Drawing.Size(8+12 + cajuNaSirku, 12 + teas.Length / cajuNaSirku);
           // this.MinimumSize = new System.Drawing.Size(8+12 + cajuNaSirku, 12 + teas.Length / cajuNaSirku);
            this.contextMenu = new System.Windows.Forms.ContextMenu();
            this.menuItemExit = new System.Windows.Forms.MenuItem();
            System.Windows.Forms.MenuItem[] polozkyMenu = new System.Windows.Forms.MenuItem[2*teas.Length+1];
            for(int i = 0;i<teas.Length;i++)
            {
                polozkyMenu[2*i]=teas[i].menuItem;
                polozkyMenu[2*i+1]=teas[i].menuItemWater;
                teas[i].menuItem.Index = 2 * i;
                teas[i].menuItemWater.Index = 2 * i + 1;                
            };
            polozkyMenu[polozkyMenu.Length - 1] = menuItemExit;
            this.contextMenu.MenuItems.AddRange(polozkyMenu);
            this.menuItemExit.Index = polozkyMenu.Length - 1;
            this.menuItemExit.Text = "E&xit";
            this.menuItemExit.Click += new System.EventHandler(quitMenuItem_Click);
            notifyIcon.ContextMenu = this.contextMenu;
            gongSound = new SoundPlayer(@"gong.wav");           
        }

        void loadTeas()
        {
            TeaData teaData = new TeaData();
            /* teaData.data = new List<TeaDataItem>();
             teaData.data.Add(new TeaDataItem("test", 150, 223, "1.jpg"));
             teaData.data.Add(new TeaDataItem("test", 150, 223, "1.jpg"));
             teaData.data.Add(new TeaDataItem("test", 150, 223, "1.jpg"));
             teaData.data.Add(new TeaDataItem("test", 150, 223, "ds1.jpg"));

             teaData.data.Add(new TeaDataItem("test", 150, 223, "1.jpg"));
             teaData.data.Add(new TeaDataItem("test", 150, 223, "1.jpg"));
             teaData.data.Add(new TeaDataItem("test", 150, 223, "1.jpg"));
             teaData.data.Add(new TeaDataItem("test", 150, 223, "aasfe.aef"));

             //TextWriter tw = new StringWriter();
             TextWriter tw = new StreamWriter("save.xml");
             System.Xml.Serialization.XmlSerializer xml = new System.Xml.Serialization.XmlSerializer(typeof(Čajovač.TeaData));   
             xml.Serialize(tw, teaData);
 //            System.IO.Directory.CreateDirectory("Save");
             //File.WriteAllText(tw);
             //File.WriteAllBytes(@"Results\sv.xml", );
             tw.Close();
             */
            string teasXML;
            try
            {
                teasXML = File.ReadAllText("teas.xml");
            }
            catch(System.IO.FileNotFoundException)
            {
                MessageBox.Show("Missing file \"teas.xml\"");
                quitMenuItem_Click(null,null);
                System.Windows.Forms.Application.Exit();
                teas = new Tea[0];
                return;
            }
            System.Xml.Serialization.XmlSerializer xml = new System.Xml.Serialization.XmlSerializer(typeof(Čajovač.TeaData));
            var dsr = new StringReader(teasXML);
            try
            {
                teaData = (TeaData)xml.Deserialize(dsr);
            }
            catch (System.InvalidOperationException)
            {
                MessageBox.Show("File \"teas.xml\" corrupted");
                quitMenuItem_Click(null, null);
                System.Windows.Forms.Application.Exit();
                teas = new Tea[0];
                return;
            }
            teas = new Tea[teaData.data.Count];           
            for(int i = 0;i<teaData.data.Count;i++)
            {
                teas[i]= new Tea(this,teaData.data[i],i);
            }
        }


        public void button_Click(object sender, EventArgs e)
        {
            int id = 0;
            Button button = sender as Button;
            PictureBox tsi = sender as PictureBox;
            if (button != null)
                id = (int)(button.Tag);
            if (tsi != null)
                id = (int)(tsi.Tag);
            Tea tea = teas[id];
            if(tea.timingWater)
            {
                tea.timingWater = false;
                tea.running = false;
                tea.timer.Stop();
            }
            if(!tea.running)
            {
                tea.running = true;
                tea.timeStart = DateTime.Now;
                tea.timer.Start();
            }
            else
            {
                tea.running = false;
                tea.timer.Stop();
                var timeSinceStartTimeTemp = DateTime.Now - tea.timeStart;                                                        
                TimeSpan timeSinceStartTime = new TimeSpan(timeSinceStartTimeTemp.Hours, timeSinceStartTimeTemp.Minutes, timeSinceStartTimeTemp.Seconds + tea.elapsed);
                tea.elapsed = (int)(timeSinceStartTime.TotalSeconds);                
            }           
        }
        public void buttonWater_Click(object sender, EventArgs e)
        {
            int id = 0;
            Button button = sender as Button;
            PictureBox tsi = sender as PictureBox;
            if (button != null)
                id = (int)(button.Tag);
            if (tsi != null)
                id = (int)(tsi.Tag);
            Tea tea = teas[id];
            if (!tea.timingWater)
            {
                tea.timingWater = true;
                tea.running = false;
                tea.timer.Stop();
            }
            if (!tea.running)
            {
                tea.running = true;
                tea.timeStart = DateTime.Now;
                tea.timer.Start();
            }
            else
            {
                tea.running = false;
                tea.timer.Stop();
                var timeSinceStartTimeTemp = DateTime.Now - tea.timeStart;
                TimeSpan timeSinceStartTime = new TimeSpan(timeSinceStartTimeTemp.Hours, timeSinceStartTimeTemp.Minutes, timeSinceStartTimeTemp.Seconds + tea.elapsed);
                tea.elapsed = (int)(timeSinceStartTime.TotalSeconds);
            }
        }
        public void buttonReset_Click(object sender, EventArgs e)
        {
            int id = 0;
            Button button = sender as Button;
            PictureBox tsi = sender as PictureBox;
            if (button != null)
                id = (int)(button.Tag);
            if (tsi != null)
                id = (int)(tsi.Tag);
            Tea tea = teas[id];

            tea.timer.Stop();
            tea.running = false;
            tea.labelMin.Text = (tea.goal / 60).ToString();
            tea.labelSec.Text = twoDigits(tea.goal % 60);
            tea.elapsed = 0;
        }
        public void timer_Tick(object sender, EventArgs e)
        {
            Timer b = (Timer)(sender);
            Tea tea = teas[(int)(b.Tag)];

            var timeSinceStartTimeTemp = DateTime.Now - tea.timeStart;
            TimeSpan timeSinceStartTime = new TimeSpan(timeSinceStartTimeTemp.Hours,
                                              timeSinceStartTimeTemp.Minutes,
                                              timeSinceStartTimeTemp.Seconds + tea.elapsed);
            TimeSpan goal;
            if (tea.timingWater)
            {
                goal = new TimeSpan(0, 0, tea.goalWater);
            }
            else 
            {
                goal = new TimeSpan(0, 0, tea.goal);
            }
            TimeSpan remainingTime = goal - timeSinceStartTime; PBRunning=true; 
            changeProgressBar(goal.TotalSeconds,remainingTime.TotalSeconds);
            if (remainingTime.TotalSeconds > 0)
            {
                tea.labelMin.Text = remainingTime.Minutes.ToString();
                tea.labelSec.Text = twoDigits(remainingTime.Seconds);
                notifyIcon.Text = remainingTime.Minutes.ToString() + ":" + twoDigits(remainingTime.Seconds);
            }
            else
            {
                tea.timer.Stop();
                tea.labelMin.Text = "0";
                tea.labelSec.Text = "00";
                notifyIcon.Text = "Čajovač";
                gong();
            }
        }



        bool PBRunning;
        bool PBPaused;
        void changeProgressBar(double PBGoal,double PBLeft)
        {
            if(PBRunning)
            {
                windows7ProgressBar.State = wyDay.Controls.ProgressBarState.Normal;
                windows7ProgressBar.ShowInTaskbar = true;
            }
            else if (PBPaused)
            {
                windows7ProgressBar.State = wyDay.Controls.ProgressBarState.Pause;
                windows7ProgressBar.ShowInTaskbar = true;
            }
            else
            {
                windows7ProgressBar.ShowInTaskbar = false;
            }
            if (PBGoal == 0)
                PBGoal = 1;
            windows7ProgressBar.Value =(int) (100*((PBGoal-PBLeft)/PBGoal));
        }

       



        void gong()
        {
            try { gongSound.Play(); }
            catch { }

        }
        public string twoDigits(int n)
        {
            if(n<10)
                return "0" + n.ToString();
            else 
                return n.ToString();
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                notifyIcon.Visible = true;
                this.ShowInTaskbar = false;
            }
            else
            {
                notifyIcon.Visible = false;
                this.ShowInTaskbar = true;
            }
        }
        private void quitMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        



    }
}
