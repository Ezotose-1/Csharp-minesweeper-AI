# Minesweeper AI
C# Console Mines game with Artificial Intelligence to resolve it.



## Installation
* Download the file thank's to the github page. Or clone the project with the git link :
```shell
git clone https://github.com/Ezotose-1/Mines.git
```



## Utilisation
### Manual play
At the start of the game you have to choose playing or let the bot play. To manually play press 'N', '0' or 'false' 
```console
Auto Play : (N/Y)
> N
```
This script is a console program using the C# NET Core language. You can use it by following the instructions of the input.  
These type of menuing is used : 
```console
Enter a letter X :
> A
Enter a position Y :
> 0
```
Just enter the letter of the column and the number of the line.

You can tagged potential mine by adding '@' before the letter: 
```console
Enter a letter X :
> @A
Enter a position Y :
> 0
```

Game finish when you found all mines or when you hit one.

When mines exploses, it is mark by a asterisk character.
Bomb's neighbours are marked by the mines count.
Case far away from mines is marked by a minus char '-'


### IA play
I implemented a basic IA that can play round by round. You can choose to let it play at the start of the game : 
```console
Auto Play the round : (N/Y)
> [ENTER]
```



### File architecture
```
Mines
└─ Board.cs     -< Game Board where all methods are
└─ Bot.cs       -< Bot methods
└─ Case.cs      -< Case object
└─ Program.cs   -< Main rounds program
```


   
### License
----
Developped by Pierre B.  
Languages : C#  
Free to use  
MIT  
