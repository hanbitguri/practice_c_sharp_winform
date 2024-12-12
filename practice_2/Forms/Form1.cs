using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace practice_2
{
   

    public partial class Form1 : Form
    {

        class USER
        {
            public string userName = "";
            public int aScore = 0;
            public int bScore = 0;
            public int cScore = 0;
            public int count = 0;
            public int sum = 0;

            public static Random randomValue = new Random();
            public USER()
            {
                this.aScore = randomValue.Next(1, 51);
                this.bScore = randomValue.Next(1, 51);
                this.cScore = randomValue.Next(1, 51);
            }

        }

        USER user1 = new USER();
        USER user2 = new USER();
        


        private USER currentUSer = null;
        private bool gameStart = false;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void panel_Click(object sender, EventArgs e)
        {
            if (!gameStart)
            {
                return;
            }
            PictureBox choosedScore = (PictureBox)sender;
            string typeOfScore = choosedScore.Tag.ToString();
            FieldInfo score = typeof(USER).GetField(typeOfScore);

            if (currentUSer.Equals(user1))
            {
                listBox1.Items.Add((String.Format("{0}(이/가) {1}점을 획득하였습니다.", currentUSer.userName, score.GetValue(user2))));
                currentUSer.sum += (int)score.GetValue(user2);
                currentUSer = user2;
            }
            else
            {
                listBox2.Items.Add((String.Format("{0}(이/가) {1}점을 획득하였습니다.", currentUSer.userName, score.GetValue(user1))));
                currentUSer.sum += (int)score.GetValue(user1);
                currentUSer = user1;

            }
            currentUSer.count++;

            if(user1.count==5 && user2.count == 5)
            {
                string result = string.Format("게임이 끝났습니다. {0}는 {1}점 , {2}는 {3}점 으로 {4}가 이겼습니다.",
                    user1.userName,user1.sum,user2.userName,user2.sum , user1.sum>user2.sum?user1.userName : user2.userName
                    );
                MessageBox.Show(result);
            }

            label1.Text = $"현재 차례 : {currentUSer.userName}";

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            panel_Click(sender, e);
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            panel_Click(sender, e);
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            panel_Click(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text)|| string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("플레이어 이름을 입력해주세요!");
                return;
            }
            user1.userName = textBox2.Text;
            user2.userName = textBox3.Text;

            Random randomValue = new Random();
            int initTurn = randomValue.Next(0, 2);
            switch (initTurn)
            {
                case 0:
                    currentUSer = user1;
                    break;
                case 1:
                    currentUSer = user2;
                    break;
            }

            TextBox[] textBoxes = { textBox2, textBox3 };
            foreach (TextBox textBox in textBoxes)
            {
                textBox.Hide();
            }

            button1.Hide();
            label1.Text = String.Format("현재 차례 : {0}", currentUSer.userName);
            label1.Show();


        }
    }
}


