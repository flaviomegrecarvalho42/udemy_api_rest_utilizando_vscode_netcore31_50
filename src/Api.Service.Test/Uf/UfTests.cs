using System;
using System.Collections.Generic;
using Api.Domain.Dtos.Uf;

namespace Api.Service.Test.Uf
{

    /// <summary>
    /// Classe criada para conter as classes necessárias para trabalhar nos testes
    /// </summary>
    public class UfTests
    {
        public static string Name { get; set; }
        public static string Sigla { get; set; }
        public static Guid IdUf { get; set; }
        public List<UfDto> listUfDto = new List<UfDto>();
        public UfDto ufDto;

        public UfTests()
        {
            SetPublicProprities();
            CreateUfDto();
            CreateListUfDto();
        }

        /// <summary>
        /// Método para setar as proprities acima
        /// </summary>
        private void SetPublicProprities()
        {
            Name = Faker.Address.UsState();
            Sigla = Faker.Address.UsState().Substring(1, 3);
            IdUf = Guid.NewGuid();
        }

        /// <summary>
        /// Método para instanciar o UfDto
        /// </summary>
        private void CreateUfDto()
        {
            ufDto = new UfDto
            {
                Name = Name,
                Sigla = Sigla,
                Id = IdUf
            };
        }

        /// <summary>
        /// Método para instanciar o List<UfDto>
        /// </summary>
        private void CreateListUfDto()
        {
            for (var i = 0; i < 10; i++)
            {
                var ufDto = new UfDto()
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Address.UsState(),
                    Sigla = Faker.Address.UsState().Substring(1, 3)
                };

                listUfDto.Add(ufDto);
            }
        }
    }
}
