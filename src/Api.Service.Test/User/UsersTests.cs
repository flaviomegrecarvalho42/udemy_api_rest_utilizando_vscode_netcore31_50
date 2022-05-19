using System;
using System.Collections.Generic;
using Api.Domain.Dtos.User;

namespace Api.Service.Test.User
{
    /// <summary>
    /// Classe criada para conter as classes necessárias para trabalhar nos testes
    /// </summary>
    public class UsersTests
    {
        public static string UserName { get; set; }
        public static string UserEmail { get; set; }
        public static string UserNameUpdated { get; set; }
        public static string UserEmailUpdated { get; set; }
        public static Guid UserId { get; set; }
        public List<UserDto> listUserDto = new List<UserDto>();
        public UserDto userDto;
        public UserCreateDto userCreateDto;
        public UserCreateResultDto userCreateResultDto;
        public UserUpdateDto userUpdateDto;
        public UserUpdateResultDto userUpdateResultDto;

        public UsersTests()
        {
            SetPublicProprities();
            CreateListUserDto();
            CreateDtos();
        }

        /// <summary>
        /// Método para setar as proprities acima
        /// </summary>
        private void SetPublicProprities()
        {
            UserId = Guid.NewGuid();
            UserName = Faker.Name.FullName();
            UserEmail = Faker.Internet.Email();
            UserNameUpdated = Faker.Name.FullName();
            UserEmailUpdated = Faker.Internet.Email();
        }

        /// <summary>
        /// Método para instanciar os DTOs de User
        /// </summary>
        private void CreateDtos()
        {
            userDto = new UserDto()
            {
                Id = UserId,
                Name = UserName,
                Email = UserEmail
            };

            userCreateDto = new UserCreateDto()
            {
                Name = UserName,
                Email = UserEmail
            };

            userCreateResultDto = new UserCreateResultDto()
            {
                Id = UserId,
                Name = UserName,
                Email = UserEmail,
                CreateAt = DateTime.UtcNow
            };

            userUpdateDto = new UserUpdateDto()
            {
                Id = UserId,
                Name = UserNameUpdated,
                Email = UserEmailUpdated
            };

            userUpdateResultDto = new UserUpdateResultDto()
            {
                Id = UserId,
                Name = UserNameUpdated,
                Email = UserEmailUpdated,
                UpdateAt = DateTime.UtcNow
            };
        }

        /// <summary>
        /// Método para instanciar o List<UserDto>
        /// </summary>
        private void CreateListUserDto()
        {
            for (var i = 0; i < 10; i++)
            {
                var dto = new UserDto()
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email()
                };

                listUserDto.Add(dto);
            }

        }
    }
}
