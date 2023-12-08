#pragma once
#ifndef ENGINE_H
#define ENGINE_H
class Engine {
public:

    Engine(DWORD pid, DWORD offsets[], DWORD base_address);
    ~Engine();

    bool ActivateCheat();
    bool DeactivateCheat();

private:
    int num_offsets_;
    DWORD pid_;
    DWORD offsets_[10];
    DWORD base_address_;
    HANDLE processHandle_;
    bool EnableDebugPrivilege();
};
extern "C" __declspec(dllexport) Engine * CreateEngine(DWORD pid, DWORD offsets[], DWORD base_address);
extern "C" __declspec(dllexport) bool DestroyEngine(Engine * engine);
extern "C" __declspec(dllexport) bool ActivateCheat(Engine * engine);
#endif