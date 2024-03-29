﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GameStore.Pages {
    public partial class Login : System.Web.UI.Page {
        [Obsolete]
        protected void Page_Load(object sender, EventArgs e) {
            if (IsPostBack) {
                string name = Request.Form["name"];
                string password = Request.Form["password"];
                if (name != null && password != null
                        && FormsAuthentication.Authenticate(name, password)) {
                    FormsAuthentication.SetAuthCookie(name, false);
                    Response.Redirect(Request["ReturnUrl"] ?? "/");
                } else {
                    ModelState.AddModelError("fail", "Логин или пароль не правильны." +
                        "Пожалуйста введите данные заново");
                }
            }
        }
    }
}