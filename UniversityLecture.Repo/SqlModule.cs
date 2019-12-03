using Autofac;

using UniversityLecture.DAL;
using UniversityLecture.DAL.Interfaces;
using UniversityLecture.Repo.Interfaces;


namespace UniversityLecture.Repo
{
    public class SqlModule : Module
    {
        private string _connectionString;
        public SqlModule(string connectionString)
        {
            _connectionString = connectionString;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context => new ULDbContext(_connectionString)).As<IDbContext>();
            builder.RegisterType<SqlRepository>().As<IRepository>();
            base.Load(builder);
        }
    }
}
