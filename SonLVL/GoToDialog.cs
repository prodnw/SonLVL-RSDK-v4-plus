using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SonicRetro.SonLVL
{
	public partial class GoToDialog : Form
	{
		public GoToDialog()
		{
			InitializeComponent();
		}

		private void gotoEntity_CheckedChanged(object sender, EventArgs e)
		{
			entityPos.Enabled = gotoEntity.Checked;
		}

		private void gotoPosition_CheckedChanged(object sender, EventArgs e)
		{
			xpos.Enabled = gotoPosition.Checked;
			ypos.Enabled = gotoPosition.Checked;
		}
	}
}
