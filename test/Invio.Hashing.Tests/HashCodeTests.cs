using System;
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
        public void Consistency(object[] left, object[] right) {
            Assert.Equal(HashCode.From(left), HashCode.From(right));
        }

        [Fact]
        public void Consistency_FromNull() {
            // From the perspective of generating hash codes, 'null' is as good of a value as any.

            Assert.Equal(HashCode.From(null), HashCode.From(null));
        }

        [Fact]
        public void Consistency_Params_FromNull() {
            // From the perspective of generating hash codes, 'null' is as good of a value as any.

            object[] values = null;

            Assert.Equal(HashCode.From(values), HashCode.From(values));
        }

        [Fact]
        public void Consistency_Enumerable_FromNull() {
            // From the perspective of generating hash codes, 'null' is as good of a value as any.

            IEnumerable<object> values = null;

            Assert.Equal(HashCode.From(values), HashCode.From(values));
        }

        private static object[] ToArray(object value) {
            return new object[] { value };
        }

        private static object[] ToArray(params object[] values) {
            return values;
        }

    }

}
