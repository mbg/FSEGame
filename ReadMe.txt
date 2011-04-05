============================================
== FSEGame ReadMe
============================================

This .zip file contains all files required
to compile, run and distribute FSEGame.

All source files belonging to the game itself
are contained within the FSEGame folder. Here
is an overview of the directory layout:

\ 				- Solution Directory
\FSEGame\ 			- Game/Engine Project Directory
\FSEGame\FSEGame\		- Game/Engine Source Code
\FSEGame\FSEGameContent\	- Game Asset Directory
\Debug\				- Debug Output Directory (contains dependencies)

The following tools are included, but were not
primarily written for this coursework:

\PatchGen\ 			- Patch File Generator
\Autopatcher\			- Deployment Tool
\CDE-Bin.zip			- Dialogue Editor (opens/creates .dlg files and exports them to .xml)

== Version Control =========================

A copy of the source code aand game assets as 
well as all previous revisions may be obtained 
from http://github.com/mbg/FSEGame/

== Source Code =============================

The source code is organised in the following
directories:

\ 				- Game Code
\Actors\ 			- Game Actor Classes
\BattleSystem\			- Game Battle System Classes
\Engine\			- Engine Classes
\Engine\Dialogues\		- Dialogue Engine Classes (minimal implementation)
\Engine\Effects\		- Shaders
\Engine\UI\			- UI Classes

=== Important Classes ======================

The 'GameBase' (\Engine\GameBase.cs) class is the 
main engine class which is inherited by the 'FSEGame' 
class (\FSEGame.cs) which in turn is the main game 
class. The 'Program' class (\Program.cs) contains the
entry point. 