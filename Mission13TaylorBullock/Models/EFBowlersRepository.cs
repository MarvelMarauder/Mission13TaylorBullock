using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13TaylorBullock.Models
{
    public class EFBowlersRepository : IBowlersRepository
    {
        public BowlersDbContext context;

        public EFBowlersRepository(BowlersDbContext temp)
        {
            context = temp;
        }
        public IQueryable<Bowler> Bowlers => context.Bowlers;
        public IQueryable<Team> Teams => context.Teams;

        public void SaveBowler(Bowler b)
        {
            context.SaveChanges();
        }
        public void CreateBowler(Bowler b)
        {
            context.Add(b);
            context.SaveChanges();
        }

        public void DeleteBowler(Bowler b)
        {
            context.Remove(b);
            context.SaveChanges();
        }
    }
}
