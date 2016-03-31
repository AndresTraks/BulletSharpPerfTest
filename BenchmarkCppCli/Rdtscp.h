#pragma once

using namespace System;

namespace BenchmarkCppCli {

	public ref class Rdtscp
	{
	public:
		static unsigned long long Get();
	};
}
