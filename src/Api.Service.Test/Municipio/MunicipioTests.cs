using System;
using System.Collections.Generic;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Dtos.Uf;

namespace Api.Service.Test.Municipio
{
    /// <summary>
    /// Classe criada para conter as classes necessárias para trabalhar nos testes
    /// </summary>
    public class MunicipioTests
    {
        public static string Name { get; set; }
        public static int CodIBGE { get; set; }
        public static string NameUpdated { get; set; }
        public static int CodIBGEUpdated { get; set; }
        public static Guid MunicipioId { get; set; }
        public static Guid UfId { get; set; }
        public List<MunicipioDto> listMunicipioDto = new List<MunicipioDto>();
        public MunicipioDto municipioDto;
        public MunicipioCompletoDto municipioCompletoDto;
        public MunicipioCreateDto municipioCreateDto;
        public MunicipioCreateResultDto municipioCreateResultDto;
        public MunicipioUpdateDto municipioUpdateDto;
        public MunicipioUpdateResultDto municipioUpdateResultDto;

        public MunicipioTests()
        {
            SetPublicProprities();
            CreateDtos();
        }

        /// <summary>
        /// Método para setar as proprities acima
        /// </summary>
        private void SetPublicProprities()
        {
            MunicipioId = Guid.NewGuid();
            Name = Faker.Address.City();
            CodIBGE = Faker.RandomNumber.Next(1, 10000);
            NameUpdated = Faker.Address.City();
            CodIBGEUpdated = Faker.RandomNumber.Next(1, 10000);
            UfId = Guid.NewGuid();
        }

        /// <summary>
        /// Método para instanciar os DTOs de Municipio
        /// </summary>
        private void CreateDtos()
        {
            municipioDto = new MunicipioDto
            {
                Id = MunicipioId,
                Name = Name,
                CodIBGE = CodIBGE,
                UfId = UfId
            };

            municipioCompletoDto = new MunicipioCompletoDto
            {
                Id = MunicipioId,
                Name = Name,
                CodIBGE = CodIBGE,
                UfId = UfId,
                Uf = new UfDto
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Address.UsState(),
                    Sigla = Faker.Address.UsState().Substring(1, 3)
                }
            };

            municipioCreateDto = new MunicipioCreateDto
            {
                Name = Name,
                CodIBGE = CodIBGE,
                UfId = UfId
            };

            municipioCreateResultDto = new MunicipioCreateResultDto
            {
                Id = MunicipioId,
                Name = Name,
                CodIBGE = CodIBGE,
                UfId = UfId,
                CreateAt = DateTime.UtcNow
            };

            municipioUpdateDto = new MunicipioUpdateDto
            {
                Id = MunicipioId,
                Name = NameUpdated,
                CodIBGE = CodIBGEUpdated,
                UfId = UfId
            };

            municipioUpdateResultDto = new MunicipioUpdateResultDto
            {
                Id = MunicipioId,
                Name = NameUpdated,
                CodIBGE = CodIBGEUpdated,
                UfId = UfId,
                UpdateAt = DateTime.UtcNow
            };

            for (var i = 0; i < 10; i++)
            {
                var municipioDto = new MunicipioDto()
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Address.StreetAddress(),
                    CodIBGE = Faker.RandomNumber.Next(1, 10000),
                    UfId = Guid.NewGuid()
                };

                listMunicipioDto.Add(municipioDto);
            }
        }
    }
}
