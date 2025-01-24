﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Explorers_Haven.Core.Validators;
using Explorers_Haven.DataAccess.Repository;
using ExplorersHaven.Models;

namespace Explorers_Haven.Core.Services
{
    public class TravelogueService
    {
        private readonly IRepository<Travelogue> _repo;

        public TravelogueService(IRepository<Travelogue> repo)
        {
            this._repo = repo;
        }
        private bool ValidateTravelogue(Travelogue travelogue)
        {
            if (!TravelogueValidator.ValidateInput(travelogue.Name))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Add(Travelogue travelogue)
        {
            if (!ValidateTravelogue(travelogue))
            {
                throw new ArgumentException("The travelogue is not valid!");
            }
            _repo.Add(travelogue);

        }

        public Travelogue GetById(int id)
        {
            return _repo.Get(id);
        }


        public void Update(Travelogue travelogue)
        {
            if (!ValidateTravelogue(travelogue))
            {
                throw new ArgumentException("The travelogue is not valid!");
            }
            _repo.Update(travelogue);
        }

        public void Delete(int id)
        {
            if (TravelogueValidator.TravelogueExists(id))
            {
                _repo.Delete(id);
            }

        }

        public List<Travelogue> GetAll()
        {
            return _repo.GetAll();
        }

        public List<Travelogue> Find(Expression<Func<Travelogue, bool>> filter)
        {
            return _repo.Find(filter);
        }
    }
}
