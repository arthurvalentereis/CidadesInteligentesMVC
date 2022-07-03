using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaCandidato.Data.Entidade
{
    [Table("ClienteObservacao")]
    public class ClienteObservacao
    {
        [Key]
        [Column("codigo")]
        public int Codigo { get; set; }

        [Required]
        [Column("cliente_codigo")]
        [ForeignKey("Cliente")]
        public int ClienteCodigo { get; set; }
        public Cliente Cliente { get; set; }

        [StringLength(500)]
        [Required]
        [Column("observacao")]
        public string Observacao { get; set; }
    }
}
