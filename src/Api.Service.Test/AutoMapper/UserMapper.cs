using System;
using System.Collections.Generic;
using System.Linq;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Models;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class UserMapper : BaseTestService
    {
        [Fact(DisplayName = "É possível mapear os modelos de User")]
        public void Is_Possible_Map_Models_User()
        {
            var model = GetUserModel();
            var listEntity = GetListUserEntity();

            #region  Model => Entity (ModelToEntityProfile)
            var entity = Mapper.Map<UserEntity>(model);
            Assert.Equal(model.Id, entity.Id);
            Assert.Equal(model.Name, entity.Name);
            Assert.Equal(model.Email, entity.Email);
            Assert.Equal(model.CreateAt, entity.CreateAt);
            Assert.Equal(model.UpdateAt, entity.UpdateAt);
            #endregion

            #region Entity => Dto (EntityToDtoProfile)
            var userDto = Mapper.Map<UserDto>(entity);
            Assert.Equal(entity.Id, userDto.Id);
            Assert.Equal(entity.Name, userDto.Name);
            Assert.Equal(entity.Email, userDto.Email);
            Assert.Equal(entity.CreateAt, userDto.CreateAt);

            var listDto = Mapper.Map<List<UserDto>>(listEntity);
            Assert.True(listEntity.Count() == listDto.Count());
            for (var i = 0; i < listDto.Count(); i++)
            {
                Assert.Equal(listEntity[i].Id, listDto[i].Id);
                Assert.Equal(listEntity[i].Email, listDto[i].Email);
                Assert.Equal(listEntity[i].Name, listDto[i].Name);
                Assert.Equal(listEntity[i].CreateAt, listDto[i].CreateAt);
            }

            var userCreateResultDto = Mapper.Map<UserCreateResultDto>(entity);
            Assert.Equal(entity.Name, userCreateResultDto.Name);
            Assert.Equal(entity.Email, userCreateResultDto.Email);
            Assert.Equal(entity.CreateAt, userCreateResultDto.CreateAt);

            var userUpdateResultDto = Mapper.Map<UserUpdateResultDto>(entity);
            Assert.Equal(entity.Name, userUpdateResultDto.Name);
            Assert.Equal(entity.Email, userUpdateResultDto.Email);
            Assert.Equal(entity.UpdateAt, userUpdateResultDto.UpdateAt);
            #endregion

            #region Dto => Model (DtoToModelProfile)
            var userModel = Mapper.Map<UserModel>(userDto);
            Assert.Equal(userDto.Id, userModel.Id);
            Assert.Equal(userDto.Name, userModel.Name);
            Assert.Equal(userDto.Email, userModel.Email);
            Assert.Equal(userDto.CreateAt, userModel.CreateAt);

            var userCreateDto = Mapper.Map<UserCreateDto>(userModel);
            Assert.Equal(userModel.Name, userCreateDto.Name);
            Assert.Equal(userModel.Email, userCreateDto.Email);

            var userUpdateDto = Mapper.Map<UserUpdateDto>(userModel);
            Assert.Equal(userModel.Id, userUpdateDto.Id);
            Assert.Equal(userModel.Name, userUpdateDto.Name);
            Assert.Equal(userModel.Email, userUpdateDto.Email);
            #endregion
        }

        private static UserModel GetUserModel()
        {
            return new UserModel
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            };
        }

        private static List<UserEntity> GetListUserEntity()
        {
            var listEntity = new List<UserEntity>();

            for (var i = 0; i < 5; i++)
            {
                var item = new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email(),
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow
                };

                listEntity.Add(item);
            }

            return listEntity;
        }
    }
}
