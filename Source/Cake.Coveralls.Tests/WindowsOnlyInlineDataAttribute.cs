using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using Xunit.Sdk;

namespace Cake.Coveralls.Tests;

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
public class WindowsOnlyInlineDataAttribute : DataAttribute
{
    private readonly object[] data;

    /// <summary>
    /// Initializes a new instance of the <see cref="WindowsOnlyInlineDataAttribute" /> class.
    /// </summary>
    /// <param name="data">The data values to pass to the theory.</param>
    public WindowsOnlyInlineDataAttribute(params object[] data)
    {
        this.data = data;
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Skip = "This test only runs on Windows.";
        }
    }

    /// <inheritdoc />
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        return new[]
        {
            data,
        };
    }
}
