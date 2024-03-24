# Makefile

# Compiler
CSC = mcs

# Compiler flags
CFLAGS = -langversion:latest

# Source files
SOURCES = Day8.cs

# Executable name
EXECUTABLE = day8.exe

all: run

$(EXECUTABLE): $(SOURCES)
	$(CSC) $(CFLAGS) -out:$(EXECUTABLE) $(SOURCES)

run: $(EXECUTABLE)
	 ./$(EXECUTABLE)

.PHONY: clean

clean:
	rm -f $(EXECUTABLE)

