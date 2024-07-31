using ApiAuth.Domain.Primitives;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAuth.Domain.Entities;

[Table("tbl_auth")]
public class AuthEntity: Entity
{
    [Column("int_idauth")]
    public int Id { get; set; }
    [Column("vch_client_id")]
    public string ClientId { get; set; } = string.Empty;
    [Column("vch_user_name")]
    public string UserName { get; set; } = string.Empty;
    [Column("vch_password")]
    public string Password { get; set; } = string.Empty;
    [Column("int_estado")]
    public bool Estado { get; set; }
}