using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSudoku.Concrete;
using WebSudoku.Models;

namespace WebSudoku.Controllers
{
    public class SudokuController : Controller
    {
        public ViewResult StartPage()
        {
            return View("StartPage");
        }

        /// <summary>
        /// Action with example sudoku 
        /// </summary>
        /// <returns>Returns example sudoku page</returns>
        public ViewResult SudokuView()
        {
            Sudoku testSud = new Sudoku();

            testSud.InitialTestArray();
            testSud.InitPossibleOptions();

            return View(testSud);
        }

        [HttpPost]
        public PartialViewResult GetSudoku(Sudoku sudoku)   
        {
            sudoku.InitPossibleOptions();
            sudoku.FillNextCell();

            return PartialView("SudokuPartialView", sudoku);
        }

        public ViewResult RealSudoku()
        {
            return View();
        }

        [HttpPost]
        public PartialViewResult SelectLevel(int level)  
        {
            Sudoku sudoku = new Sudoku();

            sudoku.FillInitialData();
            sudoku.InitReserve();
            sudoku.RandomizeSudoku(25);
            sudoku.SetLevel((Sudoku.Level)level);

            return PartialView("RealSudokuPartialView", sudoku);
        }

        [HttpPost]
        public PartialViewResult Refresh(Sudoku sudoku)  
        {
            sudoku.RestoreFromReserve();

            return PartialView("RealSudokuPartialView", sudoku);
        }

        [HttpPost]
        public JsonResult Validate(int i, int j, int num, Sudoku sudoku) 
        {
            var rowValid = sudoku.CheckRow(i, num);
            var columnValid = sudoku.CheckColumn(j, num);
            var blockValid = sudoku.CheckBlock(i, j, num);

            if (rowValid && columnValid && blockValid)
            {
                sudoku.Cell[i][j].Data = num;
            } else
            {
                sudoku.Cell[i][j].Data = 0;
            }

            var final = sudoku.IsCompleted();

            return Json(new { Sudoku = sudoku, RowValid = rowValid, ColumnValid = columnValid, BlockValid = blockValid, Final = final });
        }
    }
}