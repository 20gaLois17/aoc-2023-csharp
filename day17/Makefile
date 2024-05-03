# Makefile

# Compiler
CSC = mcs

# Compiler flags
CFLAGS = -langversion:latest

# Source files
SOURCES = Day17.cs

# Executable name
EXECUTABLE = day17.exe

all: run

$(EXECUTABLE): $(SOURCES)
	$(CSC) $(CFLAGS) -out:$(EXECUTABLE) $(SOURCES)

run: $(EXECUTABLE)
	 ./$(EXECUTABLE)

.PHONY: clean

clean:
	rm -f $(EXECUTABLE)

