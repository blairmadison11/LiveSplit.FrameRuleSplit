using System;
using System.Xml;
using System.Windows.Forms;
using LiveSplit.Model;

namespace LiveSplit.UI.Components
{
	class FrameRuleSplit : LogicComponent
	{
		protected const double FRAME_RULE_MILLISECONDS = 349.424533333339;

		public FrameRuleSplitSettings Settings { get; set; }
		protected LiveSplitState CurrentState { get; set; }

		public override string ComponentName => "Frame Rule Split";

		public FrameRuleSplit(LiveSplitState state)
		{
			Settings = new FrameRuleSplitSettings();
			state.OnSplit += State_OnSplit;
			CurrentState = state;
		}

		public void State_OnSplit(object sender, EventArgs e)
		{
			if (CurrentState.CurrentPhase != TimerPhase.Ended)
			{
				int splitIndex = CurrentState.CurrentSplitIndex - 1;
				Time splitTime = CurrentState.Run[splitIndex].SplitTime;
				TimeSpan? splitRealTime = splitTime.RealTime;
				if (splitRealTime != null)
				{
					double frameRuleSpan = splitRealTime.Value.TotalMilliseconds / FRAME_RULE_MILLISECONDS;
					double roundedRealTimeSpan = Math.Round(Math.Round(frameRuleSpan) * FRAME_RULE_MILLISECONDS);
					CurrentState.Run[splitIndex].SplitTime = new Time(TimeSpan.FromMilliseconds(roundedRealTimeSpan));
				}
			}
		}

		public override XmlNode GetSettings(XmlDocument document) => Settings.GetSettings(document);

		public override Control GetSettingsControl(LayoutMode mode) => Settings;

		public override void SetSettings(XmlNode settings) => Settings.SetSettings(settings);

		public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode) { }

		public override void Dispose()
		{
			CurrentState.OnSplit -= State_OnSplit;
		}
	}
}
