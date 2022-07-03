using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ProvaCandidato.Data.Entidade
{
  [Table("Cidade")]
  public class Cidade
  {
    [Key]
    [Column("codigo")]
    public int Codigo { get; set; }

    [Column("nome")]
    [StringLength(50)]
    [MinLength(3)]
    [MaxLength(50)]
    [Required]
    [Display(Name = "Nome da Cidade do Cliente")]
    public string Nome { get; set; }

  }
}
