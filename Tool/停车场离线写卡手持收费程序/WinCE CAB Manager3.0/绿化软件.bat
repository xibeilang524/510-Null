@echo off
copy CeCabinet.ocx %systemroot%\
copy HexEditCtrl.ocx %systemroot%\

regsvr32 /s CeCabinet.ocx
regsvr32 /s HexEditCtrl.ocx
exit