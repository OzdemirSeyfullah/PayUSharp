using System.Reflection;
using System.Runtime.CompilerServices;

// Information about this assembly is defined by the following attributes.
// Change them to the values specific to your project.

[assembly: AssemblyTitle("PayUSharp.Core")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("PayU Turkiye")]
[assembly: AssemblyProduct("")]
[assembly: AssemblyCopyright("PayU Turkiye")]
[assembly: AssemblyTrademark("PayU Turkiye")]
[assembly: AssemblyCulture("")]

// The assembly version has the format "{Major}.{Minor}.{Build}.{Revision}".
// The form "{Major}.{Minor}.*" will automatically update the build and revision,
// and "{Major}.{Minor}.{Build}.*" will update just the revision.

[assembly:AssemblyVersion("2.1.0.0")]

// The following attributes are used to specify the signing key for the assembly,
// if desired. See the Mono documentation for more information about signing.

//[assembly: AssemblyDelaySign(false)]
//[assembly: AssemblyKeyFile("")]

[assembly:InternalsVisibleTo("PayUSharp.AutomaticLiveUpdate")]
[assembly:InternalsVisibleTo("PayUSharp.LiveUpdate")]
[assembly:InternalsVisibleTo("PayUSharp.IPN")]
[assembly:InternalsVisibleTo("PayUSharp.Token")]
[assembly:InternalsVisibleTo("DocGen")]
