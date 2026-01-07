namespace DemoMethodesExtension.Models;

/// <summary>
/// Provides extension methods for performing common mathematical operations on 32-bit signed integers.
/// </summary>
public static class Int32Extensions
{
    /// <summary>
    /// Determines whether the specified integer is a prime number.
    /// </summary>
    /// <remarks>A prime number is a natural number greater than 1 that is not divisible by any positive
    /// integer other than 1 and itself.</remarks>
    /// <param name="number">The integer value to test for primality.</param>
    /// <returns><see langword="true"/> if <paramref name="number"/> is a prime number; otherwise, <see langword="false"/>.</returns>
    public static bool IsPrime (this int number)
    {
        if (number < 2) return false;

        for (int i = 2; i <= Math.Sqrt(number); i++)
        {
            if (number % i == 0) return false;
        }

        return true;
    }

    /// <summary>
    /// Determines whether the specified integer is an even number.
    /// </summary>
    /// <param name="number">The integer value to evaluate.</param>
    /// <returns>true if the specified number is even; otherwise, false.</returns>
    public static bool IsEven(this int number)
    {
        return number % 2 == 0;
    }

    /// <summary>
    /// Calculates the factorial of the specified non-negative integer.
    /// </summary>
    /// <param name="number">The non-negative integer for which to compute the factorial.</param>
    /// <returns>The factorial of the specified number. Returns 1 if the number is 0.</returns>
    /// <exception cref="ArgumentException">Thrown when the value of number is less than 0.</exception>
    public static int Factorial(this int number)
    {
        if (number < 0) throw new ArgumentException("La valeur doit être positive.");
        return number == 0 ? 1 : number * Factorial(number - 1); // Fonction récursive
    }
}
