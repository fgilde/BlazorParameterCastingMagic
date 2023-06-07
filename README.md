# BlazorParameterCastingMagic

## Overview

The BlazorParameterCastingMagic package provides a way to handle dynamic casting of parameters in Blazor components. It is a convenient and flexible way to use implicit casting within your Blazor components to handle parameter values.

Blazor does not natively support implicit conversions for parameters. This means that even if you've defined implicit conversions in your classes, Blazor will not use them when trying to convert values for parameters in your components. This leads to casting exceptions when you try to use different types of values for the parameters.

This package introduces a workaround for this problem. It allows developers to apply dynamic casting to the parameters and bypass the casting exceptions that occur when different types of values are used for parameters. If implicit casting is not possible, the package will attempt to map the types using [Nextended.Core](https://www.nuget.org/packages/Nextended.Core/).

## Installation

Provide instructions for installing your package here.

## Usage

Firstly, you need to define the `AllowMagicCastingAttribute` on the parameters of your components where you want to enable dynamic casting. 

```csharp
[Parameter]
[AllowMagicCasting]
public FlexibleParameter Size { get; set; } = 1;
```

Then, you need to call the `ApplyMagicCasting` extension method on the parameters in the `SetParametersAsync` method of your component.

```csharp
public override Task SetParametersAsync(ParameterView parameters)
{           
    return base.SetParametersAsync(parameters.ApplyMagicCasting(this));
}
```

You can now use different types of values for your parameters without running into casting exceptions.

### Example 1

You can use a string value for the parameter in your component.

```html
<MyComponent Size="@("11px")" />
```

### Example 2

You can also use a number value for the parameter in your component.

```html
<MyComponent Size="8" />
```

Both of these examples will work without throwing a casting exception.

### Example with a Custom Class

Here is an example of a custom class `FlexibleParameter` that you can use with the `AllowMagicCastingAttribute`:

```csharp
public class FlexibleParameter
{
    public int IntValue { get; set; }
    public string StringValue { get; set; }

    public static implicit operator FlexibleParameter(int intValue)
    {
        return new FlexibleParameter { IntValue = intValue };
    }

    public static implicit operator FlexibleParameter(string stringValue)
    {
        return new FlexibleParameter { StringValue = stringValue };
    }
}
```

This class has two properties `IntValue` and `StringValue`, and implicit conversion operators for `int` and `string`. You can use this class for a parameter in your component and apply the `AllowMagicCastingAttribute`.

## Limitations

The BlazorParameterCastingMagic package uses reflection to handle dynamic casting which can be less performant than direct casting. This should not be an issue unless there is a very large number of parameters. 

## Conclusion

The BlazorParameterCastingMagic package provides a workaround for the lack of support for implicit conversions in Blazor. With this package, you can avoid casting exceptions and use different types of values for your component parameters. 
