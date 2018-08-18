using System;

namespace GameEngine
{
    public struct Cell
    {
        private int m_Row, m_Col;

        public static bool TryParse(string i_Str, out Cell? io_Cell)
        {
            const char k_Space = ' ';
            string[] tockens = i_Str.Split(k_Space);
            bool succesfallParse = false;
            int row = 0, col = 0;
            io_Cell = null;
            if (tockens.Length == 2)
            {
                succesfallParse = int.TryParse(tockens[0], out row);
                if (succesfallParse)
                {
                    succesfallParse = int.TryParse(tockens[1], out col);
                }

                if (succesfallParse)
                {
                    io_Cell = new Cell(row - 1, col - 1);
                }
            }

            return succesfallParse;
        }

        public Cell(int i_Row, int i_Col)
        {
            m_Row = i_Row;
            m_Col = i_Col;
        }

        public int Row
        {
            get
            {
                return m_Row;
            }

            set
            {
                m_Row = value;
            }
        }

        public int Col
        {
            get
            {
                return m_Col;
            }

            set
            {
                m_Col = value;
            }
        }
    }
}
