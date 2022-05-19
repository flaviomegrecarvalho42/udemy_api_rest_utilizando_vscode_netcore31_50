using System;
using System.Collections.Generic;
using Api.Domain.Dtos.Cep;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Dtos.Uf;

namespace Api.Service.Test.Cep
{
    /// <summary>
    /// Classe criada para conter as classes necessárias para trabalhar nos testes
    /// </summary>
    public class CepTests
    {
        public static string Cep { get; set; }
        public static string Logradouro { get; set; }
        public static string Numero { get; set; }
        public static string CepUpdated { get; set; }
        public static string LogradouroUpdated { get; set; }
        public static string NumeroUpdated { get; set; }
        public static Guid MunicipioId { get; set; }
        public static Guid CepId { get; set; }
        public List<CepDto> listCepDto = new List<CepDto>();
        public CepDto cepDto;
        public CepCreateDto cepCreateDto;
        public CepCreateResultDto cepCreateResultDto;
        public CepUpdateDto cepUpdateDto;
        public CepUpdateResultDto cepUpdateResultDto;

        public CepTests()
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
            CepId = Guid.NewGuid();
            Cep = Faker.RandomNumber.Next(10000, 99999).ToString();
            Logradouro = Faker.Address.StreetName();
            Numero = Faker.RandomNumber.Next(1, 1000).ToString();
            CepUpdated = Faker.RandomNumber.Next(10000, 99999).ToString();
            LogradouroUpdated = Faker.Address.StreetName();
            NumeroUpdated = Faker.RandomNumber.Next(1, 1000).ToString();
        }

        /// <summary>
        /// Método para instanciar os DTOs de CEP
        /// </summary>
        private void CreateDtos()
        {
            cepDto = new CepDto
            {
                Id = CepId,
                Cep = Cep,
                Logradouro = Logradouro,
                Numero = Numero,
                MunicipioId = MunicipioId,
                Municipio = new MunicipioCompletoDto
                {
                    Id = MunicipioId,
                    Name = Faker.Address.City(),
                    CodIBGE = Faker.RandomNumber.Next(1, 10000),
                    UfId = Guid.NewGuid(),
                    Uf = new UfDto
                    {
                        Id = Guid.NewGuid(),
                        Name = Faker.Address.UsState(),
                        Sigla = Faker.Address.UsState().Substring(1, 3)
                    }
                }
            };

            cepCreateDto = new CepCreateDto
            {
                Cep = Cep,
                Logradouro = Logradouro,
                Numero = Numero,
                MunicipioId = MunicipioId
            };

            cepCreateResultDto = new CepCreateResultDto
            {
                Id = CepId,
                Cep = Cep,
                Logradouro = Logradouro,
                Numero = Numero,
                MunicipioId = MunicipioId,
                CreateAt = DateTime.UtcNow
            };

            cepUpdateDto = new CepUpdateDto
            {
                Id = MunicipioId,
                Cep = CepUpdated,
                Logradouro = LogradouroUpdated,
                Numero = NumeroUpdated,
                MunicipioId = MunicipioId,
            };

            cepUpdateResultDto = new CepUpdateResultDto
            {
                Id = MunicipioId,
                Cep = CepUpdated,
                Logradouro = LogradouroUpdated,
                Numero = NumeroUpdated,
                MunicipioId = MunicipioId,
                UpdateAt = DateTime.UtcNow
            };

            for (var i = 0; i < 10; i++)
            {
                var cepDto = new CepDto()
                {
                    Id = Guid.NewGuid(),
                    Cep = Faker.RandomNumber.Next(10000, 99999).ToString(),
                    Logradouro = Faker.Address.StreetAddress(),
                    Numero = Faker.RandomNumber.Next(1, 1000).ToString(),
                    MunicipioId = Guid.NewGuid(),
                    Municipio = new MunicipioCompletoDto
                    {
                        Id = MunicipioId,
                        Name = Faker.Address.City(),
                        CodIBGE = Faker.RandomNumber.Next(1, 10000),
                        UfId = Guid.NewGuid(),
                        Uf = new UfDto
                        {
                            Id = Guid.NewGuid(),
                            Name = Faker.Address.UsState(),
                            Sigla = Faker.Address.UsState().Substring(1, 3)
                        }
                    }
                };

                listCepDto.Add(cepDto);
            }
        }
    }
}
