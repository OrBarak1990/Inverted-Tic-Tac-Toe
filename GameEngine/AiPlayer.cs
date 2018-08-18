using System.Collections.Generic;

namespace GameEngine
{
    public class AiPlayer
    {
        private enum eMarkClassification
        {
            LowClassification,
            None,
            DeadMIxed,
            MyAndNone,
            My,
            DeadAndNone,
            DeadMyAndNone,
            DeadAndMy,
            AllDead
        }

        private const char k_X = 'X', k_O = 'O', k_Blank = ' ';
        private readonly char r_MySign;
        private List<Line> m_AllLines;
        private char[,] m_Board;
        private int m_BoardLength;

        public AiPlayer(char[,] i_Board)
        {
            m_Board = i_Board;
            m_BoardLength = m_Board.GetLength(0);
            initializeLocations();
            r_MySign = k_O;
        }

        public Cell GetCellToMark()
        {
            updateDirections();
            Cell? cell = null;
            cell = getCellFromDeadDirection();
            if (cell == null)
            {
                cell = getCellFromMyOwnershipDirection();
            }

            if (cell == null)
            {
                cell = getCellWithNoOwnershipDirection();
            }

            if (cell == null)
            {
                cell = getCellFromRivalOwnershipDirection();
            }

            if (cell == null)
            {
                cell = getRivalStrikeCell();
            }

            if (cell == null)
            {
                cell = firstValidCell();
            }

            return cell.Value;
        }

        private void initializeLocations()
        {
            int allLines = (m_BoardLength * 2) + 2;
            m_AllLines = new List<Line>(allLines);
            for (int index = 0; index < m_BoardLength; ++index)
            {
                m_AllLines.Add(new Line(Line.eDirection.Row, index));
                m_AllLines.Add(new Line(Line.eDirection.Col, index));
            }

            m_AllLines.Add(new Line(Line.eDirection.MainDiagonal, 0));
            m_AllLines.Add(new Line(Line.eDirection.SecondaryDiagonal, m_BoardLength - 1));
        }

        private void updateDirections()
        {
            foreach (Line line in m_AllLines)
            {
                if (!line.Full)
                {
                    line.update(m_Board);
                }
            }
        }

        private Cell? getCellFromDeadDirection()
        {
            Cell? saveToMark = null, currentMark = null;
            eMarkClassification? saveClassification = null;
            foreach (Line line in m_AllLines)
            {
                if (line.avilailbleLine() && !line.Alive)
                {
                    currentMark = getAvailableCell(line);
                }

                makeClassification(ref currentMark, ref saveToMark, ref saveClassification);

                if (saveToMark != null && saveClassification == eMarkClassification.AllDead)
                {
                    break;
                }
            }

            return saveToMark;
        }

        private Cell? getCellFromMyOwnershipDirection()
        {
            Cell? saveToMark = null, currentMark = null;
            eMarkClassification? saveClassification = null;
            foreach (Line line in m_AllLines)
            {
                if (line.avilailbleLine() && myOwnership(line.Woner))
                {
                    currentMark = getAvailableCell(line);
                }

                makeClassification(ref currentMark, ref saveToMark, ref saveClassification);

                if (saveToMark != null && saveClassification == eMarkClassification.AllDead)
                {
                    break;
                }
            }

            return saveToMark;
        }

        private Cell? getCellWithNoOwnershipDirection()
        {
            Cell? saveToMark = null, currentMark = null;
            eMarkClassification? saveClassification = null;
            foreach (Line line in m_AllLines)
            {
                if (line.avilailbleLine() && (line.Woner == Line.eWonerShip.none))
                {
                    currentMark = getAvailableCell(line);
                }

                makeClassification(ref currentMark, ref saveToMark, ref saveClassification);

                if (saveToMark != null && saveClassification == eMarkClassification.AllDead)
                {
                    break;
                }
            }

            return saveToMark;
        }

        private Cell? getCellFromRivalOwnershipDirection()
        {
            Cell? saveToMark = null, currentMark = null;
            eMarkClassification? saveClassification = null;
            foreach (Line line in m_AllLines)
            {
                if (line.avilailbleLine() && this.rivalOwnership(line.Woner))
                {
                    currentMark = getAvailableCell(line);
                }

                makeClassification(ref currentMark, ref saveToMark, ref saveClassification);
                if (saveToMark != null && saveClassification == eMarkClassification.AllDead)
                {
                    break;
                }
            }

            return saveToMark;
        }

        private Cell? getRivalStrikeCell()
        {
            Cell? saveToMark = null, currentMark = null;
            eMarkClassification? saveClassification = null;
            bool computerStrike = false;
            for (int row = 0; row < m_BoardLength; ++row)
            {
                for (int col = 0; col < m_BoardLength; ++col)
                {
                    if (m_Board[row, col] == k_Blank)
                    {
                        currentMark = new Cell(row, col);
                        computerStrike = haveComputerStrike(currentMark.Value);
                        if (!computerStrike)
                        {
                            makeClassification(ref currentMark, ref saveToMark, ref saveClassification);
                        }
                        else
                        {
                            currentMark = null;
                        }
                    }
                }
            }

            return saveToMark;
        }

        private Cell firstValidCell()
        {
            Cell? toMark = null;
            bool emptySquare = false;
            for (int row = 0; row < m_BoardLength && !emptySquare; ++row)
            {
                for (int col = 0; col < m_BoardLength; ++col)
                {
                    if (m_Board[row, col] == k_Blank)
                    {
                        toMark = new Cell(row, col);
                        emptySquare = true;
                        break;
                    }
                }
            }

            return toMark.Value;
        }

        private void makeClassification(ref Cell? io_CurrentMark, ref Cell? io_SaveToMark, ref eMarkClassification? io_SaveClassification)
        {
            if (io_SaveToMark == null && io_CurrentMark != null)
            {
                io_SaveClassification = makePriorityCheck(io_CurrentMark.Value);
                io_SaveToMark = new Cell(io_CurrentMark.Value.Row, io_CurrentMark.Value.Col);
            }
            else if (io_CurrentMark != null)
            {
                eMarkClassification currentClassification = makePriorityCheck(io_CurrentMark.Value);
                if (((int)currentClassification) > ((int)io_SaveClassification))
                {
                    io_SaveToMark = new Cell(io_CurrentMark.Value.Row, io_CurrentMark.Value.Col);
                }
            }
        }

        private eMarkClassification makePriorityCheck(Cell i_SearchCell)
        {
            int deadLines = 0, noWonerLines = 0, myWOneredLines = 0, rivalWonerdLines = 0;
            bool secondaryDiagonal = i_SearchCell.Col == (m_BoardLength - i_SearchCell.Row - 1);
            bool mainDiagonal = i_SearchCell.Col == i_SearchCell.Row;
            foreach (Line line in m_AllLines)
            {
                if ((line.Direction == Line.eDirection.Row && line.Index == i_SearchCell.Row) ||
                        (line.Direction == Line.eDirection.Col && line.Index == i_SearchCell.Col) ||
                        (secondaryDiagonal && line.Direction == Line.eDirection.SecondaryDiagonal) ||
                        (mainDiagonal && line.Direction == Line.eDirection.MainDiagonal))
                {
                    if (!line.Alive)
                    {
                        deadLines++;
                    }
                    else if (line.Woner == Line.eWonerShip.none)
                    {
                        noWonerLines++;
                    }
                    else if (this.myOwnership(line.Woner))
                    {
                        myWOneredLines++;
                    }
                    else if (this.rivalOwnership(line.Woner))
                    {
                        rivalWonerdLines++;
                    }
                }
            }

            return classificationData(mainDiagonal, secondaryDiagonal, deadLines, noWonerLines, myWOneredLines, rivalWonerdLines);
        }

        private eMarkClassification classificationData(bool i_MainDiagonal, bool i_SecondaryDiagonal, int I_DeadLines, int I_NoWonerLines, int I_MyWOneredLines, int i_RivalWonerdLines)
        {
            eMarkClassification classification;
            int maxLines = 2;
            if (i_MainDiagonal && i_SecondaryDiagonal)
            {
                maxLines = 4;
            }
            else if (i_MainDiagonal || i_SecondaryDiagonal)
            {
                maxLines = 3;
            }

            if (I_DeadLines == maxLines)
            {
                classification = eMarkClassification.AllDead;
            }
            else if (I_DeadLines > 0 && I_DeadLines + I_MyWOneredLines == maxLines)
            {
                classification = eMarkClassification.DeadAndMy;
            }
            else if (I_MyWOneredLines > 0 && I_DeadLines > 0 && I_DeadLines + I_MyWOneredLines + I_NoWonerLines == maxLines)
            {
                classification = eMarkClassification.DeadMyAndNone;
            }
            else if (I_DeadLines > 0 && I_DeadLines + I_NoWonerLines == maxLines)
            {
                classification = eMarkClassification.DeadAndNone;
            }
            else if (I_MyWOneredLines == maxLines)
            {
                classification = eMarkClassification.My;
            }
            else if (I_MyWOneredLines > 0 && I_MyWOneredLines + I_NoWonerLines == maxLines)
            {
                classification = eMarkClassification.MyAndNone;
            }
            else if (I_DeadLines > 0)
            {
                classification = eMarkClassification.DeadMIxed;
            }
            else if (I_NoWonerLines == maxLines)
            {
                classification = eMarkClassification.None;
            }
            else
            {
                classification = eMarkClassification.LowClassification;
            }

            return classification;
        }

        private Cell? getAvailableCell(Line i_ALine)
        {
            Cell? toMark = null;
            switch (i_ALine.Direction)
            {
                case Line.eDirection.Row:
                    toMark = noStrikeSquareByRow(i_ALine.Index);
                    break;
                case Line.eDirection.Col:
                    toMark = noStrikeCellByCol(i_ALine.Index);
                    break;
                case Line.eDirection.SecondaryDiagonal:
                    toMark = noStrike(i_ALine);
                    break;
                case Line.eDirection.MainDiagonal:
                    toMark = noStrike(i_ALine);
                    break;
            }

            return toMark;
        }

        private bool makeAStrike(Cell i_ToMark, Line.eDirection i_Direction)
        {
            bool makeAStrike = true;
            switch (i_Direction)
            {
                case Line.eDirection.Row:
                    makeAStrike = haveAstrike(Line.eDirection.Col, i_ToMark.Col);
                    break;
                case Line.eDirection.Col:
                    makeAStrike = haveAstrike(Line.eDirection.Row, i_ToMark.Row);
                    break;
                case Line.eDirection.SecondaryDiagonal:
                    makeAStrike = strikeSearch(i_ToMark);
                    break;
                case Line.eDirection.MainDiagonal:
                    makeAStrike = strikeSearch(i_ToMark);
                    break;
            }

            if (i_ToMark.Col == i_ToMark.Row && !makeAStrike)
            {
                makeAStrike = haveAstrike(Line.eDirection.MainDiagonal, 0);
            }

            if (i_ToMark.Col == m_BoardLength - i_ToMark.Row - 1 && !makeAStrike)
            {
                makeAStrike = haveAstrike(Line.eDirection.SecondaryDiagonal, m_BoardLength - 1);
            }

            return makeAStrike;
        }

        private bool strikeSearch(Cell i_SearcCell)
        {
            bool makeAStrike;
            makeAStrike = haveAstrike(Line.eDirection.Col, i_SearcCell.Col);
            if (makeAStrike == false)
            {
                makeAStrike = haveAstrike(Line.eDirection.Row, i_SearcCell.Row);
            }

            return makeAStrike;
        }

        private Cell? noStrike(Line i_ALine)
        {
            Cell? toMark = null;
            toMark = noStrikeSquareByRow(i_ALine.Index);
            if (toMark == null)
            {
                toMark = noStrikeCellByCol(i_ALine.Index);
            }

            return toMark;
        }

        private Cell? noStrikeCellByCol(int i_Col)
        {
            bool strike;
            Cell? toPass = null;
            for (int row = 0; row < m_BoardLength; ++row)
            {
                if (m_Board[row, i_Col] == k_Blank)
                {
                    toPass = new Cell(row, i_Col);
                    strike = makeAStrike(toPass.Value, Line.eDirection.Col);
                    if (!strike)
                    {
                        break;
                    }
                    else
                    {
                        toPass = null;
                    }
                }
            }

            return toPass;
        }

        private Cell? noStrikeSquareByRow(int i_Row)
        {
            bool strike;
            Cell? toPass = null;
            for (int col = 0; col < m_BoardLength; ++col)
            {
                if (m_Board[i_Row, col] == k_Blank)
                {
                    toPass = new Cell(i_Row, col);
                    strike = makeAStrike(toPass.Value, Line.eDirection.Row);
                    if (!strike)
                    {
                        break;
                    }
                    else
                    {
                        toPass = null;
                    }
                }
            }

            return toPass;
        }

        private bool haveAstrike(Line.eDirection i_DirectionToLook, int i_index)
        {
            bool strike = false;
            foreach (Line line in m_AllLines)
            {
                if (line.Direction == i_DirectionToLook && line.Index == i_index)
                {
                    if (line.OneCellFromAstrike())
                    {
                        strike = true;
                    }

                    break;
                }
            }

            return strike;
        }

        private bool haveComputerStrike(Cell i_ToMark)
        {
            bool computerStrike;
            computerStrike = computerStrikeByDirection(Line.eDirection.Col, i_ToMark.Col);
            if (!computerStrike)
            {
                computerStrike = computerStrikeByDirection(Line.eDirection.Row, i_ToMark.Row);
                if (i_ToMark.Col == i_ToMark.Row && !computerStrike)
                {
                    computerStrike = computerStrikeByDirection(Line.eDirection.MainDiagonal, 0);
                }

                if (i_ToMark.Col == m_BoardLength - i_ToMark.Row - 1 && !computerStrike)
                {
                    computerStrike = computerStrikeByDirection(Line.eDirection.SecondaryDiagonal, m_BoardLength - 1);
                }
            }

            return computerStrike;
        }

        private bool computerStrikeByDirection(Line.eDirection i_DirectionToLook, int i_index)
        {
            bool computerStrike = false;
            foreach (Line line in m_AllLines)
            {
                if (line.Direction == i_DirectionToLook && line.Index == i_index)
                {
                    if (line.OneCellFromAstrike() && this.myOwnership(line.Woner))
                    {
                        computerStrike = true;
                    }

                    break;
                }
            }

            return computerStrike;
        }

        private bool myOwnership(Line.eWonerShip i_LineWonerShip)
        {
            return (i_LineWonerShip == Line.eWonerShip.oMark && r_MySign == k_O) || (i_LineWonerShip == Line.eWonerShip.xMark && r_MySign == k_X);
        }

        private bool rivalOwnership(Line.eWonerShip i_LineWonerShip)
        {
            return (i_LineWonerShip == Line.eWonerShip.oMark && r_MySign == k_X) || (i_LineWonerShip == Line.eWonerShip.xMark && r_MySign == k_O);
        }
    }
}