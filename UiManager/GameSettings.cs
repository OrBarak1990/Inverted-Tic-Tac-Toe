using System;
using System.Windows.Forms;

namespace UiManager
{
    public partial class GameSettings : Form
    {
        private bool againstCpmuter = true;

        public GameSettings()
        {
            InitializeComponent();
        }
        
        public bool AgainstComputer
        {
            get
            {
                return againstCpmuter;
            }
        }

        public string PlayerOneName
        {
            get
            {
                return playerOneText.Text;
            }
        }

        public string PlayerTwoName
        {
            get
            {
                return playerTwoText.Text;
            }
        }

        public int BoardSize
        {
            get
            {
                return (int)rowNumeric.Value;
            }
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            againstCpmuter = !(sender as CheckBox).Checked;
            if (!againstCpmuter)
            {
                playerTwoText.Enabled = true;
                playerTwoText.Clear();
            }
            else
            {
                playerTwoText.Enabled = false;
                playerTwoText.SelectedText = "[computer]";
            }
        }

        private void Play_Click(object sender, EventArgs e)
        {
            if (playerOneText.Text == string.Empty && (playerTwoCheckBox.Checked && (playerTwoText.Text == string.Empty || playerTwoText.Text == "[computer]")))
            {
                MessageBox.Show("Please choose names for both players");
            }
            else if (playerTwoCheckBox.Checked && (playerTwoText.Text == string.Empty || playerTwoText.Text == "[computer]"))
            {
                MessageBox.Show("Please choose name for player 2 or unchek box to playe against computer");
            }
            else if (playerOneText.Text == string.Empty)
            {
                MessageBox.Show("Please choose name for player 1");
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void Numeric_Change(object sender, EventArgs e)
        {
            if ((sender as NumericUpDown).Name == rowNumeric.Name)
            {
                colNumeric.Value = rowNumeric.Value;
            }
            else
            {
                rowNumeric.Value = colNumeric.Value;
            }
        }
    }
}
