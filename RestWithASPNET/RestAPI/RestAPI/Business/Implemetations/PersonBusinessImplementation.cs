﻿using RestAPI.Data.Converter.Implementations;
using RestAPI.Data.VO;
using RestAPI.Hypermedia.Utils;
using RestAPI.Model;
using RestAPI.Model.Context;
using RestAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RestAPI.Business.Implemetations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IPersonRepository _respository;
        private readonly PersonConverter _converter;

        public PersonBusinessImplementation(IPersonRepository repository)
        {
            _respository = repository;
            _converter = new PersonConverter();
        }

        public List<PersonVO> FindAll()
        {
            return _converter.Parse(_respository.FindAll());
        }

        public PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            var sort = (!string.IsNullOrWhiteSpace(sortDirection)) && !sortDirection.Equals("desc") ? "asc" : "desc";
            var size = (pageSize < 1) ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            string query = @"select *  from person p where 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(name)) query = query  + $" and p.first_name like '%{name}%' ";
            query += $" order by p.first_name {sort} limit {size} offset {offset} ";

            string countQuery = @"select count(*)  from person p where 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(name)) countQuery = countQuery + $" and p.first_name like '%{name}%' ";

            var persons = _respository.FindWithPagedSearch(query);
            int totalResults = _respository.GetCount(countQuery);

            return new PagedSearchVO<PersonVO> {
                CurrentPage = page,
                List = _converter.Parse(persons),
                PageSize = size,
                SortDirections = sort,
                TotalResult = totalResults
            };
        }

        public PersonVO FindById(long id)
        {
            return _converter.Parse(_respository.FindById(id));
        }

        public List<PersonVO> FindByName(string firstName, string lastName)
        {
            return _converter.Parse(_respository.FindByName(firstName, lastName));
        }

        public PersonVO Create(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _respository.Create(personEntity);
            return _converter.Parse(personEntity);
        }

        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _respository.Update(personEntity);
            return _converter.Parse(personEntity);
        }

        public PersonVO Disable(long id)
        {
            var personEntity = _respository.Disable(id);
            return _converter.Parse(personEntity);
        }

        public void Delete(long id)
        {
            _respository.Delete(id);
        }
    }
}
