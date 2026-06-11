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

6/5
The goal for today was to start adding in the code for NPC relationships, letting not only factions react to player actions but NPCs as well.

The system took a bit to get working, since it needed to interact with a few other systems like faction reputation and there was a bug that needed to be worked out with one of the scripts. Though once that bug got fixed and the scripts and events were communication as they should everything worked quite well. The bug being that merchants would just increase prices no matter what and they had to be told that they can't keep increasing their prices, to their chagrin.

For tomorrow, I will be looking at some minor improvements to this new system. As there are some things that I believe I have missed or overlooked. Just need to go over everything again and make sure I haven't missed anything.

6/8

The goal for today was mostly to add in some improvements and some fixes to the relationship systems added in last week.

Most of the current systems work well so they didn't need to be changed too much, the biggest use being that a ChangeRelation function would constantly fire even if a relationship hadn't been changed, so a check was added to it to make sure it is firing correctly.

Along with that, companions now check relationships as well instead of just memories, along with a clear method to reset relationships as well as the memories of the NPCs. A constants file was also added so that the process of replacing of hardcoded strings with these values can begin though it will likely be slow.

6/9

The goal for today is likely the biggest one and also the one that will take the longest for this project, but it was getting the system for dynamic quests down and in a working state.

I am glad to say that it is working well. Because of the amount of different quests that will be here I decided to just start with the bones and one of the quests that can come up, just so that it is shown to be working and is ready to be expanded later. The one quest that can come up is a bandit attack which depends on a few conditions: refugees arrived in a place that makes sense, the refugee count is high enough, a protection quest already exists, and then it can generate said quest.

Another check is severity of the quest and its consequences, which in this one is the refugees and their numbers and effects on merchant pricing. This can be easily expanded and already there are plans to add conditions for war, factions, resources, crime, migration pressure, and political instability. For now though, this is a good spot.

6/10

The goal for today was to expand the current dynamic quest system to allow it to take in more conditions and respond well to multiple different world pressures at once.

There is still only the bandit attack quest, but with the new conditions it responds to each pressure and generates a quest for it: Shortage becomes relief quest, refugees becomes protection quest, stolen supplies becomes recovery quest, road danger becomes caravan escort quest, and faction conflict becomes diplomacy quest. All from one event that can happen in the fantasy sim!

If one of these events already exists the game will take note of it and discard it so that duplicates don't appear. Also, clearing quests is now allowed for testing. For tomorrow, expansion on the dynamic quest system will continue!

6/11

For today, the goal was for the world to react further to quests that are generated and for quests to have multiple statuses that can be shown to the player.

The first thing was to add completion status to quests: active, completed, failed, and expired. Since this is the big foundation that everything else for today needs for it to function. Importance on the quest was then added: low, medium, high, and urgent. And finally, resolutions.

This is all so that the world can be more reactive with these quests that come up, with resolutions effecting other events in the world beyond. Like how factions see you and if the world gets better or worse depending on what happens.

Tomorrow, the focus is going to be on making improvements to the system as it is in a good spot now.