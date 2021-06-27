using Microsoft.AspNetCore.Http;
using RestAPI.Data.VO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestAPI.Business
{
    public interface IFileBusiness
    {
        public byte[] GetFile(string fileName);
        public Task<FileDetailVO> SaveFiletToDisk(IFormFile file);
        public Task<List<FileDetailVO>> SaveFilestToDisk(IList<IFormFile> file); 
    }
}
