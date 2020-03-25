using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace TeduCoreApp.Areas.Admin.Models.BusinessModel
{
    public class ReflectionController
    {
        //Get all controller
        public List<Type> GetControllers(string namespaces)
        {
            List<Type> listController = new List<Type>();
            Assembly assembly = Assembly.GetExecutingAssembly();
            IEnumerable<Type> types = assembly.GetTypes().Where(type => typeof(Controller).IsAssignableFrom(type) && type.Namespace.Contains(namespaces)).OrderBy(x => x.Name);
            return types.ToList();
        }

        //Get all action
        public List<string> GetActions(Type controller)
        {
            List<string> listAction = new List<string>();
            IEnumerable<MemberInfo> memberInfor = controller.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly |
            BindingFlags.Public).Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.RuntimeCompatibilityAttribute), true).Any()).OrderBy(x => x.Name);
            foreach (MemberInfo method in memberInfor)
            {
                if (method.ReflectedType.IsPublic && !method.IsDefined(typeof(NonActionAttribute)))
                {
                    listAction.Add(method.Name.ToString());
                }
            }
            return listAction;
        }
    }
}