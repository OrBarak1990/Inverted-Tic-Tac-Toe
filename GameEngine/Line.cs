namespace GameEngine
{
    public class Line
    {
        public enum eDirection
        {
            Row,
            Col,
            SecondaryDiagonal,
            MainDiagonal
        }

        public enum eWonerShip
        {
            xMark,
            oMark,
            none
        }

        private eWonerShip m_Woner;
        private readonly int r_Index;
        private readonly eDirection r_Direction;
        private bool m_AliveLine, m_FullLine;
        private Cell? m_LastEmptyCell = null, m_firstEmptyCell = null;
        private const char k_XSign = 'X', k_OSign = 'O', k_EmptySing = ' ';

        public Line(eDirection i_Direction, int i_Index)
        {
            r_Index = i_Index;
            r_Direction = i_Direction;
            m_AliveLine = true;
            m_FullLine = false;
            m_Woner = eWonerShip.none;
        }

        public eWonerShip Woner
        {
            get
            {
                return m_Woner;
            }
        }

        public int Index
        {
            get
            {
                return r_Index;
            }
        }

        public eDirection Direction
        {
            get
            {
                return r_Direction;
            }
        }

        public bool Alive
        {
            get
            {
                return m_AliveLine;
            }

            set
            {
                m_AliveLine = value;
            }
        }

        public bool Full
        {
            get
            {
                return m_FullLine;
            }

            set
            {
                m_FullLine = value;
            }
        }

        public Cell? LastEmptyCell
        {
            get
            {
                return m_LastEmptyCell;
            }
        }

        public Cell? firstEmptyCell
        {
            get
            {
                return m_firstEmptyCell;
            }
        }

        public bool avilailbleLine()
        {
            return !Full && (m_firstEmptyCell != null || (m_LastEmptyCell != null && m_Woner == eWonerShip.none));
        }

        public bool OneCellFromAstrike()
        {
            return m_LastEmptyCell != null && Woner != eWonerShip.none;
        }

        public void update(char[,] i_GameBoard)
        {
            this.m_LastEmptyCell = null;
            this.m_firstEmptyCell = null;
            this.m_Woner = eWonerShip.none;
            switch (r_Direction)
            {
                case eDirection.Row:
                    rowUpdate(i_GameBoard);
                    break;
                case eDirection.Col:
                    colUpdate(i_GameBoard);
                    break;
                case eDirection.SecondaryDiagonal:
                    SecondaryDiagonalUpdate(i_GameBoard);
                    break;
                case eDirection.MainDiagonal:
                    MainDiagonalUpdate(i_GameBoard);
                    break;
            }
        }

        private void MainDiagonalUpdate(char[,] i_GameBoard)
        {
            int emtyCells = 0, xCells = 0, oCells = 0;
            for (int index = 0; index < i_GameBoard.GetLength(0); ++index)
            {
                addCountingSquare(i_GameBoard[index, index], ref emtyCells, ref xCells, ref oCells);
                if (emtyCells == 1 && m_LastEmptyCell == null)
                {
                    m_LastEmptyCell = new Cell(index, index);
                }
            }

            makeAdjusments(emtyCells, xCells, oCells, i_GameBoard.GetLength(0));
        }

        private void SecondaryDiagonalUpdate(char[,] i_GameBoard)
        {
            int emtyCells = 0, xCells = 0, oCells = 0, col;
            for (int row = 0; row < i_GameBoard.GetLength(0); ++row)
            {
                col = i_GameBoard.GetLength(0) - row - 1;
                addCountingSquare(i_GameBoard[row, col], ref emtyCells, ref xCells, ref oCells);
                if (emtyCells == 1 && m_LastEmptyCell == null)
                {
                    m_LastEmptyCell = new Cell(row, col);
                }
            }

            makeAdjusments(emtyCells, xCells, oCells, i_GameBoard.GetLength(0));
        }

        private void colUpdate(char[,] i_GameBoard)
        {
            int emtyCells = 0, xCells = 0, oCells = 0;
            for (int row = 0; row < i_GameBoard.GetLength(0); ++row)
            {
                addCountingSquare(i_GameBoard[row, r_Index], ref emtyCells, ref xCells, ref oCells);
                if (emtyCells == 1 && m_LastEmptyCell == null)
                {
                    m_LastEmptyCell = new Cell(row, r_Index);
                }
            }

            makeAdjusments(emtyCells, xCells, oCells, i_GameBoard.GetLength(0));
        }

        private void rowUpdate(char[,] i_GameBoard)
        {
            int emtyCells = 0, xCells = 0, oCells = 0;
            for (int col = 0; col < i_GameBoard.GetLength(0); ++col)
            {
                addCountingSquare(i_GameBoard[r_Index, col], ref emtyCells, ref xCells, ref oCells);
                if (emtyCells == 1 && m_LastEmptyCell == null)
                {
                    m_LastEmptyCell = new Cell(r_Index, col);
                }
            }

            makeAdjusments(emtyCells, xCells, oCells, i_GameBoard.GetLength(0));
        }

        private void makeAdjusments(int i_EmtyCells, int i_XCells, int i_OCells, int i_SquareAmount)
        {
            if (i_XCells > 0 && i_OCells > 0)
            {
                m_AliveLine = false;
            }

            if (i_EmtyCells > 1)
            {
                m_firstEmptyCell = new Cell(m_LastEmptyCell.Value.Row, m_LastEmptyCell.Value.Col);
                m_LastEmptyCell = null;
            }

            if (i_XCells == 0 && i_OCells > 0)
            {
                m_Woner = eWonerShip.oMark;
            }
            else if (i_XCells > 0 && i_OCells == 0)
            {
                m_Woner = eWonerShip.xMark;
            }

            if (i_XCells + i_OCells == i_SquareAmount)
            {
                m_FullLine = true;
            }
        }

        private void addCountingSquare(char i_Sign, ref int o_EmtyCells, ref int o_XCells, ref int o_OCells)
        {
            if (i_Sign == k_XSign)
            {
                o_XCells++;
            }
            else if (i_Sign == k_EmptySing)
            {
                o_EmtyCells++;
            }
            else
            {
                o_OCells++;
            }
        }
    }
}
