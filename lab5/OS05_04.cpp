#include <iostream>
#include <unistd.h>
#include <sys/types.h>
#include <sys/syscall.h>
#include <sys/time.h>

using namespace std;

int main()
{
    cout << "CurrentProcessId: " << getpid() << "\n";
    cout << "CurrentThreadId: " << syscall(SYS_gettid) << "\n";
    cout << "CurrentThreadPriority: " << getpriority(PRIO_PROCESS, syscall(SYS_gettid)) << "\n";
    cpu_set_t cpuset;
    CPU_ZERO(&cpuset);
    sched_getaffinity(0, sizeof(cpuset), &cpuset);

    cout << "Номера доступных процессоров: ";
    for (int i = 0; i < CPU_SETSIZE; ++i) {
        if (CPU_ISSET(i, &cpuset)) {
            cout << i << " ";
        }
    }

    return 0;
}