#Battleship State Tracker

#Problem statement 
The task is to implement a Battleship state tracking API for a single player that must
support the following logic:
• Create a board
• Add a battleship to the board
• Take an “attack” at a given position, and report back whether the attack
resulted in a hit or a miss.
The API should not support the entire game, just the state tracker. No graphical
interface or persistence layer is required. 

#setting up project environment

BattleShip game in .Net CORE 5.0 using c#
# Requirements
- Visual Studio 
- .Net CORE 5.0
- C#
# Structure
- Solution contains 4 projects 
- BattleShipStateTrackerModels : class library contains different data models.
- BattleshipStateTrackerServices : class library contain service methods to handle business logic. 
- BattleshipStateTrackerAPI - • ASP.NET CORE Web API contain controller with  method to Create a board , Add a battleship to the board and Take an “attack” at a given position
                              with swagger support to test 
                              • uses session to save game state 
                              
- BattleshipStateTrackerServicesTests - the Unit test Project using MSTest 

# Building
- Open Visual Studio ->Right click on BattleshipStateTrackerAPI ->Properties ->Build tab -> Output section -> select "XML Documentation file"
                       This will generate swagger UI Documentation based on XML comments
- Build solution -> Build -> Build All 
                        Make sure package dependancies Swashbuckle.AspNetCore and Swashbuckle.AspNetCore.Annotations have installed
- Right click on BattleshipStateTrackerAPI project and click on "set as start up proect" 

# Execution
-- Open Visual Studio -> Run -> Start Without Debugging
This will launch http://localhost:25304/swagger/index.html  , this is swagger UI with documentation related to each API methods and model structure 
to generate documentation click on link /swagger/v1/swagger.json  this will generate API json based document.

# Testing
- Open BattleshipStateTrackerServicesTests project -> open Test Explorer -> Run all tests


                        
