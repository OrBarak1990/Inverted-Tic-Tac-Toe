using GameEngine;

namespace UiManager
{
    public class Manager
    {
        private Engine m_Engine;
        
        public void StartGame()
        {
            GameSettings settings = new GameSettings();
            settings.ShowDialog();
            if (settings.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                initializeGame(settings);
            }
        }

        private void initializeGame(GameSettings i_Settings)
        {
            m_Engine = new Engine(i_Settings.BoardSize, i_Settings.AgainstComputer, i_Settings.PlayerOneName, i_Settings.PlayerTwoName);
            GameBoards boards = new GameBoards(i_Settings.BoardSize, i_Settings.PlayerOneName, i_Settings.PlayerTwoName, m_Engine);
            boards.ShowDialog();
        }
    }
}
