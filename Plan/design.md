# Game Design Document
## Game Analysis
Arvy's Apples (I haven't really decided on a name yet) is a 3D survival game in which the player has to perform tasks in an apple orchard Test while staying alive.

## Mission Statement
You apply to an apple orchard for work when you discover that there's more to the application process than a paper and an interview. Strange things start happening as you try to survive in a Test apple orchard as time goes on.

## Genre
This is a survival RPG game (very closed-world).

## Platforms
PC

## Target Audience
This game is designed for bored gamers who want to experience an interesting gameplay.

## Storyline & Characters
The player applies to work at an apple orchard, Arvy's Apples, but he/she didn't know that, to get accepted, you must succeed in a Test orchard environment. You must fill five crates with apples, while staying alive. Aaron Arvy is the owner of the orchard who set up the Test.

## Gameplay
### Overview
This game is an adventure RPG game that is currently only for singleplayer. The game is built for PC. The key gameplay features are its eccentricity and its storyline.

### Player Experience
You start out in an empty apple orchard with nothing but a note and five empty crates. You find that you are in a trial to show your future employer that you can handle an apple orchard. You must stay alive by eating apples and drinking water from the river, and avoiding giant green caterpillars.

### Gameplay Guidelines
Principles:
- Make things weird. Make as much weird content as possible.
- Maintain a balance between chance and skill.
- Force player to figure it out.

### Game Objectives & Rewards
As this is a fairly simple game (though it's detailed), there are no objectives or rewards other than beating the game.

### Gameplay Mechanics
To win, the player must place eight red apples in each of five crates. The player has three health gauges; literal health, hunger and thirst. There are three types of apples; red apples (for restoring fullness and storing in crates), yellow apples (for restoring health) and green apples for a random choice of a predefined set of outcomes.

As time progresses, his/her food level decreases, and his/her quenched level decreases twice as fast. To increase his/her food level, the player must either eat red apples (which will always increase his/her food level) or eat a green apple with hopes of receiving the randomly-selected outcome of health refill.

Caterpillars hurt the player when colliding with him/her quickly. However, their large size makes them clumsy in the orchard.

## Character Attributes
Player abilities: navigate, eat apples, drink water, put apples in crate.

## Level Design
There is only one level, the Test orchard. This orchard is on a small square of terrain the middle of a void (I know, I might change it later on). It almost 100 trees, a river, and inhabitants such as giant green caterpillars.

## Control Scheme
The player can move with WASD, look around with mouse movement, and sprint with _left shift_. He/she can left-click to pick up an item and right-click to use it.

## Game Aesthetics & User Interface
The game is low-poly, and the UI is standard.

## Schedule and Tasks
Core
- ADD
  - Credits
  - Options, and customizable controls or just a control help page
  - Make box completion icons progress bars
- FIX
  - Throwing paper directly down causing it to glitch through the floor, and (rarely) apples fall through the floor
  - Pause menu cursor
  - "press any key" responding immediately and bypassing check (fixed?)
  - Continuous jumping when falling on (edge of?) crate
- CHANGE
  - Internal: make Util, etc. classes with static methods rather than empties
Cosmetic / Extra
- ADD
  - Re-update stems to apple models?
  - More with green apples
    - Strange shaders
    - Strange mobs
  - Shaders (for weird FX)
  - Music (horror soundtrack?)
  - Extra mobs (butterflies?)
  - Mushrooms & flowers
  - Flash low health
