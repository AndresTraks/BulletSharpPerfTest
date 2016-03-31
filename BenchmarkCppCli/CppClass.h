#pragma once

class CppClass
{
public:
	void empty();
	int zero();
	int identity(int n);
};

namespace BenchmarkCppCli {

	public ref class CppClass
	{
	private:
		::CppClass* _native;

		~CppClass();
		!CppClass();

	public:
		CppClass();

		void Empty();
		int Zero();
		int Identity(int n);
	};
}
