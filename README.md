# Fun Race

## Time spent
Apart from this README, I spent 5 hours working on this exercise.

## Parts I found difficult

The first difficulty I found was to implement the path following. I used to use some path editors and beziers libraries from the Asset Store, so to implement it was the first thing it took me some time. In the end, I decided to implement a simple solution lerping between points.

I wasted more time than I would have liked with the collisions with the enemies. Because of me not working with Unity since half a year ago (and even more with Collisions), I spent time trying to figure out why some Collisions were not being triggered, until I remembered the "[Collision action matrix](https://docs.unity3d.com/Manual/CollidersOverview.html)" from the Unity documentation. 

In general, I found myself slower than usual (modeling simple shapes with probuilder, navigating in the unity editor, doing animations, etc), as I didn't use Unity in months.

## Things to improve
The look and feel of the game:
 - Transitions between game states. They are not smooth, and they lack some animations. I also wanted to implement a task scheduler which could also have helped with this, but I didn't have time to do it.
 - The camera. I didn't spend time on it, and I just implemented the simplest camera to follow a target. I could have added some limits to the camera speed and rotation and the camera movements would look smoother.
 - Art and animations. Didn't spend time on it. The look and feel of the game can be improved with some humanoid character, better animations and a better background and global illumination of the scene.
 - Camera position, player speed, enemies behaviour can be improved by tweaking some values, so the player can have a better game experience.

The code:
- Readability of the code in the class LevelPath can be improved as well. I think by storing segments instead of points, the readability would improve significantly
 - The PlayerController reads the input and controls the object. These two concerns must be separated into different components if we want to implement ghosts.
 - I didn't have time to review the whole code, so there might be more things that can be improved and I'm missing here.

## Next Steps
 - Implement ghosts, so you have the feeling of competition.
 - Implement more enemies.
 - Tweak the values of the Player and Enemies, so the game is more enjoyable.
 - Implement a proper UI with good transitions between game states.
 - Implement a more rewarding winning sequence.
 - Implement a level generator from the tiles and some more configuration.
 - Improve the graphics with humanoids animations and metaphors for the enemies.

## My opinion

To be honest, I expected more from myself in this exercise. Even though I enjoyed doing it, I found myself slow using Unity. I noticed the time without using it.

As I mentioned before, there are a lot of things missing in the prototype and I would have liked to either be faster or have more time. I focused on the core gameplay and implementing, at least, one enemy, a level direction changes and a basic game loop. I didn't have time for anything else, so the graphics, transitions, animations are poor.

Regarding the code, I didn't think about the scalability of it, I put myself in prototype mode and I wrote the simplest solutions for the problems I found.

## The project

The project has been developed using Unity 2018.3.14f1 and ProBuilder tool.

It has only one scene that contains the whole game loop and some simple UI.

In terms of code these are the main classes:
 - **GameManager**: It's the manager of the game. It has a reference to all the elements of the game and it is responsible for controlling the main game loop.
 - **EventManager**: Class to forward events to objects that are registered to them.
 - **InputManager**: Controls the mouse input and forwards mouse events using the EventManager.
 - **UIManager**: Attached to the Canvas, it controls the visibility of the main menu and winning screens.
 - **PlayerController**: Class to control the player. It reads the mouse input and controls player movement and collisions.
 - **LevelPath**: Represents the path the player must follow during the level. It gets all the waypoints at the start and provides a method to place a transform in a specific path point.
 - **ZigZagEnemy**: The only enemy I implemented. This class controls the enemy movement.
 - **FollowCamera**: Simple implementation for a camera to follow a target.
