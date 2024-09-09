using System.Collections.Generic;
using System.Windows.Forms;

namespace SonicRetro.SonLVL
{
	public partial class FileSelectDialog : Form
	{
		public FileSelectDialog(string title, List<string> files)
		{
			InitializeComponent();
			Text = title;
			foreach (var item in files)
			{
				var parent = treeView1.Nodes;
				foreach (var it2 in item.Split('/'))
				{
					if (parent.ContainsKey(it2))
						parent = parent[it2].Nodes;
					else
						parent = parent.Add(it2, it2).Nodes;
				}
			}
		}

		private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			button1.Enabled = e.Node.Nodes.Count == 0;
		}

		private void treeView1_DoubleClick(object sender, System.EventArgs e)
		{
			if (button1.Enabled)
			{
				DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		public string SelectedPath => treeView1.SelectedNode.FullPath;
	}
}
