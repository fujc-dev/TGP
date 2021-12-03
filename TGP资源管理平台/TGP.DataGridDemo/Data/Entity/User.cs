using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TGP.DataGridDemo
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User
    {
        public User()
        {
            this.Roles = new ObservableCollection<Role>();
            this.Jobs = new ObservableCollection<string>();
            this.Tests = new ObservableCollection<Test>();
        }

        /// <summary>
        /// 用户代码
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public String UserName { get; set; }

        /// <summary>
        /// 职务集合
        /// </summary>
        public ICollection<String> Jobs { get; private set; }

        public ICollection<Test> Tests { get; private set; }

        /// <summary>
        /// 角色集合
        /// </summary>
        public ICollection<Role> Roles { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ICollection<User> CreateUsers()
        {
            var items = new ObservableCollection<User>();

            var user = new User() { UserCode = "01", UserName = "甲" };
            user.Roles.Add(new Role() { RoleCode = "A1", RoleName = "管理员" });
            user.Roles.Add(new Role() { RoleCode = "A2", RoleName = "销售员" });
            user.Jobs.Add("董事长");
            user.Tests.Add(new Test() {  Value= true});
            items.Add(user);


            user = new User() { UserCode = "02", UserName = "乙" };
            user.Roles.Add(new Role() { RoleCode = "A1", RoleName = "管理员" });
            user.Roles.Add(new Role() { RoleCode = "B1", RoleName = "服务员" });
            user.Roles.Add(new Role() { RoleCode = "C1", RoleName = "人事管理员" });
            user.Roles.Add(new Role() { RoleCode = "D1", RoleName = "调度员" });
            user.Jobs.Add("总经理");
            user.Tests.Add(new Test() { Value = false });
            items.Add(user);

            user = new User() { UserCode = "03", UserName = "丙" };
            user.Roles.Add(new Role() { RoleCode = "D1", RoleName = "调度员" });
            items.Add(user);

            user = new User() { UserCode = "04", UserName = "丁" };
            user.Jobs.Add("经理");
            user.Jobs.Add("出纳");
            user.Jobs.Add("车间主管");
            user.Tests.Add(new Test() { Value = true });
            items.Add(user);

            return items;
        }
    }

    /// <summary>
    /// 角色
    /// </summary>
    public class Role
    {
        /// <summary>
        ///角色代码
        /// </summary>
        public String RoleCode { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public String RoleName { get; set; }
    }

    public class Test {
        public Boolean Value { get; set; }
    }
}
