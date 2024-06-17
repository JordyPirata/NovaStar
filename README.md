# Game Project

An immersive exploration and crafting game developed in Unity.

## Table of Contents
1. [Module Descriptions](#module-descriptions)
2. [User Descriptions](#user-descriptions)
3. [Features and Functions](#features-and-functions)

## Module Descriptions

### Main Menu Module
The Main Menu is the initial screen displayed to the player upon starting the game. It contains all the necessary options to begin, configure, and personalize the gaming experience.

### Game Module
The core component where the player experiences the game. In this module, the player explores the world, collects materials and items to enhance skills and powers, and obtains better materials and items through laboratories. It includes events such as day and night cycles, rain, and ambient music.

### Inventory and Fusion Module
This module allows players to store and create new objects. It serves as a tool for interacting with collected items, which provide PowerUps or special abilities.

### HUD Module
The Heads-Up Display (HUD) is a graphical overlay that provides important information to the player in a visual and easily accessible manner during gameplay.

### Pause Module
This module offers options to save the game, exit, and resume later.

## User Descriptions

### Player User
This user has access to all game modules and is the primary target of the gaming experience. Players can access the main menu to start the game, choose or create maps, and use various tools to monitor their health and improve their abilities through the HUD and inventory/fusion system.

## Features and Functions

### Main Menu Module
The Main Menu includes:
- **Start**: Displays a brief video and offers game settings, play button, and exit button.
- **General Settings**: Adjust general volume, music volume, language, and mouse sensitivity.
- **Map Selection**: A list of the player’s saved games. Options include creating a new game, renaming, playing, or deleting an existing one. Players can save up to 5 games simultaneously.
- **Map Customization**: An interactive map allows users to modify world size, temperature, and humidity ranges, affecting biome appearance.

### Game Module

#### Physics System
A character in Unity with jumping, gliding, and flying abilities using Rigidbody and Collider components to simulate physical interactions and collision detection. Animations and Raycasting ensure balanced movement and terrain detection. Visual and sound effects enhance the experience.

#### Terrain Generation
- **Generation Algorithm**: Procedural world creation using Perlin Noise for detailed and coherent terrain.
- **Biome Generation**: Assigns biomes based on humidity, height, and temperature using Robert Whittaker’s classification.
- **Mesh Generator**: Creates the 3D structure of the map, allowing textures, physics, and collisions.
- **Texture Algorithm**: Assigns textures to the mesh based on biome type.
- **Chunk Algorithm**: Manages and loads environment elements efficiently by dividing the world into smaller "chunks".
- **Seed**: An initial value for unique world generation, allowing players to share and explore the same worlds.

#### Resource Distribution and Vegetation Generation
Determines the location and quantity of natural resources and vegetation based on environmental factors.

#### Debuffs
- **Heat**: Causes dehydration and health damage.
- **Heatstroke**: Severe heat exposure leading to disorientation and fatigue.
- **Fatigue**: Reduces speed and agility.
- **Cold**: Causes health loss and fatigue.
- **Hypothermia**: Severe cold exposure leading to significant health loss.
- **Hunger**: Reflects lack of food, causing fatigue and slower health regeneration.
- **Dehydration**: Affects negatively when hydration levels are dangerously low.
- **Disorientation**: Visual dizziness effect.
- **Fainting**: Temporary unconsciousness, preventing any action.
- **Death**: Respawn at teleport points or the main spawn point, with no material loss.

#### Skill and Power Improvements
Players can enhance skills and powers by gaining experience, leveling up, and finding special items:
- Speed
- Flying
- Gliding
- Detect nearby materials
- Know temperature and humidity
- Know time of day
- Teleportation

#### Material and Item Acquisition
Players collect materials and items during exploration to craft objects, improve equipment, and progress in laboratories.
