# Invio.Hashing

[![Build status](https://ci.appveyor.com/api/projects/status/l19hojuuhbp2myjc/branch/master?svg=true)](https://ci.appveyor.com/project/carusology/invio-hashing/branch/master)
[![Travis CI](https://img.shields.io/travis/invio/Invio.Hashing.svg?maxAge=3600&label=travis)](https://travis-ci.org/invio/Invio.Hashing)
[![NuGet](https://img.shields.io/nuget/v/Invio.Hashing.svg)](https://www.nuget.org/packages/Invio.Hashing/)
[![Coverage](https://coveralls.io/repos/github/invio/Invio.Hashing/badge.svg?branch=master)](https://coveralls.io/github/invio/Invio.Hashing?branch=master)

A basic implementation for generating hash codes for data objects.

# Installation
The latest version of this package is available on NuGet. To install, run the following command:

```
PM> Install-Package Invio.Hashing
```

# Usage

There is a single class in the package: `HashCode`. By calling one of its `From` implementations, a caller can get consistent hash codes from an unbounded collection of objects, like so:

```csharp
using Invio.Hashing;

var myHashCode = HashCode.From(123, "456", null, new List<Guid> { Guid.NewGuid() });
```

It is intended for use within data objects, like so:

```csharp
using Invio.Hashing;

namespace MyNamespace {

    public class MyClass : IEquatable<MyClass> {

        public String MyProperty { get; set; }
        public int MyField;

        public override bool Equals(Object that) {
            return this.Equals(that as MyClass);
        }

        public override bool Equals(MyClass that) {
            return that != null
                && this.MyProperty == that.MyProperty
                && this.MyField == that.MyField;
        }

        public override int GetHashCode() {
            return HashCode.From(
                this.MyProperty,
                this.MyField
            );
        }

    }

}
```

That's it. <3