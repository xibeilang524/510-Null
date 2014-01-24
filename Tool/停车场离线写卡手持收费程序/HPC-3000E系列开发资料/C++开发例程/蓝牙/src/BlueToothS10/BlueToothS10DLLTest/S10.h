
#pragma once

enum S10_OperationMode
{
	ATCommandMode,			//AT命令模式
	CommunicationMode		//通信模式
};
enum S10_Role
{
	Slave,
	Master,
	Loopback
};
enum S10_Coms
{
	COM1 = 1,
	COM2,
	COM3,
	COM4,
	COM5,
	COM6,
	COM7,
	COM8
};

enum S10_BaudRate
{
	Baud9600	= 9600,
	Baud19200	= 19200,
	Baud38400	= 38400,
	Baud57600	= 57600,
	Baud115200	= 115200,
	Baud230400	= 230400,
	Baud460800	= 460800,
	Baud921600	= 921600,
	Baud1382400	= 1382400
};

enum StopBit
{
	OneStopBit = 0,
	TwoStopBits = 1
};

enum ParityBit
{
	NoneParityBit,
	OddParityBit,
	EvenParityBit
};

struct BlueToothDevice
{
	unsigned char addr[6];
	unsigned long deviceClass;
	short rssi;
};
