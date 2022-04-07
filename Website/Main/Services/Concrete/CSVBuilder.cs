
using Main.Services.Abstract;
using System.Text;

namespace Main.Services.Concrete
{
    public class CSVBuilder : ICSVBuilder
    {
        private readonly string[] _cols;
        private readonly int _rowLimit;

        private int _rows = 0;
        private StringBuilder _csv = new();

        public CSVBuilder(string[] cols, int rowLimit = Int32.MaxValue)
        {
            _cols = cols;
            _rowLimit = rowLimit;

            if (cols.Length == 0)
            {
                throw new Exception("No columns found, cannot validate new CSV row; call setColumns() first");
            }

            if (rowLimit <= 0)
            {
                throw new Exception("Row limit must be greater than 0");
            }

        }

        public void addRow(object[] data)
        {
            if (data.Length != _cols.Length)
            {
                throw new ArgumentException($"Row data for #{_rows + 1} does not contain the correct number of columns: found {data.Length}, expected {_cols.Length}");
            }

            if (_rows >= _rowLimit)
            {
                throw new Exception($"Row limit has been reached ({_rowLimit}), cannot add new row");
            }

            if (_csv.Length == 0)
            {
                _csv.AppendJoin(',', _cols);
            }

            _csv.Append('\n');
            _csv.AppendJoin(',', data);
            _rows++;

        }

        public override string ToString()
        {
            return _csv.ToString();
        }

    }
}
