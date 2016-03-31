#include <intrin.h>
#include "Main.h"

EXPORT unsigned long long rdtscp()
{
	unsigned int aux;
	return __rdtscp(&aux);
}
