using FluentNHibernate.Mapping;
using System;

namespace Nhibernate.Select.Entity
{
	public class User
	{
		public virtual int Id { get; set; }

		public virtual string AboutMe { get; set; }

		public virtual int? Age { get; set; }

		public virtual DateTime CreationDate { get; set; }

		public virtual string DisplayName { get; set; }
	
		public virtual int DownVotes { get; set; }

		public virtual string EmailHash { get; set; }

		public virtual DateTime LastAccessDate { get; set; }

		public virtual string Location { get; set; }

		public virtual int Reputation { get; set; }	

		public virtual int UpVotes { get; set; }

		public virtual int Views { get; set; }

		public virtual string WebsiteUrl { get; set; }

		public virtual int? AccountId { get; set; }
	}

	public class UserMapping : ClassMap<User>
	{
		public UserMapping()
		{
			Table("Users");
			Schema("dbo");

			Id(x => x.Id).GeneratedBy.Identity().Column("Id");

			Map(x => x.AboutMe).Length(4001).Nullable();
			Map(x => x.Age).Nullable();
			Map(x => x.CreationDate).Not.Nullable();
			Map(x => x.DisplayName).Length(40).Not.Nullable();
			Map(x => x.DownVotes).Not.Nullable();
			Map(x => x.EmailHash).Length(40).Nullable();
			Map(x => x.LastAccessDate).Not.Nullable();
			Map(x => x.Location).Length(100).Nullable();
			Map(x => x.Reputation).Not.Nullable();
			Map(x => x.UpVotes).Not.Nullable();
			Map(x => x.Views).Not.Nullable();
			Map(x => x.WebsiteUrl).Length(200).Nullable();
			Map(x => x.AccountId).Nullable();
		}
	}
}
