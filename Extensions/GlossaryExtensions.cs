using glossary.Services;
using System.Collections.Generic;

namespace glossary.Extensions {
    public static class GlossaryExtensions {
        public static IEnumerable<Glossary> Replace<Glossary>(this IEnumerable<Glossary> enumerable, int index, Glossary value)
        {
            int current = 0;
            foreach (var item in enumerable)
            {
                yield return current == index ? value : item;
                current++;
            }
        }
        public static IEnumerable<Glossary> Remove<Glossary>(this IEnumerable<Glossary> enumerable, int index)
        {
            int current = 0;
            foreach (var item in enumerable)
            {
                if (current != index)
                    yield return item;

                current++;
            }
        }
    }

}