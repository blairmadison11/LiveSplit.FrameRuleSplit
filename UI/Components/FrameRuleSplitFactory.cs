using System;
using LiveSplit.Model;

namespace LiveSplit.UI.Components
{
	class FrameRuleSplitFactory : IComponentFactory
	{
		public string ComponentName => "Frame Rule Split";

		public string Description => "Rounds each split to the nearest frame rule.";

		public ComponentCategory Category => ComponentCategory.Control;

		public IComponent Create(LiveSplitState state) => new FrameRuleSplit(state);

		public string UpdateName => ComponentName;

		public string XMLURL => "";

		public string UpdateURL => "http://livesplit.org/update/";

		public Version Version => Version.Parse("1.0");
	}
}
