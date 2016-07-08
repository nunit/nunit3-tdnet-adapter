# NUnit 3.x adapter for [TestDriven.Net](http://testdriven.net)

The is the project home of the 'NUnitTDNet' NuGet package:
https://www.nuget.org/packages/NUnitTDNet/

This package includes the 'nunit.framework' assembly and an adaptor for executing NUnit 3.x unit tests with all versions of TestDriven.Net. You must reference this package if you want to execute NUnit 3.x tests with a version of TestDriven.Net earlier than TestDriven.Net 3.10.

If you're using TestDriven.Net 3.10 or later, this adaptor is built in can be used to execute any project that references the 'nunit.framework' assembly. There are similar adapters going all the way back to NUnit 2.2.

It is recomended that you reference this package if you want to use a modern version of NUnit 3.x, but you know there are people who might run the tests using a version of TestDriven.Net that you don't control (that would otherwise be incompatible). There is no harm referencing this package if you're using TestDriven.Net 3.10 or later (but it is no longer required).
