﻿using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Linq;

namespace Hackathon.Domain
{
    public class Repository
    {
        public Repository()
        {
            _Session = NHibernateSessionFactory.OpenSession();
        }
        private ISession _Session;
        public T Get<T>(object id)
        {
            return _Session.Get<T>(id);
        }
        public IQueryable<T> Get<T>()
        {
            return _Session.Query<T>();
        }
        private ISessionFactory _NhibernateSessionFactory;
        public ISessionFactory NHibernateSessionFactory
        {
            get
            {
                if (_NhibernateSessionFactory == null)
                {
                    _NhibernateSessionFactory = CreateSessionFactory();
                };
                return _NhibernateSessionFactory;
            }
        }
        private static string DbFile;
        public static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
              .Database(
                MsSqlConfiguration.MsSql2008.ConnectionString(c =>
                c.Server("ylkx1ic1so.database.windows.net,1433")
                .Database("hackathon")
                .Username("pabreetzio")
                .Password("angryEl3ctron"))
              )
              .Mappings(m =>
                m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
              //.ExposeConfiguration(BuildSchema)
              .BuildSessionFactory();
        }
        private static void BuildSchema(NHibernate.Cfg.Configuration config)
        {
            // delete the existing db on each run
            if (File.Exists(DbFile))
                File.Delete(DbFile);

            // this NHibernate tool takes a configuration (with mapping info in)
            // and exports a database schema from it
            new SchemaExport(config)
              .Create(false, true);
        }
        public void Save<T>(IEnumerable<T> objects)
        {
            var list = objects.ToList();
            for(int i=0;i<list.Count;i++)
            {
                _Session.Save(list[i]);
              if(i % 20 == 0)
              {
                _Session.Flush();
                _Session.Clear();
              }
            }
        }
    }
}
