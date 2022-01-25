using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace SonicRetro.SonLVL.GUI
{
	public static class Extensions
	{
		public static void ShowHide(this System.Windows.Forms.Control ctrl)
		{
			if (ctrl.Visible)
				ctrl.Hide();
			else
				ctrl.Show();
		}

		public static Color Blend(this Color back, Color blend)
		{
			double A = blend.A / 255d;
			return Color.FromArgb(255, (int)(((1 - A) * back.R) + (A * blend.R)), (int)(((1 - A) * back.G) + (A * blend.G)), (int)(((1 - A) * back.B) + (A * blend.B)));
		}

		public static Rectangle Scale(this Rectangle r, int factor)
		{
			return new Rectangle(r.X * factor, r.Y * factor, r.Width * factor, r.Height * factor);
		}

		public static Rectangle Scale(this Rectangle r, int h, int v)
		{
			return new Rectangle(r.X * h, r.Y * v, r.Width * h, r.Height * v);
		}

		public static void Swap<T>(this IList<T> list, int a, int b)
		{
			T tmp = list[a];
			list[a] = list[b];
			list[b] = tmp;
		}

		public static void Move<T>(this IList<T> list, int src, int dst)
		{
			T tmp = list[src];
			list.Insert(dst, tmp);
			list.RemoveAt(src > dst ? src + 1 : src);
		}

		public static void Move<T>(this T[] list, int src, int dst)
		{
			T tmp = list[src];
			if (src > dst)
			{
				for (int i = src; i > dst; i--)
					list[i] = list[i - 1];
			}
			else
			{
				for (int i = src; i < dst; i++)
					list[i] = list[i + 1];
			}
			list[dst] = tmp;
		}

		public static void AddOrSet<T>(this IList<T> list, int index, T item)
		{
			if (list.Count <= index)
				list.Add(item);
			else
				list[index] = item;
		}

		public static Color Invert(this Color color)
		{
			return Color.FromArgb(color.A, 255 - color.R, 255 - color.G, 255 - color.B);
		}
	}
}
