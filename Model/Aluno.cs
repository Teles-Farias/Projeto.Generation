using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace blogpessoal.Model
{
    public class Aluno
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id {get; set;}

        [Column(TypeName = "Varchar")]
        [StringLength(255)]
        public string Nome {get; set;} = string.Empty;

        [Column(TypeName = "Varchar")]
        [StringLength(255)]
        public string Idade {get; set;} = string.Empty;

        [Column(TypeName = "Varchar")]
        [StringLength(255)]
        public string NotaPrimeiroSemestre {get; set;} = string.Empty;

        [Column(TypeName = "Varchar")]
        [StringLength(255)]
        public string NotaSegundoSemestre {get; set;} = string.Empty;

        [Column(TypeName = "Varchar")]
        [StringLength(255)]
        public string Professor {get; set;} = string.Empty;

        [Column(TypeName = "Varchar")]
        [StringLength(255)]
        public string Sala {get; set;} = string.Empty;

        
    }
}