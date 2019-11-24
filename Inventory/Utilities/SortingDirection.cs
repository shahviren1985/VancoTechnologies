
using System.Web.UI.WebControls;

namespace ITM.Utilities
{
    public class SortingDirection
    {
        public static SortDirection Direction;

        public static SortDirection SortDirection
        {
            get
            {
                if (Direction == null)
                {
                    Direction = SortDirection.Ascending;
                }
                return (SortDirection)Direction;
            }
            set
            {
                Direction = value;
            }
        }
    }
}
