using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSudoku.Concrete;

namespace WebSudoku.Models
{
    public class Sudoku
    {
        private Random rnd = new Random();
        private const int sudokuLength = 9;

        public SudokuCell[][] ReservCell { get; set; }
        public SudokuCell[][] Cell { get; set; }

        public enum Level : int
        {
            Easy = 1,
            Medium = 2,
            Hard = 3
        }

        public Sudoku()
        {

        }

        public SudokuCell[][] InitCell(SudokuCell[][] sudokuCells)
        {
            sudokuCells = new SudokuCell[sudokuLength][];
            for (int i = 0; i < sudokuCells.Length; i++)
            {
                sudokuCells[i] = new SudokuCell[sudokuLength];
            }
            return sudokuCells;
        }

        public void FillInitialData()
        {
            Cell = InitCell(Cell);
            //Replace 9 and 3 usages with constants
            //Merge Mass and ResrveMass into single method and call it right after constructor call

            //Cell = new SudokuCell[sudokuLength][];
            //for (int i = 0; i < this.Cell.Length; i++)
            //{
            //    Cell[i] = new SudokuCell[sudokuLength];
            //}

            for (int p = 0; p < 3; p++)
            {
                if (p == 2)
                {
                    int count = 3;
                    for (int i = 6; i < 9; i++)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            Cell[i][j] = new SudokuCell(count);
                            count++;
                            if (count == 10)
                            {
                                count = 1;
                            }
                        }
                        if (i == 6)
                        {
                            count = 6;
                        }
                        else if (i == 7)
                        {
                            count = 9;
                        }
                    }
                }
                else if (p == 1)
                {
                    int count = 2;
                    for (int i = 3; i < 6; i++)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            Cell[i][j] = new SudokuCell(count);
                            count++;
                            if (count == 10)
                            {
                                count = 1;
                            }
                        }
                        if (i == 3)
                        {
                            count = 5;
                        }
                        else if (i == 4)
                        {
                            count = 8;
                        }
                    }
                }
                else
                {
                    int count = 1;
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            Cell[i][j] = new SudokuCell(count);
                            count++;
                            if (count == 10)
                            {
                                count = 1;
                            }

                        }
                        if (i == 0)
                        {
                            count = 4;
                        }
                        else if (i == 1)
                        {
                            count = 7;
                        }
                    }
                }
            }
        }

        public void InitReserve()
        {
            ReservCell = InitCell(ReservCell);
            //ReservCell = new SudokuCell[this.Cell.Length][];
            //for (int i = 0; i < this.ReservCell.Length; i++)
            //{
            //    ReservCell[i] = new SudokuCell[this.Cell[i].Length];
            //}

            for (int i = 0; i < this.Cell.Length; i++)
            {
                for (int j = 0; j < this.Cell[i].Length; j++)
                {
                    ReservCell[i][j] = new SudokuCell(0);
                }
            }
        }

        public void SwapArrayCell(ref SudokuCell cell1, ref SudokuCell cell2)
        {
            var temp = cell2;
            cell2 = cell1;
            cell1 = temp;
        }

        public void SwapRows(int x, int x2)
        {
            for (int i = 0; i < Cell.Length; i++)
            {
                SwapArrayCell(ref Cell[x][i], ref Cell[x2][i]);
            }
        }

        public void SwapColumns(int y, int y2)
        {
            for (int i = 0; i < Cell.Length; i++)
            {
                SwapArrayCell(ref Cell[i][y], ref Cell[i][y2]);
            }
        }

        public void Transpose()
        {
            for (int i = 0; i < Cell.Length; i++)
            {
                for (int j = 0; j < Cell[i].Length; j++)
                {
                    SwapArrayCell(ref Cell[j][i], ref Cell[i][j]);
                }

            }
        }

        public void SwapSelectedRows()
        {
            int rowrand = rnd.Next(0, 3); // # block
            int x;
            int x2;
            do
            {
                x = rowrand * 3 + rnd.Next(0, 3);  // # Row
                x2 = rowrand * 3 + rnd.Next(0, 3); // # Row

            } while (x == x2);

            SwapRows(x, x2);
        }

        public void SwapSelectedColumns()
        {
            int columnrand = rnd.Next(0, 3);
            int y;
            int y2;

            do
            {
                y = 3 * columnrand + rnd.Next(0, 3);
                y2 = 3 * columnrand + rnd.Next(0, 3);

            } while (y == y2);

            SwapColumns(y, y2);
        }

        public void SwapSelectedRowsBlock()
        {
            int x;
            int x2;
            do
            {
                x = rnd.Next(0, 3) * 3;
                x2 = rnd.Next(0, 3) * 3;

            } while (x == x2);

            for (int i = 0; i < 3; i++)
            {
                SwapRows(x + i, x2 + i);
            }
        }

        public void SwapSelectedColumnsBlock()
        {
            int y;
            int y2;
            do
            {
                y = rnd.Next(0, 3) * 3;
                y2 = rnd.Next(0, 3) * 3;

            } while (y == y2);
            SwapColumns(y, y2);
        }

        public void RandomizeSudoku(int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                int random = rnd.Next(1, 5);

                switch (random)
                {
                    case 1:
                        {
                            Transpose();
                        }
                        break;

                    case 2:
                        {
                            SwapSelectedColumns();
                        }
                        break;
                    case 3:
                        {
                            SwapSelectedRows();
                        }
                        break;
                    case 4:
                        {
                            SwapSelectedRowsBlock();
                        }
                        break;
                    case 5:
                        {
                            SwapSelectedColumnsBlock();
                        }
                        break;
                }
            }
        }

        public void InitialTestArray()
        {
            Cell = new SudokuCell[9][];
            Cell[0] = new SudokuCell[9];
            Cell[1] = new SudokuCell[9];
            Cell[2] = new SudokuCell[9];
            Cell[3] = new SudokuCell[9];
            Cell[4] = new SudokuCell[9];
            Cell[5] = new SudokuCell[9];
            Cell[6] = new SudokuCell[9];
            Cell[7] = new SudokuCell[9];
            Cell[8] = new SudokuCell[9];

            Cell[0][0] = new SudokuCell(4);
            Cell[0][1] = new SudokuCell(2);
            Cell[0][2] = new SudokuCell(0);
            Cell[0][3] = new SudokuCell(0);
            Cell[0][4] = new SudokuCell(6);
            Cell[0][5] = new SudokuCell(3);
            Cell[0][6] = new SudokuCell(0);
            Cell[0][7] = new SudokuCell(5);
            Cell[0][8] = new SudokuCell(0);

            Cell[1][0] = new SudokuCell(3);
            Cell[1][1] = new SudokuCell(9);
            Cell[1][2] = new SudokuCell(0);
            Cell[1][3] = new SudokuCell(7);
            Cell[1][4] = new SudokuCell(0);
            Cell[1][5] = new SudokuCell(2);
            Cell[1][6] = new SudokuCell(0);
            Cell[1][7] = new SudokuCell(1);
            Cell[1][8] = new SudokuCell(4);

            Cell[2][0] = new SudokuCell(1);
            Cell[2][1] = new SudokuCell(5);
            Cell[2][2] = new SudokuCell(6);
            Cell[2][3] = new SudokuCell(9);
            Cell[2][4] = new SudokuCell(8);
            Cell[2][5] = new SudokuCell(0);
            Cell[2][6] = new SudokuCell(0);
            Cell[2][7] = new SudokuCell(2);
            Cell[2][8] = new SudokuCell(0);

            Cell[3][0] = new SudokuCell(7);
            Cell[3][1] = new SudokuCell(0);
            Cell[3][2] = new SudokuCell(0);
            Cell[3][3] = new SudokuCell(5);
            Cell[3][4] = new SudokuCell(0);
            Cell[3][5] = new SudokuCell(6);
            Cell[3][6] = new SudokuCell(2);
            Cell[3][7] = new SudokuCell(0);
            Cell[3][8] = new SudokuCell(1);

            Cell[4][0] = new SudokuCell(5);
            Cell[4][1] = new SudokuCell(0);
            Cell[4][2] = new SudokuCell(4);
            Cell[4][3] = new SudokuCell(0);
            Cell[4][4] = new SudokuCell(0);
            Cell[4][5] = new SudokuCell(0);
            Cell[4][6] = new SudokuCell(7);
            Cell[4][7] = new SudokuCell(0);
            Cell[4][8] = new SudokuCell(6);

            Cell[5][0] = new SudokuCell(0);
            Cell[5][1] = new SudokuCell(6);
            Cell[5][2] = new SudokuCell(0);
            Cell[5][3] = new SudokuCell(8);
            Cell[5][4] = new SudokuCell(9);
            Cell[5][5] = new SudokuCell(7);
            Cell[5][6] = new SudokuCell(5);
            Cell[5][7] = new SudokuCell(0);
            Cell[5][8] = new SudokuCell(3);

            Cell[6][0] = new SudokuCell(0);
            Cell[6][1] = new SudokuCell(7);
            Cell[6][2] = new SudokuCell(0);
            Cell[6][3] = new SudokuCell(4);
            Cell[6][4] = new SudokuCell(0);
            Cell[6][5] = new SudokuCell(8);
            Cell[6][6] = new SudokuCell(1);
            Cell[6][7] = new SudokuCell(6);
            Cell[6][8] = new SudokuCell(0);

            Cell[7][0] = new SudokuCell(8);
            Cell[7][1] = new SudokuCell(0);
            Cell[7][2] = new SudokuCell(5);
            Cell[7][3] = new SudokuCell(6);
            Cell[7][4] = new SudokuCell(7);
            Cell[7][5] = new SudokuCell(0);
            Cell[7][6] = new SudokuCell(4);
            Cell[7][7] = new SudokuCell(3);
            Cell[7][8] = new SudokuCell(0);

            Cell[8][0] = new SudokuCell(0);
            Cell[8][1] = new SudokuCell(4);
            Cell[8][2] = new SudokuCell(2);
            Cell[8][3] = new SudokuCell(0);
            Cell[8][4] = new SudokuCell(1);
            Cell[8][5] = new SudokuCell(5);
            Cell[8][6] = new SudokuCell(0);
            Cell[8][7] = new SudokuCell(0);
            Cell[8][8] = new SudokuCell(8);
        }

        public bool CheckRow(int row, int num)
        {
            for (int i = 0; i < Cell.Length; i++)
            {
                if (Cell[row][i].Data == num)
                {
                    return false;
                }
            }
            return true;
        }

        public bool CheckColumn(int column, int num)
        {
            for (int i = 0; i < Cell.Length; i++)
            {
                if (Cell[i][column].Data == num)
                {
                    return false;
                }
            }
            return true;
        }

        public bool CheckBlock(int row, int column, int num)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Cell[row - (row % 3) + j][column - (column % 3) + i].Data == num)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void InitPossibleOptions()
        {
            for (int i = 0; i < Cell.Length; i++)
            {
                for (int j = 0; j < Cell[i].Length; j++)
                {
                    if (Cell[i][j].IsEmptyCell)
                    {
                        Cell[i][j].possibleOpt = new List<int>();

                        for (int num = 1; num < 10; num++)
                        {
                            if (CheckBlock(i, j, num) && CheckColumn(j, num) && CheckRow(i, num))
                            {
                                Cell[i][j].possibleOpt.Add(num);
                            }
                        }
                    }
                }
            }
        }

        public SudokuCell[][] FillNextCell()
        {
            for (int i = 0; i < Cell.Length; i++)
            {
                for (int j = 0; j < Cell[i].Length; j++)
                {
                    if (Cell[i][j].IsEmptyCell)
                    {
                        if (Cell[i][j].possibleOpt.Count == 1)
                        {
                            Cell[i][j].Data = Cell[i][j].possibleOpt.First();
                            Cell[i][j].possibleOpt.Remove(Cell[i][j].Data);

                            for (int column = 0; column < Cell.Length; column++)
                            {
                                Cell[column][j].possibleOpt.Remove(Cell[i][j].Data);
                            }
                            for (int row = 0; row < Cell[i].Length; row++)
                            {
                                Cell[j][row].possibleOpt.Remove(Cell[i][j].Data);
                            }
                            for (int o = 0; o < 3; o++)
                            {
                                for (int q = 0; q < 3; q++)
                                {
                                    Cell[i - (i % 3) + q][j - (j % 3) + o].possibleOpt.Remove(Cell[i][j].Data);
                                }
                            }
                            return Cell;
                        }
                    }
                }
            }
            return Cell;
        }

        public void DeleteCellValue(int rowIndex, int columnIndex)
        {
            Cell[rowIndex][columnIndex].Data = 0;
        }

        public void DeleteCellValue(int cellIndex)
        {
            DeleteCellValue(Convert.ToInt32(Math.Floor(cellIndex / 9d)), cellIndex % 9);
        }

        public void SetInitialState(int cellcount)
        {
            List<int> alreadyDeletedElements = new List<int>();
            for (int i = 0; i < cellcount; i++)
            {
                int cellIndex;
                do
                {
                    cellIndex = rnd.Next(0, 80);
                } while (alreadyDeletedElements.Contains(cellIndex));

                alreadyDeletedElements.Add(cellIndex);
                DeleteCellValue(cellIndex);
            }
        }

        public bool IsCompleted()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (Cell[i][j].IsEmptyCell)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void SetLevel(Enum level)
        {
            switch (level)
            {
                case Level.Easy:
                    {
                        this.SetInitialState(30);
                    }
                    break;
                case Level.Medium:
                    {
                        this.SetInitialState(35);
                    }
                    break;
                case Level.Hard:
                    {
                        this.SetInitialState(40);
                    }
                    break;
            }

            this.FillReserve();
        }

        public void FillReserve()
        {
            for (int i = 0; i < Cell.Length; i++)
            {
                for (int j = 0; j < Cell[i].Length; j++)
                {
                    this.ReservCell[i][j] = this.Cell[i][j];
                }
            }
        }

        public void RestoreFromReserve()
        {
            for (int i = 0; i < Cell.Length; i++)
            {
                for (int j = 0; j < Cell[i].Length; j++)
                {
                    this.Cell[i][j] = this.ReservCell[i][j];
                }
            }
        }
    }
}



