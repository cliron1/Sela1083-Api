using System.ComponentModel.DataAnnotations;

namespace WebApi.Data.Entities;

public class User {
	public int Id { get; set; }
	[Required, MinLength(2)]
	public string Name { get; set; }
	[Required, EmailAddress]
	public string Email { get; set; }
	public string Pwd { get; set; }
}
