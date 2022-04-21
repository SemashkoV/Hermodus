
using MyBlog.Data;
using MyBlog.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyBlog.Repo
{
   public class EFWatchRespository: IWatchRepository
    {
        EFDbContext context = new EFDbContext();
        bool Succeeded;

        public IEnumerable<Watch> WatchIEnum
        {
            get
            {
             return   context.Watches;
            }

           
        }

        public IQueryable<Watch> WatchList
        {
            get
            {
                return context.Watches.AsQueryable();
            }

          
        }

        public Watch Details(int? Id)
        {
            Watch dbEntry = context.Watches.Find(Id);

            return dbEntry;
        }
        public Watch Delete(int? Id)
        {
            Watch dbEntry = context.Watches.Find(Id);
            if (dbEntry != null)
            {
                context.Watches.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public bool Save(Watch watch)
        {


            if (watch.Id == 0)
            {

                context.Watches.Add(watch);
                if (context.SaveChanges() > 0)
                {
                    Succeeded = true;
                }
                else
                {
                    Succeeded = false;
                }


            }
            else
            {
                Watch dbEntry = context.Watches.Find(watch.Id);
                if (dbEntry != null)
                {
                    dbEntry.Id = watch.Id;
                    dbEntry.CompanyId = watch.CompanyId;
                    dbEntry.Title = watch.Title;
                    dbEntry.Model = watch.Model;
                    dbEntry.Content = watch.Content;
                    dbEntry.Image = watch.Image;
                    dbEntry.Article = watch.Article;
                    dbEntry.Country = watch.Country;
                    dbEntry.Movement = watch.Movement;
                    dbEntry.Frame = watch.Frame;
                    dbEntry.Face = watch.Face;
                    dbEntry.Bracelet = watch.Bracelet;
                    dbEntry.Protection = watch.Protection;
                    dbEntry.Backlight = watch.Backlight;
                    dbEntry.Glass = watch.Glass;
                    dbEntry.Calendar = watch.Calendar;
                    dbEntry.Size = watch.Size;

             

                    if (context.SaveChanges() > 0)
                    {
                        Succeeded = true;
                    }
                    else
                    {
                        Succeeded = false;
                    }
                    watch.Id = dbEntry.Id;
                }
            }


            return Succeeded;
        }
    }
}
