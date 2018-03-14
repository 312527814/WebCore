using System;
using System.Collections.Generic;
using System.Text;
using Web.Service.IRepository;
using Web.Service.Interceptor;
using System.Threading.Tasks;
using Web.Service.Core;
using Web.Service.Connection;

namespace Web.Service.Domain
{
    public class Teacher : Entity
    {
        IConnectionManager connectionManager;
        private IMySession session;
        private ITeacherRepository teacherRepository;
        private IShoolRepository shoolRepository;
        public Teacher() { }
        public Teacher(
            IConnectionManager connectionManager,
            IMySession session,
            ITeacherRepository teacherRepository,
            IShoolRepository shoolRepository)
        {
            this.connectionManager = connectionManager;
            this.session = session;
            this.teacherRepository = teacherRepository;
            this.shoolRepository = shoolRepository;
        }
        public string Name { get; set; }

        public Teacher get(int id)
        {
            return teacherRepository.Get(id); 
        }
        public string test()
        {
            return session.UserId.ToString();
        }
        public async Task<IList<Teacher>> list(string name)
        {

            var id = session.UserId;
            var s = teacherRepository.list(name);
            var s2 = teacherRepository.SetConnstr<ITeacherRepository>(c =>
            {
                c.SlaveConnstr = connectionManager.MasterConnstr;
            }).list(name);
            var s3 = teacherRepository.list(name);

            return await this.Run(() =>
           {
               return teacherRepository.list(name);
           });

        }

        public async Task TestError([Ny]string s)
        {

            await Task.Run(() =>
            {
                teacherRepository.list("");
            });
        }
    }

}
