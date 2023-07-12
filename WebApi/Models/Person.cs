using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApi.Models;

public class Person {
    public int Id { get; set; }

    [Required, JsonPropertyName("fullName")]
    public string Name { get; set; }

    public string Email { get; set; }

    [JsonIgnore]
    public string Pwd { get; set; }
}
