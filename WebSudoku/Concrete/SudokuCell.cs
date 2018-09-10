using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using WebSudoku.Models;

namespace WebSudoku.Concrete
{
    public class SudokuCell      
    {
        public SudokuCell()
        {
            possibleOpt = new List<int>();
        }

        public SudokuCell(int num) : this()
        {
            this.Data = num;
        }

        public int Data { get; set; }

        public List<int> possibleOpt { get; set; }

        [ScriptIgnore]
        public bool IsEmptyCell
        {
            get
            {
                return this.Data == 0;
            }
        }
    }
}