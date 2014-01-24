using System;
using System.Runtime.InteropServices;

namespace BuzzerDLL_CSharp
{
    class BuzzerDll
    {
        /*********************************************************************************************************
        ** Function name:   		epcBuzzerOn
        ** Descriptions:    		本函数使蜂鸣器蜂鸣指定时间
        ** input parameters:   	    dwOntime    蜂鸣器持续蜂鸣的时间(ms)，其中0表示一直蜂鸣
        ** output parameters:   	无
        ** Returned value:     	    TRUE:成功;FALSE:失败
        ** Note:                    本函数会阻塞dwOntime(ms)时间，但dwOntime为0时会立刻返回
        *********************************************************************************************************/
        [DllImport("epcBuzzerLib.dll")]
        public static extern uint epcBuzzerOn(uint dwOntime);

        /*********************************************************************************************************
        ** Function name:   		epcBuzzerOff
        ** Descriptions:    		本函数使蜂鸣器禁止
        ** input parameters:   	    无
        ** output parameters:   	无
        ** Returned value:     	    TRUE:成功;FALSE:失败
        *********************************************************************************************************/
        [DllImport("epcBuzzerLib.dll")]
        public static extern uint epcBuzzerOff();

        /*********************************************************************************************************
        ** Function name:   		epcBuzzerGetStatus
        ** Descriptions:    		本函数获取蜂鸣器的状态
        ** input parameters:   	    无
        ** output parameters:   	无
        ** Returned value:     	    0--蜂鸣器蜂鸣；1--蜂鸣器禁止；其它--出现错误
        *********************************************************************************************************/
        [DllImport("epcBuzzerLib.dll")]
        public static extern uint epcBuzzerGetStatus();

        /*********************************************************************************************************
        ** Function name:   		epcBuzzerAsyncOn
        ** Descriptions:    		本函数使蜂鸣器蜂鸣指定时间
        ** input parameters:   	    dwOntime    蜂鸣器持续蜂鸣的时间(ms)，其中0表示一直蜂鸣
        ** output parameters:   	无
        ** Returned value:     	    TRUE:成功;FALSE:失败
        ** Note:                    本函数以异步方式执行，不会被阻塞
        *********************************************************************************************************/
        [DllImport("epcBuzzerLib.dll")]
        public static extern uint epcBuzzerAsyncOn(uint dwOntime);

        /*********************************************************************************************************
        ** Function name:   		epcBuzzerBeeps
        ** Descriptions:    		本函数使蜂鸣器发出指定次数的鸣叫
        ** input parameters:   	    dwFlashTimes        蜂鸣器鸣叫次数
        **                          dwOntime            蜂鸣器蜂鸣时间(ms)
        **                          dwOfftime           蜂鸣器禁止时间(ms)
        ** output parameters:   	无
        ** Returned value:     	    TRUE:成功;FALSE:失败
        ** Note:                    本函数以异步方式执行，不会被阻塞
        *********************************************************************************************************/
        [DllImport("epcBuzzerLib.dll")]
        public static extern uint epcBuzzerBeeps (uint dwFlashTimes, uint dwOntime, uint dwOfftime);
    }
}