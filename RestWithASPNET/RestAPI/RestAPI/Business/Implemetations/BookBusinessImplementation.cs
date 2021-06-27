using RestAPI.Data.Converter.Implementations;
using RestAPI.Data.VO;
using RestAPI.Repository;
using RestWithASPNETUdemy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.Business.Implemetations
{
    public class BookBusinessImplementation : IBookBusiness
    {
        private readonly IRepository<Book> _respository;
        private readonly BookConverter _converter;

        public BookBusinessImplementation(IRepository<Book> repository)
        {
            _respository = repository;
            _converter = new BookConverter();
        }

        public List<BookVO> FindAll()
        {
            return _converter.Parse(_respository.FindAll());
        }

        public BookVO FindById(long id)
        {
            return _converter.Parse(_respository.FindById(id));
        }

        public BookVO Create(BookVO book)
        {
            var personEntity = _converter.Parse(book);
            personEntity = _respository.Create(personEntity);
            return _converter.Parse(personEntity);
        }

        public BookVO Update(BookVO book)
        {
            var personEntity = _converter.Parse(book);
            personEntity = _respository.Update(personEntity);
            return _converter.Parse(personEntity);
        }

        public void Delete(long id)
        {
            _respository.Delete(id);
        }
    }
}
