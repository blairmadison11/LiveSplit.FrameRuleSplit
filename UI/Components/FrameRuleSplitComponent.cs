using System;
using System.Xml;
using System.Windows.Forms;
using LiveSplit.Model;

namespace LiveSplit.UI.Components
{
	class FrameRuleSplit : LogicComponent
	{
		// Number of milliseconds in a single NTSC frame rule
		protected const double FRAME_RULE_MILLISECONDS = 349.42453333334;

		// The equation to calculate the frame rate of the NTSC console is:
		// ([frequency of CPU] * [# of PPU ticks per CPU cycle]) / (([# of PPU ticks per scanline] * [# of scanlines]) - [odd frame idle skip])
		// (1789772.7272727 * 3) / ((341 * 262) - 0.5) = ~ 60.09881389744

		// The equation to calculate the length of a frame rule is:
		// [# of frames in frame rule] / [frame rate of console]
		// 21 frames / 60.09881389744 fps = ~ 349.42453333334 milliseconds


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
				TimeSpan? timeElapsed = CurrentState.Run[splitIndex].SplitTime.RealTime;
				if (timeElapsed != null)
				{
					double frameRulesElapsed = timeElapsed.Value.TotalMilliseconds / FRAME_RULE_MILLISECONDS;
					double timeElapsedRoundedToFrameRule = Math.Round(Math.Round(frameRulesElapsed) * FRAME_RULE_MILLISECONDS);
					CurrentState.Run[splitIndex].SplitTime = new Time(TimeSpan.FromMilliseconds(timeElapsedRoundedToFrameRule));
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
