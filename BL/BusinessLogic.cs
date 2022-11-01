using DAL;
using ENTITY;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL
{
    public class BusinessLogic
    {
        private Dal _dal = new Dal();
        public Configuration GetConfiguration()
        {
            return _dal.GetConfiguration();
        }

        public string Return(string request)
        {
            if (!string.IsNullOrEmpty(_dal.Return(request)))
            {
                return _dal.Return(request);
            }
            else
            {
                Learn(request);
                return "";
                
            }
            
        }

        public void Learn(string request)
        {
            Console.WriteLine("Non capisco, insegnami cosa avrei dovuto rispondere?");
            string response = Console.ReadLine();
            _dal.Learn(request,response, "Phrases");
        }
    }
}
