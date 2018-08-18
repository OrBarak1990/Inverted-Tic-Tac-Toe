using System;

namespace GameEngine
{
    public class Engine
    {
        private const char k_X = 'X', k_O = 'O', k_Blank = ' ';
        private char[,] m_Board;
        private int m_ScorePlayerX = 0, m_ScorePlayerO = 0;
        private PlayerTurn m_Turn;
        private readonly AiPlayer m_ComputerOponent = null;
        private MarkNotifier[,] m_Handlers;
        private string m_PlayerOneName, m_PlayerTwoName;

        public event Action<eMoveResult> GameResult;

        public event Action<PlayerTurn> ChangeTurn;

        public int ScorePlayerX
        {
            get
            {
                return m_ScorePlayerX;
            }

            set
            {
                m_ScorePlayerX = value;
            }
        }

        public int ScorePlayerO
        {
            get
            {
                return m_ScorePlayerO;
            }

            set
            {
                m_ScorePlayerO = value;
            }
        }

        public Engine(int i_BoardSize, bool i_ComputerPlayer, string i_PlayerOneName, string i_PlayerTwoName)
        {
            m_Turn = new PlayerTurn(PlayerTurn.ePlayerTurn.PlayerX);
            m_Board = new char[i_BoardSize, i_BoardSize];
            m_Handlers = new MarkNotifier[i_BoardSize, i_BoardSize];
            
            m_PlayerOneName = i_PlayerOneName;
            m_PlayerTwoName = i_PlayerTwoName;
            initializeBoard();
            if (i_ComputerPlayer)
            {
                m_ComputerOponent = new AiPlayer(m_Board);
            }
        }

        public eMoveResult playerQuit()
        {
            eMoveResult result;
            this.updateResult();
            if (m_Turn.IsPlayerO())
            {
                result = eMoveResult.OLoose;
            }
            else
            {
                result = eMoveResult.XLoose;
            }

            OnGameResult(result);
            return result;
        }

        public MarkNotifier Handler(int i_Row, int i_Col)
        {
            MarkNotifier handler = null;
            if (i_Row < m_Handlers.GetLength(0) && i_Col < m_Handlers.GetLength(0) && i_Row >= 0 && i_Col >= 0)
            {
                handler = m_Handlers[i_Row, i_Col];
            }

            return handler;
        }

        public void startAnotherGame()
        {
            this.initializeBoard();
            if (this.computerTurn())
            {
                Cell compCell = m_ComputerOponent.GetCellToMark();
                manageMarkOnBoard(compCell);
            }

            OnChangeTurn(m_Turn);
        }

        public void MakeAMark(Cell i_Cell)
        {
            eMoveResult? result = null;
            Cell? io_compCell = null;
            result = checkVadiliation(i_Cell);
            if (result == null)
            {
                result = manageMarkOnBoard(i_Cell);
                if (this.computerTurn())
                {
                    io_compCell = m_ComputerOponent.GetCellToMark();
                    result = manageMarkOnBoard(io_compCell.Value);
                }
            }

            OnChangeTurn(m_Turn);
        }

        private void updateResult()
        {
            if (m_Turn.IsPlayerX())
            {
                m_ScorePlayerO++;
            }
            else
            {
                m_ScorePlayerX++;
            }
        }

        private void OnGameResult(eMoveResult i_Result)
        {
            if (GameResult != null)
            {
                GameResult.Invoke(i_Result);
            }
        }

        private void OnChangeTurn(PlayerTurn m_Turn)
        {
            if (ChangeTurn != null)
            {
                ChangeTurn.Invoke(m_Turn);
            }
        }

        private void initializeBoard()
        {
            for (int row = 0; row < m_Board.GetLength(0); ++row)
            {
                for (int col = 0; col < m_Board.GetLength(0); ++col)
                {
                    m_Board[row, col] = k_Blank;
                    m_Handlers[row, col] = new MarkNotifier();
                }
            }
        }

        private bool computerTurn()
        {
            return m_Turn.IsPlayerO() && m_ComputerOponent != null;
        }

        private eMoveResult manageMarkOnBoard(Cell i_Cell)
        {
            eMoveResult result;
            this.mark(i_Cell);
            if (this.checkForLooser(i_Cell))
            {
                result = updateLooseResult();
                OnGameResult(result);
            }
            else if (this.chekForTie())
            {
                result = eMoveResult.Tie;
                OnGameResult(eMoveResult.Tie);
            }
            else
            {
                result = eMoveResult.Marked;
                m_Turn++;
            }

            return result;
        }

        private eMoveResult updateLooseResult()
        {
            eMoveResult result;
            char looseSign = m_Turn.getSign();
            if (looseSign == k_X)
            {
                result = eMoveResult.XLoose;
            }
            else
            {
                result = eMoveResult.OLoose;
            }

            updateResult();
            return result;
        }

        private eMoveResult? checkVadiliation(Cell i_Cell)
        {
            eMoveResult? resul = null;
            if (i_Cell.Row < 0 || i_Cell.Row >= m_Board.GetLength(0) ||
                i_Cell.Col < 0 || i_Cell.Col >= m_Board.GetLength(0))
            {
                resul = eMoveResult.OutOfBoardLimits;
            }
            else if (m_Board[i_Cell.Row, i_Cell.Col] != k_Blank)
            {
                resul = eMoveResult.CaughtCell;
            }

            return resul;
        }

        private void mark(Cell i_Cell)
        {
            if (m_Turn.IsPlayerX())
            {
                m_Board[i_Cell.Row, i_Cell.Col] = k_X;
                m_Handlers[i_Cell.Row, i_Cell.Col].Mark = k_X.ToString();
            }
            else
            {
                m_Board[i_Cell.Row, i_Cell.Col] = k_O;
                m_Handlers[i_Cell.Row, i_Cell.Col].Mark = k_O.ToString();
            }
        }

        private bool checkForLooser(Cell i_Cell)
        {
            bool loose;
            if (winByRow(i_Cell.Row, m_Board[i_Cell.Row, i_Cell.Col]))
            {
                loose = true;
            }
            else if (winByColumn(i_Cell.Col, m_Board[i_Cell.Row, i_Cell.Col]))
            {
                loose = true;
            }
            else if (winByDiagonal(m_Board[i_Cell.Row, i_Cell.Col]))
            {
                loose = true;
            }
            else
            {
                loose = false;
            }

            return loose;
        }

        private bool winByDiagonal(char i_PlayerSign)
        {
            bool win;
            if (winByMainDiagonal(i_PlayerSign))
            {
                win = true;
            }
            else if (winBySecondaryDiagonal(i_PlayerSign))
            {
                win = true;
            }
            else
            {
                win = false;
            }

            return win;
        }

        private bool winByMainDiagonal(char i_PlayerSign)
        {
            bool win = true;
            for (int i = 0; i < m_Board.GetLength(0); ++i)
            {
                if (m_Board[i, i] != i_PlayerSign)
                {
                    win = false;
                    break;
                }
            }

            return win;
        }

        private bool winBySecondaryDiagonal(char i_PlayerSign)
        {
            bool win = true;
            int col;
            for (int row = 0; row < m_Board.GetLength(0); ++row)
            {
                col = m_Board.GetLength(0) - row - 1;
                if (m_Board[row, col] != i_PlayerSign)
                {
                    win = false;
                    break;
                }
            }

            return win;
        }

        private bool winByRow(int i_Row, char i_PlayerSign)
        {
            bool win = true;
            for (int col = 0; col < m_Board.GetLength(0); col++)
            {
                if (m_Board[i_Row, col] != i_PlayerSign)
                {
                    win = false;
                    break;
                }
            }

            return win;
        }

        private bool winByColumn(int i_Col, char i_PlayerSign)
        {
            bool win = true;
            for (int row = 0; row < m_Board.GetLength(0); row++)
            {
                if (m_Board[row, i_Col] != i_PlayerSign)
                {
                    win = false;
                    break;
                }
            }

            return win;
        }

        private bool chekForTie()
        {
            bool tie = true;
            for (int row = 0; row < m_Board.GetLength(0) && tie; ++row)
            {
                for (int col = 0; col < m_Board.GetLength(0); ++col)
                {
                    if (m_Board[row, col] == k_Blank)
                    {
                        tie = false;
                        break;
                    }
                }
            }

            return tie;
        }
    }
}
