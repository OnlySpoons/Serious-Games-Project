# Serious-Games-Project
 
### Change Log:

Change Log - 04/01/2022
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

Change Log - 03/01/2022
- Initial commit
- Implemented game scene
	- Input console using Roslyn to compile at runtime
		- Not working currently!
	- Output console to display errors/output from runtime scripts
		- Not working currently!
	- Objectives, question number and score text boxes to track stats
	- Run button to compile scripts at runtime 
	- Hint button to display up to 3 hints per question 