﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class FriendCheck : Form
    {
        public FriendCheck()
        {
            InitializeComponent();
        }

        DBConnection db = Program.DB;
        
       
       int y = 5;

        private void Get_List()
        {
            try
            {
                string sql ="select  UR_NM , UR_CD from USER_TB  WHERE ur_cd  in (select FR_UR_FK from FRIEND_TB where  FR_FR_FK= '" + db.UR_CD + "' and FR_ACEP_ST = '0') ";
                db.ExecuteReader(sql);
                int i = 0;
                y = 0;
                if (db.Reader.Read())
                {
                    while (db.Reader.Read())
                    {

                        panel1.Controls.Add(Create_Control(i, db.Reader[0].ToString(), db.Reader[1].ToString()));
                        i++;
                    }
                }
                else
                {
                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show("끝", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
      
        }

        private Panel Create_Control(int i, string name, string code)
        {        
            Panel boardPan = new Panel();
            boardPan.Size = new Size(210, 60);
            panel1.Controls.Add(boardPan);
            boardPan.Location = new Point(2, y);
            y += 53;
          
            Button button2 = new Button();
            boardPan.Controls.Add(button2);
            button2.Location = new System.Drawing.Point(145, 19);
            button2.Name = code;
            button2.Size = new System.Drawing.Size(35, 35);
            button2.TabIndex = i;
            button2.Text = "무시";
            button2.UseVisualStyleBackColor = true;
            button2.Click += new EventHandler(Ignoring_Click);

            Button button1 = new Button();
            boardPan.Controls.Add(button1);
            button1.Location = new System.Drawing.Point(107, 19);
            button1.Name = code;
            button1.Size = new System.Drawing.Size(35, 35);
            button1.TabIndex = i;
            button1.Text = "수락";
            button1.UseVisualStyleBackColor = true;
            button1.Click += new EventHandler(Accept_Click);

            UserCustomControl.Profile FriendProfile = new UserCustomControl.Profile();
            FriendProfile.Set_Profile_Size(FontStyle.Bold);
            boardPan.Controls.Add(FriendProfile);
            FriendProfile.Location = new System.Drawing.Point(5, 19);
            FriendProfile.USERNAME = name;
            FriendProfile.Size = new System.Drawing.Size(100, 35);
            FriendProfile.TabIndex = i;

            return boardPan;
        }

        private void Accept_Click(object render, EventArgs e)
        {
            //수락시 insert 와 0값을 1로 update
            Button mybtn = (Button)render;
            string FriendCode = mybtn.Name;

            
            string sql;

            sql = "update FRIEND_TB set  FR_ACEP_ST  = '1'  where FR_FR_FK = '" + db.UR_CD + "'  and   FR_UR_FK = '" + FriendCode + "' ";
            db.ExecuteNonQuery(sql);

            sql = "insert into FRIEND_TB values('" + db.UR_CD + "', '" + FriendCode + "' , null, '1' )";
            db.ExecuteNonQuery(sql);

            panel1.Controls.Clear();
          
            
            Get_List();
        }
        private void Ignoring_Click(object render, EventArgs e)
        {
            //무시 클릭시 delete
            Button mybtn = (Button)render;
            string FriendCode = mybtn.Name;
           
            //DataRow currRow = friend_request_tb.Rows[i];

            string sql;
            sql = "delete from FRIEND_TB where FR_FR_FK = '" + db.UR_CD + "' and  FR_UR_FK = '" + FriendCode + "'";
            db.ExecuteNonQuery(sql);

            panel1.Controls.Clear();
            Get_List();
        }

        private void FriendCheck_Load(object sender, EventArgs e)
        {
            panel1.HorizontalScroll.Maximum = 0;
            panel1.VerticalScroll.Maximum = 0;
            panel1.AutoScroll = false;
            panel1.VerticalScroll.Visible = false;
            panel1.AutoScroll = true;

            Get_List();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
