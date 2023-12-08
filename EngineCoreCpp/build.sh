cmake -B build/x86 -S . -G "Visual Studio 17 2022" -A Win32
cmake --build build/x86 --config Release
cmake -B build/x64 -S . -G "Visual Studio 17 2022" -A x64
cmake --build build/x64 --config Release