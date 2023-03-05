using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClassLib.Classes
{
	public class Margin
	{
		int top, right, bottom, left;

		public int Top { get => top; set => top = value; }
		public int Right { get => right; set => right = value; }
		public int Bottom { get => bottom; set => bottom = value; }
		public int Left { get => left; set => left = value; }
		public int HorizontalMargins { get => left + right; }
		public int VerticalMargins { get => top + bottom; }

		public Margin()
		{

		}
		public Margin(int _all)
		{
			top = _all;
			right = _all;
			bottom = _all;
			left = _all;
		}

		public Margin(int _top, int _right, int _bottom, int _left)
		{
			top = _top;
			right = _right;
			bottom = _bottom;
			left = _left;
		}
	}
}
