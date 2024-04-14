using System.ComponentModel;
using System.Windows.Forms;

namespace SonicRetro.SonLVL
{
	internal class NumericUpDownPadded : NumericUpDown
	{
		protected override void UpdateEditText()
		{
			if (!string.IsNullOrEmpty(Text) && (Text.Length != 1 || !(Text == "-")))
				Text = System.Convert.ToInt32(Value).ToString("X6");
		}
	}
}
