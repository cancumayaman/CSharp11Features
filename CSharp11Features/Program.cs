using System.Text.Unicode;

namespace CSharp11Features
{
    public class Program
    {
        public static void Main()
        {
            //Console.WriteLine("C# 11 YENİ ÖZELLİKLER DEMO UYGULAMASI\n");
            //Console.WriteLine("=====================================\n");

            //RawStringLiteralsDemo.Demo();
            //Console.WriteLine("\n" + new string('-', 50) + "\n");

            //GenericMathDemo.Demo();
            //Console.WriteLine("\n" + new string('-', 50) + "\n");

            //GenericAttributesDemo.Demo();
            //Console.WriteLine("\n" + new string('-', 50) + "\n");

            //Utf8StringLiteralsDemo.Demo();
            //Console.WriteLine("\n" + new string('-', 50) + "\n");

            //NewlinesInInterpolationDemo.Demo();
            //Console.WriteLine("\n" + new string('-', 50) + "\n");

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

            Console.WriteLine("\n\nDemo tamamlandı. Çıkmak için bir tuşa basın...");
            Console.ReadKey();
        }
    }

    // 1. Raw String Literals (Ham Metin Dizeleri) Örneği
    public class RawStringLiteralsDemo
    {
        public static void Demo()
        {
            Console.WriteLine("=== Raw String Literals Demo ===\n");

            // Amaç: Çok satırlı veya özel karakterler (", \, {) içeren metinleri kaçış karakterleri (\) kullanmadan daha okunaklı bir şekilde yazmaktır.
            // Problem: Özellikle JSON, XML, HTML gibi metinleri veya kod parçacıklarını bir string içinde tanımlamak, sürekli kaçış karakterleri kullanmayı gerektiriyordu. Bu durum kodun okunabilirliğini düşürüyordu.
            // Çözüm: C# 11 ile gelen "raw string literals", metni en az üç çift tırnak (""") arasına alarak bu sorunu çözer. Metin içindeki her şey, olduğu gibi kabul edilir.

            // Eski yöntem - kaçış karakterleri ile
            string oldJson = "{\r\n  \"name\": \"Ahmet\",\r\n  \"age\": 25,\r\n  \"address\": \"Istanbul\\\\Kadikoy\"\r\n}";
            Console.WriteLine("Eski yöntem:");
            Console.WriteLine(oldJson);

            // C# 11 ile - Raw String Literals
            // Metin, başlangıç ve bitişteki üç tırnak arasındaki girintiler korunarak olduğu gibi alınır.
            string newJson = """
        {
          "name": "Ahmet",
          "age": 25,
          "address": "Istanbul\Kadikoy"
        }
        """;
            Console.WriteLine("\nYeni yöntem:");
            Console.WriteLine(newJson);

            // Değişken ekleme (interpolation) örneği
            // Başına eklenen dolar ($) sayısı, interpolation için kullanılacak küme parantezi sayısını belirler.
            // $$ kullandığımız için değişkenleri {{degisken}} formatında ekleyebiliriz. Bu, metin içinde tek { } karakterlerinin serbestçe kullanılmasına olanak tanır.
            string name = "Mehmet";
            int age = 30;
            string interpolatedJson = $$"""
        {
          "name": "{{name}}",
          "age": {{age}}
        }
        """;
            Console.WriteLine("\nDeğişken ekleme ile:");
            Console.WriteLine(interpolatedJson);
        }
    }

    // 2. Generic Math Support (Genel Matematik Desteği) Örneği
    public class GenericMathDemo
    {
        // Amaç: Farklı sayısal türler (int, double, decimal vb.) için aynı matematiksel işlemi yapan tek bir genel (generic) metot yazabilmektir.
        // Problem: Eskiden, aynı mantığa sahip bir toplama işlemi için bile her sayısal tür için ayrı bir metot (overload) yazmak gerekiyordu. Bu, kod tekrarına yol açıyordu.
        // Çözüm: C# 11 ile gelen `System.Numerics.INumber<T>` gibi yeni arayüzler sayesinde, metotlarımızı genel olarak tanımlayabiliriz.
        // Bu arayüzler, `+`, `-`, `*`, `/` gibi operatörlerin ve `T.Zero` gibi statik özelliklerin genel türler üzerinde kullanılmasına olanak tanıyan "static abstract interface members" özelliğini kullanır.

        // Eski yöntem - Her tür için ayrı metot
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

        // C# 11 ile - Genel (Generic) matematik
        // `where T : System.Numerics.INumber<T>` kısıtlaması, bu metodun sadece sayısal özelliklere sahip türlerle çalışacağını garanti eder.
        // `T.Zero`, gelen türün sıfır değerini (int için 0, double için 0.0) verir.
        public static T Sum<T>(T[] numbers) where T : System.Numerics.INumber<T>
        {
            T sum = T.Zero;
            foreach (var number in numbers)
            {
                sum = sum + number; // Operatörler doğrudan T türü üzerinde kullanılabilir.
            }
            return sum;
        }

        public static void Demo()
        {
            Console.WriteLine("=== Generic Math Demo ===\n");

            int[] intArray = { 1, 2, 3, 4, 5 };
            double[] doubleArray = { 1.5, 2.5, 3.5 };

            // Eski yöntem
            Console.WriteLine($"Eski yöntem - Int toplam: {SumInt(intArray)}");
            Console.WriteLine($"Eski yöntem - Double toplam: {SumDouble(doubleArray)}");

            // Yeni yöntem ile tek bir metot tüm sayısal türler için çalışır.
            Console.WriteLine($"\nYeni yöntem - Int toplam: {Sum(intArray)}");
            Console.WriteLine($"Yeni yöntem - Double toplam: {Sum(doubleArray)}");
            Console.WriteLine($"Yeni yöntem - Decimal toplam: {Sum(new decimal[] { 10.5m, 20.5m })}");
        }
    }

    // 3. Generic Attributes (Genel Nitelikler) Örneği
    public class GenericAttributesDemo
    {
        // Amaç: Attribute (Nitelik) sınıflarında genel (generic) tür parametreleri kullanabilmektir.
        // Problem: C# 11 öncesinde, bir attribute'un bir türü parametre olarak alması gerekiyorsa, bu `typeof(TypeName)` şeklinde yapılıyordu. Bu hem daha uzun bir yazım sunuyor hem de derleme zamanı tür güvenliğini tam olarak sağlamıyordu.
        // Çözüm: Artık attribute sınıfları da `Attribute<T>` şeklinde genel olarak tanımlanabilir. Bu, daha temiz bir sözdizimi ve daha güçlü tür denetimi sağlar.

        // Doğrulayıcı arayüzü
        public interface IValidator
        {
            bool Validate(object value);
        }

        public class EmailValidator : IValidator
        {
            public bool Validate(object value) =>
                value is string email && email.Contains("@");
        }

        // Eski yöntem - Genel olmayan attribute
        public class OldValidationAttribute : Attribute
        {
            public Type ValidatorType { get; }
            public OldValidationAttribute(Type validatorType) // Tür, `Type` nesnesi olarak alınır.
            {
                ValidatorType = validatorType;
            }
        }

        // C# 11 ile - Genel (Generic) attribute
        public class ValidationAttribute<T> : Attribute where T : IValidator // `T` türü doğrudan parametre olarak kullanılır ve kısıtlanabilir.
        {
            public Type ValidatorType => typeof(T);
        }

        // Eski kullanım
        public class OldUser
        {
            [OldValidation(typeof(EmailValidator))]
            public string Email { get; set; }
        }

        // Yeni kullanım - daha okunaklı ve tür-güvenli
        public class NewUser
        {
            [Validation<EmailValidator>]
            public string Email { get; set; }
        }

        public static void Demo()
        {
            Console.WriteLine("=== Generic Attributes Demo ===\n");

            var validator = new EmailValidator();
            Console.WriteLine($"test@example.com geçerli mi? {validator.Validate("test@example.com")}");
            Console.WriteLine($"invalid-email geçerli mi? {validator.Validate("invalid-email")}");
        }
    }

    // 4. UTF-8 String Literals (UTF-8 Metin Dizeleri) Örneği
    public class Utf8StringLiteralsDemo
    {
        public static void Demo()
        {
            Console.WriteLine("=== UTF-8 String Literals Demo ===\n");

            // Amaç: Bir metni doğrudan UTF-8 formatında bir byte dizisine dönüştürmektir.
            // Problem: Eskiden bir metni UTF-8 byte dizisine çevirmek için `System.Text.Encoding.UTF8.GetBytes()` metodunu çağırmak gerekiyordu. Bu, hem daha uzun kod yazımına neden oluyor hem de çalışma zamanında bir dönüşüm maliyeti oluşturuyordu.
            // Çözüm: C# 11 ile bir metin dizesinin sonuna `u8` eki ekleyerek, derleyicinin bu metni doğrudan UTF-8 byte dizisi (`ReadOnlySpan<byte>`) olarak derlemesini sağlayabiliriz. Bu, daha performanslı ve daha kısa bir yazım sunar.

            // Eski yöntem
            byte[] oldUtf8 = System.Text.Encoding.UTF8.GetBytes("Hello World");
            Console.WriteLine("Eski yöntem - UTF8 byte'ları:");
            Console.WriteLine(string.Join(", ", oldUtf8.Take(10)) + "...");

            // C# 11 ile
            ReadOnlySpan<byte> newUtf8 = "Hello World"u8; // "u8" eki metni derleme zamanında byte dizisine çevirir.           
            Console.WriteLine("\nYeni yöntem - UTF8 byte'ları:");
            Console.WriteLine(string.Join(", ", newUtf8.ToArray().Take(10)) + "...");

            // Ters işlem: UTF-8 byte dizisini tekrar string'e çevirme
            string originalString = System.Text.Encoding.UTF8.GetString(newUtf8);

            // Performans avantajı özellikle web sunucuları gibi sık metin işleyen uygulamalarda önemlidir.
            ReadOnlySpan<byte> jsonResponse = """{"status":"success"}"""u8;
            Console.WriteLine($"\nJSON yanıtı byte uzunluğu: {jsonResponse.Length}");
        }
    }

    // 5. Newlines in String Interpolation (Değişken Eklemede Alt Satırlar) Örneği
    public class NewlinesInInterpolationDemo
    {
        public static void Demo()
        {
            Console.WriteLine("=== Newlines in String Interpolation Demo ===\n");

            // Amaç: String interpolation (`$"{...}"`) içindeki ifadelerin birden fazla satıra yayılmasına izin vermektir.
            // Problem: C# 11 öncesinde, `{}` içindeki ifadeler (örneğin, koşullu ifadeler veya LINQ sorguları) tek bir satırda olmak zorundaydı. Bu, karmaşık ifadelerin okunmasını zorlaştırıyordu.
            // Çözüm: Artık interpolation bloğu içindeki kodlar okunabilirliği artırmak için birden fazla satıra bölünebilir. Bu özellik, özellikle "raw string literals" ile birleştiğinde çok güçlü hale gelir.

            int x = 15;

            // Eski yöntem - Tek satırda yazım zorunluluğu
            var oldMessage = $"Result:\n {(x > 10 ? "Large" : "Small")}";
            Console.WriteLine("Eski yöntem:");
            Console.WriteLine(oldMessage);

            // C# 11 ile - Çok satırlı yazım
            // `{}` içindeki koşullu ifade (ternary operator) okunabilirlik için birden fazla satıra bölünmüştür.
            var newMessage = $"""
        Result: {(x > 10
                ? "Sayı 10'dan büyük"
                : "Sayı 10'dan küçük veya eşit")}
        """;
            Console.WriteLine("\nYeni yöntem:");
            Console.WriteLine(newMessage);

            // LINQ sorgusu ile örnek
            var students = new[]
            {
                new { Name = "Ali", Grade = 85 },
                new { Name = "Ayşe", Grade = 92 },
                new { Name = "Mehmet", Grade = 78 }
            };

            // LINQ sorgusu doğrudan interpolation içinde ve birden fazla satıra yayılarak kullanılmıştır.
            var report = $"""

        Başarılı öğrenciler:
        {string.Join("\n", students
                .Where(s => s.Grade >= 80)
                .Select(s => $"- {s.Name}: {s.Grade}"))}
        """;
            Console.WriteLine(report);
        }
    }

    // 6. List Patterns (Liste Desenleri) Örneği
    public class ListPatternsDemo
    {
        public static void Demo()
        {
            Console.WriteLine("=== List Patterns Demo ===\n");

            // Amaç: Dizilerin ve listelerin elemanlarını daha bildirimsel ve okunaklı bir sözdizimi ile kontrol etmektir.
            // Problem: Bir dizinin belirli elemanlara sahip olup olmadığını, belirli bir desenle başlayıp bittiğini kontrol etmek için `Length` ve indeks (`[0]`, `[1]`) kontrolleri yapmak gerekiyordu. Bu, kodun karmaşıklaşmasına neden oluyordu.
            // Çözüm: C# 11, liste desenleri ile `is` ve `switch` ifadelerinde dizileri ve listeleri desen eşleştirme ile kontrol etme yeteneği sunar.

            int[] numbers = { 1, 2, 3, 4, 5 };

            // Eski yöntem
            if (numbers.Length >= 2 && numbers[0] == 1 && numbers[1] == 2)
            {
                Console.WriteLine("Eski yöntem: İlk iki eleman 1 ve 2");
            }

            // C# 11 ile
            // `[1, 2, ..]` deseni: "Dizi 1 ve 2 ile başlar ve devamında herhangi bir sayıda eleman olabilir" anlamına gelir.
            // `..` (slice pattern) geri kalan elemanları temsil eder.
            if (numbers is [1, 2, ..])
            {
                Console.WriteLine("Yeni yöntem: 1 ve 2 ile başlıyor");
            }

            // `switch` ifadesi ile kullanımı
            string result = numbers switch
            {
                [] => "Boş dizi", // Dizi boş mu?
                [var single] => $"Tek eleman: {single}", // Dizide sadece bir eleman mı var?
                [var firstItem, var second] => $"İki eleman: {firstItem}, {second}", // Dizide sadece iki eleman mı var?
                [1, 2, 3, ..] => "1, 2, 3 ile başlıyor", // Dizi 1, 2, 3 ile mi başlıyor?
                [.., var lastItem] => $"Son eleman: {lastItem}", // Dizinin son elemanını al.
                _ => "Diğer durumlar"
            };

            Console.WriteLine($"\nDesen eşleştirme sonucu: {result}");

            // Dilim (slice) deseni ile aradaki elemanları yakalama
            // `[var first, .. var middle, var last]` deseni: ilk elemanı `first`'e, son elemanı `last`'e ve aradaki tüm elemanları `middle` adlı yeni bir diziye atar.
            // Paging de kullanışlı olabilir
            if (numbers is [var first, .. var middle, var last])
            {
                Console.WriteLine($"\nİlk: {first}, Son: {last}");
                Console.WriteLine($"Orta: {string.Join(", ", middle)}");
            }
        }
    }

    // 7. File-local Types (Dosya Kapsamında Tipler) Örneği
    // `file` anahtar kelimesi, bu sınıfın sadece bu dosya (`Program.cs`) içinde görünür ve kullanılabilir olmasını sağlar.
    // Başka bir dosyadan bu sınıfa erişilemez.    
    file class HelperClass
    {
        public static void Log(string message)
        {
            Console.WriteLine($"[Dosya-içi] {message}");
        }
    }

    public class FileLocalTypesDemo
    {
        public static void Demo()
        {
            Console.WriteLine("=== File-local Types Demo ===\n");

            // Amaç: Bir türün (class, struct, interface vb.) yalnızca tanımlandığı kaynak dosyası içinde erişilebilir olmasını sağlamaktır.
            // Problem: Bazen sadece tek bir dosyada kullanılacak yardımcı sınıflar oluştururuz. Bu sınıfları `internal` olarak tanımlasak bile projenin tamamından erişilebilir olurlar. Bu durum, istenmeyen kullanımlara veya isim çakışmalarına yol açabilir.
            // Çözüm: `file` erişim belirleyicisi, bir türün kapsamını mevcut dosya ile sınırlar. Bu, özellikle kaynak oluşturucular (source generators) tarafından üretilen kodlarda isim çakışmalarını önlemek için çok kullanışlıdır.

            // Dosya kapsamında tanımlanan sınıfı kullanma
            HelperClass.Log("Bu sınıf sadece bu dosyada görünür");

            Console.WriteLine("Dosya-içi türler, yalnızca tanımlandıkları dosyada kullanılabilir.");
            Console.WriteLine("Bu, isim çakışmalarını önler ve daha iyi kapsülleme (encapsulation) sağlar.");
        }
    }

    // 8. Required Members (Zorunlu Üyeler) Örneği
    // Eski yöntem: Zorunlu alanları doldurmak için genellikle constructor (yapıcı metot) kullanılırdı.
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

    // C# 11 ile
    public class NewUser
    {
        // Amaç: Bir sınıfın veya yapının nesnesi oluşturulurken bazı özelliklerin (property) mutlaka başlatılmasını zorunlu kılmaktır.
        // Problem: Nesne başlatıcıları (`new User { ... }`) kullanılırken, hangi özelliklerin doldurulmasının zorunlu olduğu derleyici tarafından kontrol edilemiyordu. Bu, `null` referans hatalarına veya eksik veri durumlarına yol açabiliyordu.
        // Çözüm: `required` anahtar kelimesi, bir özelliğin nesne başlatıcıda mutlaka bir değer alması gerektiğini belirtir. Eğer değer atanmazsa, derleme zamanında hata alınır.

        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public int? Age { get; set; } // `required` olmadığı için bu alan isteğe bağlıdır.
    }

    public class RequiredMembersDemo
    {
        public static void Demo()
        {
            Console.WriteLine("=== Required Members Demo ===\n");

            // Eski yöntem
            var oldUser = new OldUser("Ali", "Veli");
            Console.WriteLine($"Eski yöntem: {oldUser.FirstName} {oldUser.LastName}");

            // Yeni yöntem
            var newUser = new NewUser
            {
                FirstName = "Ayşe",    // Zorunlu
                LastName = "Yılmaz" // Zorunlu
                                    // Age isteğe bağlı olduğu için atanmadı.
            };
            Console.WriteLine($"Yeni yöntem: {newUser.FirstName} {newUser.LastName}");

            // Aşağıdaki kod derleme hatasına neden olur, çünkü zorunlu alanlar atanmamıştır:
            // var error = new NewUser(); // Hata: Required member 'FirstName' must be set.
        }
    }

    // 9. Auto-default Structs (Otomatik Varsayılan Değerli Yapılar) Örneği
    // Eski struct: Yapıcı metot (constructor) içinde tüm alanlara bir değer atanması zorunluydu.
    public struct OldPoint
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Label { get; set; }

        public OldPoint(int x)
        {
            X = x;
            Y = 0;       // Açıkça atanmak zorunda.
            Label = "";  // Açıkça atanmak zorunda.
        }
    }

    // C# 11 struct
    public struct NewPoint
    {
        // Amaç: `struct` yapıcı metotlarında (constructor) tüm alanları açıkça atama zorunluluğunu kaldırmaktır.
        // Problem: C# 11 öncesinde, bir `struct` için parametreli bir yapıcı metot tanımlandığında, o metot içinde `struct`'ın tüm alanlarına (`X`, `Y`, `Label` gibi) bir değer atanması gerekiyordu. Bu, gereksiz atamalara yol açabiliyordu.
        // Çözüm: Artık yapıcı metot içinde atanmayan alanlar, derleyici tarafından otomatik olarak varsayılan değerlerine (`0`, `null`, `false` vb.) atanır.

        public int X { get; set; }
        public int Y { get; set; }
        public string Label { get; set; }

        public NewPoint(int x)
        {
            X = x;
            // Y ve Label alanları otomatik olarak varsayılan değerlerini (0 ve null) alır.
        }
    }

    public class AutoDefaultStructsDemo
    {
        public static void Demo()
        {
            Console.WriteLine("=== Auto-default Structs Demo ===\n");

            var oldPoint = new OldPoint(10);
            Console.WriteLine($"Eski struct: X={oldPoint.X}, Y={oldPoint.Y}, Label='{oldPoint.Label}'");

            var newPoint = new NewPoint(10);
            Console.WriteLine($"Yeni struct: X={newPoint.X}, Y={newPoint.Y}, Label='{newPoint.Label}'");
            Console.WriteLine("Y ve Label otomatik olarak varsayılan değerlerini aldı!");
        }
    }

    // 10. Pattern Match Span<char> (Span<char> için Desen Eşleştirme) Örneği
    public class PatternMatchSpanDemo
    {
        // Amaç: `Span<char>` veya `ReadOnlySpan<char>` türlerini, liste desenleri (list patterns) kullanarak eşleştirebilmektir.
        // Problem: C# 11 öncesinde, liste desenleri sadece diziler ve `List<T>` gibi koleksiyonlarla çalışıyordu. `Span<T>` gibi performans odaklı türler için bu kullanışlı sözdizimi mevcut değildi. Bir metnin sonunun ".txt" olup olmadığını kontrol etmek için `EndsWith` gibi metotlar kullanmak gerekiyordu.
        // Çözüm: Artık `Span<char>` da bir `string` gibi desen eşleştirmeye tabi tutulabilir. Bu, özellikle bellek ayırmadan (allocation-free) metin işleme senaryolarında kodu basitleştirir.

        // `ReadOnlySpan<char>` üzerinde doğrudan desen eşleştirme kullanılıyor.
        static ReadOnlySpan<char> FindFileType(ReadOnlySpan<char> fileName)
        {
            // `fileName` bir string olmasa bile, karakter dizisi gibi desen eşleştirmeye tabi tutulabilir.
            return fileName switch
            {
                [.., '.', 't', 'x', 't'] => "Metin dosyası",
                [.., '.', 'j', 's', 'o', 'n'] => "JSON dosyası",
                [.., '.', 'x', 'm', 'l'] => "XML dosyası",
                [.., '.', 'c', 's'] => "C# dosyası",
                _ => "Bilinmeyen dosya türü"
            };
        }

        public static void Demo()
        {
            Console.WriteLine("=== Pattern Match Span<char> Demo ===\n");

            string[] files = { "document.txt", "data.json", "config.xml", "program.cs", "image.png" };

            foreach (var file in files)
            {
                // Bir string, `ReadOnlySpan<char>`'a örtülü olarak dönüştürülebilir.
                var type = FindFileType(file);
                Console.WriteLine($"{file} -> {type}");
            }
        }
    }

    // 11. Extended nameof Scope (Genişletilmiş nameof Kapsamı) Örneği
    public class ExtendedNameofDemo
    {
        // Attribute tanımı
        public class ParameterInfoAttribute : Attribute
        {
            public string Name { get; }
            public ParameterInfoAttribute(string name) => Name = name;
        }

        // Amaç: `nameof` operatörünün kullanım alanını genişleterek, metot ve lambda parametrelerinin isimlerini attribute'lar içinde de kullanılabilir hale getirmektir.
        // Problem: C# 11 öncesinde, bir metot parametresinin adını, o metoda uygulanan bir attribute içinde `nameof` ile alamazdınız.
        // Çözüm: Artık bir parametrenin adı (`nameof(value)`), o parametrenin ait olduğu metoda veya lambda'ya uygulanan bir attribute içinde kullanılabilir.

        // C# 11 ile, metot parametresinin adını (`value`) attribute içinde kullanabiliyoruz.
        [ParameterInfo(nameof(value))]
        public void ProcessValue(string value)
        {
            // `nameof` ayrıca null kontrolü gibi durumlarda parametre adını manuel yazma hatasını önler.
            ArgumentNullException.ThrowIfNull(value, nameof(value));
            Console.WriteLine($"İşlenen değer: {value}");
        }

        public static void Demo()
        {
            Console.WriteLine("=== Extended nameof Scope Demo ===\n");

            var demo = new ExtendedNameofDemo();

            // `nameof`'un lambda ifadelerinde kullanımı
            // Lambda ifadesinin parametresi olan `val`'ın adı, `nameof(val)` ile güvenli bir şekilde alınabilir.
            var validator = (string val) =>
                string.IsNullOrEmpty(val)
                    ? $"{nameof(val)} boş olamaz"
                    : "Geçerli";

            Console.WriteLine(validator(""));
            Console.WriteLine(validator("Test"));

            demo.ProcessValue("Örnek değer");
        }
    }

    // 12. Numeric IntPtr (Sayısal IntPtr) Örneği
    public class NumericIntPtrDemo
    {
        public static void Demo()
        {
            Console.WriteLine("=== Numeric IntPtr Demo ===\n");

            // Amaç: İşaretçi (pointer) boyutundaki tamsayılar olan `IntPtr` ve `UIntPtr`'ı, `nint` ve `nuint` takma adlarıyla daha kolay ve doğrudan sayısal işlemler için kullanılabilir hale getirmektir.
            // Problem: `IntPtr`, temel olarak bir işaretçiyi temsil etse de, üzerinde doğrudan aritmetik işlemler (+, -, *, /) yapılamıyordu. İşlem yapmak için `ToInt64()` gibi dönüşümler gerekiyordu, bu da kodu karmaşıklaştırıyordu.
            // Çözüm: C# 11 ile `nint` (signed) ve `nuint` (unsigned) türleri, `IntPtr` ve `UIntPtr` için birer takma ad olarak tanıtıldı ve bu türler artık standart sayısal türler gibi davranır. Bu, özellikle `unsafe` kod veya platformlar arası (interop) senaryolarda işaretçi aritmetiğini basitleştirir.

            // Eski yöntem: Aritmetik işlem için dönüşüm gerekiyordu.
            IntPtr oldPtr1 = new IntPtr(100);
            IntPtr oldPtr2 = new IntPtr(50);
            IntPtr oldSum = new IntPtr(oldPtr1.ToInt64() + oldPtr2.ToInt64());
            Console.WriteLine($"Eski yöntem toplam: {oldSum}");

            // C# 11 ile: `nint` türü, platforma bağlı boyutta (32-bit veya 64-bit) bir tamsayıyı temsil eder ve doğrudan aritmetik işlemlere izin verir.
            nint newPtr1 = 100;
            nint newPtr2 = 50;
            nint newSum = newPtr1 + newPtr2;
            Console.WriteLine($"Yeni yöntem toplam: {newSum}");

            // Tüm aritmetik operatörler desteklenir.
            nint a = 100, b = 30;
            Console.WriteLine($"\nAritmetik işlemler:");
            Console.WriteLine($"Toplama: {a} + {b} = {a + b}");
            Console.WriteLine($"Çıkarma: {a} - {b} = {a - b}");
            Console.WriteLine($"Çarpma: {a} * {b} = {a * b}");
            Console.WriteLine($"Bölme: {a} / {b} = {a / b}");
            Console.WriteLine($"Mod: {a} % {b} = {a % b}");
        }
    }

    // 13. ref Fields ve scoped ref Örneği
    public ref struct RefFieldsDemo
    {
        // Amaç: `ref struct` içinde referans (ref) alanlar tanımlayarak, başka bir değişkenin bellekteki yerine doğrudan işaret etmeyi sağlamaktır. `scoped` anahtar kelimesi ise referansların güvenli olmayan bir şekilde kaçmasını engeller.
        // Problem: Yüksek performans gerektiren senaryolarda, veriyi kopyalamak yerine ona referansla erişmek önemlidir.
        // Ancak referansların ömrünü yönetmek karmaşıktır ve "dangling pointers" (geçersiz referanslar) gibi hatalara yol açabilir.
        // Çözüm:
        // 1. `ref` alanlar: Bir `ref struct`, başka bir değişkenin bellekteki konumuna referans tutabilir. Bu sayede, `struct` üzerinden yapılan değişiklikler orijinal değişkeni etkiler.
        // 2. `scoped` anahtar kelimesi: Bir referansın (örneğin `Span<T>`) tanımlandığı kapsamın dışına (örneğin, metottan return edilerek) çıkmasını engeller. Bu, yığın (stack) üzerinde ayrılan belleğe güvenli erişimi garanti eder.

        private ref int value; // Bu alan, dışarıdaki bir `int` değişkeninin referansını tutar.

        public RefFieldsDemo(ref int source)
        {
            value = ref source;
        }

        public void Increment()
        {
            value++; // Bu işlem, referans alınan orijinal `number` değişkenini değiştirir.
        }

        public static void Demo()
        {
            Console.WriteLine("=== ref Fields ve scoped ref Demo ===\n");

            // `ref` alan örneği
            int number = 10;
            Console.WriteLine($"Başlangıç değeri: {number}");

            var refStruct = new RefFieldsDemo(ref number);
            refStruct.Increment();
            Console.WriteLine($"ref alan ile artırdıktan sonra: {number}"); // Orijinal değişkenin değeri değişti.

            // `scoped` kullanımı
            DemoScoped();
        }

        private static void DemoScoped()
        {
            Span<int> numbers = stackalloc int[] { 1, 2, 3, 4, 5 };

            // `scoped`, `scopedSpan`'in bu metot dışına kaçmasını (örneğin return edilmesini) engeller.
            // Bu, `scopedSpan`'in işaret ettiği belleğin her zaman geçerli kalacağını garanti eder.
            scoped Span<int> scopedSpan = numbers;

            Process(scopedSpan);
            Console.WriteLine($"\nscoped span işlendi: {string.Join(", ", scopedSpan.ToArray())}");
        }

        // Parametre olarak gelen `span`'in de bu metot dışına çıkamayacağı `scoped` ile belirtilmiştir.
        private static void Process(scoped Span<int> span)
        {
            // `span` sadece bu metot içinde güvenle kullanılabilir.
            for (int i = 0; i < span.Length; i++)
            {
                span[i] = span[i] * 2;
            }
        }
    }

    // 14. Improved Method Group Conversion (Geliştirilmiş Metot Grubu Dönüşümü) Örneği
    public class MethodGroupConversionDemo
    {
        private static readonly List<int> numbers = new() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        // Test için kullanılacak metotlar
        private static bool IsEven(int number) => number % 2 == 0;
        private static string ConvertNumberToText(int number) => $"Sayı: {number}";

        public static void Demo()
        {
            Console.WriteLine("=== Improved Method Group Conversion Demo ===\n");

            // Amaç: Bir metot adının (metot grubu) bir delegeye (delegate) dönüştürülmesi sırasında oluşan bellek ayırmalarını (allocation) azaltmaktır.
            // Problem: C# 11 öncesinde, bir metot adını (örneğin `IsEven`) LINQ gibi bir yerde kullandığınızda, derleyici her kullanım için yeni bir delege nesnesi oluşturuyordu. Bu, özellikle döngüler içinde veya sık çağrılan kodlarda gereksiz bellek kullanımına yol açıyordu.
            // Çözüm: C# 11 derleyicisi artık bu dönüşümleri daha akıllıca yapar. Eğer mümkünse, aynı metot grubu için oluşturulan delege nesnesini önbelleğe alır ve tekrar kullanır. Bu, bellek ayırmalarını azaltarak performansı artırır. Geliştirici olarak yapmanız gereken ek bir şey yoktur, bu bir derleyici optimizasyonudur.

            Console.WriteLine("C# 11 öncesi: Metot grupları her kullanımda yeni bir delege nesnesi oluştururdu.");
            Console.WriteLine("C# 11 ile: Derleyici delegeleri otomatik olarak önbelleğe alır.\n");

            // Metot grubu kullanımı - C# 11 ile optimize edildi
            // `Where(IsEven)` çağrısında `IsEven` için bir delege oluşturulur ve önbelleğe alınır.
            var evenNumbers = numbers.Where(IsEven).ToList();
            Console.WriteLine($"Çift sayılar: {string.Join(", ", evenNumbers)}");

            // Tekrar kullanım - aynı önbelleğe alınmış delege kullanılır, yeni nesne oluşturulmaz.
            var evenNumbers2 = numbers.Where(IsEven).ToList();
            Console.WriteLine($"Çift sayılar tekrar: {string.Join(", ", evenNumbers2)}");

            // `Select` ile metot grubu kullanımı
            var texts = numbers.Take(3).Select(ConvertNumberToText);
            Console.WriteLine($"\nMetne dönüştürüldü: {string.Join(", ", texts)}");

            Console.WriteLine("\nC# 11 ile bu işlemler daha az bellek ayırmasıyla sonuçlanır!");
        }
    }

    // 15. Warning Wave 7 (Uyarı Dalgası 7) Örneği
    public class WarningWave7Demo
    {
        // Amaç: `<AnalysisLevel>7.0</AnalysisLevel>` veya daha üstü ayarlandığında, kod kalitesini artırmaya yönelik yeni derleyici uyarıları sunmaktır.
        // Problem: Bazı yaygın kodlama hataları veya stil sorunları derleyici tarafından fark edilmiyordu. Bu durum, potansiyel hatalara veya tutarsız koda yol açabiliyordu.
        // Çözüm: "Warning Waves", yeni uyarıları mevcut projelere kademeli olarak eklemeyi sağlar. Wave 7, özellikle null kontrolü, isim standartları ve `required` üyelerle ilgili yeni uyarılar getirir.

        // CS8981 uyarısı örneği - küçük harfle başlayan public tür
        // public class user { } // Bu, "user" küçük harfle başladığı için bir uyarı verir.

        // Düzeltilmiş versiyon
        public class User
        {
            public string Name { get; set; } = "";
        }

        // Nullable referans uyarıları
        public class NullableExample
        {
            private string? nullableText; // Bu alan null olabilir.

            // Potansiyel null referans uyarısı
            public int UnsafeTextLength()
            {
                // return nullableText.Length; // Bu satır, `nullableText` null olabileceğinden CS8602 uyarısı verir.
                return nullableText?.Length ?? 0; // Düzeltilmiş hali: Null ise 0 döndür.
            }
        }

        // `required` üyeler uyarısı
        public class RequiredExample
        {
            public required string Name { get; set; }
            public required int Age { get; set; }

            // Eğer yapıcı metot `required` üyeleri başlatmazsa, CS9035 uyarısı verir.
            // `SetsRequiredMembers` attribute'u, derleyiciye bu metodun zorunlu üyeleri atadığını bildirir ve uyarıyı kaldırır.
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

            Console.WriteLine("C# 11 Uyarı Dalgası 7 yeni derleyici uyarıları sunar:");
            Console.WriteLine("1. CS8981: Küçük harfle başlayan public türler için uyarı.");
            Console.WriteLine("2. CS8602: Potansiyel null referans kullanımı için uyarı.");
            Console.WriteLine("3. CS9035: `required` üyeler başlatılmadığında uyarı.");
            Console.WriteLine("4. CS8618: Null atanamayan özelliklerin başlatılmamasıyla ilgili uyarılar.");

            // Güvenli kullanım örnekleri
            var user = new User { Name = "Test Kullanıcısı" };
            Console.WriteLine($"\nKullanıcı adı: {user.Name}");

            var nullableExample = new NullableExample();
            Console.WriteLine($"Güvenli metin uzunluğu: {nullableExample.UnsafeTextLength()}");

            var requiredExample = new RequiredExample("Ali", 25);
            Console.WriteLine($"Zorunlu üye örneği: {requiredExample.Name}, {requiredExample.Age} yaşında");

            Console.WriteLine("\nBu uyarılar daha güvenli kod yazmamıza yardımcı olur!");
        }
    }

}