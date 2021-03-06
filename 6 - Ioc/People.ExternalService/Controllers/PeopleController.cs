﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Peoples.Repositories.Interface;

namespace People.ExternalService.Controllers
{
    [Route("api/[controller]")]
    public class PeopleController : Controller
    {
        private readonly IPeopleRepository _proPeopleRepository;
        //static readonly IPeopleRepository PeopleRepo = new PeopleRepositoryInMemory();

        //TODO: 02 - Dependencia
        public PeopleController(IPeopleRepository proPeopleRepository)
        {
            _proPeopleRepository = proPeopleRepository;
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Person>), 200)]
        [AllowAnonymous]
        public IEnumerable<Person> Get()
        {
            var list = _proPeopleRepository.GetPeople();
            foreach (var person in list)
                person.LastName = $"{person.LastName} - By Api Rest Service";

            return list;
        }
        [HttpGet("{lastName}")]
        [ProducesResponseType(typeof(Person), 200)]
        public Person Get(string lastName)
        {
            return _proPeopleRepository.GetPeople().FirstOrDefault(x => x.LastName == lastName);
        }

        [HttpPost]
        public void Post([FromBody]Person value)
        {
            _proPeopleRepository.AddPerson(value);
        }


        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete("{lastname}")]
        public void Delete(string lastname)
        {
            _proPeopleRepository.DeletePerson(lastname);
        }
    }
}