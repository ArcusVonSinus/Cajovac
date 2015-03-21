using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Čajovač
{
    class Tea
    {
        public string name;
        int goal=90;
        int goalWater=120;
        Form1 dad;
        public System.Windows.Forms.GroupBox groupBox;
        public System.Windows.Forms.Button button;
        public System.Windows.Forms.PictureBox pictureBoxFuck;
        public System.Windows.Forms.Button buttonWater;
        public System.Windows.Forms.Button buttonReset;
        public System.Windows.Forms.Label labelM;
        public System.Windows.Forms.Label labelMin;
        public System.Windows.Forms.Label labelSec;
        public Tea(Form1 dad,string name)
        {
            this.name = name;
            this.dad = dad;

            groupBox = new System.Windows.Forms.GroupBox();
            button = new System.Windows.Forms.Button();
            pictureBoxFuck = new System.Windows.Forms.PictureBox();
            buttonWater = new System.Windows.Forms.Button();
            buttonReset = new System.Windows.Forms.Button();
            labelM = new System.Windows.Forms.Label();
            labelMin = new System.Windows.Forms.Label();
            labelSec = new System.Windows.Forms.Label();
            //
            // imageButton
            //
            Bitmap imageButton;
            try
            {
                imageButton = (Bitmap)Image.FromFile(@"1.jpg", true);
            }
            catch (System.IO.FileNotFoundException)
            {
                imageButton = global::Čajovač.Properties.Resources.Rooibos;
            }
            // 
            // groupBox1
            //             
            groupBox.Location = new System.Drawing.Point(12, 12);
            groupBox.Size = new System.Drawing.Size(256, 256);
            dad.Controls.Add(groupBox);
            // 
            // button
            //
            groupBox.Controls.Add(button);
            button.BackgroundImage = imageButton;
            button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button.Location = new System.Drawing.Point(0, 0);
            button.Size = new System.Drawing.Size(256, 256);
            button.TabIndex = 0;
            button.UseVisualStyleBackColor = true;
            button.Click += new System.EventHandler(dad.button_Click);
            // 
            // pictureBoxFuck
            // 
            groupBox.Controls.Add(pictureBoxFuck);
            pictureBoxFuck.BackgroundImage = imageButton.Clone(new System.Drawing.Rectangle(78, 161, 116, 52), imageButton.PixelFormat);
            pictureBoxFuck.Location = new System.Drawing.Point(79, 159);
            pictureBoxFuck.Size = new System.Drawing.Size(116, 52);
            pictureBoxFuck.TabIndex = 5;
            pictureBoxFuck.TabStop = false;
            pictureBoxFuck.Click += new System.EventHandler(dad.button_Click);
            // 
            // buttonWater
            // 
            groupBox.Controls.Add(buttonWater);
            buttonWater.BackgroundImage = global::Čajovač.Properties.Resources.water;
            buttonWater.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            buttonWater.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            buttonWater.FlatAppearance.BorderSize = 0;
            buttonWater.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonWater.Location = new System.Drawing.Point(7, 222);
            buttonWater.Size = new System.Drawing.Size(27, 27);
            buttonWater.TabIndex = 7;
            buttonWater.UseVisualStyleBackColor = false;
            buttonWater.Click += new System.EventHandler(dad.buttonWater_Click);
            // 
            // buttonReset
            // 
            groupBox.Controls.Add(buttonReset);
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
            // 
            // labelM
            // 
            groupBox.Controls.Add(labelM);
            labelM.BackColor = System.Drawing.Color.White;
            labelM.FlatStyle = System.Windows.Forms.FlatStyle.System;
            labelM.Font = new System.Drawing.Font("Microsoft YaHei", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            labelM.Location = new System.Drawing.Point(190, 199);
            labelM.Margin = new System.Windows.Forms.Padding(0);
            labelM.Size = new System.Drawing.Size(18, 47);
            labelM.TabIndex = 4;
            labelM.Text = ":";
            labelM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelMin
            // 
            groupBox.Controls.Add(labelMin);
            labelMin.BackColor = System.Drawing.Color.White;
            labelMin.FlatStyle = System.Windows.Forms.FlatStyle.System;
            labelMin.Font = new System.Drawing.Font("Zenzai Itacha", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            labelMin.Location = new System.Drawing.Point(131, 192);
            labelMin.Name = "labelVRMin";
            labelMin.Size = new System.Drawing.Size(60, 56);
            labelMin.TabIndex = 2;
            labelMin.Text = goal.ToString();
            labelMin.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelSec
            // 
            groupBox.Controls.Add(labelSec);
            labelSec.BackColor = System.Drawing.Color.White;
            labelSec.FlatStyle = System.Windows.Forms.FlatStyle.System;
            labelSec.Font = new System.Drawing.Font("Zenzai Itacha", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            labelSec.Location = new System.Drawing.Point(207, 192);
            labelSec.Margin = new System.Windows.Forms.Padding(0);
            labelSec.Size = new System.Drawing.Size(46, 56);
            labelSec.TabIndex = 3;
            labelSec.Text = dad.twoDigits(goal);
            labelSec.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            //
            //
            this.groupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFuck)).EndInit();
        }       
    }
    
}
