#include <iostream>
#include "Windows.h"

using namespace std;

int main()
{
	SetConsoleCP(1251);
	SetConsoleOutputCP(1251);
	cout << "CurrentProcessId: " << GetCurrentProcessId() << "\n";
	cout << "CurrentThreadId: " << GetCurrentThreadId() << "\n";
	cout << "PriorityClass: " << GetPriorityClass(GetCurrentProcess()) << "\n";
	cout << "ThreadPriority: " << GetThreadPriority(GetCurrentThread()) << "\n";
	DWORD_PTR processAffinityMask;
	DWORD_PTR systemAffinityMask;
	cout << "Affinity mask: " << GetProcessAffinityMask(GetCurrentProcess(), &processAffinityMask, &systemAffinityMask) << "\n";
	SYSTEM_INFO systemInfo;
	GetSystemInfo(&systemInfo);
	cout << "Available processors: " << systemInfo.dwNumberOfProcessors << "\n";
	DWORD_PTR threadAffinityMask;
	DWORD_PTR threadSystemAffinityMask;
	cout << "Processor allocated to current thread: " << GetProcessAffinityMask(GetCurrentThread(), &threadAffinityMask, &threadSystemAffinityMask) << "\n";
}