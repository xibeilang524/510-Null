

class MyString
{
public:
	MyString(void);
	~MyString(void);
	int StringToByte(CString sString, BYTE *pData, int Length);
	void ByteToString(const BYTE *pData, CString *sString, int Length);
};
