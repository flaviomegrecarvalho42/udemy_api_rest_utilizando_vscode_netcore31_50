using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.Municipio
{
    public class MunicipioCreateDto
    {
        [Required(ErrorMessage = "Nome do município é campo obrigatório")]
        [StringLength(60, ErrorMessage = "Nome do município deve ter no máximo {1} caracteres")]
        public string Name { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "código do IBGE inválido")]
        public int CodIBGE { get; set; }

        [Required(ErrorMessage = "UF é campo obrigatório")]
        public Guid UfId { get; set; }
    }
}
