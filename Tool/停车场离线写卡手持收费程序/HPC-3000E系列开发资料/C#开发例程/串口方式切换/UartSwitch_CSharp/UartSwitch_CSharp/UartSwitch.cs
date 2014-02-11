using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace UartSwicthSpace
{

enum UartSwich_State:byte
{
   UART_NC  =  0,                      /* 设置接口无连接方式 */
   UART_TTL,                           /* 设置接口TTL方式 */
   UART_RS232,                         /* 设置接口RS232方式 */
   UART_RS485                          /* 设置接口RS485方式 */
}

class UartSwichSet
{
/*********************************************************************************************************
** Function name:   		UartSwitchInit
** Descriptions:    		本函数是初始化串口切换
** input parameters:   	    无
** output parameters:   	无
** Returned value:     	    TRUE:成功;FALSE:失败
** Note:                    
*********************************************************************************************************/
[System.Runtime.InteropServices.DllImport("UartSwitch.dll")]
public static extern bool UartSwitchInit();


/*********************************************************************************************************
** Function name:   		UartSwitchSetting
** Descriptions:    		本函数是用于选择串口输出方式
** input parameters:   	    dwSwitch             串口输出方式选择
** output parameters:   	无
** Returned value:     	    TRUE:成功;FALSE:失败
** Note:                    
*********************************************************************************************************/
[System.Runtime.InteropServices.DllImport("UartSwitch.dll")]
public static extern bool UartSwitchSetting(UartSwich_State dwSwitch);



/*********************************************************************************************************
** Function name:   		UartSwitchDeInit
** Descriptions:    		本函数是初始化串口切换
** input parameters:   	    无
** output parameters:   	无
** Returned value:     	    TRUE:成功;FALSE:失败
** Note:                    
*********************************************************************************************************/
[System.Runtime.InteropServices.DllImport("UartSwitch.dll")]
public static extern bool  UartSwitchDeInit();


}
}