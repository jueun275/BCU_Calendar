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
    public partial class FriendList : Form
    {
        DBConnection db = Program.DB;
        DataTable friendTable = null;
        DataTable friend_group_tb = null;
        
        Panel[] pan;
        Label[] label;
        Panel[] FrGr_pan;
        Label[] FrGr_btnlabel;

        int location;
        int locationbtn;
        int[] flag;

        public FriendList()
        {
            InitializeComponent();
            location = 0;
            locationbtn = 0;
        }

        private Control Create_FriendProfile(int i, DataTable dataTable) //친구 프로필 생성
        {
            DataRow currRow = dataTable.Rows[i];
            UserCustomControl.Profile FriendProfile = new UserCustomControl.Profile();
            FriendProfile.Size = new System.Drawing.Size(150, 25);
            FriendProfile.Set_Profile_Size(FontStyle.Bold);
            FriendProfile.USERNAME = currRow["UR_NM"].ToString();
            FriendProfile.Location = new System.Drawing.Point(0,location * 25);
            FriendProfile.MouseClick += new MouseEventHandler(mouse_MouseClick);
            FriendProfile.BackColor = Color.Aqua;


          //   FriendProfile.SendToBack();

          //FriendProfile.TabIndex = i;

          location++; // 전역

            return FriendProfile;
        }
        

        private void UploadeList()
        {
            flag = new int[friendTable.Rows.Count + 1];
            label = new Label[friendTable.Rows.Count];
            FrGr_pan = new Panel[friend_group_tb.Rows.Count + 1];
            pan = new Panel[friend_group_tb.Rows.Count + 1];
            FrGr_btnlabel = new Label[friend_group_tb.Rows.Count + 1];
            Label[] FrGr_Nmlabel = new Label[friend_group_tb.Rows.Count + 1];
            

            //[0] 기본적으로 생성해주는 모든친구 버튼과 목록이 저장되는 판넬 생성 
            FrGr_pan[0] = new Panel();
            FrGr_pan[0].Font = new System.Drawing.Font("함초롬돋움", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            FrGr_pan[0].Location = new System.Drawing.Point(12, 5);
            FrGr_pan[0].Name = "btn";
            FrGr_pan[0].TabIndex = 0;
            FrGr_pan[0].Size = new System.Drawing.Size(223, 40);
            FrGr_pan[0].BackColor = Color.Red;
          //  FrGr_pan[0].BringToFront();

            FrGr_pan[0].Click += new EventHandler(GroupList_Click);
            panel1.Controls.Add(FrGr_pan[0]);
            FrGr_pan[0].Show();
            flag[0] = 1;

            FrGr_Nmlabel[0] = new Label();
            FrGr_Nmlabel[0].Font = new System.Drawing.Font("함초롬돋움", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            FrGr_Nmlabel[0].Location = new System.Drawing.Point(10,10);
            FrGr_Nmlabel[0].Name = " FrGr_pan";
            FrGr_Nmlabel[0].Size = new System.Drawing.Size(100, 20);
            FrGr_Nmlabel[0].TabIndex =0;
            FrGr_Nmlabel[0].Text = "기본그룹";
           // FrGr_Nmlabel[0].BringToFront();
            FrGr_Nmlabel[0].Visible = true;

            FrGr_pan[0].Controls.Add(FrGr_Nmlabel[0]);
            FrGr_Nmlabel[0].Show();



            FrGr_btnlabel[0] = new Label();
            FrGr_btnlabel[0].Font = new System.Drawing.Font("함초롬돋움", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            FrGr_btnlabel[0].Location = new System.Drawing.Point(FrGr_pan[0].Size.Width - 35, 8);
            FrGr_btnlabel[0].Name = " FrGr_pan";
            FrGr_btnlabel[0].BackColor = Color.Silver;
            FrGr_btnlabel[0].ForeColor = Color.White;
            FrGr_btnlabel[0].Size = new System.Drawing.Size(25, 25);
            FrGr_btnlabel[0].TextAlign = ContentAlignment.MiddleCenter;
            FrGr_btnlabel[0].TabIndex = 0;
            FrGr_btnlabel[0].Text = "▲";
            //FrGr_Nmlabel[0].BringToFront();
            FrGr_btnlabel[0].Visible = true;
            FrGr_btnlabel[0].Click += new EventHandler(GroupList_Click2);
            FrGr_pan[0].Controls.Add(FrGr_btnlabel[0]);
            FrGr_btnlabel[0].Show();



            pan[0] = new Panel();
            pan[0].AutoSize = true;
            pan[0].Location = new Point(FrGr_pan[0].Location.X, FrGr_pan[0].Location.Y + 40);
           
            panel1.Controls.Add(pan[0]);

            for (int i = 0; i < friendTable.Rows.Count; i++)
            {             
                pan[0].Controls.Add(Create_FriendProfile(i , friendTable));
 
            }
           
           
            //기본적 생성이 아닌 사용자가 설정한 그룹들 가져온다
            for (int i = 0; i < friend_group_tb.Rows.Count; i++) 
            {
                //int location = 0;
                DataRow currRow;
                currRow = friend_group_tb.Rows[i];
                FrGr_pan[i + 1] = new Panel();
                FrGr_pan[i + 1].Font = new System.Drawing.Font("함초롬돋움", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
                FrGr_pan[i + 1].Size = new System.Drawing.Size(223, 40);
                FrGr_pan[i + 1].Location = new System.Drawing.Point(12, pan[0] .Location.Y + pan[0].Size.Height + i * FrGr_pan[i + 1].Size.Height);
                FrGr_pan[i + 1].Name = "btn";
                FrGr_pan[i + 1].TabIndex = i + 1; // 클릭 함수를 실행 했을때 몇번쨰 FrGr_pan 이눌렸는지 알려줄 값 설정
                //FrGr_pan[i + 1].BringToFront();
                FrGr_pan[i + 1].BackColor = Color.BurlyWood;
                FrGr_pan[i + 1].Click += new EventHandler(GroupList_Click);

                panel1.Controls.Add(FrGr_pan[i + 1]);
                FrGr_pan[i + 1].Show();

                flag[i + 1] = i + 1;

                FrGr_Nmlabel[i + 1] = new Label();
                FrGr_Nmlabel[i + 1].Font = new System.Drawing.Font("함초롬돋움", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
                FrGr_Nmlabel[i + 1].Location = new System.Drawing.Point(10, 10);
                FrGr_Nmlabel[i + 1].Name = " FrGr_pan";
                FrGr_Nmlabel[i + 1].Size = new System.Drawing.Size(100, 20);
                FrGr_Nmlabel[i + 1].TabIndex = i + 1;  // 클릭 함수를 실행 했을때 몇번쨰 FrGr_Nmlabel 이눌렸는지 알려줄 값 설정
                FrGr_Nmlabel[i + 1].Text = currRow["FRGR_NM"].ToString();

                // FrGr_Nmlabel[0].BringToFront();
                FrGr_Nmlabel[0].Visible = true;

                FrGr_pan[i + 1].Controls.Add(FrGr_Nmlabel[i + 1]);
                FrGr_Nmlabel[i + 1].Show();

                //panel1.Controls.Add(pan[i + 1]);
                FrGr_btnlabel[i + 1] = new Label();
                FrGr_btnlabel[i + 1].Font = new System.Drawing.Font("함초롬돋움", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
                FrGr_btnlabel[i + 1].Location = new System.Drawing.Point(FrGr_pan[0].Size.Width - 35, 8);
                FrGr_btnlabel[i + 1].Name = " FrGr_pan";
                FrGr_btnlabel[i + 1].Size = new System.Drawing.Size(30, 20);
                FrGr_btnlabel[i + 1].TabIndex = i + 1;
                FrGr_btnlabel[i + 1].Text = "▼";
                FrGr_btnlabel[i + 1].BackColor = Color.Silver;
                FrGr_btnlabel[i + 1].ForeColor = Color.White;
                FrGr_btnlabel[i + 1].Size = new System.Drawing.Size(25, 25);
                FrGr_btnlabel[i + 1].TextAlign = ContentAlignment.MiddleCenter;
                // FrGr_Nmlabel[0].BringToFront();
                FrGr_btnlabel[i + 1].Visible = true;
                FrGr_btnlabel[i + 1].Click += new EventHandler(GroupList_Click2);
                FrGr_pan[i + 1].Controls.Add(FrGr_btnlabel[i + 1]);
                FrGr_btnlabel[i + 1].Show();


                location++;
            }

        }
        private void GetFriendsList() //친구 목록 가져오기
        {
            db.AdapterOpen("select  UR_NM ,UR_CD from USER_TB  WHERE ur_cd  in (select FR_FR_FK from FRIEND_TB where FR_UR_FK = '" + db.UR_CD + "') ORDER BY  UR_NM ASC");

            DataSet DS = new DataSet();
            db.Adapter.Fill(DS, "friend_tb");
            friendTable = DS.Tables["friend_tb"];

            //db.Close();
        }

        private void FriendList_Load(object sender, EventArgs e)
        {
            button1.MouseEnter += new EventHandler(OnTopPanMouseEnter);
            button3.MouseEnter += new EventHandler(OnTopPanMouseEnter);
            button4.MouseEnter += new EventHandler(OnTopPanMouseEnter);

            button1.MouseEnter += new EventHandler(OnTopPanMouseLeave);
            button3.MouseEnter += new EventHandler(OnTopPanMouseLeave);
            button4.MouseEnter += new EventHandler(OnTopPanMouseLeave);

            db.UR_CD = "U100000";
            panel1.HorizontalScroll.Maximum = 0;
            panel1.VerticalScroll.Maximum = 0;
            panel1.AutoScroll = false;
            panel1.VerticalScroll.Visible = false;
            panel1.AutoScroll = true;

            GetFriendsList();
            GetGroupList();
            UploadeList();
            GetGroupMamber();


        }

        private void GetGroupList()
        {
            db.AdapterOpen("select FRGR_CD, FRGR_NM from FRIEND_GROUP_TB WHERE FRGR_UR_FK = '" + db.UR_CD + "' ORDER BY FRGR_NM DESC");
            DataSet ds = new DataSet();
            db.Adapter.Fill(ds, "friend_group_tb");
            friend_group_tb = ds.Tables["friend_group_tb"];
        }

        private void GetGroupMamber() // 그룹 멤버 반환 
        {

            for (int i = 0; i < friend_group_tb.Rows.Count; i++)  //panal 생성구간 create panel 로뺄까...?
            {
                location = 0;
                pan[i + 1] = new Panel();
                panel1.Controls.Add(pan[i + 1]);
                pan[i + 1].Location = new System.Drawing.Point(FrGr_pan[i + 1].Location.X, FrGr_pan[i + 1].Location.Y + FrGr_pan[i + 1].Size.Height+10);
                pan[i + 1].BackColor = Color.Blue;
                pan[i + 1].Visible = false;
                DataRow currRow = friend_group_tb.Rows[i];

                db.AdapterOpen("select  UR_NM from USER_TB  where UR_CD in (select FR_FR_FK from FRIEND_TB where FR_FRGR_FK = '" + currRow["FRGR_CD"].ToString() + "') ORDER BY  UR_NM ASC");

                DataSet rs = new DataSet();
                db.Adapter.Fill(rs, "groupMemberTb");
                DataTable groupMemberTb = rs.Tables["groupMemberTb"];
                
                if (groupMemberTb.Rows.Count != 0)
                {
                    pan[i + 1].Size = new System.Drawing.Size(150, 25 * (groupMemberTb.Rows.Count));
                   
                }
                else
                {
                    pan[i + 1].Size = new System.Drawing.Size(150, 25 * groupMemberTb.Rows.Count );
                }
               
                for (int j = 0; j < groupMemberTb.Rows.Count; j++) // 그룹 목록에 그룹원 추가
                {
                    pan[i + 1].Controls.Add( Create_FriendProfile(j, groupMemberTb));
                }
            }
        }

        private void GroupList_Click(object render, EventArgs e)// 위치 조절 함수
        {
            int i = 0;
            Panel myPan = (Panel)render;
            i = myPan.TabIndex ;
            Check_visible(i);

        }
        private void GroupList_Click2 (object render, EventArgs e)
        {
            int i = 0;
            Label myPan = (Label)render;
            i = myPan.TabIndex;
            Check_visible(i);
        }
        private void  Check_visible(int i)
        {

            if (!pan[i].Visible) //i 번쨰리스트가 보이지 않는 상태이면 ( 리스트를 펼칠때)
            {
                if (i == 0)
                {
                    FrGr_btnlabel[0].Text = "▲";
                }
                else
                {
                    i -= 1;
                    DataRow currRow = friend_group_tb.Rows[i];
                    FrGr_btnlabel[i + 1].Text = "▲";
                    i += 1;
                }
                //리스트 펼치는 버튼 밑에만 생각 하면됨 
                //리스트판넬의 위치를 잡을떄 목록버튼 밑에다가 둬서 버튼이동만 생각 
                for (int j = i + 1; j < friend_group_tb.Rows.Count + 1; j++) //최대 그룹리스트 갯수만큼 +1 은 기본그룹
                {
                    FrGr_pan[j].Location = new Point(FrGr_pan[j].Location.X, FrGr_pan[j].Location.Y + pan[i].Size.Height);
                    pan[j].Location = new System.Drawing.Point(FrGr_pan[j].Location.X, FrGr_pan[j].Location.Y + +FrGr_pan[j].Size.Height); 
                }
                pan[i].Visible = true;
            }
            else //리스트를 닫을떄
            {
                if (i == 0)
                {
                    FrGr_btnlabel[0].Text = "▼";
                }
                else
                {
                    i -= 1;
                    DataRow currRow = friend_group_tb.Rows[i];
                    FrGr_btnlabel[i + 1].Text = "▼";
                    i += 1;
                }
                for (int j = i + 1; j < friend_group_tb.Rows.Count + 1; j++)
                {
                    FrGr_pan[j].Location = new Point(FrGr_pan[j].Location.X, FrGr_pan[j].Location.Y - pan[i].Size.Height);
                    pan[j].Location = new System.Drawing.Point(FrGr_pan[j].Location.X, FrGr_pan[j].Location.Y + FrGr_pan[j].Size.Height);
                }
                pan[i].Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            location = 0;
            bool check = false;

            string sql = "select  UR_NM ,UR_ID from USER_TB  WHERE ur_cd  in (select FR_FR_FK from FRIEND_TB where FR_UR_FK = '" + db.UR_CD + "') and (UR_NM ='" + textBox1.Text + "' OR UR_ID='" + textBox1.Text + "') ORDER BY  UR_NM ASC" ;

            db.ExecuteReader(sql);
            if (db.Reader.Read())
            {
                db.AdapterOpen(sql);
                DataSet rs = new DataSet();
                db.Adapter.Fill(rs, "search");
                DataTable search = rs.Tables["search"];

                FriendSearch friendSearch = new FriendSearch(search);
                friendSearch.ShowDialog();
                check = true;

            }
         
            if (!check)
            {
                panel1.Visible = false;
                panel2.Visible = true;
            }
            textBox1.Text = "";        
        }

        private void button4_Click(object sender, EventArgs e) // 취소
        {
            panel2.Visible = false;
            panel1.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            AddFriend addFriend = new AddFriend();
            addFriend.ShowDialog();
          
        }
        private void mouse_MouseClick(object sender, MouseEventArgs e)
        {

                UserCustomControl.Profile myPan = (UserCustomControl.Profile)sender;
                int i = myPan.TabIndex;
                DataRow currRow = friendTable.Rows[i];
                Point mousePoint = new Point(e.X, e.Y);
                FriendModify friendModify = new FriendModify(currRow, friend_group_tb, mousePoint);
                friendModify.ShowDialog();

        }
 

        private void OnTopPanMouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = Color.SlateGray;
        }
        private void OnTopPanMouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = Color.Transparent;
        }


    }
}



