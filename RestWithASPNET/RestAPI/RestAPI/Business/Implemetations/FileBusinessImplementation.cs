using Microsoft.AspNetCore.Http;
using RestAPI.Data.VO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.Business.Implemetations
{
    public class FileBusinessImplementation : IFileBusiness
    {
        private readonly string _basePath;
        private readonly IHttpContextAccessor _context;

        public FileBusinessImplementation(IHttpContextAccessor context)
        {
            _context = context;
            _basePath = Directory.GetCurrentDirectory() + "\\UploadDir\\";
        }

        public byte[] GetFile(string fileName)
        {
            var filePath = _basePath + fileName;
            return File.ReadAllBytes(filePath);
        }

        public async Task<FileDetailVO> SaveFiletToDisk(IFormFile file)
        {
            FileDetailVO FileDetail = new FileDetailVO();
            var fileType = Path.GetExtension(file.FileName);
            var baseUrl = _context.HttpContext.Request.Host;

            if (fileType.ToLower() == ".pdf" ||
                fileType.ToLower() == ".jpg" ||
                fileType.ToLower() == ".png" ||
                fileType.ToLower() == ".jpeg")
            {
                var docName = Path.GetFileName(file.FileName);
                if (file != null && file.Length > 0)
                {
                    var destination = Path.Combine(_basePath, "", docName);

                    FileDetail.DocumentName = docName;
                    FileDetail.DocType = fileType;
                    FileDetail.DocUrl = Path.Combine(baseUrl + "/api/file/v1" + FileDetail.DocumentName);

                    using var stream = new FileStream(destination, FileMode.Create);
                    await file.CopyToAsync(stream);
                }
            }

            return FileDetail;
        }

        public async Task<List<FileDetailVO>> SaveFilestToDisk(IList<IFormFile> files)
        {
            List<FileDetailVO> list = new List<FileDetailVO>();
            foreach (var file in files)
            {
                list.Add(await SaveFiletToDisk(file));
            }

            return list;
        }
    }
}
