using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class IEnumerableExtensions
{
    public static bool TryGetFirst<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate, out T result)
    {
        if (enumerable != null)
        {
            if (enumerable.Any(predicate))
            {
                result = enumerable.First(predicate);
                return true;
            }
        }
        result = default(T);
        return false;
    }
}
