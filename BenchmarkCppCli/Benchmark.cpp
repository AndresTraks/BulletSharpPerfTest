#include "Benchmark.h"

#pragma managed(push, off)
void empty_n()
{
}

int zero_n()
{
	return 0;
}

int identity_n(int n)
{
	return n;
}
#pragma managed(pop)

void BenchmarkCppCli::Benchmark::Empty()
{
	empty_n();
}

int BenchmarkCppCli::Benchmark::Zero()
{
	return zero_n();
}

int BenchmarkCppCli::Benchmark::Identity(int n)
{
	return identity_n(n);
}
