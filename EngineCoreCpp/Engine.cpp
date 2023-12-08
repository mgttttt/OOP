#include "pch.h"
#include "Engine.h"
#include <iostream>
#include <stdio.h>
#include <Windows.h>

Engine::Engine(DWORD pid, DWORD offsets[], DWORD base_address) : pid_(pid), offsets_(), base_address_(base_address) {
    num_offsets_ = sizeof(offsets) / sizeof(offsets[0]);
    for (int i = 0; i < num_offsets_; i++)
    {
        offsets_[i] = offsets[i];
        
    }
    processHandle_ = OpenProcess(PROCESS_ALL_ACCESS, FALSE, pid_);
    if (processHandle_ == nullptr) {
        std::cerr << "Error opening process: " << GetLastError() << std::endl;
    }
}

Engine::~Engine() {
    CloseHandle(processHandle_);
}

bool Engine::ActivateCheat() {
    if (!EnableDebugPrivilege())
        return false;
    LPVOID nop = (LPVOID)0x90;
    bool flag = true;
    std::cout << offsets_[0] << std::endl;
    flag = flag && WriteProcessMemory(processHandle_, reinterpret_cast<LPVOID>(offsets_[0] + base_address_), &nop, 1, nullptr) != 0 &&
        WriteProcessMemory(processHandle_, reinterpret_cast<LPVOID>(offsets_[0] + base_address_ + 1), &nop, 1, nullptr) != 0;
    for (int i = 0; i < 6; i++)
    {
        flag = flag && WriteProcessMemory(processHandle_, reinterpret_cast<LPVOID>(offsets_[1] + base_address_ + i), &nop, 1, nullptr) != 0;
    }
    return flag;
}

bool Engine::DeactivateCheat()
{
    bool flag = true;
    LPVOID dataToWrite[] = { (LPVOID)0x29, (LPVOID)0x08 };
    flag = flag && WriteProcessMemory(processHandle_, reinterpret_cast<LPVOID>(offsets_[0] + base_address_), &dataToWrite[0], 1, nullptr) != 0 &&
        WriteProcessMemory(processHandle_, reinterpret_cast<LPVOID>(offsets_[0] + base_address_ + 1), &dataToWrite[1], 1, nullptr) != 0;
    LPVOID dataToWriteMoney[] = { (LPVOID)0x29, (LPVOID)0x88, (LPVOID)0x74, (LPVOID)0x04, (LPVOID)0x00, (LPVOID)0x00 };
    for (int i = 0; i < 6; i++)
    {
        flag = flag && WriteProcessMemory(processHandle_, reinterpret_cast<LPVOID>(offsets_[1] + base_address_ + i), &dataToWrite[i], 1, nullptr) != 0;
    }
    return flag;
}

bool Engine::EnableDebugPrivilege() {
    HANDLE hToken = nullptr;
    LUID luid = { 0 };
    if (OpenProcessToken(processHandle_, TOKEN_ADJUST_PRIVILEGES, &hToken)) {
        if (LookupPrivilegeValue(NULL, SE_DEBUG_NAME, &luid)) {
            TOKEN_PRIVILEGES tokenPriv = { 0 };
            tokenPriv.PrivilegeCount = 1;
            tokenPriv.Privileges[0].Luid = luid;
            tokenPriv.Privileges[0].Attributes = SE_PRIVILEGE_ENABLED;
            return AdjustTokenPrivileges(hToken, FALSE, &tokenPriv, sizeof(TOKEN_PRIVILEGES), nullptr, nullptr) != 0;
        }
    }
    return false;
}

Engine* CreateEngine(DWORD pid, DWORD offsets[], DWORD base_address) {
    return new Engine(pid, offsets, base_address);
}


bool DestroyEngine(Engine* engine) {
    bool res = engine->DeactivateCheat();
    delete engine;
    return res;
}

bool ActivateCheat(Engine* engine) {
    return engine->ActivateCheat();
}

