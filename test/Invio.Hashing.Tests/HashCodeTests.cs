using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Invio.Hashing {

    public class HashCodeTests {

        public static IEnumerable<object[]> Consistency_Data {
            get {
                var value = 123;
                var reference = new object();

                yield return ToArray(ToArray(null), ToArray(null));
                yield return ToArray(ToArray(value), ToArray(value));
                yield return ToArray(ToArray(reference), ToArray(reference));
                yield return ToArray(ToArray(null, value), ToArray(null, value));
                yield return ToArray(ToArray(value, null), ToArray(value, null));
                yield return ToArray(ToArray(null, reference), ToArray(null, reference));
                yield return ToArray(ToArray(reference, null), ToArray(reference, null));
                yield return ToArray(ToArray(value, reference), ToArray(value, reference));
                yield return ToArray(ToArray(null, value, reference), ToArray(null, value, reference));
                yield return ToArray(ToArray(reference, null, value), ToArray(reference, null, value));
                yield return ToArray(ToArray(value, reference, null), ToArray(value, reference, null));
            }
        }

        [Theory]
        [MemberData(nameof(Consistency_Data))]
        public void From_Consistency(object[] left, object[] right) {
            Assert.Equal(HashCode.From(left), HashCode.From(right));
        }

        [Fact]
        public void From_Consistency_FromNull() {
            // From the perspective of generating hash codes, 'null' is as good of a value as any.

            Assert.Equal(HashCode.From(null), HashCode.From(null));
        }

        [Fact]
        public void From_Consistency_Params_FromNull() {
            // From the perspective of generating hash codes, 'null' is as good of a value as any.

            object[] values = null;

            Assert.Equal(HashCode.From(values), HashCode.From(values));
        }

        [Fact]
        public void From_Consistency_Enumerable_FromNull() {
            // From the perspective of generating hash codes, 'null' is as good of a value as any.

            IEnumerable<object> values = null;

            Assert.Equal(HashCode.From(values), HashCode.From(values));
        }

        [Fact]
        public void FromList_ConsistencyWithFrom_Null() {
            Assert.Equal(HashCode.From(null), HashCode.FromList(null));
        }

        [Fact]
        public void FromList_ConsistencyWithSelf_Null() {
            Assert.Equal(HashCode.FromList(null), HashCode.FromList(null));
        }

        [Fact]
        public void FromList_Equal() {

            // Arrange

            var left = new List<String> { "Foo", null, "Bar", null };
            var right = new List<String> { "Foo", null, "Bar", null };

            // Act

            var leftHashCode = HashCode.FromList(left);
            var rightHashCode = HashCode.FromList(right);

            // Assert

            Assert.Equal(leftHashCode, rightHashCode);
        }

        [Fact]
        public void FromList_UnequalByOrder() {

            // Arrange

            var left = new List<String> { "Foo", null, "Bar", null };
            var right = new List<String> { "Bar", null, "Foo", null };

            // Act

            var leftHashCode = HashCode.FromList(left);
            var rightHashCode = HashCode.FromList(right);

            // Assert

            Assert.False(
                leftHashCode.Equals(rightHashCode),
                $"Since order matters for '{nameof(HashCode)}.{nameof(HashCode.FromList)}', " +
                $"the left and right hashcodes should not be equal."
            );
        }

        [Fact]
        public void FromList_UnequalSize() {

            // Arrange

            var left = new List<String> { "Foo", null, "Bar", null };
            var right = new List<String> { "Foo", null, "Bar" };

            // Act

            var leftHashCode = HashCode.FromList(left);
            var rightHashCode = HashCode.FromList(right);

            // Assert

            Assert.False(
                leftHashCode.Equals(rightHashCode),
                "Since the left list has an extra item in its list, it should not be considered equal."
            );
        }

        [Theory]
        [MemberData(nameof(Consistency_Data))]
        public void FromList_Consistency(IEnumerable left, IEnumerable right) {
            Assert.Equal(HashCode.FromList(left), HashCode.FromList(right));
        }

        [Fact]
        public void FromSet_ConsistencyWithFrom_Null() {
            Assert.Equal(HashCode.From(null), HashCode.FromSet(null));
        }

        [Fact]
        public void FromSet_ConsistencyWithSelf_Null() {
            Assert.Equal(HashCode.FromSet(null), HashCode.FromSet(null));
        }

        [Fact]
        public void FromSet_Equal_SameOrder() {

            // Arrange

            var left = new List<String> { "Foo", null, "Bar", null };
            var right = new List<String> { "Foo", null, "Bar", null };

            // Act

            var leftHashCode = HashCode.FromSet(left);
            var rightHashCode = HashCode.FromSet(right);

            // Assert

            Assert.Equal(leftHashCode, rightHashCode);
        }

        [Fact]
        public void FromSet_Equal_DifferentOrder() {

            // Arrange

            var left = new List<String> { "Foo", null, "Bar", null };
            var right = new List<String> { null, "Bar", "Foo", null };

            // Act

            var leftHashCode = HashCode.FromSet(left);
            var rightHashCode = HashCode.FromSet(right);

            // Assert

            Assert.Equal(leftHashCode, rightHashCode);
        }

        [Fact]
        public void FromSet_UnequalSize() {

            // Arrange

            var left = new List<String> { "Foo", null, "Bar", null };
            var right = new List<String> { "Foo", null, "Bar" };

            // Act

            var leftHashCode = HashCode.FromSet(left);
            var rightHashCode = HashCode.FromSet(right);

            // Assert

            Assert.False(
                leftHashCode.Equals(rightHashCode),
                "Since the left set has an extra item in its list, it should not be considered equal."
            );
        }

        [Theory]
        [MemberData(nameof(Consistency_Data))]
        public void FromSet_Consistency(IEnumerable left, IEnumerable right) {
            Assert.Equal(HashCode.FromSet(left), HashCode.FromSet(right));
        }

        private static object[] ToArray(object value) {
            return new object[] { value };
        }

        private static object[] ToArray(params object[] values) {
            return values;
        }

    }

}
