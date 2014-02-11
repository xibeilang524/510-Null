#pragma once
#include <vector>
using namespace std;

class CConfFile
{
public:
	CConfFile(CString path);
	bool Read(int & panid,vector<int> &channel,vector<int> &speed);
private :
	CString path;
};