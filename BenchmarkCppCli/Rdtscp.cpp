#include "Rdtscp.h"
#include <intrin.h>

#pragma managed(push, off)
inline unsigned long long rdtscp()
{
	unsigned int aux;
	return __rdtscp(&aux);
}
#pragma managed(pop)

unsigned long long BenchmarkCppCli::Rdtscp::Get()
{
	return rdtscp();
}
