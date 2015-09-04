using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Text;

namespace Čajovač
{
    public class Tea
    {                
        Form1 dad;
        TeaDataItem teaData;
        Font myFont;
        int id;
        public int goal
        {
            get
            {
                return teaData.goal;
            }
        }
        public int goalWater
        {
            get
            {
                return teaData.goalWater;
            }
        }
        public string name
        {
            get
            {
                return teaData.name;
            }
        }
        public DateTime timeStart;
        public bool running = false;
        public int elapsed;
        public bool timingWater = false;

        public System.Windows.Forms.GroupBox groupBox;
        public System.Windows.Forms.Button button;
        public System.Windows.Forms.PictureBox pictureBoxFuck;
        public System.Windows.Forms.Button buttonWater;
        public System.Windows.Forms.Button buttonReset;
        public System.Windows.Forms.Label labelM;
        public System.Windows.Forms.Label labelMin;
        public System.Windows.Forms.Label labelSec;
        public System.Windows.Forms.MenuItem menuItem;
        public System.Windows.Forms.MenuItem menuItemWater;
        public System.Windows.Forms.Timer timer;
        public void reposition()
        {
            int caju = dad.teas.Length;
            int sirkaCaje = 256;
            int w = dad.ClientSize.Width;
            int h = dad.ClientSize.Height;
            int maxCajuNaSirku = (w - 12) / (256 + 12);
            int maxCajuNaVysku = (h - 12) / (256 + 12);

            int cajuNaSirku;
            if (maxCajuNaSirku <= 1)
            {
                cajuNaSirku = 1;
            }
            else
            {
                int maxZbytek = 0;
                int maxZbytekPocet = maxCajuNaSirku;
                for (int i = maxCajuNaSirku; i > 1; i--)
                {
                    int z = caju % i;
                    if (z == 0 && caju / i <= maxCajuNaVysku)
                    {
                        maxZbytekPocet = i;
                        break;
                    }
                    if (z > maxZbytek && caju/i <= maxCajuNaVysku)
                    {
                        maxZbytek = z;
                        maxZbytekPocet = i;
                    }
                }
                cajuNaSirku = maxZbytekPocet;
            }
            int cajuNaVysku = (caju + cajuNaSirku - 1) / cajuNaSirku;
            dad.ClientSize = new System.Drawing.Size(w,Math.Max(h,cajuNaVysku*(256+12)+12));
            h = dad.ClientSize.Height;
            int wPadding = (w - cajuNaSirku * sirkaCaje) / (cajuNaSirku + 1);
            int hPadding = (h - cajuNaVysku * sirkaCaje) / (cajuNaVysku + 1);
            int wLastPadding;
            if (caju % cajuNaSirku == 0)
            {
                wLastPadding = wPadding;
            }
            else
            {
                wLastPadding = (w - (caju % cajuNaSirku) * sirkaCaje) / ((caju % cajuNaSirku) + 1);
            }

            if (id / cajuNaSirku == cajuNaVysku - 1)
            {
                groupBox.Location = new System.Drawing.Point(wLastPadding + (sirkaCaje + wLastPadding) * (id % cajuNaSirku), hPadding + (sirkaCaje + hPadding) * (id / cajuNaSirku));
            }
            else
            {
                groupBox.Location = new System.Drawing.Point(wPadding + (sirkaCaje + wPadding) * (id % cajuNaSirku), hPadding + (sirkaCaje + hPadding) * (id / cajuNaSirku));
            }


            

            //dad.Scale(new SizeF(0.5f, 0.5f));
            //groupBox.Scale(new SizeF(0.5f, 0.5f));
        }
        public Tea(Form1 dad,TeaDataItem teaData, int id)
        {            
            this.dad = dad;
            this.teaData = teaData;
            this.id = id;
            this.myFont = dad.myFont;

            groupBox = new System.Windows.Forms.GroupBox();
            button = new System.Windows.Forms.Button();
            pictureBoxFuck = new System.Windows.Forms.PictureBox();
            buttonWater = new System.Windows.Forms.Button();
            buttonReset = new System.Windows.Forms.Button();
            labelM = new System.Windows.Forms.Label();
            labelMin = new System.Windows.Forms.Label();
            labelSec = new System.Windows.Forms.Label();
            menuItem = new System.Windows.Forms.MenuItem();
            menuItemWater = new System.Windows.Forms.MenuItem();
            timer = new System.Windows.Forms.Timer();
            groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFuck)).BeginInit();
            //
            // imageButton
            //
            Bitmap imageButton;
            if (teaData.imageFile != "")
            {
                try
                {
                    imageButton = (Bitmap)Image.FromFile(@"Images\" + teaData.imageFile, true);
                }
                catch (System.IO.FileNotFoundException)
                {
                    imageButton = global::Čajovač.Properties.Resources.noImage;
                }
            }
            else
            {
                imageButton = global::Čajovač.Properties.Resources.noImage;
            }
            // 
            // groupBox1
            //             
            groupBox.Name = "groupBox";
            groupBox.TabIndex = 1;
            groupBox.TabStop = false;
            dad.Controls.Add(groupBox);
            groupBox.Controls.Add(pictureBoxFuck);
            groupBox.Controls.Add(buttonWater);
            groupBox.Controls.Add(buttonReset);
            groupBox.Controls.Add(labelM);
            groupBox.Controls.Add(labelMin);
            groupBox.Controls.Add(labelSec);
            groupBox.Controls.Add(button);
            groupBox.Size = new System.Drawing.Size(256, 256);
            // 
            // button
            //
            button.BackgroundImage = imageButton;
            button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button.Location = new System.Drawing.Point(0, 0);
            button.Name = "button";
            button.TabIndex = 1;
            button.UseVisualStyleBackColor = true;
            button.Click += new System.EventHandler(dad.button_Click);
            button.Tag = id;
            button.Size = new System.Drawing.Size(256, 256);
            // 
            // pictureBoxFuck
            // 


            pictureBoxFuck.BackgroundImage = imageButton.Clone(new System.Drawing.Rectangle(79, 159, 116, 52), imageButton.PixelFormat);
            pictureBoxFuck.Name = "pictureBoxFuck";
            pictureBoxFuck.Size = new System.Drawing.Size(116, 52);
            pictureBoxFuck.TabIndex = 5;
            pictureBoxFuck.TabStop = false;
            pictureBoxFuck.Click += new System.EventHandler(dad.button_Click);
            pictureBoxFuck.Tag = id;
            pictureBoxFuck.Location = new System.Drawing.Point(79, 159);
            // 
            // buttonWater
            // 
            buttonWater.BackColor = System.Drawing.Color.Brown;
            buttonWater.BackgroundImage = global::Čajovač.Properties.Resources.water;
            buttonWater.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            buttonWater.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            buttonWater.FlatAppearance.BorderSize = 0;
            buttonWater.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonWater.Name = "buttonWater";
            buttonWater.TabIndex = 7;
            buttonWater.UseVisualStyleBackColor = false;
            buttonWater.Click += new System.EventHandler(dad.buttonWater_Click);
            buttonWater.Tag = id; buttonWater.Location = new System.Drawing.Point(7, 222);
            buttonWater.Size = new System.Drawing.Size(27, 27);

            
            // 
            // buttonReset
            // 
            buttonReset.BackColor = System.Drawing.Color.Brown;
            buttonReset.BackgroundImage = global::Čajovač.Properties.Resources.reset;
            buttonReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            buttonReset.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            buttonReset.FlatAppearance.BorderSize = 0;
            buttonReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonReset.Location = new System.Drawing.Point(35, 221);
            buttonReset.Size = new System.Drawing.Size(27, 27);
            buttonReset.TabIndex = 6;
            buttonReset.UseVisualStyleBackColor = false;
            buttonReset.Click += new System.EventHandler(dad.buttonReset_Click);
            buttonReset.Tag = id;
            buttonReset.Location = new System.Drawing.Point(35, 221);
            buttonReset.Size = new System.Drawing.Size(27, 27);

            
            // 
            // labelM
            // 
            labelM.BackColor = System.Drawing.Color.White;
            labelM.FlatStyle = System.Windows.Forms.FlatStyle.System;
            labelM.Font = new System.Drawing.Font("Microsoft YaHei", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));            
            labelM.Margin = new System.Windows.Forms.Padding(0);
            labelM.TabIndex = 4;
            labelM.Text = ":";
            labelM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            labelM.Location = new System.Drawing.Point(190, 199);
            labelM.Size = new System.Drawing.Size(18, 47);

            labelMin.Location = new System.Drawing.Point(131, 192);
            labelMin.Size = new System.Drawing.Size(60, 56);
            // 
            // labelMin
            // 
            labelMin.BackColor = System.Drawing.Color.White;
            labelMin.FlatStyle = System.Windows.Forms.FlatStyle.System;
            labelMin.Location = new System.Drawing.Point(131, 192);
            labelMin.Size = new System.Drawing.Size(60, 56);

            labelMin.Font = myFont;
            //labelMin.Font = new System.Drawing.Font("Zenzai Itacha", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));            
            labelMin.Name = "labelVRMin";            
            labelMin.TabIndex = 2;
            labelMin.Text = (goal/60).ToString();
            labelMin.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelSec
            // 
            labelSec.Font = myFont;
            labelSec.BackColor = System.Drawing.Color.White;
            labelSec.FlatStyle = System.Windows.Forms.FlatStyle.System;
            //labelSec.Font = new System.Drawing.Font("Zenzai Itacha", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            labelSec.Margin = new System.Windows.Forms.Padding(0);
            labelSec.TabIndex = 3;
            labelSec.Text = dad.twoDigits(goal%60);
            labelSec.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            labelSec.Location = new System.Drawing.Point(207, 192);
            labelSec.Size = new System.Drawing.Size(46, 56);

            labelM.Location = new System.Drawing.Point(190, 199);
            labelM.Size = new System.Drawing.Size(18, 47);

            labelMin.Location = new System.Drawing.Point(131, 192);
            labelMin.Size = new System.Drawing.Size(60, 56);
            //
            // MenuItems
            //
            menuItem.Text = name;
            menuItemWater.Text = name + " water";
            menuItem.Click += new System.EventHandler(dad.button_Click);
            menuItemWater.Click += new System.EventHandler(dad.buttonWater_Click);
            menuItem.Tag = id;
            menuItemWater.Tag = id;
            // 
            // timerOolong
            // 
            timer.Interval = 200;
            timer.Tick += new System.EventHandler(dad.timer_Tick);
            timer.Tag = id;
            //
            //
            //
            this.groupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFuck)).EndInit();

            reposition();
        }       
    }
    public class TeaDataItem
    {
        public string name;
        public int goal;
        public int goalWater;
        public string imageFile;
        public bool enabled;
        public string poznamka = "";
        public TeaDataItem()
        {

        }
        public TeaDataItem(string name, int goal, int goalWater, string imageFile)
        {
            this.name = name;
            this.goal = goal;
            this.goalWater = goalWater;
            this.imageFile = imageFile;
            enabled = true;
        }
        public TeaDataItem(string name,int goal, int goalWater,string poznamka, string imageFile)
        {
            this.name = name;
            this.goal = goal;
            this.goalWater = goalWater;
            this.imageFile = imageFile;
            this.poznamka = poznamka;
            enabled = true;
        }
    }
    public class TeaData
    {

        public TeaData()
        {

        }
        public int cajuNaSirku = 3;
        public int numberOfEnabled
        {
            get
            {
                int i = 0;
                foreach(TeaDataItem tdi in data)
                {
                    if (tdi.enabled)
                        i++;
                }
                return i;
            }
        }
        public TeaDataItem nthEnabled(int n)
        {
            int i = 0;
            for(int j = 0;j<data.Count;j++)
            {

                if (data[j].enabled)
                {
                    if(i==n)
                    {
                        return data[j];
                    }
                    i++;
                }
            }
            return null;
        }
        public List<TeaDataItem> data;
    }
}
