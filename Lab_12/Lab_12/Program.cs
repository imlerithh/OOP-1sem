using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Collections;

namespace Lab_12
{
    class Program
    {
        static void Main(string[] args)
        { 
             string className = "Lab_12.Airplane";
             Reflector.Assembly(className);
             Reflector.IsPublicConstructors(className);
        //   Reflector.IsPublicConstructors("Lab_12.Reflector");
             Reflector.PublicMethods(className);
             Reflector.FieldsAndProperties(className);
             Reflector.Interfaces(className);
             Reflector.MethodsByParametr(className, "string");
             Reflector.Invoke(Assembly.LoadFrom("Lab_12.dll").GetType("Lab_12.Program", true, true), "UpperCase", new object[] {"testing the invoke" });

             Airplane a = new Airplane("ss",new DateTime(1,1,1,18,30,0),new ArrayList() {DayOfWeek.Saturday});
             object o = Reflector.Create<Airplane>(a);
             Console.WriteLine(o.GetType());
        }
        public string UpperCase(string s)
        { 
            return s.ToUpper();
        }
    }

    public static class Reflector
    {
        public static void Assembly(string className)
        {
            Type myType = Type.GetType(className, false, true);
            WriteToFile(myType.Name, "Assembly", myType.Assembly.ToString());
        }
        public static void IsPublicConstructors(string className)
        {
            Type myType = Type.GetType(className, false, true);
            string info = "true";
            if (myType.GetConstructors().Count() == 0) info = "false";
            WriteToFile(myType.Name, "IsPublicConstructors", info);

        }
        public static void PublicMethods(string className)
        {
            Type myType = Type.GetType(className, false, true);
            string info = "";
            foreach (MethodInfo method in myType.GetMethods())
            {
                if (method.IsPublic)
                {
                    string modificator = "";
                    modificator += "public ";
                    if (method.IsStatic)
                        modificator += "static ";
                    if (method.IsVirtual)
                        modificator += "virtual ";
                    info += modificator + method.ReturnType.Name + ' ' + method.Name + '(';
                    //получаем все параметры
                    ParameterInfo[] parameters = method.GetParameters();
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        info += parameters[i].ParameterType.Name + ' ' + parameters[i].Name;
                        if (i + 1 < parameters.Length) info += ", ";
                    }
                    info += ")\n";
                }
            }
            WriteToFile(myType.Name, "PublicMethods", info);
        }
        public static void FieldsAndProperties(string className)
        {
            Type myType = Type.GetType(className, false, true);
            string info = "Fields:\n";
            foreach (FieldInfo field in myType.GetFields())
            {
                info += field.FieldType + " " + field.Name + '\n';
            }
            info += "Properties:\n";
            foreach (PropertyInfo prop in myType.GetProperties())
            {
                info += prop.PropertyType + " " + prop.Name + '\n';
            }
            WriteToFile(myType.Name, "FieldsAndProperties", info);
        }
        public static void Interfaces(string className)
        {
            Type myType = Type.GetType(className, false, true);
            string info = "";
            foreach (Type i in myType.GetInterfaces())
                info += i.Name;
            WriteToFile(myType.Name, "Interfaces", info);
        }
        public static void MethodsByParametr(string className, string parameter)
        {
            Type myType = Type.GetType(className, false, true);
            string info = "";
            foreach (MethodInfo method in myType.GetMethods())
            {
                    ParameterInfo[] parameters = method.GetParameters();
                    for (int i = 0; i < parameters.Length; i++)
                    {
                    if (parameters[i].ParameterType.Name.ToUpper() == parameter.ToUpper())
                    {
                        info += method.Name + '\n';
                        break;
                    }
                    }
            }
            WriteToFile(myType.Name, "MethodsByParametr; parameter - " + parameter, info);
        }
        public static void Invoke(Type t, string methodName, object[] parameters)
        {
            // получаем метод UpperCase
            MethodInfo method = t.GetMethod(methodName);
            // создаем экземпляр класса Program
            object obj = Activator.CreateInstance(t);
            // вызываем метод, передаем ему значения для параметров и получаем результат
            object result = method.Invoke(obj, parameters);

            string info = "Called method: " + methodName + "\nParameters: ";
            foreach (object o in parameters)
                info += o.ToString();
            info += "\nResult: " + result;
            WriteToFile(t.Name, "Invoke", info);
        }
        public static T Create<T>(T obj)
        {
            Type t = obj.GetType();
            object newObj = Activator.CreateInstance(t);
            return (T)newObj;

        }
        public static void WriteToFile(string className, string methodName, string info)
        {
            using (StreamWriter sw = new StreamWriter("Reflector.txt", true, System.Text.Encoding.Default))
            {
                sw.WriteLine("Class: " + className); 
                sw.WriteLine("Method Name: " + methodName); 
                sw.WriteLine(info + '\n');
            }
        }
    }
}
