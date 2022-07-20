using Bebrand.Application.ViewModels;
using Bebrand.Application.ViewModels.ClientView;
using Bebrand.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Application.Interfaces
{
    public interface IFromFileAppService
    {
        Task<QueryResultResource<List<ClientViewModel>>> GetDataFromCSVFile(IFormFile Inputfile);
    }
}
