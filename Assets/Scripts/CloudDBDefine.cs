using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class CloudDBDefine {

    // 登录信息
    internal const string Api_Url = "https://i.lywos.com:8143";
    internal const string User_Name = "diamondplan";
    internal const string User_Password = "000000";

    //定义表格 用户信息
    internal const int Table_UserInfo = 744;

    //定义表格 游戏用户
    internal const int Table_GameUser = 754;

    //定义表格 游戏排名
    internal const int Table_Ranking = 759;

    

    //定义查询 获取排名前100
    internal const int Query_GetRankingTop100 = 764;
}

