# SeleniumTest
1) The project was built, tested and run in Visual Studio Professional 2013 on a Windows 7 machine.
Also built, tested and run with the Microsoft .NET Framework version 4.5.51209,
Firefox version 37.0.2 and Internet Explorer version 11.0.9600.17801

2) Test runs on IE proved to be flakey when attempting to click on some controls. In particular the "My Account"
button on the home page was particularly troublesome. Sometimes it works, other times not and I haven't quite got
the solution nailed down yet.

3) To run the tests using IE a change to the app.config file will be needed. Simply update the BrowserString key.
Additionally, the latest version of IEDriverServer should be in the path.

4) Exception handling could be improved. If I had access to test hooks on the backend to cleanup state then
more complete exception handling would make sense. State cleanup through the UI is difficult so I didn't pursue it.

The tests are built as Visual Studio Unit tests. They can be run from within Visual Studio itself or run using MSTest.exe.
To run within Visual Studio:
1) Load the solution in Visual Studio
2) Build the solution
3) From the menu select TEST -> Run -> All Tests

To run from the command line:
1) Copy the contents of the bin/Debug folder to a target directory on the test machine.
2) If MSTest.exe is on your path simply run "MSTest.exe /testcontainer:seleniumtest.dll".
Note that MSTest.exe can generally be found at \Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\MSTest.exe
