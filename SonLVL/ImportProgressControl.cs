using System;
using System.Windows.Forms;

namespace SonicRetro.SonLVL
{
	public partial class ImportProgressControl : UserControl
	{
		public ImportProgressControl()
		{
			InitializeComponent();
			action = new Action<int>(setProgress);
		}

		readonly Action<int> action;
		void setProgress(int prog)
		{
			progressBar1.Value = prog;
		}

		public int CurrentProgress
		{
			get => progressBar1.Value;
			set
			{
				if (InvokeRequired)
					Invoke(action, value);
				else
					progressBar1.Value = value;
			}
		}

		public int MaximumProgress
		{
			get { return progressBar1.Maximum; }
			set { progressBar1.Maximum = value; }
		}
	}
}
