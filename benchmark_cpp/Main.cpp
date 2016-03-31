#define EXPORT extern "C" __declspec(dllexport)

EXPORT void empty()
{
}

EXPORT int zero()
{
	return 0;
}

EXPORT int identity(int n)
{
	return n;
}
