using System.Collections.Generic;
using System.Threading.Tasks;
using Piranha.AspNetCore.Identity.EF;
using Piranha.Manager;

namespace Piranha.AspNetCore.Identity.EF.Manager.Areas.Manager.Models
{
    public class UserEditModel
    {
        #region Properties

        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber { get; set; }
        
        public string Email { get; set; }

        public string Password { get; set; }

        #endregion

        public static async Task<UserEditModel> GetById(AppUserRepository userRepository, string id)
        {
            var user = await userRepository.GetByIdAsync(id);

            if (user != null)
            {
                var model = IdentityEFModule.Mapper.Map<IdentityAppUser, UserEditModel>(user);

                return model;
            }

            throw new KeyNotFoundException($"No page found with the id '{id}'");
        }

        public static async Task<UserEditModel> Create(AppUserRepository userRepository, string id)
        {
            var user = !string.IsNullOrEmpty(id) ? await userRepository.GetByIdAsync(id) : null;

            if (user != null)
            {
                var model = IdentityEFModule.Mapper.Map<IdentityAppUser, UserEditModel>(user);

                return model;
            }

            throw new KeyNotFoundException($"No page type found with the id '{id}'");
        }

        public async Task<bool> Save(AppUserRepository userRepository, bool? reset)
        {
            var isNew = false;

            var user = await userRepository.GetByIdAsync(Id);

            if(user == null)
                isNew = true;

            if (isNew)
            {
                user = new IdentityAppUser();

                IdentityEFModule.Mapper.Map<UserEditModel, IdentityAppUser>(this, user);

                await userRepository.InsertAsync(user);

                Id = user.Id;

                return true;
            }
            else
            {
                return false;
            }


            
        }
    }
}