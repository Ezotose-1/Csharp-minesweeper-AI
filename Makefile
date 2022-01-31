BIN= Cshap-Minesweeper
SRC= Board.cs Bot.cs Case.cs Program.cs Move.cs
CC=mcs               # We use csc and not mcs to have Graphics
FLAGS=-optimize+     # Allows compiler optimization

all: $(BIN)

$(BIN): $(SRC)
	$(CC) $(FLAGS) $^ -out:$@
	@chmod +x $@

clean:
	$(RM) $(BIN)

.PHONY: all clean
