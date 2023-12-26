build debug:  dotnet publish -r wasi-wasm -c debug /p:MSBuildEnableWorkloadResolver=false /p:UseAppHost=false --self-contained
build release:  dotnet publish -r wasi-wasm -c release /p:MSBuildEnableWorkloadResolver=false /p:UseAppHost=false --self-contained
build release with no debug symbols:  dotnet publish -r wasi-wasm -c release /p:MSBuildEnableWorkloadResolver=false /p:UseAppHost=false /p:NativeDebugSymbols=false --self-contained 
build file path: bin\debug\net8.0\wasi-wasm\publish
manual: https://github.com/dotnet/runtimelab/blob/feature/NativeAOT-LLVM/docs/using-nativeaot/compiling.md 
