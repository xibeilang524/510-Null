using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace IR_CSharp
{
    class Buzzer
    {
        [System.Runtime.InteropServices.DllImport("epcBuzzerLib.dll")]
        /*********************************************************************************************************
        ** Function name:   		epcBuzzerOff
        ** Descriptions:    		本函数使蜂鸣器禁止
        ** input parameters:   	    无
        ** output parameters:   	无
        ** Returned value:     	    TRUE:成功;FALSE:失败
        *********************************************************************************************************/
        private static extern bool epcBuzzerOn(uint dwOntime);

        [System.Runtime.InteropServices.DllImport("epcBuzzerLib.dll")]
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
        private static extern bool epcBuzzerBeeps(uint dwFlashTimes, uint dwOntime, uint dwOfftime);


        [System.Runtime.InteropServices.DllImport("epcBuzzerLib.dll")]

        /*********************************************************************************************************
        ** Function name:   		epcBuzzerOff
        ** Descriptions:    		本函数使蜂鸣器禁止
        ** input parameters:   	    无
        ** output parameters:   	无
        ** Returned value:     	    TRUE:成功;FALSE:失败
        *********************************************************************************************************/
        private static extern bool epcBuzzerOff ();


        public static void BeepOK()
        {
            epcBuzzerBeeps(1, 300, 0);
        }
        public static void BeepError()
        {
            epcBuzzerBeeps(3, 100, 50);
        }
        public static void BeepClose()
        {
            epcBuzzerOff();
        }
        public static void BeepAlarm()
        {
            epcBuzzerBeeps(100, 100, 50);
        }
    }
}
