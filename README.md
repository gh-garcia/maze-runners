
            ███╗   ███╗ █████╗ ███████╗███████╗                          
            ████╗ ████║██╔══██╗╚══███╔╝██╔════╝                          
            ██╔████╔██║███████║  ███╔╝ █████╗                            
            ██║╚██╔╝██║██╔══██║ ███╔╝  ██╔══╝                            
            ██║ ╚═╝ ██║██║  ██║███████╗███████╗                          
            ╚═╝     ╚═╝╚═╝  ╚═╝╚══════╝╚══════╝                          
            ██████╗ ██╗   ██╗███╗   ██╗███╗   ██╗███████╗██████╗ ███████╗
            ██╔══██╗██║   ██║████╗  ██║████╗  ██║██╔════╝██╔══██╗██╔════╝
            ██████╔╝██║   ██║██╔██╗ ██║██╔██╗ ██║█████╗  ██████╔╝███████╗
            ██╔══██╗██║   ██║██║╚██╗██║██║╚██╗██║██╔══╝  ██╔══██╗╚════██║
            ██║  ██║╚██████╔╝██║ ╚████║██║ ╚████║███████╗██║  ██║███████║
            ╚═╝  ╚═╝ ╚═════╝ ╚═╝  ╚═══╝╚═╝  ╚═══╝╚══════╝╚═╝  ╚═╝╚══════╝
This repository hosts the code and resources for a first-year college project: a maze game developed in C#. Created as part of a Programming course, the project demonstrates foundational programming skills in C# and explores concepts in game design and logic.

## Demo

![Demo of the Game](./alpha/DEMOX.gif)

Welcome to **Maze Runners**! Navigate through a dynamically generated maze, avoid traps, and race to the exit before your opponent does.

## 🎯 Objective
Your goal is to escape the maze by reaching the **exit** located in the top-left corner. But beware! The maze is filled with traps like **Darts**, **Sand**, and **Wizards** that can slow you down or harm you. Use your skills wisely to outsmart your opponent and claim victory!

Good luck, and may the best maze runner win! 🏃‍♂️🏃‍♀️



# 🎮 Game Controls

- **⬆️⬇️⬅️➡️ Arrow Keys**: Move the player in the maze  
- **R**: 🎲 Roll the dice  
- **1️⃣ 2️⃣ 3️⃣ 4️⃣ 5️⃣**: Select the token  
- **⏎ Enter**: ✅ Confirm selection or action  
# Prerequisites
Ensure you have the following installed on your system:
  
- [.NET SDK](https://dotnet.microsoft.com/en-us/download/dotnet)

To check if .NET is installed, open a terminal or command prompt and run:
dotnet --version
If you see a version number, .NET is installed.

# Running the Game
Follow these steps to run your .NET console game:

1. Navigate to the alpha\bin\Debug\net8.0\ folder 
2. Locate alpha.exe file and run it.


# Timeline and Features

| Task Name                     | Status          | Importance | Notes                                                                 |
|-------------------------------|-----------------|------------|-----------------------------------------------------------------------|
| Static Board                  | <span style="color:green">Done</span> | Low        |                                                                       |
| Player Moving/In Bounds       | <span style="color:green">Done</span> | High       |                                                                       |
| Wall collision                | <span style="color:green">Done</span> | High       |                                                                       |
| Two players moving            | <span style="color:green">Done</span> | High       |                                                                       |
| Champions and Champion selection | <span style="color:green">Done</span> | High     |                                                                       |
| Turns                         | <span style="color:green">Done</span> | High     |                                                                       |
| Dice throws                   | <span style="color:pink">Finally done</span> | High     | At the beginning of each turn, the player rolls the dice. When they finish their moves, a message appears saying that their turn is over and the other player must roll. |
| Maze Generation Algorithm     | <span style="color:pink">Finally done</span> | High       | Depth First Search (considered Prim)                                  |
| Health/Player death           | <span style="color:green">Done</span> | Low        | Player dies when its health reaches 0, respawns in the center square  |
| Win condition                 | <span style="color:green">Done</span> | High       | The exit is in the top-left corner. The players start in the center, and the goal is to reach the exit. |
| Basic UI                      | <span style="color:green">Done</span> | Low        |                                                                       |
| Traps             | <span style="color:green">Done</span> | High     | **Dart** - Player health is decreased by 1<br>**Sand** - Player loses the rest of its turn<br>**Wizard** - Player is teleported to the other player's position |
| Randomize traps and objects   | <span style="color:green">Done</span> | Medium     |                                                                       |
| Skills and Cooldowns          | <span style="color:green">Done</span> | High       | **Vi: Lucky Charm** - There is a 1 in 1000 chance to be teleported to the exit<br>**Calibre: Mercy** - There is a 50% chance that if health reaches 0, a health point is granted<br>**Jayce: Heal** - Gains +1 health every 5 turns<br>**Viktor: Sprint** - Gains +1 to dice throw every 5 turns<br>**Jinx: Tap Disarm** - Can disarm all traps |


# 🔗 Useful Links

- **[Maze Generation Algorithms](https://en.wikipedia.org/wiki/Maze_generation_algorithm)**: Explore various algorithms used for generating mazes, including Depth-First Search, Prim's algorithm, and more.  
- **[Depth-First Search (DFS)](https://en.wikipedia.org/wiki/Depth-first_search)**: Learn more about the Depth-First Search algorithm, which is used for maze generation in this project.  
- **[ASCII Art Maze Generator](https://www.asciiart.eu/ascii-maze-generator)**: Explore ASCII art mazes for inspiration or reference in maze design.  
