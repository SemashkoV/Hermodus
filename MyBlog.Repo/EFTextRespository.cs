
using MyBlog.Data;
using MyBlog.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyBlog.Repo
{
   public class EFTextRespository:ITextRepository
    {
        EFDbContext context = new EFDbContext();
        bool Succeeded;

        public IEnumerable<Text> TextIEnum
        {
            get
            {
             return   context.Texts;
            }

           
        }

        public IQueryable<Text> TextList
        {
            get
            {
                return context.Texts.AsQueryable();
            }

          
        }

        public Text Details(int? Id)
        {
            Text dbEntry = context.Texts.Find(Id);

            return dbEntry;
        }
        public Text Delete(int? Id)
        {
            Text dbEntry = context.Texts.Find(Id);
            if (dbEntry != null)
            {
                context.Texts.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public bool Save(Text text)
        {


            if (text.Id == 0)
            {

                context.Texts.Add(text);
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
                Text dbEntry = context.Texts.Find(text.Id);
                if (dbEntry != null)
                {
                    dbEntry.Id = text.Id;
                    dbEntry.Texts = text.Texts;
                    dbEntry.Create_time = text.Create_time;
                    dbEntry.UserId = text.UserId;
                    //   context.Images.Add(dbEntry);

                    if (context.SaveChanges() > 0)
                    {
                        Succeeded = true;
                    }
                    else
                    {
                        Succeeded = false;
                    }
                    text.Id = dbEntry.Id;
                }
            }


            return Succeeded;
        }
    }
}
