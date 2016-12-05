using System;
using System.Collections.Generic;

namespace Invio.Hashing {

    /// <summary>
    ///   An <see cref="IEqualityComparer{T}" /> implementation that takes in objects.
    ///   If all of the given objects being evaluated are of type <see cref="String" />,
    ///   they will use the predefined form of string comparison. Otherwise, the
    ///   default hash code and equality comparison for each type will be used.
    /// </summary>
    public sealed class IfStringComparer : EqualityComparer<Object> {

        /// <summary>
        ///   Gets an <see cref="IfStringComparer" /> object that performs a case-sensitive
        ///   string comparison using the word comparison rules of the current culture
        ///   when <see cref="String" /> objects are found.
        ///   All other objects, or objects of mixed types, use their default comparison.
        /// </summary>
        public static IfStringComparer CurrentCulture { get; }

        /// <summary>
        ///   Gets an <see cref="IfStringComparer" /> object that performs a case-insensitive
        ///   string comparison using the word comparison rules of the current culture
        ///   when <see cref="String" /> objects are found.
        ///   All other objects, or objects of mixed types, use their default implementations.
        /// </summary>
        public static IfStringComparer CurrentCultureIgnoreCase { get; }

        /// <summary>
        ///   Gets an <see cref="IfStringComparer" /> object that performs a case-sensitive
        ///   ordinal string comparison when <see cref="String" /> objects are found.
        ///   All other objects, or objects of mixed types, use their default implementations.
        /// </summary>
        public static IfStringComparer Ordinal { get; }

        /// <summary>
        ///   Gets an <see cref="IfStringComparer" /> object that performs a case-insensitive
        ///   ordinal string comparison when <see cref="String" /> objects are found.
        ///   All other objects, or objects of mixed types, use the default comparison.
        /// </summary>
        public static IfStringComparer OrdinalIgnoreCase { get; }

        static IfStringComparer() {
            CurrentCulture =
                new IfStringComparer(StringComparer.CurrentCulture);
            CurrentCultureIgnoreCase =
                new IfStringComparer(StringComparer.CurrentCultureIgnoreCase);
            Ordinal =
                new IfStringComparer(StringComparer.Ordinal);
            OrdinalIgnoreCase =
                new IfStringComparer(StringComparer.OrdinalIgnoreCase);
        }

        private IEqualityComparer<String> stringComparer { get; }

        private IfStringComparer(IEqualityComparer<String> stringComparer) {
            this.stringComparer = stringComparer;
        }

        /// <inheritdoc />
        public override int GetHashCode(Object that) {
            if (that == null) {
                return 0;
            }

            var thatString = that as String;

            if (thatString != null) {
                return this.stringComparer.GetHashCode(thatString);
            }

            return that.GetHashCode();
        }

        /// <inheritdoc />
        public override bool Equals(Object left, Object right) {
            if (left == null || right == null) {
                return left == null && right == null;
            }

            var leftString = left as String;
            var rightString = right as String;

            if (leftString != null && rightString != null) {
                return this.stringComparer.Equals(leftString, rightString);
            }

            return left.Equals(right);
        }

    }

}
