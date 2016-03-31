#include "CppClass.h"

void CppClass::empty()
{
}

int CppClass::zero()
{
	return 0;
}

int CppClass::identity(int n)
{
	return n;
}


CppClass* CppClass_new()
{
	return new CppClass();
}

void CppClass_empty(CppClass* obj)
{
	obj->empty();
}

int CppClass_zero(CppClass* obj)
{
	return obj->zero();
}

int CppClass_identity(CppClass* obj, int n)
{
	return obj->identity(n);
}

void CppClass_delete(CppClass* obj)
{
	delete obj;
}
