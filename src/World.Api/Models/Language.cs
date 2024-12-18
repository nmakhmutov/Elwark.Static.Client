using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace World.Api.Models;

[JsonConverter(typeof(JsonConverter))]
public readonly struct Language : IComparable<Language>, IEquatable<Language>
{
    public static readonly Language English = new("en");

    private readonly string _value;

    public Language(string value) =>
        _value = value.ToUpper();

    public Language(CultureInfo culture) =>
        _value = culture.TwoLetterISOLanguageName.ToUpper();

    public override string ToString() =>
        _value;

    public bool Equals(Language other) =>
        _value == other._value;

    public override bool Equals(object? obj) =>
        obj is Language other && Equals(other);

    public override int GetHashCode() =>
        _value.GetHashCode();

    public int CompareTo(Language other) =>
        string.Compare(_value, other._value, StringComparison.Ordinal);

    public static bool operator ==(Language left, Language right) =>
        left.Equals(right);

    public static bool operator !=(Language left, Language right) =>
        !(left == right);

    public static implicit operator Language(CultureInfo language) =>
        new(language);

    private sealed class JsonConverter : JsonConverter<Language>
    {
        public override Language Read(ref Utf8JsonReader reader, Type _, JsonSerializerOptions options) =>
            new(JsonSerializer.Deserialize<string>(ref reader, options)!);

        public override void Write(Utf8JsonWriter writer, Language value, JsonSerializerOptions options) =>
            writer.WriteStringValue(value._value);
    }
}
