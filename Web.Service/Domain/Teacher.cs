using System;
using System.Collections.Generic;
using System.Text;
using Web.Service.IRepository;
using Web.Service.Interceptor;
using System.Threading.Tasks;

namespace Web.Service.Domain
{
    public class Teacher : Entity
    {
        private ITeacherRepository teacherRepository;
        private IShoolRepository shoolRepository;
        public Teacher() { }
        public Teacher(ITeacherRepository teacherRepository, IShoolRepository shoolRepository)
        {
            this.teacherRepository = teacherRepository;
            this.shoolRepository = shoolRepository;
    }
        public string Name { get; set; }

        public Teacher get(int id)
        {
            return teacherRepository.Get(id);
        }

        public async Task<IList<Teacher>> list(string name)
        {
            var s = teacherRepository.list(name);
            shoolRepository.list();
            var s2 = teacherRepository.UseMasterConn<ITeacherRepository>().list(name);
            var s3 = teacherRepository.list(name);

            return await this.Run(() =>
           {
               return teacherRepository.UseMasterConn<ITeacherRepository>().list(name);
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
