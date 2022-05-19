using System;

namespace Api.Domain.Dtos.Municipio
{
    public class MunicipioUpdateResultDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CodIBGE { get; set; }
        public Guid UfId { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
