using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nhibernate.Select.Entity
{
    public class Post
    {
        public virtual int Id { get; set; }
        public virtual int? AcceptedAnswerId { get; set; }
        public virtual int? AnswerCount { get; set; }
        public virtual string Body { get; set; }
        public virtual DateTime? ClosedDate { get; set; }
        public virtual int? CommentCount { get; set; }
        public virtual DateTime? CommunityOwnedDate { get; set; }
        public virtual DateTime CreationDate { get; set; }
        public virtual int? FavoriteCount { get; set; }
        public virtual DateTime LastActivityDate { get; set; }
        public virtual DateTime? LastEditDate { get; set; }
        public virtual string LastEditorDisplayName { get; set; }
        public virtual int? LastEditorUserId { get; set; }
        public virtual User OwnerUser { get; set; }
        public virtual int? ParentId { get; set; }
        public virtual int PostTypeId { get; set; }
        public virtual int Score { get; set; }
        public virtual string Tags { get; set; }
        public virtual string Title { get; set; }
        public virtual int ViewCount { get; set; }
    }

    public class PostMapping : ClassMap<Post>
    {
        public PostMapping()
        {
            Table("Posts");
            Schema("dbo");

            Id(x => x.Id).GeneratedBy.Identity().Column("Id");

            Map(x => x.AcceptedAnswerId).Nullable();
            Map(x => x.AnswerCount).Nullable();
            Map(x => x.Body).Length(4001).Not.Nullable();
            Map(x => x.ClosedDate).Nullable();
            Map(x => x.CommentCount).Not.Nullable();
            Map(x => x.CommunityOwnedDate).Nullable();
            Map(x => x.CreationDate).Not.Nullable();
            Map(x => x.FavoriteCount).Nullable();
            Map(x => x.LastActivityDate).Not.Nullable();
            Map(x => x.LastEditDate).Nullable();
            Map(x => x.LastEditorDisplayName).Length(40).Nullable();
            Map(x => x.LastEditorUserId).Nullable();
            Map(x => x.ParentId).Nullable();
            Map(x => x.PostTypeId).Not.Nullable();
            Map(x => x.Score).Not.Nullable();
            Map(x => x.Tags).Length(150).Nullable();
            Map(x => x.Title).Length(250).Nullable();
            Map(x => x.ViewCount).Not.Nullable();

            References(x => x.OwnerUser).Column("OwnerUserId").Nullable();
        }
    }
}


