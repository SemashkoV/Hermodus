
using MyBlog.Data;
using MyBlog.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyBlog.Repo
{
   public class EFCSSRespository: ICSSRepository
    {
        EFDbContext context = new EFDbContext();
        bool Succeeded;

        public IEnumerable<CSS> CSSIEnum
        {
            get
            {
             return   context.CSS;
            }

           
        }

        public IQueryable<CSS> CSSList
        {
            get
            {
                return context.CSS.AsQueryable();
            }

          
        }

        public CSS Details(int? Id)
        {
            CSS dbEntry = context.CSS.Find(Id);

            return dbEntry;
        }
        public CSS Delete(int? Id)
        {
            CSS dbEntry = context.CSS.Find(Id);
            if (dbEntry != null)
            {
                context.CSS.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public bool Save(CSS css)
        {


            if (css.Id == 0)
            {

                context.CSS.Add(css);
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
                CSS dbEntry = context.CSS.Find(css.Id);
                if (dbEntry != null)
                {
                    dbEntry.Id = css.Id;
                    dbEntry.Code = css.Code;


                    if (context.SaveChanges() > 0)
                    {
                        Succeeded = true;
                    }
                    else
                    {
                        Succeeded = false;
                    }
                    css.Id = dbEntry.Id;
                }
            }


            return Succeeded;
        }
    }
}
