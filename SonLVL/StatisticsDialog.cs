using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SonicRetro.SonLVL
{
	public partial class StatisticsDialog : Form
	{
		public StatisticsDialog()
		{
			InitializeComponent();
			listView1.ListViewItemSorter = new ListViewColumnSorter();
			listView2.ListViewItemSorter = new ListViewColumnSorter();
			listView4.ListViewItemSorter = new ListViewColumnSorter();
		}

		private void StatisticsDialog_Load(object sender, EventArgs e)
		{
			Dictionary<int, int> counts = new Dictionary<int, int>();
			foreach (var item in LevelData.ObjTypes)
				counts.Add(item.Key, 0);
			foreach (ObjectEntry item in LevelData.Objects)
				if (counts.ContainsKey(item.ID))
					counts[item.ID]++;
				else
					counts.Add(item.ID, 1);
			listView1.BeginUpdate();
			foreach (KeyValuePair<int, int> item in counts)
			{
				ListViewItem lvi = new ListViewItem(item.Key.ToString("X2"));
				lvi.SubItems[0].Tag = item.Key;
				lvi.SubItems.Add(new ListViewItem.ListViewSubItem(lvi, item.Value.ToString()) { Tag = item.Value });
				listView1.Items.Add(lvi);
			}
			listView1.Sort();
			listView1.EndUpdate();
			counts.Clear();
			for (int i = 0; i < LevelData.NewChunks.chunkList.Length; i++)
				counts.Add(i, 0);
			for (int y = 0; y < LevelData.FGHeight; y++)
				for (int x = 0; x < LevelData.FGWidth; x++)
					if (counts.ContainsKey(LevelData.Scene.layout[y][x]))
						counts[LevelData.Scene.layout[y][x]]++;
					else
						counts.Add(LevelData.Scene.layout[y][x], 1);
			for (int layer = 0; layer < 8; layer++)
				for (int y = 0; y < LevelData.BGHeight[layer]; y++)
					for (int x = 0; x < LevelData.BGWidth[layer]; x++)
						if (counts.ContainsKey(LevelData.Background.layers[layer].layout[y][x]))
							counts[LevelData.Background.layers[layer].layout[y][x]]++;
						else
							counts.Add(LevelData.Background.layers[layer].layout[y][x], 1);
			listView2.BeginUpdate();
			foreach (KeyValuePair<int, int> item in counts)
			{
				ListViewItem lvi = new ListViewItem(item.Key.ToString("X2"));
				lvi.SubItems[0].Tag = item.Key;
				lvi.SubItems.Add(new ListViewItem.ListViewSubItem(lvi, item.Value.ToString()) { Tag = item.Value });
				listView2.Items.Add(lvi);
			}
			listView2.Sort();
			listView2.EndUpdate();
			counts.Clear();
			for (int i = 0; i < LevelData.NewTiles.Length; i++)
				counts.Add(i, 0);
			for (int i = 0; i < LevelData.NewChunks.chunkList.Length; i++)
				for (int y = 0; y < 8; y++)
					for (int x = 0; x < 8; x++)
						if (counts.ContainsKey(LevelData.NewChunks.chunkList[i].tiles[y][x].tileIndex))
							counts[LevelData.NewChunks.chunkList[i].tiles[y][x].tileIndex]++;
						else
							counts.Add(LevelData.NewChunks.chunkList[i].tiles[y][x].tileIndex, 1);
			listView4.BeginUpdate();
			foreach (KeyValuePair<int, int> item in counts)
			{
				ListViewItem lvi = new ListViewItem(item.Key.ToString("X3"));
				lvi.SubItems[0].Tag = item.Key;
				lvi.SubItems.Add(new ListViewItem.ListViewSubItem(lvi, item.Value.ToString()) { Tag = item.Value });
				listView4.Items.Add(lvi);
			}
			listView4.Sort();
			listView4.EndUpdate();
		}

		private void listView_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			ListView list = (ListView)sender;
			ListViewColumnSorter sort = (ListViewColumnSorter)list.ListViewItemSorter;
			if (sort.Column == e.Column)
				sort.Order = sort.Order == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
			else
			{
				sort.Column = e.Column;
				sort.Order = e.Column == 0 ? SortOrder.Ascending : SortOrder.Descending;
			}
			list.Sort();
		}
	}

	public class ListViewColumnSorter : System.Collections.IComparer
	{
		public int Column { get; set; }
		public SortOrder Order { get; set; }

		public ListViewColumnSorter()
		{
			Column = 1;
			Order = SortOrder.Descending;
		}

		public int Compare(object x, object y)
		{
			ListViewItem it1 = (ListViewItem)x;
			ListViewItem it2 = (ListViewItem)y;
			int result = ((int)it1.SubItems[Column].Tag).CompareTo(it2.SubItems[Column].Tag);
			return Order == SortOrder.Ascending ? result : -result;
		}
	}
}
