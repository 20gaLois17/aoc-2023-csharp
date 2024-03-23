# Makefile

# Compiler
CSC = mcs

# Compiler flags
CFLAGS = -langversion:latest

# Source files
SOURCES = Day6.cs

# Executable name
EXECUTABLE = day6.exe

all: run

$(EXECUTABLE): $(SOURCES)
	$(CSC) $(CFLAGS) -out:$(EXECUTABLE) $(SOURCES)

run: $(EXECUTABLE)
	 ./$(EXECUTABLE)

.PHONY: clean

clean:
	rm -f $(EXECUTABLE)

