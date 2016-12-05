using System;
using System.Collections.Generic;
using Xunit;

namespace Invio.Hashing {

    public class IfStringComparerTests {

        [Theory]
        [MemberData(nameof(NeverEqual_Data))]
        [MemberData(nameof(CaseInsensitiveEqual_Data))]
        public void CurrentCulture_NotEqual(Object left, Object right) {

            // Arrange

            var comparer = IfStringComparer.CurrentCulture;

            // Act

            var isEqual = comparer.Equals(left, right);

            // Assert

            Assert.False(isEqual);
        }

        [Theory]
        [MemberData(nameof(AlwaysEqual_Data))]
        public void CurrentCulture_Equal(Object left, Object right) {

            // Arrange

            var comparer = IfStringComparer.CurrentCulture;

            // Act

            var isEqual = comparer.Equals(left, right);
            var leftHashCode = comparer.GetHashCode(left);
            var rightHashCode = comparer.GetHashCode(right);

            // Assert

            Assert.True(isEqual);
            Assert.Equal(leftHashCode, rightHashCode);
        }

        [Theory]
        [MemberData(nameof(NeverEqual_Data))]
        public void CurrentCultureIgnoreCase_NotEqual(Object left, Object right) {

            // Arrange

            var comparer = IfStringComparer.CurrentCultureIgnoreCase;

            // Act

            var isEqual = comparer.Equals(left, right);

            // Assert

            Assert.False(isEqual);
        }

        [Theory]
        [MemberData(nameof(AlwaysEqual_Data))]
        [MemberData(nameof(CaseInsensitiveEqual_Data))]
        public void CurrentCultureIgnoreCase_Equal(Object left, Object right) {

            // Arrange

            var comparer = IfStringComparer.CurrentCultureIgnoreCase;

            // Act

            var isEqual = comparer.Equals((String)left, (String)right);
            var leftHashCode = comparer.GetHashCode(left);
            var rightHashCode = comparer.GetHashCode(right);

            // Assert

            Assert.True(isEqual);
            Assert.Equal(leftHashCode, rightHashCode);
        }

        [Theory]
        [MemberData(nameof(NeverEqual_Data))]
        [MemberData(nameof(CaseInsensitiveEqual_Data))]
        public void Ordinal_NotEqual(Object left, Object right) {

            // Arrange

            var comparer = IfStringComparer.Ordinal;

            // Act

            var isEqual = comparer.Equals(left, right);

            // Assert

            Assert.False(isEqual);
        }

        [Theory]
        [MemberData(nameof(AlwaysEqual_Data))]
        public void Ordinal_Equal(Object left, Object right) {

            // Arrange

            var comparer = IfStringComparer.Ordinal;

            // Act

            var isEqual = comparer.Equals(left, right);
            var leftHashCode = comparer.GetHashCode(left);
            var rightHashCode = comparer.GetHashCode(right);

            // Assert

            Assert.True(isEqual);
            Assert.Equal(leftHashCode, rightHashCode);
        }

        [Theory]
        [MemberData(nameof(NeverEqual_Data))]
        public void OrdinalIgnoreCase_NotEqual(Object left, Object right) {

            // Arrange

            var comparer = IfStringComparer.OrdinalIgnoreCase;

            // Act

            var isEqual = comparer.Equals(left, right);

            // Assert

            Assert.False(isEqual);
        }

        [Theory]
        [MemberData(nameof(AlwaysEqual_Data))]
        [MemberData(nameof(CaseInsensitiveEqual_Data))]
        public void OrdinalIgnoreCase_Equal(Object left, Object right) {

            // Arrange

            var comparer = IfStringComparer.OrdinalIgnoreCase;

            // Act

            var isEqual = comparer.Equals((String)left, (String)right);
            var leftHashCode = comparer.GetHashCode(left);
            var rightHashCode = comparer.GetHashCode(right);

            // Assert

            Assert.True(isEqual);
            Assert.Equal(leftHashCode, rightHashCode);
        }

        public static IEnumerable<object[]> AlwaysEqual_Data {
            get {
                return new List<object[]> {
                    new object[] { null, null },
                    new object[] { "", "" },
                    new object[] { "Foo", "Foo" }
                };
            }
        }

        public static IEnumerable<object[]> NeverEqual_Data {
            get {
                return new List<object[]> {
                    new object[] { null, "Foo" },
                    new object[] { 65, "A" },
                    new object[] { 'A', "A" },
                    new object[] { 5, 6 },
                    new object[] { Guid.NewGuid(), Guid.NewGuid() },
                    new object[] { new object(), null }
                };
            }
        }

        public static IEnumerable<object[]> CaseInsensitiveEqual_Data {
            get {
                return new List<object[]> {
                    new object[] { "Foo", "FOO" },
                    new object[] { "BaR", "bar" }
                };
            }
        }

    }

}
