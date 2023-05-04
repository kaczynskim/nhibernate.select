using BenchmarkDotNet.Running;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Nhibernate.Select.Entity;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Linq;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Nhibernate.Select
{
    class Program
    {
        static async Task Main(string[] args)
        {


            var results = BenchmarkRunner.Run<GetPostsListBenchmark>();

            var sessionFactory = CreateSessionFactory();

            using (var session = sessionFactory.OpenSession())
            {
                //// selecting all columns:
                //var posts = await session
                //    .Query<Post>()
                //    .Fetch(x => x.OwnerUser)
                //    .Where(x => x.CreationDate < new DateTime(2010, 1, 1))
                //    .OrderByDescending(x => x.CreationDate)
                //    .Take(200)
                //    .ToListAsync();

                //foreach (var item in posts)
                //{
                //    Console.WriteLine($"Title: {item.Title}, CreationDate: {item.CreationDate}, Author: {item.DisplayName}");
                //}

                //// selection only necessary columns:
                //var posts1 = await session
                //    .Query<Post>()
                //    .Fetch(x => x.OwnerUser)
                //    .Where(x => x.CreationDate < new DateTime(2010, 1, 1))
                //    .OrderByDescending(x => x.CreationDate)
                //    .Take(200)
                //    .Select(x => new { x.Title, x.CreationDate, x.OwnerUser.DisplayName })
                //    .ToListAsync();

                //foreach (var item in posts1)
                //{
                //    Console.WriteLine($"Title: {item.Title}, CreationDate: {item.CreationDate}, Author: {item.DisplayName}");
                //}

                // splitting query:

                //var post_ids = await session
                //    .Query<Post>()
                //    .Where(x => x.CreationDate < new DateTime(2010, 1, 1))
                //    .OrderByDescending(x => x.CreationDate)
                //    .Take(200)
                //    .Select(x => x.Id)
                //    .ToListAsync();

                //var posts2 = await
                //    session
                //        .Query<Post>()
                //        .Fetch(x => x.OwnerUser)
                //        .Where(x => post_ids.Contains(x.Id))
                //        .Select(x => new { x.Title, x.CreationDate, x.OwnerUser.DisplayName })
                //        .ToListAsync();

                //foreach (var item in posts2)
                //{
                //    Console.WriteLine($"Title: {item.Title}, CreationDate: {item.CreationDate}, Author: {item.DisplayName}");
                //}

                var posts1 = await session
                    .Query<Post>()
                    .Fetch(x => x.OwnerUser)
                    .Where(x => x.CreationDate < new DateTime(2010, 1, 1))
                    .OrderByDescending(x => x.CreationDate)
                    .Skip(0)
                    .Take(100)
                    .Select(x => new { x.Title, x.CreationDate, x.OwnerUser.DisplayName })
                    .ToListAsync();

                var posts2 = await session
                    .Query<Post>()
                    .Fetch(x => x.OwnerUser)
                    .Where(x => x.CreationDate < new DateTime(2010, 1, 1))
                    .OrderByDescending(x => x.CreationDate)
                    .Skip(100)
                    .Take(100)
                    .Select(x => new { x.Title, x.CreationDate, x.OwnerUser.DisplayName })
                    .ToListAsync();

                posts1.AddRange(posts2);

            }
        }

        private static ISessionFactory CreateSessionFactory()
        {
            return 
                Fluently.Configure()
                .Database(
                     MsSqlConfiguration.MsSql2012.ShowSql().ConnectionString("Server=.;Database=StackOverflow2010;Trusted_Connection=True;"))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Program>())
                .BuildSessionFactory();
        }
    }
}
