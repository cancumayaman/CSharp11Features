namespace CSharp11Features
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("C# 11 NEW FEATURES DEMO APPLICATION\n");
            Console.WriteLine("=====================================\n");

            RawStringLiteralsDemo.Demo();
            Console.WriteLine("\n" + new string('-', 50) + "\n");

            GenericMathDemo.Demo();
            Console.WriteLine("\n" + new string('-', 50) + "\n");

            GenericAttributesDemo.Demo();
            Console.WriteLine("\n" + new string('-', 50) + "\n");

            Utf8StringLiteralsDemo.Demo();
            Console.WriteLine("\n" + new string('-', 50) + "\n");

            NewlinesInInterpolationDemo.Demo();
            Console.WriteLine("\n" + new string('-', 50) + "\n");

            ListPatternsDemo.Demo();
            Console.WriteLine("\n" + new string('-', 50) + "\n");

            FileLocalTypesDemo.Demo();
            Console.WriteLine("\n" + new string('-', 50) + "\n");

            RequiredMembersDemo.Demo();
            Console.WriteLine("\n" + new string('-', 50) + "\n");

            AutoDefaultStructsDemo.Demo();
            Console.WriteLine("\n" + new string('-', 50) + "\n");

            PatternMatchSpanDemo.Demo();
            Console.WriteLine("\n" + new string('-', 50) + "\n");

            ExtendedNameofDemo.Demo();
            Console.WriteLine("\n" + new string('-', 50) + "\n");

            NumericIntPtrDemo.Demo();
            Console.WriteLine("\n" + new string('-', 50) + "\n");

            RefFieldsDemo.Demo();
            Console.WriteLine("\n" + new string('-', 50) + "\n");

            MethodGroupConversionDemo.Demo();
            Console.WriteLine("\n" + new string('-', 50) + "\n");

            WarningWave7Demo.Demo();

            Console.WriteLine("\n\nDemo completed. Press any key to exit...");
            Console.ReadKey();
        }
    }

    // 1. Raw String Literals Example
    public class RawStringLiteralsDemo
    {
        public static void Demo()
        {
            Console.WriteLine("=== Raw String Literals Demo ===\n");

            // Old way - with escape characters
            string oldJson = "{\r\n  \"name\": \"Ahmet\",\r\n  \"age\": 25,\r\n  \"address\": \"Istanbul\\\\Kadikoy\"\r\n}";
            Console.WriteLine("Old way:");
            Console.WriteLine(oldJson);

            // With C# 11 - Raw String Literals
            string newJson = """
        {
          "name": "Ahmet",
          "age": 25,
          "address": "Istanbul\Kadikoy"
        }
        """;
            Console.WriteLine("\nNew way:");
            Console.WriteLine(newJson);

            // Interpolation example
            string name = "Mehmet";
            int age = 30;
            string interpolatedJson = $$"""
        {
          "name": "{{name}}",
          "age": {{age}}
        }
        """;
            Console.WriteLine("\nWith Interpolation:");
            Console.WriteLine(interpolatedJson);
        }
    }

    // 2. Generic Math Support Example
    public class GenericMathDemo
    {
        // Old way - Separate method for each type
        public static int SumInt(int[] numbers)
        {
            int sum = 0;
            foreach (var number in numbers)
                sum += number;
            return sum;
        }

        public static double SumDouble(double[] numbers)
        {
            double sum = 0;
            foreach (var number in numbers)
                sum += number;
            return sum;
        }

        // With C# 11 - Generic math
        public static T Sum<T>(T[] numbers) where T : System.Numerics.INumber<T>
        {
            T sum = T.Zero;
            foreach (var number in numbers)
            {
                sum = sum + number;
            }
            return sum;
        }

        public static void Demo()
        {
            Console.WriteLine("=== Generic Math Demo ===\n");

            int[] intArray = { 1, 2, 3, 4, 5 };
            double[] doubleArray = { 1.5, 2.5, 3.5 };

            // Old way
            Console.WriteLine($"Old way - Int sum: {SumInt(intArray)}");
            Console.WriteLine($"Old way - Double sum: {SumDouble(doubleArray)}");

            // New way
            Console.WriteLine($"\nNew way - Int sum: {Sum(intArray)}");
            Console.WriteLine($"New way - Double sum: {Sum(doubleArray)}");
            Console.WriteLine($"New way - Decimal sum: {Sum(new decimal[] { 10.5m, 20.5m })}");
        }
    }

    // 3. Generic Attributes Example
    // Validator interface
    public interface IValidator
    {
        bool Validate(object value);
    }

    public class EmailValidator : IValidator
    {
        public bool Validate(object value) =>
            value is string email && email.Contains("@");
    }

    // Old way - Non-generic attribute
    public class OldValidationAttribute : Attribute
    {
        public Type ValidatorType { get; }
        public OldValidationAttribute(Type validatorType)
        {
            ValidatorType = validatorType;
        }
    }

    // With C# 11 - Generic attribute
    public class ValidationAttribute<T> : Attribute where T : IValidator
    {
        // We can use constraint such as T is Class
        public Type ValidatorType => typeof(T);
    }

    public class GenericAttributesDemo
    {
        // Old usage
        public class OldUser
        {
            [OldValidation(typeof(EmailValidator))]
            public string Email { get; set; }
        }

        // New usage
        public class NewUser
        {            
            [Validation<EmailValidator>]
            public string Email { get; set; }
        }

        public static void Demo()
        {
            Console.WriteLine("=== Generic Attributes Demo ===\n");

            var validator = new EmailValidator();
            Console.WriteLine($"Is test@example.com valid? {validator.Validate("test@example.com")}");
            Console.WriteLine($"Is invalid-email valid? {validator.Validate("invalid-email")}");
        }
    }

    // 4. UTF-8 String Literals Example
    public class Utf8StringLiteralsDemo
    {
        public static void Demo()
        {
            Console.WriteLine("=== UTF-8 String Literals Demo ===\n");

            // Old way
            byte[] oldUtf8 = System.Text.Encoding.UTF8.GetBytes("Hello World");
            Console.WriteLine("Old way - UTF8 bytes:");
            Console.WriteLine(string.Join(", ", oldUtf8.Take(10)) + "...");

            // With C# 11
            ReadOnlySpan<byte> newUtf8 = "Hello World"u8;
            Console.WriteLine("\nNew way - UTF8 bytes:");
            Console.WriteLine(string.Join(", ", newUtf8.ToArray().Take(10)) + "...");

            // Performance example
            ReadOnlySpan<byte> jsonResponse = """{"status":"success"}"""u8;
            Console.WriteLine($"\nJSON response byte length: {jsonResponse.Length}");
        }
    }

    // 5. Newlines in String Interpolation Example
    public class NewlinesInInterpolationDemo
    {
        public static void Demo()
        {
            Console.WriteLine("=== Newlines in String Interpolation Demo ===\n");

            int x = 15;

            // Old way - Single line
            // Şuan hata vermez çünkü şuan version 11 ama öncesinde multiline stringlerde hata veriyordu.
            var oldMessage = $"Result: {(x > 10 ? "Large" : "Small")}";
            Console.WriteLine("Old way:");
            Console.WriteLine(oldMessage);

            // With C# 11 - Multi-line
            var newMessage = $"""
        Result: {(x > 10
                ? "Number is greater than 10"
                : "Number is less than or equal to 10")}
        """;
            Console.WriteLine("\nNew way:");
            Console.WriteLine(newMessage);

            // LINQ example
            var students = new[]
            {
            new { Name = "Ali", Grade = 85 },
            new { Name = "Ayşe", Grade = 92 },
            new { Name = "Mehmet", Grade = 78 }
        };

            var report = $"""

        Successful students:
        {string.Join("\n", students
                .Where(s => s.Grade >= 80)
                .Select(s => $"- {s.Name}: {s.Grade}"))}
        """;
            Console.WriteLine(report);
        }
    }

    // 6. List Patterns Example
    public class ListPatternsDemo
    {
        public static void Demo()
        {
            Console.WriteLine("=== List Patterns Demo ===\n");

            int[] numbers = { 1, 2, 3, 4, 5 };

            // Old way
            if (numbers.Length >= 2 && numbers[0] == 1 && numbers[1] == 2)
            {
                Console.WriteLine("Old way: First two elements are 1 and 2");
            }

            // With C# 11
            if (numbers is [1, 2, ..])
            {
                Console.WriteLine("New way: Starts with 1 and 2");
            }

            // With switch expression
            string result = numbers switch
            {
                [] => "Empty array",
                [var single] => $"Single element: {single}",
                [var firstItem, var second] => $"Two elements: {firstItem}, {second}",
                [1, 2, 3, ..] => "Starts with 1, 2, 3",
                [.., var lastItem] => $"Last element: {lastItem}",
                _ => "Other cases"
            };

            Console.WriteLine($"\nPattern matching result: {result}");

            // Slice pattern
            if (numbers is [var first, .. var middle, var last])
            {
                Console.WriteLine($"\nFirst: {first}, Last: {last}");
                Console.WriteLine($"Middle: {string.Join(", ", middle)}");
            }
        }
    }

    // 7. File-local Types Example
    file class HelperClass
    {
        public static void Log(string message)
        {
            Console.WriteLine($"[File-local] {message}");
        }
    }

    public class FileLocalTypesDemo
    {
        public static void Demo()
        {
            Console.WriteLine("=== File-local Types Demo ===\n");

            // Using file-scoped class
            HelperClass.Log("This class is only visible in this file");

            Console.WriteLine("File-local types can only be used in the file where they are defined.");
            Console.WriteLine("This prevents naming conflicts and provides better encapsulation.");
        }
    }

    // 8. Required Members Example
    // Old way
    public class OldUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public OldUser(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }

    // With C# 11
    public class NewUser
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public int? Age { get; set; } // Optional
    }

    public class RequiredMembersDemo
    {
        public static void Demo()
        {
            Console.WriteLine("=== Required Members Demo ===\n");

            // Old way
            var oldUser = new OldUser("Ali", "Veli");
            Console.WriteLine($"Old way: {oldUser.FirstName} {oldUser.LastName}");

            // New way
            var newUser = new NewUser
            {
                FirstName = "Ayşe",    // Required
                LastName = "Yılmaz" // Required
                                    // Age is optional
            };
            Console.WriteLine($"New way: {newUser.FirstName} {newUser.LastName}");

            // The following code would cause a compile-time error:
            // var error = new NewUser(); // FirstName and LastName are required!
        }
    }

    // 9. Auto-default Structs Example
    // Old struct
    public struct OldPoint
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Label { get; set; }

        public OldPoint(int x)
        {
            X = x;
            Y = 0;       // Must be explicitly assigned
            Label = "";  // Must be explicitly assigned
        }
    }

    // C# 11 struct
    public struct NewPoint
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Label { get; set; }

        public NewPoint(int x)
        {
            X = x;
            // Y and Label automatically get their default values
        }
    }

    public class AutoDefaultStructsDemo
    {
        public static void Demo()
        {
            Console.WriteLine("=== Auto-default Structs Demo ===\n");

            var oldPoint = new OldPoint(10);
            Console.WriteLine($"Old struct: X={oldPoint.X}, Y={oldPoint.Y}, Label='{oldPoint.Label}'");

            var newPoint = new NewPoint(10);
            Console.WriteLine($"New struct: X={newPoint.X}, Y={newPoint.Y}, Label='{newPoint.Label}'");
            Console.WriteLine("Y and Label automatically got their default values!");
        }
    }

    // 10. Pattern Match Span<char> Example
    public class PatternMatchSpanDemo
    {
        // Before C# 11 , We couldn't use pattern matching directly on spans. Only arrays.
        static ReadOnlySpan<char> FindFileType(ReadOnlySpan<char> fileName)
        {
            return fileName switch
            {
                [.., '.', 't', 'x', 't'] => "Text file",
                [.., '.', 'j', 's', 'o', 'n'] => "JSON file",
                [.., '.', 'x', 'm', 'l'] => "XML file",
                [.., '.', 'c', 's'] => "C# file",
                _ => "Unknown file type"
            };
        }

        public static void Demo()
        {
            Console.WriteLine("=== Pattern Match Span<char> Demo ===\n");

            string[] files = { "document.txt", "data.json", "config.xml", "program.cs", "image.png" };

            foreach (var file in files)
            {
                var type = FindFileType(file);
                Console.WriteLine($"{file} -> {type}");
            }
        }
    }

    // 11. Extended nameof Scope Example
    public class ExtendedNameofDemo
    {
        // Attribute definition
        public class ParameterInfoAttribute : Attribute
        {
            public string Name { get; }
            public ParameterInfoAttribute(string name) => Name = name;
        }

        // With C# 11, we can use the method parameter's name
        [ParameterInfo(nameof(value))]
        public void ProcessValue(string value)
        {
            ArgumentNullException.ThrowIfNull(value, nameof(value));
            Console.WriteLine($"Processed value: {value}");
        }

        public static void Demo()
        {
            Console.WriteLine("=== Extended nameof Scope Demo ===\n");

            var demo = new ExtendedNameofDemo();

            // nameof in lambda expressions
            // we can use nameof in lambda expressions and local functions (val)
            var validator = (string val) =>
                string.IsNullOrEmpty(val)
                    ? $"{nameof(val)} cannot be empty"
                    : "Valid";

            Console.WriteLine(validator(""));
            Console.WriteLine(validator("Test"));

            demo.ProcessValue("Example value");
        }
    }

    // 12. Numeric IntPtr Example
    public class NumericIntPtrDemo
    {
        public static void Demo()
        {
            Console.WriteLine("=== Numeric IntPtr Demo ===\n");

            // This uses in unsafe code or interop scenarios
            // We cannot use int or long directly for pointer arithmetic because they are not pointer-sized integers.
            // Adresses can vary in size depending on the platform (32-bit vs 64-bit) so int size is not enough.
            // Old way
            // 0x12345678
            IntPtr oldPtr1 = new IntPtr(100);
            IntPtr oldPtr2 = new IntPtr(50);
            IntPtr oldSum = new IntPtr(oldPtr1.ToInt64() + oldPtr2.ToInt64());
            Console.WriteLine($"Old way sum: {oldSum}");

            // With C# 11
            // nint is a new type that represents a pointer-sized integer instead of IntPtr
            nint newPtr1 = 100;
            nint newPtr2 = 50;
            nint newSum = newPtr1 + newPtr2;
            Console.WriteLine($"New way sum: {newSum}");

            // All arithmetic operators
            nint a = 100, b = 30;
            Console.WriteLine($"\nArithmetic operations:");
            Console.WriteLine($"Addition: {a} + {b} = {a + b}");
            Console.WriteLine($"Subtraction: {a} - {b} = {a - b}");
            Console.WriteLine($"Multiplication: {a} * {b} = {a * b}");
            Console.WriteLine($"Division: {a} / {b} = {a / b}");
            Console.WriteLine($"Modulus: {a} % {b} = {a % b}");
        }
    }

    // 13. ref Fields and scoped ref Example
    public ref struct RefFieldsDemo
    {
        private ref int value;

        public RefFieldsDemo(ref int source)
        {
            value = ref source;
        }

        public void Increment()
        {
            value++; // Modifies the original value
        }

        public static void Demo()
        {
            Console.WriteLine("=== ref Fields and scoped ref Demo ===\n");

            // ref field example
            int number = 10;
            Console.WriteLine($"Starting value: {number}");

            var refStruct = new RefFieldsDemo(ref number);
            refStruct.Increment();
            Console.WriteLine($"After incrementing with ref field: {number}");

            // scoped usage
            DemoScoped();
        }

        private static void DemoScoped()
        {
            Span<int> numbers = stackalloc int[] { 1, 2, 3, 4, 5 };

            // scoped prevents the span from escaping the method
            scoped Span<int> scopedSpan = numbers;

            // Before scoped span, we could use Span<T> but it could escape the method scope so if we returned it, it would cause issues it can loose its validity.
            Process(scopedSpan);
            Console.WriteLine($"\nscoped span processed: {string.Join(", ", scopedSpan.ToArray())}");
        }

        private static void Process(scoped Span<int> span)
        {
            // span can only be used within this method
            for (int i = 0; i < span.Length; i++)
            {
                span[i] = span[i] * 2;
            }
        }
    }

    // 14. Improved Method Group Conversion Example
    public class MethodGroupConversionDemo
    {
        private static readonly List<int> numbers = new() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        // Methods to be used for testing
        private static bool IsEven(int number) => number % 2 == 0;
        private static string ConvertNumberToText(int number) => $"Number: {number}";

        public static void Demo()
        {
            Console.WriteLine("=== Improved Method Group Conversion Demo ===\n");

            // Old state information
            Console.WriteLine("Before C# 11: Method groups would make a new delegate allocation for each use.");
            Console.WriteLine("With C# 11: The compiler automatically caches delegates.\n");

            //Every uses, the compiler would create a new delegate instance.
            // With C# 11, the compiler optimizes this by caching the delegate for method groups.
            // Method group usage - optimized with C# 11
            var evenNumbers = numbers.Where(IsEven).ToList();
            Console.WriteLine($"Even numbers: {string.Join(", ", evenNumbers)}");

            // Multiple uses - the same cached delegate is used
            var evenNumbers2 = numbers.Where(IsEven).ToList();
            Console.WriteLine($"Even numbers again: {string.Join(", ", evenNumbers2)}");

            // Method group with Select
            var texts = numbers.Take(3).Select(ConvertNumberToText);
            Console.WriteLine($"\nConverted to text: {string.Join(", ", texts)}");

            Console.WriteLine("\nWith C# 11, these operations result in fewer memory allocations!");
        }
    }

    // 15. Warning Wave 7 Example
    public class WarningWave7Demo
    {
        // CS8981 warning example - public type starting with lowercase
        // public class user { } // This would give a warning

        // Corrected version
        public class User
        {
            public string Name { get; set; } = "";
        }

        // Nullable reference warnings
        public class NullableExample
        {
            private string? nullableText;

            // Potential null reference warning
            public int UnsafeTextLength()
            {
                // return nullableText.Length; // Would give CS8602 warning
                return nullableText?.Length ?? 0; // Corrected
            }
        }

        // Required members warning
        public class RequiredExample
        {
            public required string Name { get; set; }
            public required int Age { get; set; }

            // If constructor does not initialize required members, it gives a warning
            [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
            public RequiredExample(string name, int age)
            {
                Name = name;
                Age = age;
            }
        }

        public static void Demo()
        {
            Console.WriteLine("=== Warning Wave 7 Demo ===\n");

            Console.WriteLine("C# 11 Warning Wave 7 introduces new compiler warnings:");
            Console.WriteLine("1. CS8981: Warning for public types starting with lowercase");
            Console.WriteLine("2. CS8602: Warning for potential null reference usage");
            Console.WriteLine("3. CS9035: Warning when required members are not initialized");
            Console.WriteLine("4. CS8618: Non-nullable property warnings");

            // Safe usage examples
            var user = new User { Name = "Test User" };
            Console.WriteLine($"\nUser name: {user.Name}");

            var nullableExample = new NullableExample();
            Console.WriteLine($"Safe text length: {nullableExample.UnsafeTextLength()}");

            var requiredExample = new RequiredExample("Ali", 25);
            Console.WriteLine($"Required example: {requiredExample.Name}, {requiredExample.Age} years old");

            Console.WriteLine("\nThese warnings help us write safer code!");
        }
    }

}
