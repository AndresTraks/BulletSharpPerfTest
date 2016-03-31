#pragma once

using namespace System;

namespace BenchmarkCppCli {

	public ref class Benchmark
	{
	public:
		static void Empty();
		static int Zero();
		static int Identity(int n);
	};
}
