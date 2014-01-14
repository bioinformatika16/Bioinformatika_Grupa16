using System.Collections.Generic;


namespace MSA
{
    /// <summary>
    /// Class that represents one cell in the dynamic programming matrix.
    /// </summary>
    class Cell
    {
        private Cell prevoius_Cell;
        private List<Cell> previousCells = new List<Cell>();
        private int row;
        private int column;
        private int score;
        private PreviousCellDirection direction;

        /// <summary>
        /// Constructor that initialize row nad column number.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public Cell(int row, int col)
        {
            this.column = col;
            this.row = row;

        }
        public enum PreviousCellDirection { Left, Above, Diagonal };
        public Cell(int row, int col, int score)
        {
            this.column = col;
            this.row = row;
            this.score = score;

        }
        public Cell(int row, int col, int score, Cell Prev)
        {
            this.column = col;
            this.row = row;
            this.score = score;
            this.prevoius_Cell = Prev;

        }
        public Cell(int row, int col, int score, Cell Prev, PreviousCellDirection direction)
        {
            this.column = col;
            this.row = row;
            this.score = score;
            this.prevoius_Cell = Prev;
            this.direction = direction;

        }
        public Cell CellPointer
        {
            set { this.prevoius_Cell = value; }
            get { return this.prevoius_Cell; }

        }
        public List<Cell> PrevCellPointer
        {
            set { this.previousCells = value; }
            get { return this.previousCells; }

        }
        public Cell this[int index]
        {
            set { this.previousCells[index] = value; }

            get { return this.previousCells[index]; }
        }
        /// <summary>
        /// Getter and setter for score
        /// </summary>
        public int CellScore
        {
            set { this.score = value; }
            get { return this.score; }

        }
        /// <summary>
        /// Getter and setter for row
        /// </summary>
        public int CellRow
        {
            set { this.row = value; }
            get { return this.row; }

        }
        /// <summary>
        /// Getter and setter for column
        /// </summary>
        public int CellColumn
        {
            set { this.column = value; }
            get { return this.column; }

        }
        /// <summary>
        /// Getter and setter method for direction type
        /// </summary>
        public PreviousCellDirection Type
        {
            set { this.direction = value; }
            get { return this.direction; }

        }
        public Cell(){}
    }
}
