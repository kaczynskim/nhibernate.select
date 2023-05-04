using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using Nhibernate.Select.Entity;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Nhibernate.Select
{
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net70)]
    public class GetPostsListBenchmark
    {
        ISessionFactory sessionFactory;

        [GlobalSetup]
        public void Setup()
        {
            sessionFactory = CreateSessionFactory();
        }

        [Benchmark]
        [Arguments(200)]
        [Arguments(2000)]
        public async Task Select(int n)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var posts = await session
                    .Query<Post>()
                    .Fetch(x => x.OwnerUser)
                    .Where(x => x.CreationDate < new DateTime(2010, 1, 1))
                    .OrderByDescending(x => x.CreationDate)
                    .Take(n)
                    .Select(x => new PostRecord(x.Title, x.CreationDate, x.OwnerUser.DisplayName))
                    .ToListAsync();
            }

            return;
        }

        [Benchmark(Baseline = true)]
        [Arguments(200)]
        [Arguments(2000)]
        public async Task AllColumns(int n)
        {
            using (var session = sessionFactory.OpenSession())
            { 
                var posts = await session
                    .Query<Post>()
                    .Fetch(x => x.OwnerUser)
                    .Where(x => x.CreationDate < new DateTime(2010, 1, 1))
                    .OrderByDescending(x => x.CreationDate)
                    .Take(n)
                    .ToListAsync();
            }

            return;
        }

        [Benchmark()]
        [Arguments(200)]
        [Arguments(2000)]
        public async Task AllColumns_Stateless(int n)
        {
            using (var session = sessionFactory.OpenStatelessSession())
            {
                var posts = await session
                    .Query<Post>()
                    .Fetch(x => x.OwnerUser)
                    .Where(x => x.CreationDate < new DateTime(2010, 1, 1))
                    .OrderByDescending(x => x.CreationDate)
                    .Take(n)
                    .ToListAsync();
            }

            return;
        }

        [Benchmark()]
        [Arguments(200)]
        [Arguments(2000)]
        public async Task NVarchar_Excluded(int n)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var posts = await session
                    .Query<Post>()
                    .Fetch(x => x.OwnerUser)
                    .Where(x => x.CreationDate < new DateTime(2010, 1, 1))
                    .OrderByDescending(x => x.CreationDate)
                    .Take(n)
                    .Select(x => new
                    {
                        x.AcceptedAnswerId,
                        x.AnswerCount,
                        x.Body,
                        x.ClosedDate,
                        x.CommentCount,
                        x.CommunityOwnedDate,
                        x.CreationDate,
                        x.FavoriteCount,
                        x.Id,
                        x.LastActivityDate,
                        x.LastEditDate,
                        x.LastEditorDisplayName,
                        x.LastEditorUserId,
                        x.ParentId,
                        x.PostTypeId,
                        x.Score,
                        x.Tags,
                        x.Title,
                        x.ViewCount,
                        x.OwnerUser.AccountId,
                        x.OwnerUser.Age,
                        OwnerUserDate = (DateTime?)x.OwnerUser.CreationDate,
                        x.OwnerUser.DisplayName,
                        DownVotes = (int?)x.OwnerUser.DownVotes,
                        x.OwnerUser.EmailHash,
                        UserId = (int?)x.OwnerUser.Id,
                        LastAccessDate = (DateTime?)x.OwnerUser.LastAccessDate,
                        x.OwnerUser.Location,
                        UpVotes = (int?)x.OwnerUser.UpVotes,
                        Views = (int?)x.OwnerUser.Views,
                        x.OwnerUser.WebsiteUrl
                    })
                    .ToListAsync();

            }

            return;
        }

        private static ISessionFactory CreateSessionFactory()
        {
            return
                Fluently.Configure()
                .Database(
                     MsSqlConfiguration.MsSql2012.ConnectionString("Server=.;Database=StackOverflow2010;Trusted_Connection=True;"))
                .Mappings(m =>
                    m.FluentMappings.AddFromAssemblyOf<Program>())
              .BuildSessionFactory();
        }
    }
}
