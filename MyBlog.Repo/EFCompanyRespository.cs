
using MyBlog.Data;
using MyBlog.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyBlog.Repo
{
   public class EFCompanyRespository: ICompanyRepository
    {
        EFDbContext context = new EFDbContext();
        bool Succeeded;

        public IEnumerable<Company> CompanyIEnum
        {
            get
            {
             return   context.Companies;
            }

           
        }

        public IQueryable<Company> CompanyList
        {
            get
            {
                return context.Companies.AsQueryable();
            }

          
        }

        public Company Details(int? Id)
        {
            Company dbEntry = context.Companies.Find(Id);

            return dbEntry;
        }
        public Company Delete(int? Id)
        {
            Company dbEntry = context.Companies.Find(Id);
            if (dbEntry != null)
            {
                context.Companies.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public bool Save(Company watch)
        {


            if (watch.Id == 0)
            {

                context.Companies.Add(watch);
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
                Company dbEntry = context.Companies.Find(watch.Id);
                if (dbEntry != null)
                {
                    dbEntry.Id = watch.Id;
                    dbEntry.Name = watch.Name;
                    dbEntry.Country = watch.Country;




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
