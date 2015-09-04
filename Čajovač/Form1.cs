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
        //Font
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);
        private PrivateFontCollection fonts = new PrivateFontCollection();    
        public Font myFont;   
             
        SoundPlayer gongSound;
        private System.Windows.Forms.ContextMenu contextMenu;
        private System.Windows.Forms.MenuItem menuItemExit;
        public Tea[] teas;
        public int cajuNaSirku = 3;
        public Form1()
        {
            InitializeComponent();

            //FONT - Zenzai Itacha
            byte[] fontData = Properties.Resources.ZenzaiItacha;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Properties.Resources.ZenzaiItacha.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.ZenzaiItacha.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);
            myFont = new Font(fonts.Families[0], 30.0F);
            //this.ClientSize = new System.Drawing.Size(1,1);

            loadTeas();

            this.Width = 9000;
            this.Height = 3000;
            this.ClientSize = new System.Drawing.Size(12 + (256 + 12) * cajuNaSirku, 12 + (256 + 12) * (((teas.Length - 1) / cajuNaSirku) + 1));
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
            }
            polozkyMenu[polozkyMenu.Length - 1] = menuItemExit;
            this.contextMenu.MenuItems.AddRange(polozkyMenu);
            this.menuItemExit.Index = polozkyMenu.Length - 1;
            this.menuItemExit.Text = "E&xit";
            this.menuItemExit.Click += new System.EventHandler(quitMenuItem_Click);
            notifyIcon.ContextMenu = this.contextMenu;
            gongSound = new SoundPlayer(Properties.Resources.gong);           
        }

        void loadTeas()
        {
            TeaData teaData = new TeaData();
            string teasXML;
            try
            {
                teasXML = File.ReadAllText("teas.tea");
            }
            catch(System.IO.FileNotFoundException)
            {
                try
                {
                    teasXML = File.ReadAllText("teas.xml");
                }
                catch (System.IO.FileNotFoundException)
                {
                    MessageBox.Show("Missing file \"teas.tea\", sample file created");
                    File.WriteAllText("teas.tea", Properties.Resources.DefaultTeas);
                    if(!Directory.Exists("Images"))
                    {
                        Directory.CreateDirectory("Images");
                    }
                    if (!File.Exists(@"Images\1.jpg"))
                    {
                        Properties.Resources.Sample.Save("Images\\1.jpg");
                    }
                    if (!File.Exists(@"Images\2.jpg"))
                    {
                        Properties.Resources.Sample.Save("Images\\2.jpg");
                    }
                    teasXML = Properties.Resources.DefaultTeas;
                }
            }
            System.Xml.Serialization.XmlSerializer xml = new System.Xml.Serialization.XmlSerializer(typeof(Čajovač.TeaData));
            var dsr = new StringReader(teasXML);
            try
            {
                teaData = (TeaData)xml.Deserialize(dsr);
            }
            catch (System.InvalidOperationException)
            {
                MessageBox.Show("File \"teas.xml\" corrupted, delete it to create sample file.");
                quitMenuItem_Click(null, null);
                System.Windows.Forms.Application.Exit();
                teas = new Tea[0];
                return;
            }
            cajuNaSirku = teaData.cajuNaSirku;
            this.ClientSize = new System.Drawing.Size(12 + (256 + 12) * cajuNaSirku, 589);
            teas = new Tea[teaData.numberOfEnabled];
            int j = 0;
            for(int i = 0;i<teaData.data.Count;i++)
            {
                if (teaData.data[i].enabled)
                {
                    teas[j] = new Tea(this, teaData.data[i], j);
                    j++;
                }
            }
            

        }


        public void button_Click(object sender, EventArgs e)
        {
            int id = 0;
            Button button = sender as Button;
            PictureBox tsi = sender as PictureBox;
            MenuItem mi = sender as MenuItem;
            if (button != null)
                id = (int)(button.Tag);
            if (tsi != null)
                id = (int)(tsi.Tag);
            if (mi != null)
                id = (int)(mi.Tag);

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
                PBRunning = false;
                PBPaused = true;
                changeProgressBar(0,0);
            }           
        }
        public void buttonWater_Click(object sender, EventArgs e)
        {
            int id = 0;
            Button button = sender as Button;
            PictureBox tsi = sender as PictureBox;
            MenuItem mi = sender as MenuItem;
            if (button != null)
                id = (int)(button.Tag);
            if (tsi != null)
                id = (int)(tsi.Tag);
            if (mi != null)
                id = (int)(mi.Tag);

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
                PBRunning = false;
                PBPaused = true;
                changeProgressBar(0,0);
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
            tea.timingWater = false;      
            tea.labelMin.Text = (tea.goal / 60).ToString();
            tea.labelSec.Text = twoDigits(tea.goal % 60);
            tea.elapsed = 0;
            PBRunning = false;
            PBPaused = false;
            changeProgressBar(0, 0);

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
            TimeSpan remainingTime = goal - timeSinceStartTime; 
            PBRunning=true;
            PBPaused = false;
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
        int nulaAzSto(double n)
        {
            if (n >= 100)
                n = 100;
            else if (n < 0)
                n = 0;
            return (int)(n);
        }
        void changeProgressBar(double PBGoal,double PBLeft)
        {
            if(PBRunning)
            {
                windows7ProgressBar.State = wyDay.Controls.ProgressBarState.Normal;
                windows7ProgressBar.ShowInTaskbar = true;
                if(PBGoal!=0)
                    windows7ProgressBar.Value =nulaAzSto(100*((PBGoal-PBLeft)/PBGoal));
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

        int widthForChange;
        int heightForChange;
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (teas != null)
            {
                if(widthForChange == 0)
                {
                    widthForChange = this.ClientSize.Width;
                }
                if(heightForChange == 0)
                {
                    heightForChange = this.ClientSize.Height;
                }
                if (Math.Abs(this.ClientSize.Width - widthForChange) > 100)
                {
                    widthForChange = this.ClientSize.Width;
                    heightForChange = this.ClientSize.Height;
                    foreach (Tea t in teas)
                    {
                        t.reposition();
                    }
                }
            }

        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            foreach (Tea t in teas)
            {
                t.reposition();
            }
        }
    }
}
