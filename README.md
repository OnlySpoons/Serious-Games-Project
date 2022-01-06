# Serious-Games-Project
 
### Change Log:

Change Log - 06/01/2022 - Thomas
- Save volume between sessions using PlayerPrefs

Change Log - 05/01/2022 - Thomas 
- Refactored UI for the whole project
	- Gave the main menu and options menu its own background image in the main menu scene
	- Changed game title 
	- Changed various panel images to be more appropriate
	- Changed almost every button/text colour

Change Log - 05/01/2022 - Martin 
- Added slides panel
	- Previous, next and finish buttons for navigating slide shows
- Refactored objective scripts 
- Fixed various bugs with scripts

Change Log - 04/01/2022 - Thomas & Martin
- Added main menu scene 
	- Start game button 
	- Options menu button
		- Opens options canvas and hides menu canvas 
		- Resolution and quality dropdowns
		- Volume slider
		- Fullscreen checkbox
	- Quit game button (stops editor when running in editor)
- Added objective complete panel to game scene 
	- Shows stats before transitioning to the next objective 
- Added pause and options menus to the game scene 
- Fixed UI scaling bugs across main menu and game scenes 
- Fixed issues with input and output consoles not working correctly
- Fixed issue with fullscreen settings not saving between application sessions
	- Volume intentionally doesn't save

Change Log - 03/01/2022 - Thomas
- Initial commit
- Implemented game scene
	- Input console using Roslyn to compile at runtime
		- Not working currently!
	- Output console to display errors/output from runtime scripts
		- Not working currently!
	- Objectives, question number and score text boxes to track stats
	- Run button to compile scripts at runtime 
	- Hint button to display up to 3 hints per question 