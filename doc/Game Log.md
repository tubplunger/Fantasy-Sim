# Fantasy-Sim

5/29
For today's goal there were several things I wanted to do: get the project up and going so that it can be started, and set up the messenger network to the point that it is fully working and ready for expansion as needed.

As I started building the system I found that with each piece added on that it worked better and better. Currently, the system works great: there isn't any immediate issues and the whole system works surprisingly quickly. Nothing really broke, but what became difficult was remembering the names of each script as they came up. For just four systems required the creation of 11 scripts with the main issue being that if a script name got misspelled then that would cause issues for other scripts that need it. In fact, that is another issue, if something breaks near the start of the chain then that can break multiple systems.

The complications of this system also finally made me organize thie scripts separately, as leaving them all in a single script folder quickly led to the scripts getting scattered about and hard to find.

While this system is good it does need some streamlining and improvements later. As more events come up some of the events can be added onto existing scripts, though some will necessitate their own scripts. And, considering that four of these events took 11 scripts, it can be theorized that it could quickly get out of hand.

6/1
Today's goal was to create the factions and, if everything goes well in the creation of said factions, to build and expand the political systems for them to react to player actions.

Everything went well. The creation of the factions went pretty quick so I felt good enough to create the system it needs to react to the player. Though this was more tacking on some new scripts and changing around some already created ones for all of this to work. Nothing really broke during this besides mistyping one of the script names which caused a few issues until that was fixed.

Because of the lack of visuals and things needing to go to the console for me to see if it is working I didn't really know it worked until I ran it and got those updates in the console. It worked wonderfully though and how I expected it to. Everything felt clean besides the console logs not coming in the correct order though that is a very minor issue.

What feels brittle to mean and may need improvements is the amount of scripts and their connections. With just this change the number of scripts have ballooned quite a bit and the issue of something breaking in some of the important first scripts and breaking every other script remains an issue for me. I would like to see if I can improve that, though with this project I am not sure if that is possible currently.

6/2
The goal for today was continuing with the political systems though rather than adding anything new it was trying to make some improvements.

From testing that I did yesterday I noticed that the event logs coming to the console were very disorganized and kinda spread out randomly, making it hard to parse through the console to make sure everything is working as it should and just read through it all in general.

It didn't take too long to change the console around to be more organized and it turns out you can do some cool stuff with the console, like changing text color which is one of the things I did for today. Nothing broke with these changes and what they did was noticeable immediately and can easily be changed as the project expands.

For tomorrow, I am planning on improving NPC memories and getting the foundations ready for a reactive world.

6/3
The goal for today was to expand and add to the current NPC Memory system by turning from a list of string into events that can support structured memory data in regards to different things.

This system took a while to build up and set up correctly, needing to account for a few different things in NPC memory and then work in the very basic demo scene. In the end, it works well though some fixes were needed cause emotional impact in NPCMemory didn't want to take negative values, which the system needs to for bad memories, so that had to be adjusted to actually work.

Once that was fixed everything was able to work properly, though for tomorrow some things still need to be tested within the new NPC system and the next few days will be spent on making sure it is working properly.

6/4
The goal for today was rather simple in that the plan was to add a bit more to the debugging so the NPC Memory system is easier to test. Along with a minor change to the memory system itself.

Memories were given a print debug along with a key to clear memories so that you don't have to exit the inspector each time to continually test it. Companion NPC now also reacts to betrayals and there is a new query for a strongest memory about an actor.