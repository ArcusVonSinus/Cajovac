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

namespace Čajovač
{
    //TEST
    public partial class Form1 : Form
    {
        //Times of teas
        int goalOolong = 2 * 60;        
        int goalGen = 1 * 60 + 30;        
        int goalRooibos = 5 * 60;        
        int goalVR = 2*60+30;
        int goalDS = 2*60+30;
        int goalMate = 4*60;
        //Water---       
        /*int goalWaterOolong = 10 * 60 + 21;
        int goalWaterGen = 10 * 60 + 21;
        int goalWaterRooibos = 0;
        int goalWaterVR = 29*60+8;
        int goalWaterDS = 29*60+8;
        int goalWaterMate = 22*60+41;*/
        //WaterFromBoil
        int goalWaterOolong = 6 * 60 + 43;
        int goalWaterGen = 6 * 60 + 43;
        int goalWaterRooibos = 0;
        int goalWaterVR = 25 * 60 + 30;
        int goalWaterDS = 25 * 60 + 30;
        int goalWaterMate = 18 * 60 + 41;
        //NewTea
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //Oolong------------------------
        DateTime timeStartOolong;
        bool runningOolong = false;
        int elapsedOolong;
        bool oolongTimingWater = false;
        private System.Windows.Forms.MenuItem menuItemOolong;
        private System.Windows.Forms.MenuItem menuItemOolongWater;
        //Gen------------------------
        DateTime timeStartGen;
        bool runningGen = false;
        int elapsedGen;
        bool genTimingWater = false;
        private System.Windows.Forms.MenuItem menuItemGen;
        private System.Windows.Forms.MenuItem menuItemGenWater;
        //Rooibos------------------------
        DateTime timeStartRooibos;
        bool runningRooibos = false;
        int elapsedRooibos;
        bool rooibosTimingWater = false;
        private System.Windows.Forms.MenuItem menuItemRooibos;
        private System.Windows.Forms.MenuItem menuItemRooibosWater;
        //VR------------------------
        DateTime timeStartVR;
        bool runningVR = false;
        int elapsedVR;
        bool VRTimingWater = false;
        private System.Windows.Forms.MenuItem menuItemVR;
        private System.Windows.Forms.MenuItem menuItemVRWater;
        //DS------------------------
        DateTime timeStartDS;
        bool runningDS = false;
        int elapsedDS;
        bool DSTimingWater = false;
        private System.Windows.Forms.MenuItem menuItemDS;
        private System.Windows.Forms.MenuItem menuItemDSWater;
        //Mate------------------------
        DateTime timeStartMate;
        bool runningMate = false;
        int elapsedMate;
        bool MateTimingWater = false;
        private System.Windows.Forms.MenuItem menuItemMate;
        private System.Windows.Forms.MenuItem menuItemMateWater;
        //NewTea
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        SoundPlayer gongSound;
        private System.Windows.Forms.ContextMenu contextMenu1;
        private System.Windows.Forms.MenuItem menuItemExit;
        public Form1()
        {
            InitializeComponent();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();

            this.menuItemOolong = new System.Windows.Forms.MenuItem();
            this.menuItemOolongWater = new System.Windows.Forms.MenuItem();
            this.menuItemGen = new System.Windows.Forms.MenuItem();
            this.menuItemGenWater = new System.Windows.Forms.MenuItem();
            this.menuItemRooibos = new System.Windows.Forms.MenuItem();
            this.menuItemRooibosWater = new System.Windows.Forms.MenuItem();
            this.menuItemVR = new System.Windows.Forms.MenuItem();
            this.menuItemVRWater = new System.Windows.Forms.MenuItem();
            this.menuItemDS = new System.Windows.Forms.MenuItem();
            this.menuItemDSWater = new System.Windows.Forms.MenuItem();
            this.menuItemMate = new System.Windows.Forms.MenuItem();
            this.menuItemMateWater = new System.Windows.Forms.MenuItem();
            //NewTea
            this.menuItemExit = new System.Windows.Forms.MenuItem();
            System.Windows.Forms.MenuItem[] polozkyMenu = new System.Windows.Forms.MenuItem[] { menuItemOolong, menuItemOolongWater, menuItemGen,menuItemGenWater, menuItemExit ,menuItemRooibos,menuItemRooibosWater,menuItemVR,menuItemVRWater,menuItemDS,menuItemDSWater,menuItemMate,menuItemMateWater/*NewTea*/};                        
            this.contextMenu1.MenuItems.AddRange(polozkyMenu);
            this.menuItemExit.Index = polozkyMenu.Length - 1;
            this.menuItemExit.Text = "E&xit";
            this.menuItemExit.Click += new System.EventHandler(quitMenuItem_Click);

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            //Oolong ----------------------------------------------
            this.menuItemOolong.Index = 0;
            this.menuItemOolong.Text = "O&olong";
            this.menuItemOolong.Click += new System.EventHandler(ButtonOolong_Click);

            this.menuItemOolongWater.Index = 1;
            this.menuItemOolongWater.Text = "O&olong water";
            this.menuItemOolongWater.Click += new System.EventHandler(buttonWaterOolong_Click);
            //Gen-------------------------------------------------------
            this.menuItemGen.Index = 2;
            this.menuItemGen.Text = "Genmaicha";
            this.menuItemGen.Click += new System.EventHandler(ButtonGen_Click);
                         
            this.menuItemGenWater.Index = 3;
            this.menuItemGenWater.Text = "Genmaicha water";
            this.menuItemGenWater.Click += new System.EventHandler(buttonWaterGen_Click);
            //Rooibos-------------------------------------------------------
            this.menuItemRooibos.Index = 4;
            this.menuItemRooibos.Text = "Rooibos";
            this.menuItemRooibos.Click += new System.EventHandler(ButtonRooibos_Click);
                         
            this.menuItemRooibosWater.Index = 5;
            this.menuItemRooibosWater.Text = "Rooibos water";
            this.menuItemRooibosWater.Click += new System.EventHandler(buttonWaterRooibos_Click);
            //VR-------------------------------------------------------
            this.menuItemVR.Index = 6;
            this.menuItemVR.Text = "Vzácná rosa";
            this.menuItemVR.Click += new System.EventHandler(ButtonVR_Click);
                         
            this.menuItemVRWater.Index = 7;
            this.menuItemVRWater.Text = "Vzácná rosa";
            this.menuItemVRWater.Click += new System.EventHandler(buttonWaterVR_Click);
            //DS-------------------------------------------------------
            this.menuItemDS.Index = 8;
            this.menuItemDS.Text = "Dračí studna";
            this.menuItemDS.Click += new System.EventHandler(ButtonDS_Click);
                         
            this.menuItemDSWater.Index = 9;
            this.menuItemDSWater.Text = "Dračí studna water";
            this.menuItemDSWater.Click += new System.EventHandler(buttonWaterDS_Click);
            //Mate-------------------------------------------------------
            this.menuItemMate.Index = 10;
            this.menuItemMate.Text = "Mate IQ";
            this.menuItemMate.Click += new System.EventHandler(ButtonMate_Click);
                         
            this.menuItemMateWater.Index = 11;
            this.menuItemMateWater.Text = "Mate IQ water";
            this.menuItemMateWater.Click += new System.EventHandler(buttonWaterMate_Click);

            //NewTea

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            notifyIcon.ContextMenu = this.contextMenu1;
            gongSound = new SoundPlayer(@"gong.wav");

            wyDay.Controls.Windows7ProgressBar windows7ProgressBar = new wyDay.Controls.Windows7ProgressBar();
            windows7ProgressBar.Value = 50;
        }
        
        private void ButtonOolong_Click(object sender, EventArgs e)
        {
            if(oolongTimingWater)
            {
                oolongTimingWater = false;
                runningOolong = false;
                timerOolong.Stop();
            }
            if(!runningOolong)
            {
                runningOolong = true;
                timeStartOolong = DateTime.Now;
                timerOolong.Start();
            }
            else
            {
                runningOolong = false;
                timerOolong.Stop();
                var timeSinceStartTimeTemp = DateTime.Now - timeStartOolong;                                                        
                TimeSpan timeSinceStartTime = new TimeSpan(timeSinceStartTimeTemp.Hours, timeSinceStartTimeTemp.Minutes, timeSinceStartTimeTemp.Seconds + elapsedOolong);
                elapsedOolong = (int)(timeSinceStartTime.TotalSeconds);                
            }
        }
        private void buttonWaterOolong_Click(object sender, EventArgs e)
        {
            if(!oolongTimingWater)
            {
                oolongTimingWater = true;
                runningOolong = false;
                timerOolong.Stop();
            }
            if (!runningOolong)
            {
                runningOolong = true;
                timeStartOolong = DateTime.Now;
                timerOolong.Start();
            }
            else
            {
                runningOolong = false;
                timerOolong.Stop();
                var timeSinceStartTimeTemp = DateTime.Now - timeStartOolong;
                TimeSpan timeSinceStartTime = new TimeSpan(timeSinceStartTimeTemp.Hours, timeSinceStartTimeTemp.Minutes, timeSinceStartTimeTemp.Seconds + elapsedOolong);
                elapsedOolong = (int)(timeSinceStartTime.TotalSeconds);
            }
        }        
        private void buttonResetOolong_Click(object sender, EventArgs e)
        {
            timerOolong.Stop();
            runningOolong = false;
            labelOolongMin.Text = (goalOolong / 60).ToString();
            labelOolongSec.Text = twoDigits(goalOolong % 60);
            elapsedOolong = 0;
        }
        private void timerOolong_Tick(object sender, EventArgs e)
        {
            var timeSinceStartTimeTemp = DateTime.Now - timeStartOolong;
            TimeSpan timeSinceStartTime = new TimeSpan(timeSinceStartTimeTemp.Hours,
                                              timeSinceStartTimeTemp.Minutes,
                                              timeSinceStartTimeTemp.Seconds + elapsedOolong);
            TimeSpan goal;
            if (oolongTimingWater)
            {
                goal = new TimeSpan(0, 0, goalWaterOolong);
            }
            else 
            {
                goal = new TimeSpan(0, 0, goalOolong);
            }
            TimeSpan remainingTime = goal - timeSinceStartTime;
            if (remainingTime.TotalSeconds > 0)
            {
                labelOolongMin.Text = remainingTime.Minutes.ToString();
                labelOolongSec.Text = twoDigits(remainingTime.Seconds);
                notifyIcon.Text = remainingTime.Minutes.ToString() + ":" + twoDigits(remainingTime.Seconds);
            }
            else
            {
                timerOolong.Stop();
                labelOolongMin.Text = "0";
                labelOolongSec.Text = "00";
                notifyIcon.Text = "Čajovač";
                gong();
            }

        }

        private void ButtonGen_Click(object sender, EventArgs e)
        {
            if (genTimingWater)
            {
                genTimingWater = false;
                runningGen = false;
                timerGen.Stop();
            }
            if (!runningGen)
            {
                runningGen = true;
                timeStartGen = DateTime.Now;
                timerGen.Start();
            }
            else
            {
                runningGen = false;
                timerGen.Stop();
                var timeSinceStartTimeTemp = DateTime.Now - timeStartGen;
                TimeSpan timeSinceStartTime = new TimeSpan(timeSinceStartTimeTemp.Hours, timeSinceStartTimeTemp.Minutes, timeSinceStartTimeTemp.Seconds + elapsedGen);
                elapsedGen = (int)(timeSinceStartTime.TotalSeconds);
            }
        }
        private void buttonWaterGen_Click(object sender, EventArgs e)
        {
            if (!genTimingWater)
            {
                genTimingWater = true;
                runningGen = false;
                timerGen.Stop();
            }
            if (!runningGen)
            {
                runningGen = true;
                timeStartGen = DateTime.Now;
                timerGen.Start();
            }
            else
            {
                runningGen = false;
                timerGen.Stop();
                var timeSinceStartTimeTemp = DateTime.Now - timeStartGen;
                TimeSpan timeSinceStartTime = new TimeSpan(timeSinceStartTimeTemp.Hours, timeSinceStartTimeTemp.Minutes, timeSinceStartTimeTemp.Seconds + elapsedGen);
                elapsedGen = (int)(timeSinceStartTime.TotalSeconds);
            }
        }
        private void buttonResetGen_Click(object sender, EventArgs e)
        {
            timerGen.Stop();
            runningGen = false;
            labelGenMin.Text = (goalGen / 60).ToString();
            labelGenSec.Text = twoDigits(goalGen % 60);
            elapsedGen = 0;
        }
        private void timerGen_Tick(object sender, EventArgs e)
        {
            var timeSinceStartTimeTemp = DateTime.Now - timeStartGen;
            TimeSpan timeSinceStartTime = new TimeSpan(timeSinceStartTimeTemp.Hours,
                                              timeSinceStartTimeTemp.Minutes,
                                              timeSinceStartTimeTemp.Seconds + elapsedGen);
            TimeSpan goal;
            if (genTimingWater)
            {
                goal = new TimeSpan(0, 0, goalWaterGen);
            }
            else
            {
                goal = new TimeSpan(0, 0, goalGen);
            }
            TimeSpan remainingTime = goal - timeSinceStartTime;
            if (remainingTime.TotalSeconds > 0)
            {
                labelGenMin.Text = remainingTime.Minutes.ToString();
                labelGenSec.Text = twoDigits(remainingTime.Seconds);
                notifyIcon.Text = remainingTime.Minutes.ToString() + ":" + twoDigits(remainingTime.Seconds);
            }
            else
            {
                timerGen.Stop();
                labelGenMin.Text = "0";
                labelGenSec.Text = "00";
                notifyIcon.Text = "Čajovač";
                gong();
            }

        }

        private void ButtonRooibos_Click(object sender, EventArgs e)
        {
            if (rooibosTimingWater)
            {
                rooibosTimingWater = false;
                runningRooibos = false;
                timerRooibos.Stop();
            }
            if (!runningRooibos)
            {
                runningRooibos = true;
                timeStartRooibos = DateTime.Now;
                timerRooibos.Start();
            }
            else
            {
                runningRooibos = false;
                timerRooibos.Stop();
                var timeSinceStartTimeTemp = DateTime.Now - timeStartRooibos;
                TimeSpan timeSinceStartTime = new TimeSpan(timeSinceStartTimeTemp.Hours, timeSinceStartTimeTemp.Minutes, timeSinceStartTimeTemp.Seconds + elapsedRooibos);
                elapsedRooibos = (int)(timeSinceStartTime.TotalSeconds);
            }
        }
        private void buttonWaterRooibos_Click(object sender, EventArgs e)
        {
            if (!rooibosTimingWater)
            {
                rooibosTimingWater = true;
                runningRooibos = false;
                timerRooibos.Stop();
            }
            if (!runningRooibos)
            {
                runningRooibos = true;
                timeStartRooibos = DateTime.Now;
                timerRooibos.Start();
            }
            else
            {
                runningRooibos = false;
                timerRooibos.Stop();
                var timeSinceStartTimeTemp = DateTime.Now - timeStartRooibos;
                TimeSpan timeSinceStartTime = new TimeSpan(timeSinceStartTimeTemp.Hours, timeSinceStartTimeTemp.Minutes, timeSinceStartTimeTemp.Seconds + elapsedRooibos);
                elapsedRooibos = (int)(timeSinceStartTime.TotalSeconds);
            }
        }
        private void buttonResetRooibos_Click(object sender, EventArgs e)
        {
            timerRooibos.Stop();
            runningRooibos = false;
            labelRooibosMin.Text = (goalRooibos / 60).ToString();
            labelRooibosSec.Text = twoDigits(goalRooibos % 60);
            elapsedRooibos = 0;
        }
        private void timerRooibos_Tick(object sender, EventArgs e)
        {
            var timeSinceStartTimeTemp = DateTime.Now - timeStartRooibos;
            TimeSpan timeSinceStartTime = new TimeSpan(timeSinceStartTimeTemp.Hours,
                                              timeSinceStartTimeTemp.Minutes,
                                              timeSinceStartTimeTemp.Seconds + elapsedRooibos);
            TimeSpan goal;
            if (rooibosTimingWater)
            {
                goal = new TimeSpan(0, 0, goalWaterRooibos);
            }
            else
            {
                goal = new TimeSpan(0, 0, goalRooibos);
            }
            TimeSpan remainingTime = goal - timeSinceStartTime;
            if (remainingTime.TotalSeconds > 0)
            {
                labelRooibosMin.Text = remainingTime.Minutes.ToString();
                labelRooibosSec.Text = twoDigits(remainingTime.Seconds);
                notifyIcon.Text = remainingTime.Minutes.ToString() + ":" + twoDigits(remainingTime.Seconds);
            }
            else
            {
                timerRooibos.Stop();
                labelRooibosMin.Text = "0";
                labelRooibosSec.Text = "00";
                notifyIcon.Text = "Čajovač";
                gong();
            }

        }

        private void ButtonVR_Click(object sender, EventArgs e)
        {
            if (VRTimingWater)
            {
                VRTimingWater = false;
                runningVR = false;
                timerVR.Stop();
            }
            if (!runningVR)
            {
                runningVR = true;
                timeStartVR = DateTime.Now;
                timerVR.Start();
            }
            else
            {
                runningVR = false;
                timerVR.Stop();
                var timeSinceStartTimeTemp = DateTime.Now - timeStartVR;
                TimeSpan timeSinceStartTime = new TimeSpan(timeSinceStartTimeTemp.Hours, timeSinceStartTimeTemp.Minutes, timeSinceStartTimeTemp.Seconds + elapsedVR);
                elapsedVR = (int)(timeSinceStartTime.TotalSeconds);
            }
        }
        private void buttonWaterVR_Click(object sender, EventArgs e)
        {
            if (!VRTimingWater)
            {
                VRTimingWater = true;
                runningVR = false;
                timerVR.Stop();
            }
            if (!runningVR)
            {
                runningVR = true;
                timeStartVR = DateTime.Now;
                timerVR.Start();
            }
            else
            {
                runningVR = false;
                timerVR.Stop();
                var timeSinceStartTimeTemp = DateTime.Now - timeStartVR;
                TimeSpan timeSinceStartTime = new TimeSpan(timeSinceStartTimeTemp.Hours, timeSinceStartTimeTemp.Minutes, timeSinceStartTimeTemp.Seconds + elapsedVR);
                elapsedVR = (int)(timeSinceStartTime.TotalSeconds);
            }
        }
        private void buttonResetVR_Click(object sender, EventArgs e)
        {
            timerVR.Stop();
            runningVR = false;
            labelVRMin.Text = (goalVR / 60).ToString();
            labelVRSec.Text = twoDigits(goalVR % 60);
            elapsedVR = 0;
        }
        private void timerVR_Tick(object sender, EventArgs e)
        {
            var timeSinceStartTimeTemp = DateTime.Now - timeStartVR;
            TimeSpan timeSinceStartTime = new TimeSpan(timeSinceStartTimeTemp.Hours,
                                              timeSinceStartTimeTemp.Minutes,
                                              timeSinceStartTimeTemp.Seconds + elapsedVR);
            TimeSpan goal;
            if (VRTimingWater)
            {
                goal = new TimeSpan(0, 0, goalWaterVR);
            }
            else
            {
                goal = new TimeSpan(0, 0, goalVR);
            }
            TimeSpan remainingTime = goal - timeSinceStartTime;
            if (remainingTime.TotalSeconds > 0)
            {
                labelVRMin.Text = remainingTime.Minutes.ToString();
                labelVRSec.Text = twoDigits(remainingTime.Seconds);
                notifyIcon.Text = remainingTime.Minutes.ToString() + ":" + twoDigits(remainingTime.Seconds);
            }
            else
            {
                timerVR.Stop();
                labelVRMin.Text = "0";
                labelVRSec.Text = "00";
                notifyIcon.Text = "Čajovač";
                gong();
            }

        }

        private void ButtonDS_Click(object sender, EventArgs e)
        {
            if (DSTimingWater)
            {
                DSTimingWater = false;
                runningDS = false;
                timerDS.Stop();
            }
            if (!runningDS)
            {
                runningDS = true;
                timeStartDS = DateTime.Now;
                timerDS.Start();
            }
            else
            {
                runningDS = false;
                timerDS.Stop();
                var timeSinceStartTimeTemp = DateTime.Now - timeStartDS;
                TimeSpan timeSinceStartTime = new TimeSpan(timeSinceStartTimeTemp.Hours, timeSinceStartTimeTemp.Minutes, timeSinceStartTimeTemp.Seconds + elapsedDS);
                elapsedDS = (int)(timeSinceStartTime.TotalSeconds);
            }
        }
        private void buttonWaterDS_Click(object sender, EventArgs e)
        {
            if (!DSTimingWater)
            {
                DSTimingWater = true;
                runningDS = false;
                timerDS.Stop();
            }
            if (!runningDS)
            {
                runningDS = true;
                timeStartDS = DateTime.Now;
                timerDS.Start();
            }
            else
            {
                runningDS = false;
                timerDS.Stop();
                var timeSinceStartTimeTemp = DateTime.Now - timeStartDS;
                TimeSpan timeSinceStartTime = new TimeSpan(timeSinceStartTimeTemp.Hours, timeSinceStartTimeTemp.Minutes, timeSinceStartTimeTemp.Seconds + elapsedDS);
                elapsedDS = (int)(timeSinceStartTime.TotalSeconds);
            }
        }
        private void buttonResetDS_Click(object sender, EventArgs e)
        {
            timerDS.Stop();
            runningDS = false;
            labelDSMin.Text = (goalDS / 60).ToString();
            labelDSSec.Text = twoDigits(goalDS % 60);
            elapsedDS = 0;
        }
        private void timerDS_Tick(object sender, EventArgs e)
        {
            var timeSinceStartTimeTemp = DateTime.Now - timeStartDS;
            TimeSpan timeSinceStartTime = new TimeSpan(timeSinceStartTimeTemp.Hours,
                                              timeSinceStartTimeTemp.Minutes,
                                              timeSinceStartTimeTemp.Seconds + elapsedDS);
            TimeSpan goal;
            if (DSTimingWater)
            {
                goal = new TimeSpan(0, 0, goalWaterDS);
            }
            else
            {
                goal = new TimeSpan(0, 0, goalDS);
            }
            TimeSpan remainingTime = goal - timeSinceStartTime;
            if (remainingTime.TotalSeconds > 0)
            {
                labelDSMin.Text = remainingTime.Minutes.ToString();
                labelDSSec.Text = twoDigits(remainingTime.Seconds);
                notifyIcon.Text = remainingTime.Minutes.ToString() + ":" + twoDigits(remainingTime.Seconds);
            }
            else
            {
                timerDS.Stop();
                labelDSMin.Text = "0";
                labelDSSec.Text = "00";
                notifyIcon.Text = "Čajovač";
                gong();
            }

        }

        private void ButtonMate_Click(object sender, EventArgs e)
        {
            if (MateTimingWater)
            {
                MateTimingWater = false;
                runningMate = false;
                timerMate.Stop();
            }
            if (!runningMate)
            {
                runningMate = true;
                timeStartMate = DateTime.Now;
                timerMate.Start();
            }
            else
            {
                runningMate = false;
                timerMate.Stop();
                var timeSinceStartTimeTemp = DateTime.Now - timeStartMate;
                TimeSpan timeSinceStartTime = new TimeSpan(timeSinceStartTimeTemp.Hours, timeSinceStartTimeTemp.Minutes, timeSinceStartTimeTemp.Seconds + elapsedMate);
                elapsedMate = (int)(timeSinceStartTime.TotalSeconds);
            }
        }
        private void buttonWaterMate_Click(object sender, EventArgs e)
        {
            if (!MateTimingWater)
            {
                MateTimingWater = true;
                runningMate = false;
                timerMate.Stop();
            }
            if (!runningMate)
            {
                runningMate = true;
                timeStartMate = DateTime.Now;
                timerMate.Start();
            }
            else
            {
                runningMate = false;
                timerMate.Stop();
                var timeSinceStartTimeTemp = DateTime.Now - timeStartMate;
                TimeSpan timeSinceStartTime = new TimeSpan(timeSinceStartTimeTemp.Hours, timeSinceStartTimeTemp.Minutes, timeSinceStartTimeTemp.Seconds + elapsedMate);
                elapsedMate = (int)(timeSinceStartTime.TotalSeconds);
            }
        }
        private void buttonResetMate_Click(object sender, EventArgs e)
        {
            timerMate.Stop();
            runningMate = false;
            labelMateMin.Text = (goalMate / 60).ToString();
            labelMateSec.Text = twoDigits(goalMate % 60);
            elapsedMate = 0;
        }
        private void timerMate_Tick(object sender, EventArgs e)
        {
            var timeSinceStartTimeTemp = DateTime.Now - timeStartMate;
            TimeSpan timeSinceStartTime = new TimeSpan(timeSinceStartTimeTemp.Hours,
                                              timeSinceStartTimeTemp.Minutes,
                                              timeSinceStartTimeTemp.Seconds + elapsedMate);
            TimeSpan goal;
            if (MateTimingWater)
            {
                goal = new TimeSpan(0, 0, goalWaterMate);
            }
            else
            {
                goal = new TimeSpan(0, 0, goalMate);
            }
            TimeSpan remainingTime = goal - timeSinceStartTime;
            if (remainingTime.TotalSeconds > 0)
            {
                labelMateMin.Text = remainingTime.Minutes.ToString();
                labelMateSec.Text = twoDigits(remainingTime.Seconds);
                notifyIcon.Text = remainingTime.Minutes.ToString() + ":" + twoDigits(remainingTime.Seconds);
            }
            else
            {
                timerMate.Stop();
                labelMateMin.Text = "0";
                labelMateSec.Text = "00";
                notifyIcon.Text = "Čajovač";
                gong();
            }

        }
        //NewTea



        void gong()
        {
            try { gongSound.Play(); }
            catch { }

        }
        private string twoDigits(int n)
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
