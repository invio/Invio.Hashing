using System;
using System.Collections.Generic;
using System.Linq;

namespace Invio.Hashing {

    public static class HashCode {

        // These constants has no significance other than they are prime,
        // limiting the liklihood that they will create hash codes that
        // are the same for distinct objects.

        private const int basePrime = 17;
        private const int iterationPrime = 23;
        private const int nullPrime = 31;

        public static int From(params object[] values) {
            if (values == null) {
                return HashCode.From(Enumerable.Empty<object>());
            }

            return HashCode.From((IEnumerable<object>)values);
        }

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

    }

}
