#include "CppClass.h"

#pragma managed(push, off)
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
#pragma managed(pop)


BenchmarkCppCli::CppClass::~CppClass()
{
	this->!CppClass();
}

BenchmarkCppCli::CppClass::!CppClass()
{
	delete _native;
	_native = 0;
}

BenchmarkCppCli::CppClass::CppClass()
{
	_native = new ::CppClass();
}

void BenchmarkCppCli::CppClass::Empty()
{
	_native->empty();
}

int BenchmarkCppCli::CppClass::Zero()
{
	return _native->zero();
}

int BenchmarkCppCli::CppClass::Identity(int n)
{
	return _native->identity(n);
}
