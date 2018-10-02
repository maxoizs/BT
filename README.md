# BT
Battleship game, to play with dotnet core, actually it took me while to figure out, how to use vscode, while dotnet code wasn't that hard to get around as it's pretty much the same as .net stander, I used to take things like debug and build for granted while using Visual Studio, now I've to look after the debugger and the builder's json files. 


## Requirement 
You need to have .NetCore SDK installed with VSCode or Visual Studio 

## Build
To build application you could so using `dotnet build`  which will build and restore dependencies and if you want ot restore dependencies only you could use `dotnet restore` 

Alternatively you can use Visual Studio and hit Ctrl+Ship+B

## Run 
You could run the game by calling run on the project file using `dotnet run BS\BS.csproj`

Alternatively you can use Visual Studio and hit F5

## Test
Not fully tested :( !!!
Finally if you want to run the unitests use `dotnet test`
Alternatively you can use Visual Studio and right click on test project and chose `Run Tests`



## TODO
Wire a IoC lib to aid us add more unit tests, especially to test game and player
