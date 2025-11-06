using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1___The_1_Lesson
{
    public partial class OXGame : Form
    {

        enum EnPlayer { Player1, Player2 }

        EnPlayer CurrentPlayer = EnPlayer.Player1;

        bool isGameOver = false; // لمعرفة إن كانت اللعبة انتهت أم لا



        public OXGame()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e){

            this.Text = "OX Game";

            this.BackColor = Color.Black;

            button1.FlatStyle = FlatStyle.Flat; // ضروري لتمكين التحكم في الحواف

            button1.FlatAppearance.BorderColor = Color.Black; // ← لون الحواف

            button1.FlatAppearance.BorderSize = 2; // ← سمك الحافة


            button1.Click += Button_Click;

            button2.Click += Button_Click;

            button3.Click += Button_Click;


            button4.Click += Button_Click;

            button5.Click += Button_Click;

            button6.Click += Button_Click;


            button7.Click += Button_Click;

            button8.Click += Button_Click;

            button9.Click += Button_Click;



        }


        private void Button_Click(object sender, EventArgs e){

            if (isGameOver)
                return; // إذا انتهت اللعبة، لا تفعل شيئًا

            Button btn = sender as Button;

            if (btn.Tag.ToString() != "?")
                return; // الزر مستعمل مسبقًا

            if (CurrentPlayer == EnPlayer.Player1)
            {
                btn.Image = Properties.Resources.X;
                btn.Tag = "X";
                LBLTurn.Text = "Player 2";
                CurrentPlayer = EnPlayer.Player2;
            }
            else
            {
                btn.Image = Properties.Resources.O;
                btn.Tag = "O";
                LBLTurn.Text = "Player 1";
                CurrentPlayer = EnPlayer.Player1;
            }

            CheckWinner();

        }


        private void OXGame_Paint_1(object sender, PaintEventArgs e){

            Pen whitePen = new Pen(Color.White, 10);

            whitePen.StartCap = LineCap.Round;

            whitePen.EndCap = LineCap.Round;


            int offsetX = 178; // تحريك لليمين

            int offsetY = 20;  // تحريك لأسفل


            // الخطوط الأفقية

            e.Graphics.DrawLine(whitePen, 178 + offsetX, 262 + offsetY, 534 + 178 + offsetX, 262 + offsetY);

            e.Graphics.DrawLine(whitePen, 178 + offsetX, 393 + offsetY, 534 + 178 + offsetX, 393 + offsetY);


            // الخطوط العمودية

            e.Graphics.DrawLine(whitePen, 356 + offsetX, 131 + offsetY, 356 + offsetX, 393 + 131 + offsetY);

            e.Graphics.DrawLine(whitePen, 534 + offsetX, 131 + offsetY, 534 + offsetX, 393 + 131 + offsetY);

        }



        private void DisableAllButtons()
        {
            foreach (Control c in this.Controls)
            {
                if (c is Button btn && btn.Tag != null)
                {
                    btn.Enabled = false;
                }
            }
        }



        private void DeclareWinner(Button btn)
        {
            string winnerSymbol = btn.Tag.ToString();
            string winnerName = (winnerSymbol == "X") ? "Player 1" : "Player 2";

            label4.Text = winnerName;
            MessageBox.Show(winnerName + " wins!", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);

            isGameOver = true;
        }


        private void CheckWinner()
        {
            Button[,] board = new Button[3, 3]
            {
        { button1, button2, button3 },
        { button4, button5, button6 },
        { button7, button8, button9 }
            };

            // تحقق من الصفوف
            for (int row = 0; row < 3; row++)
            {
                if (board[row, 0].Tag.ToString() != "?" &&
                    board[row, 0].Tag == board[row, 1].Tag &&
                    board[row, 1].Tag == board[row, 2].Tag)
                {
                    DeclareWinner(board[row, 0]);
                    return;
                }
            }

            // تحقق من الأعمدة
            for (int col = 0; col < 3; col++)
            {
                if (board[0, col].Tag.ToString() != "?" &&
                    board[0, col].Tag == board[1, col].Tag &&
                    board[1, col].Tag == board[2, col].Tag)
                {
                    DeclareWinner(board[0, col]);
                    return;
                }
            }

            // تحقق من القطر 1
            if (board[0, 0].Tag.ToString() != "?" &&
                board[0, 0].Tag == board[1, 1].Tag &&
                board[1, 1].Tag == board[2, 2].Tag)
            {
                DeclareWinner(board[0, 0]);
                return;
            }

            // تحقق من القطر 2
            if (board[0, 2].Tag.ToString() != "?" &&
                board[0, 2].Tag == board[1, 1].Tag &&
                board[1, 1].Tag == board[2, 0].Tag)
            {
                DeclareWinner(board[0, 2]);
                return;
            }

            // تحقق من التعادل
            bool draw = true;
            foreach (Button btn in board)
            {
                if (btn.Tag.ToString() == "?")
                {
                    draw = false;
                    break;
                }
            }

            if (draw)
            {
                isGameOver = true;
                label4.Text = "Draw";
                MessageBox.Show("It's a draw!", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }





        }


        private void RestartGame()
        {
            Button[] allButtons = { button1, button2, button3, button4, button5, button6, button7, button8, button9 };

            foreach (Button btn in allButtons)
            {
                btn.Tag = "?";
                btn.Image = null;
                btn.Enabled = true;
            }

            isGameOver = false;
            CurrentPlayer = EnPlayer.Player1;
            LBLTurn.Text = "Player 1";
            label4.Text = "";
        }

        private void label4_Click(object sender, EventArgs e)
        {



        }

        private void BTNResetGame_Click(object sender, EventArgs e)
        {

            RestartGame();

        }


    }
}
