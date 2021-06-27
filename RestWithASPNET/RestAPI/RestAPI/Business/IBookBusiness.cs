using RestAPI.Data.VO;
using RestAPI.Model;
using RestWithASPNETUdemy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.Business
{
    public interface IBookBusiness
    {
        BookVO Create(BookVO person);
        BookVO FindById(long id);
        List<BookVO> FindAll();
        BookVO Update(BookVO person);
        void Delete(long id);
    }
}
