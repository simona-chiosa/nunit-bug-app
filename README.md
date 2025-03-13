# NUnit Windows Specific-Version TFM Compatibility Issue Reproduction #1244 

## Summary 

This repository contains a simple .NET 8 WPF application with 2 test projects, one using **NUnit** and the other using **xUnit** to reproduce an issue affecting **NUnit** when the project **Target Framework Moniker (TFM)** specifies a Windows version higher than the host OS.  

- The **NUnit test project** fails to load the test DLL, resulting in no test discovery or execution.  
- The **xUnit test project**, which contains the same tests as the NUnit project, runs successfully under the same conditions, confirming that the issue is specific to NUnit.  

Initially, the problem was thought to originate from the **NUnit3TestAdapter**, as the logs suggested issues with loading the test DLL. However, further investigation revealed that the root cause may originate on  **NUnit itself**.  

## Related Issue  
- [NUnit3TestAdapter Issue #1244](https://github.com/nunit/nunit3-vs-adapter/issues/1244)  

## Environment  
- **.NET Version:** .NET 8  
- **Target Framework:** `net8.0-windows10.0.18362.0`  
- **Test Frameworks:**  
  - **NUnit >= 4.0.0** (where the issue occurs)  
  - **NUnit 3.14.0** (where the issue does not occur)  
  - **NUnit3TestAdapter 5.0.0**  
  - **Microsoft.NET.Test.Sdk 17.13.0**  
- **Host OS:** Windows Server 2019 (10.0.17763)  

## Reproducing the Issue  

1. Clone this repository and navigate to the project directory.  
2. Ensure you have a Windows environment where the OS version is **lower** than the TFM (e.g., Windows Server 2019).  
3. Run tests using:  
   ```sh
   dotnet test
## Behavior with NUnit 4.x:
- The test project DLL fails to load, and no tests are discovered.
- Initial error message (misleading)
    > NUnit Adapter 5.0.0.0: Test execution started
    >
    > Running all tests in C:\path\to\Common.Test.dll NUnit failed to load C:\path\to\Common.Test.dll
    >
    > NUnit Adapter 5.0.0.0: Test execution complete
    >
    > No test is available in NUnit.Bug.App.Test.dll. Make sure that test discoverer & executors are registered and platform & framework version settings are appropriate and try again.
- After [this improvement](https://github.com/nunit/nunit3-vs-adapter/pull/1250) in NUnit3TestAdapter, the message became clearer:
    > NUnit Adapter 5.0.0.0: Test execution started
    >
    > Running all tests in C:\path\to\Common.Test.dll
    >
    > NUnit couldn't run the 3 discovered tests: Only supported on Windows10.0.18362.0
    >
    > NUnit Adapter 5.0.0.0: Test execution complete
## Behavior with NUnit 3.14.0:
- Tests run successfully, despite the higher Windows version in TFM.

## Workarounds
1. Downgrade NUnit to 3.14.0 (confirmed to work).
2. Remove the specific Windows OS version from the TFM:
    ```xml
    <TargetFramework>net8.0-windows</TargetFramework>
    ```
3. Change the TFM to match the build environment OS version:
    ```xml
    <TargetFramework>net8.0-windows10.0.17763.0</TargetFramework>
    ```
4. Run tests on a machine with a matching or higher Windows version
5. Use xUnit instead of NUnit. The same project configuration with xUnit successfully loads the test DLL and executes tests with the same higher Windows version TFM.