# Makefile

# Compiler
CSC = mcs

# Compiler flags
CFLAGS = -langversion:latest

# Source files
SOURCES = Day7.cs

# Executable name
EXECUTABLE = day7.exe

all: run

$(EXECUTABLE): $(SOURCES)
	$(CSC) $(CFLAGS) -out:$(EXECUTABLE) $(SOURCES)

run: $(EXECUTABLE)
	 ./$(EXECUTABLE)

.PHONY: clean

clean:
	rm -f $(EXECUTABLE)

