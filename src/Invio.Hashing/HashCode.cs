using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Invio.Hashing {

    /// <summary>
    ///   Utility class used to generate consistent hash codes for objects of
    ///   any value, including "null" and "default(T)".
    /// </summary>
    public static class HashCode {

        // These constants has no significance other than they are prime,
        // limiting the liklihood that they will create hash codes that
        // are the same for multiple distinct objects.

        private const int basePrime = 17;
        private const int iterationPrime = 23;
        private const int nullPrime = 31;

        /// <summary>
        ///   Generates a consistent, deterministic hash code for a
        ///   single object or an array of objects.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The output can change if the objects in the
        ///     <paramref name="values" /> array are provided in a different
        ///     order, or if the objects in <paramref name="values" />
        ///     change state in such a way that alters their hash code.
        ///     This is because each non-null object will have its hash code
        ///     incorporated into the output.
        ///   </para>
        ///   <para>
        ///     The 'null' value is acceptable both as a value within the
        ///     <paramref name="values" /> array and as the value of the
        ///     <paramref name="values" /> array itself.
        ///   </para>
        /// </remarks>
        /// <param name="values">
        ///   An array of objects that will be used to generate a
        ///   consistent hash code.
        /// </param>
        /// <returns>
        ///   A consistent, deterministic hash code for input provided
        ///   via <paramref name="values" />.
        /// </returns>
        public static int From(params object[] values) {
            if (values == null) {
                return HashCode.From(Enumerable.Empty<object>());
            }

            return HashCode.From((IEnumerable<object>)values);
        }

        /// <summary>
        ///   Generates a consistent, deterministic hash code for an
        ///   enumeration of objects.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The output can change if the objects in the
        ///     <paramref name="values" /> array are provided in a different
        ///     order, or if any object in <paramref name="values" />
        ///     changes state in such a way that it alters its hash code.
        ///     This is because each non-null object will have its hash code
        ///     incorporated into the output.
        ///   </para>
        ///   <para>
        ///     A 'null' value is acceptable both as an item within the
        ///     <paramref name="values" /> enumeration and as the value of
        ///     the <paramref name="values" /> enumeration itself.
        ///   </para>
        /// </remarks>
        /// <param name="values">
        ///   An enumeration of objects that will be used to generate a
        ///   consistent hash code.
        /// </param>
        /// <returns>
        ///   A consistent, deterministic hash code for input provided
        ///   via <paramref name="values" />.
        /// </returns>
        public static int From(IEnumerable<object> values) {
            unchecked {
                var hash = basePrime;

                foreach (var value in values ?? Enumerable.Empty<object>()) {
                    hash *= iterationPrime;

                    if (value == null) {
                        hash += nullPrime;
                    } else {
                        hash += value.GetHashCode();
                    }
                }

                return hash;
            }
        }

        /// <summary>
        ///   Generates a consistent, deterministic hash code for an
        ///   enumeration of objects where the order of the values in
        ///   the enumeration matters in terms of equality.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The output can change if the objects in the <paramref name="values" />
        ///     enumerable are provided in a different order, or if any object in
        ///     <paramref name="values" /> changes state in such a way that it alters
        ///     its hash code. This is because each non-null object will have its own
        ///     hash code incorporated into the output.
        ///   </para>
        ///   <para>
        ///     A 'null' value is acceptable both as an item within the
        ///     <paramref name="values" /> enumeration and as the value of
        ///     the <paramref name="values" /> enumeration itself.
        ///   </para>
        /// </remarks>
        /// <param name="values">
        ///   An enumeration of objects that will be used to generate a
        ///   consistent hash code where the order of items in said enumeration
        ///   is relevant when determining its equality to another
        ///   enumeration of values.
        /// </param>
        /// <returns>
        ///   A consistent, deterministic hash code for input provided
        ///   via <paramref name="values" />.
        /// </returns>
        public static int FromList(IEnumerable values) {
            return HashCode.FromList(values, EqualityComparer<Object>.Default);
        }

        /// <summary>
        ///   Generates a consistent, deterministic hash code for an
        ///   enumeration of objects where the order of the values in
        ///   the enumeration matters in terms of equality.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The output can change if any objects in the <paramref name="values" />
        ///     enumerable are provided in a different order, or if any object changes
        ///     state in such a way that it alters the way the <paramref name="comparer" />
        ///     provided calculates its hash code.
        ///   </para>
        ///   <para>
        ///     A 'null' value is acceptable both as an item within the
        ///     <paramref name="values" /> enumeration and as the value of
        ///     the <paramref name="values" /> enumeration itself.
        ///   </para>
        /// </remarks>
        /// <param name="values">
        ///   An enumeration of objects that will be used to generate a
        ///   consistent hash code where the order of items in said enumeration
        ///   is relevant when determining its equality to another
        ///   enumeration of values.
        /// </param>
        /// <param name="comparer">
        ///   An <see cref="IEqualityComparer" /> implementation used to
        ///   generate all non-null hash codes for objects in <paramref name="values" />.
        /// </param>
        /// <returns>
        ///   A consistent, deterministic hash code for the input provided
        ///   via <paramref name="values" /> and <paramref name="comparer" />.
        /// </returns>
        public static int FromList(IEnumerable values, IEqualityComparer comparer) {
            if (comparer == null) {
                throw new ArgumentNullException(nameof(values));
            }

            if (values == null) {
                return basePrime;
            }

            unchecked {
                var hash = basePrime;
                var index = 0;

                foreach (var value in values) {
                    hash *= iterationPrime;

                    if (value == null) {
                        hash += nullPrime;
                    } else {
                        hash += comparer.GetHashCode(value);
                    }

                    hash += (++index);
                }

                return hash;
            }
        }

        /// <summary>
        ///   Generates a consistent, deterministic hash code for an
        ///   enumeration of objects where the order of the values in
        ///   the enumeration does not matter in terms of equality.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The output can change if any object in <paramref name="values" />
        ///     changes state in such a way that it alters its hash code.
        ///     This is because each non-null object will have its own
        ///     hash code incorporated into the output.
        ///   </para>
        ///   <para>
        ///     A 'null' value is acceptable both as an item within the
        ///     <paramref name="values" /> enumeration and as the value of
        ///     the <paramref name="values" /> enumeration itself.
        ///   </para>
        /// </remarks>
        /// <param name="values">
        ///   An enumeration of objects that will be used to generate a
        ///   consistent hash code where the order of items in said enumeration
        ///   is irrelevant when determining its equality to another
        ///   enumeration of values.
        /// </param>
        /// <returns>
        ///   A consistent, deterministic hash code for input provided
        ///   via <paramref name="values" />.
        /// </returns>
        public static int FromSet(IEnumerable values) {
            return HashCode.FromSet(values, EqualityComparer<Object>.Default);
        }

        /// <summary>
        ///   Generates a consistent, deterministic hash code for an
        ///   enumeration of objects where the order of the values in
        ///   the enumeration does not matter in terms of equality.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     The output can change if any object in <paramref name="values" />
        ///     changes state in such a way that it alters the way
        ///     the <paramref name="comparer" /> provided calculates
        ///     its hash code.
        ///   </para>
        ///   <para>
        ///     A 'null' value is acceptable both as an item within the
        ///     <paramref name="values" /> enumeration and as the value of
        ///     the <paramref name="values" /> enumeration itself.
        ///   </para>
        /// </remarks>
        /// <param name="values">
        ///   An enumeration of objects that will be used to generate a
        ///   consistent hash code where the order of items in said enumeration
        ///   is irrelevant when determining its equality to another
        ///   enumeration of values.
        /// </param>
        /// <param name="comparer">
        ///   An <see cref="IEqualityComparer" /> implementation used to
        ///   generate all non-null hash codes for objects in <paramref name="values" />.
        /// </param>
        /// <returns>
        ///   A consistent, deterministic hash code for the input provided
        ///   via <paramref name="values" /> and <paramref name="comparer" />.
        /// </returns>
        public static int FromSet(IEnumerable values, IEqualityComparer comparer) {
            if (comparer == null) {
                throw new ArgumentNullException(nameof(comparer));
            }

            if (values == null) {
                return basePrime;
            }

            unchecked {
                var hash = basePrime;

                foreach (var value in values) {
                    if (value == null) {
                        hash ^= nullPrime;
                    } else {
                        hash ^= comparer.GetHashCode(value);
                    }
                }

                return hash;
            }
        }

    }

}
