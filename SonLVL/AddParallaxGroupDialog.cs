using System;
using System.Windows.Forms;

namespace SonicRetro.SonLVL.GUI
{
	public partial class AddParallaxGroupDialog : Form
	{
		public AddParallaxGroupDialog()
		{
			InitializeComponent();
		}

		private void spacingNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			MainForm.Instance.PreviewLineSpacing = (int)spacingNumericUpDown.Value;
			MainForm.Instance.DrawLevel();
		}

		private void parallaxFactorNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			parallaxFactorLabel.Text = $" / 256 = +{parallaxFactorIncreaseValue.Value / 256}";
		}

		private void scrollSpeedNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			scrollSpeedLabel.Text = $" / 64 = +{scrollSpeedIncreaseValue.Value / 64}";
		}
	}
}
