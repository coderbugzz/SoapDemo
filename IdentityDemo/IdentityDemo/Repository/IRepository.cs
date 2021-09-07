using IdentityDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoapDemo;

namespace IdentityDemo.Repository
{
   public interface IRepository
    {

        Task<SoapDemoSoapClient> GetInstanceAsync();
        Task<Response<IdentityModel>> LoginAsync(LoginViewModel loginView);
         Task<Response<string>> RegisterAsync(RegisterViewModel registerView);

    }
}
