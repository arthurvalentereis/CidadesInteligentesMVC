using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProvaCandidato.Controllers.Base
{
    public class BaseController<T> : Controller
    {
        public T Controller;
        public BaseController() { }
        public BaseController(T controller)  
        {
            Controller = controller;
        }
       
    }
}