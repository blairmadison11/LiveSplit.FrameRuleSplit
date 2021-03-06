# LiveSplit.FrameRuleSplit
FrameRuleSplit is a component for LiveSplit that rounds each split (except the last split) to the nearest NTSC 21 frame rule (~0.349 seconds). This component is intended for use with games that utilize the frame rule, such as Super Mario Bros. for the NES.

To avoid rounding error propagation, this component uses a high-precision calculation of the frame rule duration.

The formula used to calculate the frame rate of the NTSC console is:<br />
<i>([frequency of CPU] * [# of PPU ticks per CPU cycle]) / (([# of PPU ticks per scanline] * [# of scanlines]) - [odd frame idle skip])</i><br />
<i>(1789772.7272727 * 3) / ((341 * 262) - 0.5) = ~ 60.09881389744</i>

The formula used to calculate the duration of a frame rule is:<br />
<i>[# of frames in frame rule] / [frame rate of console]</i><br />
<i>21 frames / 60.09881389744 fps = ~ 349.42453333334 milliseconds</i>



<h3>To install:</h3>

* Download <b>LiveSplit.FrameRuleSplit.dll</b> from the <a href="https://github.com/blairmadison11/LiveSplit.FrameRuleSplit/releases">releases section</a>.
* Place it into your <b>LiveSplit\Components</b> folder.

<h3>To enable:</h3>

1. Open the <b>Edit Layout</b> dialog.
2. Open the <b>Add Component</b> menu.
3. Open the <b>Control</b> submenu.
4. Select the <b>Frame Rule Split</b> component.
5. Click OK to exit layout settings.

<h3>To disable:</h3>

* Remove the <b>Frame Rule Split</b> component in layout settings.
