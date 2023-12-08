cmake_minimum_required(VERSION 3.14)
project(EngineCoreCpp)

set(CMAKE_CXX_STANDARD 17)

set(SOLUTION_DIR "${CMAKE_CURRENT_SOURCE_DIR}/../solution")

if(CMAKE_SIZEOF_VOID_P EQUAL 8)
    set(BUILD_SUBDIR "x64")
else()
    set(BUILD_SUBDIR "x86")
endif()

set(CMAKE_RUNTIME_OUTPUT_DIRECTORY "${SOLUTION_DIR}/${BUILD_SUBDIR}")
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY_RELEASE "${SOLUTION_DIR}/${BUILD_SUBDIR}")

set(SOURCE_FILES
    dllmain.cpp
    Engine.cpp
    pch.cpp
)

set(HEADER_FILES
    Engine.h
    framework.h
    pch.h
)

add_library(EngineCoreCpp SHARED
    ${SOURCE_FILES}
    ${HEADER_FILES}
)

target_compile_definitions(EngineCoreCpp PRIVATE ENGINE_DLL_EXPORTS)

target_include_directories(EngineCoreCpp PRIVATE ${CMAKE_CURRENT_SOURCE_DIR})
