using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TonyBlogs.DTO.UserInfo;

public static class UserContext
{
    public static UserObj CurrentUser 
    {
        get
        {
            var userobj = HttpContext.Current.Items["userobj"] as UserObj;

            return userobj;
        }

        set
        {
            HttpContext.Current.Items["userobj"] = value;
        }
    }
}
