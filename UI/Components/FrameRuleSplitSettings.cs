using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.UI.Components
{
	public partial class FrameRuleSplitSettings : UserControl
	{
		public FrameRuleSplitSettings()
		{
			InitializeComponent();
		}

		public XmlNode GetSettings(XmlDocument document)
		{
			var parent = document.CreateElement("Settings");
			CreateSettingsNode(document, parent);
			return parent;
		}

		public void SetSettings(XmlNode node)
		{
			var element = (XmlElement)node;
		}

		private int CreateSettingsNode(XmlDocument document, XmlElement parent)
		{
			return SettingsHelper.CreateSetting(document, parent, "Version", "1.0.0");
		}
	}
}
