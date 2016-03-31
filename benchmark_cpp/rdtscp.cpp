#include <intrin.h>

#define EXPORT extern "C" __declspec(dllexport)

EXPORT unsigned long long rdtscp()
{
	unsigned int aux;
	return __rdtscp(&aux);
}
