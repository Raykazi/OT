using System.Collections;
using System.Windows.Forms;

namespace TrackerClient
{
    public class ListViewItemComparer : IComparer
    {
        // Specifies the column to be sorted
        private int _columnToSort;

        // Specifies the order in which to sort (i.e. 'Ascending').
        private SortOrder _orderOfSort;

        // Case insensitive comparer object
        private CaseInsensitiveComparer _objectCompare;

        // Class constructor, initializes various elements
        public ListViewItemComparer()
        {
            // Initialize the column to '0'
            _columnToSort = 0;

            // Initialize the sort order to 'none'
            _orderOfSort = SortOrder.None;

            // Initialize the CaseInsensitiveComparer object
            _objectCompare = new CaseInsensitiveComparer();
        }

        // This method is inherited from the IComparer interface.
        // It compares the two objects passed using a case
        // insensitive comparison.
        //
        // x: First object to be compared
        // y: Second object to be compared
        //
        // The result of the comparison. "0" if equal,
        // negative if 'x' is less than 'y' and
        // positive if 'x' is greater than 'y'
        public int Compare(object x, object y)
        {
            int compareResult;
            ListViewItem listviewX, listviewY;

            // Cast the objects to be compared to ListViewItem objects
            listviewX = (ListViewItem)x;
            listviewY = (ListViewItem)y;

            // Case insensitive Compare
            compareResult = _objectCompare.Compare(
                listviewX.SubItems[_columnToSort].Text,
                listviewY.SubItems[_columnToSort].Text
            );

            // Calculate correct return value based on object comparison
            if (_orderOfSort == SortOrder.Ascending)
            {
                // Ascending sort is selected, return normal result of compare operation
                return compareResult;
            }
            else if (_orderOfSort == SortOrder.Descending)
            {
                // Descending sort is selected, return negative result of compare operation
                return (-compareResult);
            }
            else
            {
                // Return '0' to indicate they are equal
                return 0;
            }
        }

        // Gets or sets the number of the column to which to
        // apply the sorting operation (Defaults to '0').
        public int SortColumn
        {
            set
            {
                _columnToSort = value;
            }
            get
            {
                return _columnToSort;
            }
        }

        // Gets or sets the order of sorting to apply
        // (for example, 'Ascending' or 'Descending').
        public SortOrder Order
        {
            set
            {
                _orderOfSort = value;
            }
            get
            {
                return _orderOfSort;
            }
        }
    }
}
