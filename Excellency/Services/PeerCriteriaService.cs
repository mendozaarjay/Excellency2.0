﻿using Excellency.Interfaces;
using Excellency.Models;
using Excellency.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Excellency.Services
{
    public class PeerCriteriaService : IPeerCriteria
    {
        private EASDbContext _dbContext;

        public PeerCriteriaService(EASDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public PeerCriteria GetPeerCriteriaHeaderById(int id)
        {
            return _dbContext.PeerCriterias.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<PeerCriteria> PeerCriteriaHeaders()
        {
            return _dbContext.PeerCriterias.Where(a => a.IsDeleted == false);
        }

        public IEnumerable<PeerCriteriaLine> PeerCriteriaLinesByHeaderId(int HeaderId)
        {
            return _dbContext.PeerCriteriaLine
                .Include(a => a.Header)
                .Where(a => a.Header.Id == HeaderId && a.IsDeleted == false);
        }

        public void RemoveHeaderById(int id)
        {
            var item = _dbContext.PeerCriterias.FirstOrDefault(a => a.Id == id);
            item.IsDeleted = true;
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void RemoveLineById(int id)
        {
            var item = _dbContext.PeerCriteriaLine.FirstOrDefault(a => a.Id == id);
            item.IsDeleted = true;
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void SaveCriteriaLine(int HeaderId, List<PeerCriteriaLine> items)
        {
            var header = GetPeerCriteriaHeaderById(HeaderId);
            foreach (var item in items)
            {
                item.Header = header;
                if (item.Id == 0)
                {
                    _dbContext.Add(item);
                }
                else
                {
                    _dbContext.Entry(item).State = EntityState.Modified;
                }
            }
            _dbContext.SaveChanges();
        }

        public void SaveCriteriaLine(int HeaderId, PeerCriteriaLine item)
        {
            var header = GetPeerCriteriaHeaderById(HeaderId);
            item.Header = header;
            if (item.Id == 0)
            {
                _dbContext.Add(item);
            }
            else
            {
                _dbContext.Entry(item).State = EntityState.Modified;
            }
            _dbContext.SaveChanges();
        }

        public void SavePeerCriteria(PeerCriteria header, int UserId)
        {
            if (header.Id == 0)
            {
                header.CreatedBy = UserId.ToString();
                header.CreationDate = DateTime.Now;
                _dbContext.Add(header);
            }
            else
            {
                header.ModifiedBy = UserId.ToString();
                header.ModifiedDate = DateTime.Now;
                _dbContext.Entry(header).State = EntityState.Modified;
            }
            _dbContext.SaveChanges();
        }
        public EvaluationSeason ActiveSeason()
        {
            return _dbContext.EvaluationSeasons.FirstOrDefault(a => a.IsActive == true);
        }
        public bool IsWithActiveSeason()
        {
            return _dbContext.EvaluationSeasons.Any(a => a.IsActive == true);
        }

    }
}
