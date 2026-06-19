# Fantasy-Sim

1. Architecture Overview:

The main principle of Fantasy Sim was to create a simulation that avoids direct dependencies between its systems. Instead of things like QuestSystem modifying NPCs, NPCs modifying factions, and factions modifying quests the project uses Event Busses. A system publishes an event and other systems react independently to it.

An example being that the Player steals bread: An event for this is published which causes NPCMemorySystem to react, along with RelationshipSystem, FactionPoliticsSystem, and Market behavior. The Timeline then records all of the reactions that happened. 

The core infrastructure was separated out as the game progressed, these being: EventBus, World Events, Simulation Timeline, Replay System, Debug Overlay, and World-State Inspector. All split out and reponsible for communicating, logging, replay, and observability.

For NPCs they had NPCMemorySystem, RelationshipSystem, and UtilityAISystem which is responsible for memomry, emotional state, autonomous decisions, and goal evaluation. Allowing NPCs to react dynamically based on the simluation state.

World simulation had FactionPoliticsSystem, ShortageSystem, MigrationSystem, RoadDangerSystem, and FactionConflictSystem. Which created large scale pressures within the world and political and economic states for the world to be in.

Narration had DynamicQuestSystem and QuestResolutionReactionSystem. Creating quests, consequences within the world, and lifecycles for said quests. Allowing quests to emerge from the simulation rather than triggers.

It was made sure that each system is the owner of its own data. RelationshipSystem owned relationship values, NPCMemorySystem owned memories, FactionPoliticsSystem owned faction reputation, and DynamicQuestSystem owned quest generation and state. 

Other systems requested changes as needed through events rather than going and editing this data directly. Keeping responsibilities separated and side effects minimal.

2. Emergent Story Demo:

Using the Player stealing bread again. NPC Memory reacts to the event with the Baker remembering the theft, the Guard remembering the crime, and the Companion disapproving of this action. Relationship change from this, trust decreases, suspicion increases, fear may also increase. Faction Politics change with the Merchants reputation decreasing, and town guard hostility increasing. Economy and World Pressure reaction with marking instability rising, bandit activity increasing, road danger increasing, food shortages occuring.

Migration then occurs with refugees fleeing. The Dynamic Narrative creates several quests from this, and finally NPCs independently evalute things with fear, revenge, greed, loyalty, or survival. Leading to outcomes like the Baker seeking revenge, or the guard protecting their allies.

3. Debugging and Observability Tools:

With the lack of visuals within the game, a major goal was to make the entirety of the simulation state observable and easy to view for everyone. This resulted in the overlay with all of those tools that you should see when starting up the game.

The main thing within the Overlay is the Event Timeline that records all of the world events in chronological order as things happen. There is also a Replay System that can go back through the full simulation history and recent events, which allows for debugging of emergent events though it does change outcomes.

The Debug Overlay itself shows recent events, active controls, and simulation state. Making large simulations easier to go through without relying on console logs to look through everything. The World-State Inspector shows shortages, refugees, road danger, faction conflicts, quests, and NPC goals to inspect the current state of the simulation.

4. Engineering Retrospective:

Something that failed within the game was the use of hardcoded IDs that emerged with some of the early systems, which became hard to maintain and change as things expanded outward. In retrospect, I should've used either scriptable objects or JSON configs, which would have saved me some headaches.

Keybind Overload also became an issue. As more systems were added to allow the ability to demo the project, the number of input keys became difficult to manage. This requred the overlay, control list, and world inspector to somewhat fix.

Event explosions happened often as systems became interconnected with each other, with single actions creating cascading effects that made debugging difficult until the overlay and timeline and replay tools were added in.

Only two things really became difficult to deal with within the game. The biggest one being complexity of the simulation and keeping everything balanced between system independence and world interaction. Too little interaction and the world feels dead, but too much and the simulation becomes chaotic and difficult to manage.

The second was the stability of the AI. When starting with the NPCs AI they initially changed goals way too frequently, with goals changing multiple times in a few seconds. This was fixed with goal inertia and commitment, making NPCs way less mercurial.

Only two things were refactored within the game. Originally, with FactionReputationChangedEvent, systems interacted with it without updating faction state. This was refactored with FactionPoliticsSystem processing the change and then the actual state updating.

The second was NPC AI, again. It originally only selected goals, but was expanded to include commitment, peristence, execution, and debugging. Making NPC behavior feel more autonomous in the end.

For Observability, it originally relied on console logs, which was fine at the time though it evolved to the existing overlay and debugging system.

If I were to come back and make some future improvements there a few I would like to do. The main one being to make the NPCs, factions, locations, and quests into JSONs or scriptable objects. But also, further improvements to the NPCs and their AI to include things like personality traits, social networks, group behavior and so on.

A more dynamic economy would also be an improvement that I would like to make. Adding actual supply chains, regional prices, trade routes, and even resource scarity though I suppose the skeleton of all of that is somewhat there.