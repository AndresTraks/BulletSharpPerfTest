#pragma once
#include "Main.h"

class CppClass
{
public:
	void empty();
	int zero();
	int identity(int n);
};

EXPORT CppClass* CppClass_new();
EXPORT void CppClass_empty(CppClass* obj);
EXPORT int CppClass_zero(CppClass* obj);
EXPORT int CppClass_identity(CppClass* obj, int n);
EXPORT void CppClass_delete(CppClass* obj);
