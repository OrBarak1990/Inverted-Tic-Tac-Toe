using System;
using System.Text;
using System.Windows.Forms;
using GameEngine;

namespace UiManager
{
    public class GameBoards : Form
    {
        private const string k_Empty = " ", k_X = "X", k_O = "O";
        private readonly string r_PlayerOneNmae, r_PlayerTwoName;
        private Button[,] m_BoardButtons;
        private Label m_PlayerOne, m_PlayerTwo;
        private Engine m_Engine;

        public GameBoards(int i_BoardSize, string i_PlayerOne, string i_PlayerTwo, Engine i_Engine)
        {
            r_PlayerOneNmae = i_PlayerOne;
            if (i_PlayerTwo == "[computer]")
            {
                i_PlayerTwo = "computer";
            }

            r_PlayerTwoName = i_PlayerTwo;
            m_Engine = i_Engine;
            initializeComponent(i_BoardSize, i_PlayerOne, i_PlayerTwo);
        }

        private void initializeComponent(int i_BoardSize, string i_PlayerOne, string i_PlayerTwo)
        {
            m_BoardButtons = new Button[i_BoardSize, i_BoardSize];
            initializeBoard(); 
            initializeWindow();
            initializeLabels();
            m_Engine.GameResult += Engine_GameResult;
            m_Engine.ChangeTurn += Engine_ChangeTurn;
        }

        private void initializeWindow()
        {
            this.SuspendLayout();

            this.AutoSize = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 177);
            this.Name = "TicTacToeReverse";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "TicTacToeReverse";
            this.DoubleBuffered = true;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void initializeLabels()
        {
            m_PlayerOne = new Label();
            m_PlayerOne.AutoSize = true;
            m_PlayerOne.Location = new System.Drawing.Point((this.ClientSize.Width / 2) - 100, this.ClientSize.Height - m_PlayerOne.Height);
            m_PlayerOne.Padding = new Padding(0, 20, 0, 0);
            m_PlayerTwo = new Label();
            m_PlayerTwo.AutoSize = true;
            m_PlayerTwo.Location = new System.Drawing.Point((this.ClientSize.Width / 2) + m_PlayerOne.Width, this.ClientSize.Height - m_PlayerOne.Height);
            m_PlayerTwo.Padding = new Padding(0, 20, 0, 0);
            m_PlayerTwo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 177);
            m_PlayerOne.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 177);
            this.updateTextLabels();
            this.Controls.Add(m_PlayerOne);
            this.Controls.Add(m_PlayerTwo);
        }

        private void initializeBoard()
        {
            for (int row = 0; row < m_BoardButtons.GetLength(0); ++row)
            {
                for (int col = 0; col < m_BoardButtons.GetLength(0); ++col)
                {
                    Button button = initializeButton(row, col);
                    m_BoardButtons[row, col] = button;
                }
            }
        }

        private Button initializeButton(int i_Row, int i_Col)
        {
            Button button = new Button();
            button.AutoSize = true;
            button.Location = new System.Drawing.Point(i_Col * 60, i_Row * 60);
            button.Margin = new Padding(4, 2, 4, 2);
            button.Name = string.Format("button {0}", i_Row + i_Col);
            button.Size = new System.Drawing.Size(60, 60);
            button.TabIndex = i_Row + i_Col;
            button.UseVisualStyleBackColor = true;
            button.Click += Button_Click;
            button.TextChanged += Button_TextChanged;
            MarkNotifier handler = m_Engine.Handler(i_Row, i_Col);
            button.DataBindings.Add(new Binding
                ("Text", handler, "Mark"));
            this.Controls.Add(button);
            return button;
        }

        private void sendLocationOnBoard(Button i_ClickedButton)
        {
            bool cellFound = false;
            for (int row = 0; row < m_BoardButtons.GetLength(0) && !cellFound; ++row)
            {
                for (int col = 0; col < m_BoardButtons.GetLength(0); ++col)
                {
                    if (m_BoardButtons[row, col] == i_ClickedButton)
                    {
                        Cell toPass = new Cell(row, col);
                        m_Engine.MakeAMark(toPass);
                        cellFound = true;
                        break;
                    }
                }
            }
        }

        private void Engine_ChangeTurn(PlayerTurn i_Turn)
        {
            this.replaceBold(i_Turn);
        }

        private void replaceBold(PlayerTurn i_Turn)
        {
            if (i_Turn.Turn == PlayerTurn.ePlayerTurn.PlayerO)
            {
                m_PlayerOne.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 177);
                m_PlayerTwo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 177);
            }
            else
            {
                m_PlayerTwo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 177);
                m_PlayerOne.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 177);
            }
        }

        private void Engine_GameResult(eMoveResult i_Result)
        {
            StringBuilder Text = new StringBuilder();
            string caption;
            if (i_Result == eMoveResult.OLoose)
            {
                Text = Text.AppendFormat("The winner is: {0}!", r_PlayerOneNmae);
                caption = "A WIN!";
            }
            else if (i_Result == eMoveResult.XLoose)
            {
                Text = Text.AppendFormat("The winner is: {0}!", r_PlayerTwoName);
                caption = "A WIN!";
            }
            else
            {
                Text = Text.AppendFormat("Tie!", r_PlayerOneNmae);
                caption = "A Tie!";
            }

            Text.AppendFormat(@"
Would you like to play another game?");

            DialogResult result = MessageBox.Show(Text.ToString(), caption, MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                m_Engine.startAnotherGame();
                this.initializeAnotherGame();
                this.updateTextLabels();
            }
            else
            {
                this.DialogResult = DialogResult.Abort;
            }
        }

        private void updateTextLabels()
        {
            StringBuilder text = new StringBuilder();
            m_PlayerOne.Text = text.Append(r_PlayerOneNmae + ": " + m_Engine.ScorePlayerX.ToString()).ToString();
            text = new StringBuilder();
            m_PlayerTwo.Text = text.Append(r_PlayerTwoName + ": " + m_Engine.ScorePlayerO.ToString()).ToString();
        }

        private void Button_TextChanged(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button.Enabled && (button.Text == k_X || button.Text == k_O))
            {
                (sender as Button).Enabled = false;
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            (sender as Button).Enabled = false;
            sendLocationOnBoard(sender as Button);
        }

        private void initializeAnotherGame()
        {
            for(int row = 0; row < m_BoardButtons.GetLength(0); ++row)
            {
                for (int col = 0; col < m_BoardButtons.GetLength(0); ++col)
                {
                    m_BoardButtons[row, col].DataBindings.Clear();
                    m_BoardButtons[row, col].Text = k_Empty;
                    m_BoardButtons[row, col].Enabled = true;
                    m_BoardButtons[row, col].DataBindings.Add(new Binding("Text", m_Engine.Handler(row, col), "Mark"));
                }
            }
        }
    }
}
