﻿using System.Diagnostics; // Stopwatch
using static System.Diagnostics.Process; // GetCurrentProcess()

namespace Packt.Shared;

public static class Recorder
{
    private static Stopwatch timer = new();

    private static long bytesPhysicalBefore = 0;
    private static long bytesVirtualBefore = 0;

    public static void Start()
    {
        // Force some garbage collections to release memory that is no longer
        // referenced but has not been released yet.
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        // Store the current physical and virtual memory use.
        bytesPhysicalBefore = GetCurrentProcess().WorkingSet64;
        bytesVirtualBefore = GetCurrentProcess().VirtualMemorySize64;

        timer.Restart();
    }

    public static void Stop()
    {
        timer.Stop();

        long bytesPhysicalAfter = GetCurrentProcess().WorkingSet64;
        long bytesVirtualAfter = GetCurrentProcess().VirtualMemorySize64;

        WriteLine("{0:N0} physical bytes used.", 
            bytesPhysicalAfter - bytesPhysicalBefore);

        WriteLine("{0:N0} virtual bytes used.",
            bytesVirtualAfter - bytesVirtualBefore);

        WriteLine("{0} time span elapsed.", timer.Elapsed);

        WriteLine("{0:N0} total milliseconds elapsed.", 
            timer.ElapsedMilliseconds);
    }
}
