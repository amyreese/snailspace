*** SNAIL'S PACE ***
     Readme.txt

Overview
========

Snail's Pace is built on Microsoft XNA Game Studio 2.0, with LuaInterface 1.5.3,
a C# wrapper for Lua 5.1, used for scripting levels, enemies, AI, and weapons.


Project Structure
=================

Config/
- Inputs and outputs configuration settings to files, game queries these classes
  for configuration variables.

Core/
- Contains core game components, including the engine, renderer, input manager,
  sound manager, player, and Lua handler components.

Documents/
- Contains design and reference documents.

Lua/
- Common Lua libraries used by maps and characters. Includes AI, math, weapons,
  and world construction scripts.

Maps/
- Contains each level in it's own subdirectory. Each level is then broken up into
  a number of scripts that define different level segments.
- The master script in the root of the map directory defines player starting position,
  background music, and sub-scripts that make up this level.
- From there, scripts are divided into enemies, scenery, and triggers. The Enemies/
  directory defines all the enemies in the level, Scenery/ defines the environment,
  and Triggers/ specifies where weapon pickups, powerups, and camera and audio changes
  should be triggered.

Objects/
- Contains the GameObject class, which is our simple abstraction of a thing our engine
  handles, as well as all classes that extend this class, and all the subclasses that
  are used by GameObject, such as our custom Sprite class, and GameObjectBounds, our
  bounding box class.

Resources/
- Contains sounds. music, textures, and effects.

Screens/
- Contains the various "screens" used by our engine. GameScreen is the screen used
  while playing, other screens exist for menus, loading, and displaying score.


Credits
=======

Joe Andrusyszyn	- Lead engine development, Tree Fort, Queen's Den, and Credits level 
		  design, environment graphics, scoring.

Pat Dobson	- Chloride Conundrum level design, character graphics, enemy AI,
		  scoring.

Josh Gruenberg	- Engine development, environment graphics, enemy weapons design,
		  HUD, level balancing.

John Reese	- Lua development, sound development, weapons design, level balancing,
		  input handling, Credits level design, various graphics, physics.

Brian Schroth	- Camera movement, collision detection optimization, scoring.