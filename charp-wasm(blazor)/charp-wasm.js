// Set up the .NET WebAssembly runtime
import { dotnet } from './dotnet.js'

export async function GetSharpWasm() { 
    // Get exported methods from the .NET assembly
    const { getAssemblyExports, getConfig } = await dotnet
        .withDiagnosticTracing(false)
        .create();

    const config = getConfig();
    const exports = await getAssemblyExports(config.mainAssemblyName);
    return exports;
}

