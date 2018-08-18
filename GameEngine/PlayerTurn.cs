namespace GameEngine
{
    public class PlayerTurn
    {
        public enum ePlayerTurn
        {
            PlayerX = 1,
            PlayerO = 2
        }

        private ePlayerTurn m_Turn;

        public PlayerTurn(ePlayerTurn i_Turn)
        {
            m_Turn = i_Turn;
        }

        public ePlayerTurn Turn
        {
            get
            {
                return m_Turn;
            }

            set
            {
                m_Turn = value;
            }
        }

        public static PlayerTurn operator ++(PlayerTurn i_Turn)
        {
            ePlayerTurn turn;
            if (i_Turn.m_Turn == ePlayerTurn.PlayerX)
            {
                turn = ePlayerTurn.PlayerO;
            }
            else
            {
                turn = ePlayerTurn.PlayerX;
            }

            PlayerTurn newTurn = new PlayerTurn(turn);
            return newTurn;
        }

        public bool IsPlayerX()
        {
            return m_Turn == ePlayerTurn.PlayerX;
        }

        public bool IsPlayerO()
        {
            return m_Turn == ePlayerTurn.PlayerO;
        }

        public char getSign()
        {
            const char k_X = 'X', k_O = 'O';
            char sign;
            if (this.m_Turn == ePlayerTurn.PlayerX)
            {
                sign = k_X;
            }
            else
            {
                sign = k_O;
            }

            return sign;
        }

        public override string ToString()
        {
            string turn;
            if (m_Turn == ePlayerTurn.PlayerX)
            {
                turn = "Player X";
            }
            else
            {
                turn = "Player O";
            }

            return turn;
        }
    }
}
