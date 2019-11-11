using ff14bot.Managers;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ManualPlugin
{
    public partial class ManualSettings : Form
    {
        private Dictionary<uint, string> ManualDict;

        public ManualSettings()
        {
            ManualDict = new Dictionary<uint, string>();
            InitializeComponent();

            UpdateManual();

            if (InventoryManager.FilledSlots.ContainsManualitem(Settings.Instance.Id))
            {
                ManualDropBox.SelectedValue = Settings.Instance.Id;
            }
        }

        private void ManualDropBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Instance.Id = (uint)ManualDropBox.SelectedValue;
            Settings.Instance.Save();
        }

        private void ManualDropBox_Click(object sender, EventArgs e)
        {
            UpdateManual();
        }

        private void UpdateManual()
        {
            ManualDict.Clear();

            foreach (var item in InventoryManager.FilledSlots.GetManualItems())
            {
                ManualDict[item.TrueItemId] = "(" + item.Count + ")" + item.Name;
            }

            ManualDropBox.DataSource = new BindingSource(ManualDict, null);
            ManualDropBox.DisplayMember = "Value";
            ManualDropBox.ValueMember = "Key";
        }
    }
}
